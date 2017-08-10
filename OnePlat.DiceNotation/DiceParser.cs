// <copyright file="DiceParser.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/9/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/10/2017
//-----------------------------------------------------------------------
// <summary>
//       This project is licensed under the MS-PL license.
//
//       OnePlat.DiceNotation is an open source project that parses,
//  evalutes, and rolls dice that conform to the defined Dice notiation.
//  This notation is usable in any form of random dice games and role-playing
//  games like D&D and d20.
// </summary>
//-----------------------------------------------------------------------
using System;
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
        private const char OperatorDie = 'd';
        private const char OperatorChoose = 'k';
        private const char OperatorAdd = '+';
        private const char OperatorSubtract = '-';
        private const char OperatorMultiply = 'x';
        private const char OperatorDivide = '/';
        private static Regex whitespaceRegex = new Regex(@"\s+");
        #endregion

        #region Main Parse method

        /// <summary>
        /// Parses the specified dice expression and converts it into a
        /// Dice instance to perform oprations on.
        /// </summary>
        /// <param name="expression">String expression to parse</param>
        /// <returns>Dice instance</returns>
        public IDice Parse(string expression)
        {
            // first clean up expression
            expression = whitespaceRegex.Replace(expression.ToLower(), string.Empty);
            expression = expression.Replace("+-", "-");

            IDice dice = new Dice();
            DiceToken token = new DiceToken();

            // loop through the expression characters
            for (int i = 0; i < expression.Length; ++i)
            {
                char ch = expression[i];
                if (char.IsDigit(ch))
                {
                    token.Constant += ch;
                }
                else if (ch == OperatorDie)
                {
                    this.HandleOperationDie(token, expression, ref i);
                }
                else if (ch == OperatorChoose)
                {
                    this.HandleOperationChoose(token, expression, ref i);
                }
                else if (ch == OperatorAdd)
                {
                    this.HandleOperationAdd(dice, ref token);
                }
                else if (ch == OperatorSubtract)
                {
                    this.HandleOperationSubract(dice, ref token);
                }
                else if (ch == OperatorMultiply)
                {
                    this.HandleOperationMultiply(dice, token, expression, ref i);
                }
                else if (ch == OperatorDivide)
                {
                    this.HandleOperationDivide(dice, token, expression, ref i);
                }
                else
                {
                    throw new ArgumentException("Invalid character in dice notation expression", nameof(expression));
                }
            }

            this.Append(dice, token);

            return dice;
        }
        #endregion

        #region Parsing helper methods

        /// <summary>
        /// Parsing handler for the Die operation.
        /// </summary>
        /// <param name="token">Token to apply changes to</param>
        /// <param name="expression">Expression string to peek</param>
        /// <param name="i">Parsing counter at current location in expression</param>
        private void HandleOperationDie(DiceToken token, string expression, ref int i)
        {
            if (token.Constant == string.Empty)
            {
                token.Constant = "1";
            }

            token.NumberDice = int.Parse(token.Constant);
            token.Sides = int.Parse(this.DigitLookAhead(expression, ref i));
            token.Constant = string.Empty;
        }

        /// <summary>
        /// Parsing handler for the Choose operations
        /// </summary>
        /// <param name="token">Token to apply changes to</param>
        /// <param name="expression">Expression string to peek</param>
        /// <param name="i">Parsing counter at current location in expression</param>
        private void HandleOperationChoose(DiceToken token, string expression, ref int i)
        {
            token.Choose = int.Parse(this.DigitLookAhead(expression, ref i));
        }

        /// <summary>
        /// Parsing handler for the Add operation.
        /// </summary>
        /// <param name="dice">Dice to apply changes to</param>
        /// <param name="token">Token to apply changes to</param>
        private void HandleOperationAdd(IDice dice, ref DiceToken token)
        {
            this.Append(dice, token);
            token = new DiceToken();
        }

        /// <summary>
        /// Parsing handler for the Subract operation.
        /// </summary>
        /// <param name="dice">Dice to apply changes to</param>
        /// <param name="token">Token to apply changes to</param>
        private void HandleOperationSubract(IDice dice, ref DiceToken token)
        {
            this.Append(dice, token);
            token = new DiceToken()
            {
                Scalar = -1
            };
        }

        /// <summary>
        /// Parsing handler for the Multiply operation.
        /// </summary>
        /// <param name="dice">Dice to apply changes to</param>
        /// <param name="token">Token to apply changes to</param>
        /// <param name="expression">Expression string to peek</param>
        /// <param name="i">Parsing counter at current location in expression</param>
        private void HandleOperationMultiply(IDice dice, DiceToken token, string expression, ref int i)
        {
            if (token.Sides > 0)
            {
                string scalar = this.DigitLookAhead(expression, ref i);
                token.Scalar *= int.Parse(scalar);
            }
            else
            {
                token.Scalar *= int.Parse(token.Constant);
                token.Constant = string.Empty;
            }
        }

        /// <summary>
        /// Parsing handler for the Divide operation.
        /// </summary>
        /// <param name="dice">Dice to apply changes to</param>
        /// <param name="token">Token to apply changes to</param>
        /// <param name="expression">Expression string to peek</param>
        /// <param name="i">Parsing counter at current location in expression</param>
        private void HandleOperationDivide(IDice dice, DiceToken token, string expression, ref int i)
        {
            if (token.Sides > 0)
            {
                string scalar = this.DigitLookAhead(expression, ref i);
                token.Scalar *= 1 / double.Parse(scalar);
            }
            else
            {
                throw new ArgumentException("Invalid division expression in dice notation", nameof(expression));
            }
        }

        /// <summary>
        /// Looks ahead in the expression until it finds a non-digit.
        /// </summary>
        /// <param name="expression">Expression string to peek</param>
        /// <param name="i">Parsing counter at current location in expression</param>
        /// <returns>expression substring that was peeked</returns>
        private string DigitLookAhead(string expression, ref int i)
        {
            string result = string.Empty;

            while (i + 1 < expression.Length && char.IsDigit(expression[i + 1]))
            {
                result += expression[i + 1];
                ++i;
            }

            return result;
        }

        /// <summary>
        /// Helper method to append to current Dice the correct IExpressionTerm
        /// based on the specified token.
        /// </summary>
        /// <param name="dice">Dice to append data to</param>
        /// <param name="token">Token to operate on</param>
        private void Append(IDice dice, DiceToken token)
        {
            int pendingValue = string.IsNullOrEmpty(token.Constant) ? token.Sides : int.Parse(token.Constant);
            if (token.NumberDice == 0)
            {
                dice.Constant((int)(token.Scalar * pendingValue));
            }
            else
            {
                dice.Dice(pendingValue, token.NumberDice, token.Scalar, token.Choose);
            }
        }
        #endregion
    }
}
