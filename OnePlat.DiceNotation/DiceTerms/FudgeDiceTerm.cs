// <copyright file="FudgeDiceTerm.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/23/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/24/2017
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
using OnePlat.DiceNotation.DieRoller;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnePlat.DiceNotation.DiceTerms
{
    /// <summary>
    /// Fudge dice expression term that represents dice roll values.
    /// </summary>
    public class FudgeDiceTerm : IExpressionTerm
    {
        #region Members
        private const string FormatResultType = "{0}.dF";
        private const string FormatDiceTermText = "{0}f{1}";
        private const int FudgeNumberSides = 3;
        private const int FudgeFactor = -2;

        private int numberDice;
        private int? choose;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FudgeDiceTerm"/> class.
        /// </summary>
        /// <param name="numberDice">Number of dice in the expression</param>
        /// <param name="choose">How many dice to use (value should be between 1 and number of dice)</param>
        public FudgeDiceTerm(int numberDice, int? choose = null)
        {
            if (numberDice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberDice), "Number of dice must be greater than 0.");
            }

            if (choose <= 0 || choose > numberDice)
            {
                throw new ArgumentOutOfRangeException(nameof(choose), "Choose must be greater than 0 and less than number of dice.");
            }

            this.numberDice = numberDice;
            this.choose = choose;
        }

        /// <inheritdoc/>
        public IReadOnlyList<TermResult> CalculateResults(IDieRoller dieRoller)
        {
            // ensure we have a die roller.
            if (dieRoller == null)
            {
                throw new ArgumentNullException(nameof(dieRoller));
            }

            List<TermResult> results = new List<TermResult>();
            string termType = string.Format(FormatResultType, this.GetType().Name);

            // go through the number of dice and roll each one, saving them as term results.
            for (int i = 0; i < this.numberDice; i++)
            {
                int value = dieRoller.Roll(FudgeNumberSides, FudgeFactor);

                results.Add(new TermResult
                {
                    Scalar = 1,
                    Value = value,
                    Type = termType
                });
            }

            // order by their value (high to low) and only take the amount specified in choose.
            return results.OrderByDescending(d => d.Value).Take(this.choose ?? results.Count).ToList();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string variableText = (this.choose == null || this.choose == this.numberDice) ? string.Empty : "k" + this.choose;
            string result = string.Format(FormatDiceTermText, this.numberDice, variableText);

            return result;
        }
    }
}
