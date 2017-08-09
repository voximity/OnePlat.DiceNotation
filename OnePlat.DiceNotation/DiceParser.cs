// <copyright file="DiceParser.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/9/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/9/2017
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
                    this.HandleOperationDie(token);
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
                    this.HandleOperationMultiply(token);
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
        private void HandleOperationDie(DiceToken token)
        {
            if (token.Constant == string.Empty)
            {
                token.Constant = "1";
            }

            token.NumberDice = int.Parse(token.Constant);
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
            string choose = string.Empty;

            while (i + 1 < expression.Length && char.IsDigit(expression[i + 1]))
            {
                choose += expression[i + 1];
                ++i;
            }

            token.Choose = int.Parse(choose);
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
            token = new DiceToken();
            token.Scalar = -1;
        }

        /// <summary>
        /// Parsing handler for the Multiply operation.
        /// </summary>
        /// <param name="token">Token to apply changes to</param>
        private void HandleOperationMultiply(DiceToken token)
        {
            token.Scalar *= int.Parse(token.Constant);
            token.Constant = string.Empty;
        }

        /// <summary>
        /// Helper method to append to current Dice the correct IExpressionTerm
        /// based on the specified token.
        /// </summary>
        /// <param name="dice">Dice to append data to</param>
        /// <param name="token">Token to operate on</param>
        private void Append(IDice dice, DiceToken token)
        {
            int constant = int.Parse(token.Constant);
            if (token.NumberDice == 0)
            {
                dice.Constant(token.Scalar * constant);
            }
            else
            {
                dice.Dice(constant, token.NumberDice, token.Scalar, token.Choose);
            }
        }
        #endregion
    }
}
