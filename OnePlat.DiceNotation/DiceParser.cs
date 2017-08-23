// <copyright file="DiceParser.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/9/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/23/2017
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
        private const string PercentileNotation = "d%";
        private const string D100EquivalentNotation = "d100";
        private static Regex whitespaceRegex = new Regex(@"\s+");
        private static string decimalSeparator = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;

        private DiceConfiguration config = null;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of math operators for this parser. Order in the list signifies order of operations.
        /// Caller can customize the operators list by adding/removing elements in the list.
        /// </summary>
        public List<string> Operators { get; } = new List<string> { "d", "k", "l", "!", "/", "x", "*", "-", "+" };

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
                    this.TokenizeLetters(expression, tokens, ref vector, ref i, ch, prev);
                }
                else if (char.IsDigit(ch, 0))
                {
                    // if it's a digit, then increment char until you find the end of the number
                    this.TokenizeNumbers(expression, tokens, ref vector, ref i, ch);
                }
                else if ((i + 1) < expression.Length &&
                         this.Operators.Contains(ch) &&
                         char.IsDigit(expression[i + 1]) && (i == 0 ||
                         ((i - 1) > 0 && prev == this.GroupStartOperator)))
                {
                    // if the above is true, then, the token for that negative number will be "-1", not "-","1".
                    this.TokenizeUnaryOperators(expression, tokens, ref vector, ref i, ch);
                }
                else if (ch == this.GroupStartOperator)
                {
                    // if an open grouping, then if we didn't have an operator, then append the default operator.
                    if (i != 0 && (char.IsDigit(prev, 0) || prev == this.GroupEndOperator))
                    {
                        tokens.Add(this.DefaultOperator);
                    }

                    tokens.Add(ch.ToString());
                }
                else if (ch == this.GroupEndOperator)
                {
                    // if closing grouping and there's no operator, then append the default operator.
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
        /// <param name="config">Dice config to tell whether this result will be bounded or unbounded</param>
        /// <param name="dieRoller">Die roller to use</param>
        /// <returns>Dice result</returns>
        public DiceResult Parse(string expression, DiceConfiguration config, IDieRoller dieRoller)
        {
            this.config = config;

            // first clean up expression
            expression = this.CorrectExpression(expression);

            // then break the expression down into tokens.
            List<string> tokens = this.Tokenize(expression);

            // finally parse and evaluate the expression tokens
            return this.ParseLogic(expression, tokens, dieRoller);
        }
        #endregion

        #region Tokenize helper methods

        /// <summary>
        /// Cleanup the expression text to lower case, remove spaces, and replace duplicate operatiors.
        /// </summary>
        /// <param name="expression">Expression to clean up</param>
        /// <returns>Corrected expression text</returns>
        private string CorrectExpression(string expression)
        {
            // first remove any whitespace from the expression
            string result = whitespaceRegex.Replace(expression.ToLower(), string.Empty);

            // then replace duplicate operators with their resulting value
            result = result.Replace("+-", "-");
            result = result.Replace("-+", "-");
            result = result.Replace("--", "+");

            // replace any percentile notation with appropriate dice faces
            result = result.Replace(PercentileNotation, D100EquivalentNotation);

            return result;
        }

        /// <summary>
        /// Handle processing unary operators and number in the expression and breaking down to the
        /// appropriate tokens.
        /// </summary>
        /// <param name="expression">expression to parse</param>
        /// <param name="tokens">toke list to update</param>
        /// <param name="substring">portion being tokenized</param>
        /// <param name="position">current position in expression</param>
        /// <param name="ch">current character</param>
        private void TokenizeUnaryOperators(string expression, List<string> tokens, ref string substring, ref int position, string ch)
        {
            // adds the operator to the current token string
            substring += ch;

            while ((position + 1) < expression.Length &&
                   (char.IsDigit(expression[position + 1]) || expression[position + 1].ToString() == decimalSeparator))
            {
                // handles processing a number after a single unary operator
                position++;
                substring = substring + expression[position];
            }

            // now add the element to the token list
            tokens.Add(substring);
            substring = string.Empty;
        }

        /// <summary>
        /// Handle processing numbers in the expression and breaking down to the
        /// appropriate tokens.
        /// </summary>
        /// <param name="expression">expression to parse</param>
        /// <param name="tokens">toke list to update</param>
        /// <param name="substring">portion being tokenized</param>
        /// <param name="position">current position in expression</param>
        /// <param name="ch">current character</param>
        private void TokenizeNumbers(string expression, List<string> tokens, ref string substring, ref int position, string ch)
        {
            substring += ch;

            while ((position + 1) < expression.Length &&
                   (char.IsDigit(expression[position + 1]) || expression[position + 1].ToString() == decimalSeparator))
            {
                // keep processing this element while you have digits (support multi-digit numbers)
                position++;
                substring += expression[position];
            }

            // now add the element to the token list
            tokens.Add(substring);
            substring = string.Empty;
        }

        /// <summary>
        /// Handle processing letters in the expression and breaking down to the
        /// appropriate tokens.
        /// </summary>
        /// <param name="expression">expression to parse</param>
        /// <param name="tokens">toke list to update</param>
        /// <param name="substring">portion being tokenized</param>
        /// <param name="position">current position in expression</param>
        /// <param name="ch">current character</param>
        /// <param name="prev">previous character</param>
        private void TokenizeLetters(
            string expression, List<string> tokens, ref string substring, ref int position, string ch, string prev)
        {
            if (position != 0 && (char.IsDigit(prev, 0) || prev == this.GroupEndOperator) && !this.Operators.Contains(ch))
            {
                tokens.Add(this.DefaultOperator);
            }

            // if we have a single die operator (d), then default to having a default
            // number of dice (1)
            if (ch == "d" && (string.IsNullOrEmpty(prev) ||
                              this.Operators.Contains(prev) ||
                              prev == this.GroupStartOperator))
            {
                tokens.Add(this.DefaultNumDice);
            }

            // append the current character
            substring += ch;

            if (!this.Operators.Contains(ch))
            {
                // if the character isn't an operator, then loop ahead while the expression has letters
                while ((position + 1) < expression.Length && char.IsLetterOrDigit(expression[position + 1]))
                {
                    position++;
                    substring += expression[position];
                }
            }

            // now add the element to the tokens
            tokens.Add(substring);
            substring = string.Empty;
        }
        #endregion

        #region Parsing helper methods

        /// <summary>
        /// Parses the specified list of tokens with appropriate logic to convert
        /// it into a Dice instance to evaluate oprations on.
        /// </summary>
        /// <param name="expression">dice expression rolled</param>
        /// <param name="tokens">String expression to parse</param>
        /// <param name="dieRoller">Die roller to use</param>
        /// <returns>Dice result</returns>
        private DiceResult ParseLogic(string expression, List<string> tokens, IDieRoller dieRoller)
        {
            List<TermResult> results = new List<TermResult>();

            while (tokens.IndexOf("(") != -1)
            {
                // getting data between grouping symbols: "(" and ")"
                int open = tokens.LastIndexOf("(");
                int close = tokens.IndexOf(")", open);

                if (open >= close)
                {
                    throw new ArithmeticException("No matching close-open parenthesis.");
                }

                // get a subexpression list for elements within the grouping symbols
                List<string> subExpression = new List<string>();
                for (var i = open + 1; i < close; i++)
                {
                    subExpression.Add(tokens[i]);
                }

                // run the operations on the subexpression
                int subValue = this.HandleBasicOperation(results, subExpression, dieRoller);

                // when subexpression calculation is done, replace the grouping start symbol
                // and removing the tokens for the subexpression from the token list
                tokens[open] = subValue.ToString();
                tokens.RemoveRange(open + 1, close - open);
            }

            // at this point, we should have replaced all groups in the expression
            // with the appropriate values, so need to calculate last simple expression
            int value = this.HandleBasicOperation(results, tokens, dieRoller);

            // now return the dice result from the final value and TermResults list
            return new DiceResult(expression, value, results, dieRoller.GetType().ToString(), this.config);
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
                // if there's nothing in the list, just return 0
                return 0;
            }
            else if (tokens.Count == 1)
            {
                // if there is only one token, then it much be a constant
                return int.Parse(tokens[0]);
            }

            // loop through each operator in our operator list
            // operators order in the list signify their order of operations
            foreach (var op in this.Operators)
            {
                // loop through all of the tokens until we find the operator in the list
                while (tokens.IndexOf(op) != -1)
                {
                    try
                    {
                        if (op == "d")
                        {
                            // if current operator is the die operator, then process
                            // that part of the expression accordingly
                            this.HandleDieOperator(results, tokens, op, dieRoller);
                        }
                        else
                        {
                            // otherwise, treat the operator as an arimethic operator,
                            // and perform the correct math operation
                            this.HandleArithmeticOperators(results, tokens, op);
                        }
                    }
                    catch (Exception ex)
                    {
                        // if any error happens within this processing, then throw an exception
                        throw new FormatException("Dice expression string is incorrect format.", ex);
                    }
                }

                // if we are out of tokens, then just stop processing
                if (tokens.Count == 0)
                {
                    break;
                }
            }

            if (tokens.Count == 1)
            {
                // if there is only one token left, then return it as the evaluation of this list of tokens
                return int.Parse(tokens[0]);
            }
            else
            {
                // if there are left over toknes, then the parsing/evaluation failed
                throw new FormatException("Dice expression string is incorrect format: unexpected symbols in the string expression.");
            }
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
            // find the previous and next numbers in the token list
            var opPosition = tokens.IndexOf(op);
            var numberA = int.Parse(tokens[opPosition - 1]);
            var numberB = int.Parse(tokens[opPosition + 1]);

            // find the action that corresponds to the current operator, then
            // run that action to evaluate the math function
            int result = this.OperatorActions[op](numberA, numberB);

            // put the evaluation result in the first entry and remove
            // the remaining processed tokens
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
            // find the previous and next numbers in the token list
            int opPosition = tokens.IndexOf(op);
            int sides = 0;
            int? choose = null, explode = null;
            int length = 0;

            int numDice = int.Parse(tokens[opPosition - 1]);

            // allow default value for dice (if not digit is specified)
            if (opPosition + 1 < tokens.Count && char.IsDigit(tokens[opPosition + 1], 0))
            {
                sides = int.Parse(tokens[opPosition + 1]);
                length += 2;
            }
            else
            {
                sides = this.config.DefaultDieSides;
                length++;
            }

            // look-ahead to find other dice operators (like the choose-keep/drop operators)
            int keepPos = tokens.IndexOf("k");
            if (keepPos > 0)
            {
                // if that operator is found, then get the next number token
                choose = int.Parse(tokens[keepPos + 1]);
                length += 2;
            }

            int dropPos = tokens.IndexOf("l");
            if (dropPos > 0)
            {
                // if that operator is found, then get the next number token
                choose = numDice - int.Parse(tokens[dropPos + 1]);
                length += 2;
            }

            int explodePos = tokens.IndexOf("!");
            if (explodePos > 0)
            {
                // if that operator is found, then get the associated number
                if (explodePos + 1 < tokens.Count && char.IsDigit(tokens[explodePos + 1], 0))
                {
                    explode = int.Parse(tokens[explodePos + 1]);
                    length += 2;
                }
                else
                {
                    explode = sides;
                    length++;
                }
            }

            // create a dice term based on the values
            DiceTerm term = new DiceTerm(numDice, sides, 1, choose, explode);

            // then evaluate the dice term to roll dice and get the result
            IReadOnlyList<TermResult> t = term.CalculateResults(dieRoller);
            int value = t.Sum(r => (int)Math.Round(r.Value * r.Scalar));
            results.AddRange(t);

            // put the evaluation result in the first entry and remove
            // the remaining processed tokens
            tokens[opPosition - 1] = value.ToString();
            tokens.RemoveRange(opPosition, length);
        }
        #endregion
    }
}
