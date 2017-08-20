// <copyright file="DiceParser.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/9/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/20/2017
//-----------------------------------------------------------------------
// <summary>
//       This project is licensed under the MIT license.
//
//       OnePlat.DiceNotation is an open source project that parses,
//  evalutes, and rolls dice that conform to the defined Dice notiation.
//  This notation is usable in any form of random dice games and role-playing
//  games like D&D and d20.
// </summary>
//-----------------------------------------------------------------------
using OnePlat.DiceNotation.DiceTerms;
using OnePlat.DiceNotation.DieRoller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Class to parse dice notation string expressions and convert the
    /// expression into Dice instance.
    /// </summary>
    public class DiceParser
    {
        #region Members
        private static Regex whitespaceRegex = new Regex(@"\s+");
        private static string decimalSeparator = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of math operators for this parser. Order in the list signifies order of operations.
        /// Caller can customize the operators list by adding/removing elements in the list.
        /// </summary>
        public List<string> Operators { get; } = new List<string> { "d", "k", "/", "x", "*", "-", "+" };

        /// <summary>
        /// Gets the list of operator actions used by this parser. If there is an operator in the operators list,
        /// then it should have a corresponding operation here.
        /// Caller can customize the operator actions by adding/updating elements in this list.
        /// </summary>
        public Dictionary<string, Func<int, int, int>> OperatorActions { get; } = new Dictionary<string, Func<int, int, int>>
        {
            { "/", (numberA, numberB) => numberA / numberB },
            { "x", (numberA, numberB) => numberA * numberB },
            { "*", (numberA, numberB) => numberA * numberB },
            { "-", (numberA, numberB) => numberA - numberB },
            { "+", (numberA, numberB) => numberA + numberB }
        };

        /// <summary>
        /// Gets or sets the default operator to use when there's a missing operator.
        /// Basic behavior is the multiplication operator, but can be changed by caller.
        /// </summary>
        public string DefaultOperator { get; set; } = "x";

        /// <summary>
        /// Gets or sets the grouping start operator to use when discovering groups of subexpressions.
        /// Basic behavior is the ( operator, but can be changed by caller.
        /// </summary>
        public string GroupStartOperator { get; set; } = "(";

        /// <summary>
        /// Gets or sets the grouping end operator to use when discovering groups of subexpressions.
        /// Basic behavior is the ) operator, but can be changed by caller.
        /// </summary>
        public string GroupEndOperator { get; set; } = ")";

        /// <summary>
        /// Gets or sets the default number of dice when nothing is specified (defaults to 1).
        /// </summary>
        public string DefaultNumDice { get; set; } = "1";
        #endregion

        #region Main Parse methods

        /// <summary>
        /// Deconstructs the expression string in to the individual tokens that makes
        /// up the expression.
        /// </summary>
        /// <param name="expression">Expression string to parse</param>
        /// <returns>List of the expression tokens.</returns>
        public List<string> Tokenize(string expression)
        {
            List<string> tokens = new List<string>();
            string vector = string.Empty;

            // first clean up expression
            expression = this.CorrectExpression(expression);

            // loop through the expression characters
            for (var i = 0; i < expression.Length; i++)
            {
                var ch = expression[i].ToString();
                var next = (i + 1) >= expression.Length ? string.Empty : expression[i + 1].ToString();
                var prev = i == 0 ? string.Empty : expression[i - 1].ToString();

                if (char.IsLetter(ch, 0))
                {
                    // if it's a letter, then increment the char position until we find the end of the text
                    if (i != 0 && (char.IsDigit(prev, 0) || prev == this.GroupEndOperator) && !this.Operators.Contains(ch))
                    {
                        tokens.Add(this.DefaultOperator);
                    }

                    if (ch == "d" && (string.IsNullOrEmpty(prev) ||
                                      this.Operators.Contains(prev) ||
                                      prev == this.GroupEndOperator ||
                                      prev == this.GroupStartOperator))
                    {
                        tokens.Add(this.DefaultNumDice);
                    }

                    vector += ch;

                    if (!this.Operators.Contains(ch))
                    {
                        while ((i + 1) < expression.Length && char.IsLetterOrDigit(expression[i + 1]))
                        {
                            i++;
                            vector += expression[i];
                        }
                    }

                    tokens.Add(vector);
                    vector = string.Empty;
                }
                else if (char.IsDigit(ch, 0))
                {
                    // if it's a digit, then increment char until you find the end of the number
                    vector = vector + ch;

                    while ((i + 1) < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1].ToString() == decimalSeparator))
                    {
                        i++;
                        vector += expression[i];
                    }

                    tokens.Add(vector);
                    vector = string.Empty;
                }
                else if ((i + 1) < expression.Length &&
                         this.Operators.Contains(ch) &&
                         char.IsDigit(expression[i + 1]) && (i == 0 ||
                         ((i - 1) > 0 && prev == this.GroupStartOperator)))
                {
                    // if the above is true, then, the token for that negative number will be "-1", not "-","1".
                    vector = vector + ch;

                    while ((i + 1) < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1].ToString() == decimalSeparator))
                    {
                        i++;
                        vector = vector + expression[i];
                    }

                    tokens.Add(vector);
                    vector = string.Empty;
                }
                else if (ch == this.GroupStartOperator)
                {
                    // if an open parenthesis, then if we didn't have an operator, then default to multiplication.
                    if (i != 0 && (char.IsDigit(prev, 0) || prev == this.GroupEndOperator))
                    {
                        tokens.Add(this.DefaultOperator);
                    }

                    tokens.Add(ch.ToString());
                }
                else if (ch == this.GroupEndOperator)
                {
                    tokens.Add(ch);

                    if ((i + 1) < expression.Length && (char.IsDigit(next, 0) ||
                        (next != this.GroupEndOperator && !this.Operators.Contains(next))))
                    {
                        tokens.Add(this.DefaultOperator);
                    }
                }
                else
                {
                    // if not recognized character just add it as its own token.
                    tokens.Add(ch.ToString());
                }
            }

            return tokens;
        }

        /// <summary>
        /// Parses the specified dice expression and converts it into a
        /// Dice instance to perform oprations on.
        /// </summary>
        /// <param name="expression">String expression to parse</param>
        /// <param name="boundedResult">Specifies whether the result should be bounded to a minimum</param>
        /// <param name="dieRoller">Die roller to use</param>
        /// <returns>Dice result</returns>
        public DiceResult Parse(string expression, bool boundedResult, IDieRoller dieRoller)
        {
            // first clean up expression
            expression = this.CorrectExpression(expression);

            List<string> tokens = this.Tokenize(expression);
            return this.ParseLogic(expression, tokens, boundedResult, dieRoller);
        }
        #endregion

        #region Parsing helper methods

        /// <summary>
        /// Parses the specified list of tokens with appropriate logic to convert
        /// it into a Dice instance to evaluate oprations on.
        /// </summary>
        /// <param name="expression">dice expression rolled</param>
        /// <param name="tokens">String expression to parse</param>
        /// <param name="boundedResult">Specifies whether the result should be bounded to a minimum</param>
        /// <param name="dieRoller">Die roller to use</param>
        /// <returns>Dice result</returns>
        private DiceResult ParseLogic(string expression, List<string> tokens, bool boundedResult, IDieRoller dieRoller)
        {
            List<TermResult> results = new List<TermResult>();

            int value = this.HandleBasicOperation(results, tokens, dieRoller);

            return new DiceResult(expression, value, results, dieRoller.GetType().ToString(), boundedResult);
        }

        /// <summary>
        /// Processes through all of the operator and evaluates the tokens for a single strand
        /// of values.
        /// </summary>
        /// <param name="results">List of term results</param>
        /// <param name="tokens">String expression to parse</param>
        /// <param name="dieRoller">Die roller to use</param>
        /// <returns>value of operation total.</returns>
        private int HandleBasicOperation(List<TermResult> results, List<string> tokens, IDieRoller dieRoller)
        {
            if (tokens.Count == 0)
            {
                return 0;
            }
            else if (tokens.Count == 1)
            {
                // if there is only one token, then it much be a constant.
                return int.Parse(tokens[0]);
            }

            foreach (var op in this.Operators)
            {
                while (tokens.IndexOf(op) != -1)
                {
                    try
                    {
                        if (op == "d")
                        {
                            this.HandleDieOperator(results, tokens, op, dieRoller);
                        }
                        else
                        {
                            this.HandleArithmeticOperators(results, tokens, op);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException("Dice expression string is incorrect format.", ex);
                    }
                }

                if (tokens.Count == 0)
                {
                    break;
                }
            }

            return int.Parse(tokens[0]);
        }

        /// <summary>
        /// Handles specific arithmetic opearations and returns the result of the operation
        /// in the token list.
        /// </summary>
        /// <param name="results">List of term results</param>
        /// <param name="tokens">String expression to parse</param>
        /// <param name="op">current operator</param>
        private void HandleArithmeticOperators(List<TermResult> results, List<string> tokens, string op)
        {
            var opPosition = tokens.IndexOf(op);
            var numberA = int.Parse(tokens[opPosition - 1]);
            var numberB = int.Parse(tokens[opPosition + 1]);

            int result = this.OperatorActions[op](numberA, numberB);

            tokens[opPosition - 1] = result.ToString();
            tokens.RemoveRange(opPosition, 2);
        }

        /// <summary>
        /// Handles the dice operator and its sub-expressions, and returns the result of the
        /// dice rolls in the results list and token value.
        /// </summary>
        /// <param name="results">List of term results</param>
        /// <param name="tokens">String expression to parse</param>
        /// <param name="op">current operator</param>
        /// <param name="dieRoller">Die roller to use</param>
        private void HandleDieOperator(List<TermResult> results, List<string> tokens, string op, IDieRoller dieRoller)
        {
            int opPosition = tokens.IndexOf(op);
            int numDice = int.Parse(tokens[opPosition - 1]);
            int sides = int.Parse(tokens[opPosition + 1]);
            int? choose = null;
            int length = 2;

            int choosePos = tokens.IndexOf("k");
            if (choosePos > 0)
            {
                choose = int.Parse(tokens[choosePos + 1]);
                length += 2;
            }

            DiceTerm term = new DiceTerm(numDice, sides, 1, choose);
            IReadOnlyList<TermResult> t = term.CalculateResults(dieRoller);
            int value = t.Sum(r => (int)Math.Round(r.Value * r.Scalar));
            results.AddRange(t);

            tokens[opPosition - 1] = value.ToString();
            tokens.RemoveRange(opPosition, length);
        }

        /// <summary>
        /// Cleanup the expression text to lower case, remove spaces, and replace duplicate operatiors.
        /// </summary>
        /// <param name="expression">Expression to clean up</param>
        /// <returns>Corrected expression text</returns>
        private string CorrectExpression(string expression)
        {
            string result = whitespaceRegex.Replace(expression.ToLower(), string.Empty);
            result = result.Replace("+-", "-");
            result = result.Replace("-+", "-");
            result = result.Replace("--", "+");

            return result;
        }
        #endregion
    }
}
