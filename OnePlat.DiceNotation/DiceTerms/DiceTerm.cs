// <copyright file="DiceTerm.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/8/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/8/2017
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
using OnePlat.DiceNotation.DieRoller;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnePlat.DiceNotation.DiceTerms
{
    /// <summary>
    /// Dice expression term that represents dice roll values.
    /// </summary>
    public class DiceTerm : IExpressionTerm
    {
        private const string FormatResultType = "{0}.d{1}";
        private const string FormatDiceTermText = "{0}d{1}{2}";

        private int numberDice;
        private int sides;
        private int choose;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceTerm"/> class.
        /// </summary>
        /// <param name="numberDice">Number of dice in the expression</param>
        /// <param name="sides">Type of die based on number of sides</param>
        /// <param name="choose">How many dice to use (value should be between 1 and number of dice)</param>
        public DiceTerm(int numberDice, int sides, int? choose = null)
        {
            if (numberDice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberDice));
            }

            if (sides < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(sides));
            }

            this.choose = choose ?? numberDice;
            if (this.choose <= 0 || this.choose > numberDice)
            {
                throw new ArgumentOutOfRangeException(nameof(choose));
            }

            this.numberDice = numberDice;
            this.sides = sides;
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
            string termType = string.Format(FormatResultType, this.GetType().Name, this.sides);

            // go through the number of dice and roll each one, saving them as term results.
            for (int i = 0; i < this.numberDice; i++)
            {
                results.Add(new TermResult
                {
                    Scalar = 1,
                    Value = dieRoller.Roll(this.sides),
                    Type = termType
                });
            }

            // order by their value (high to low) and only take the amount specified in choose.
            return results.OrderByDescending(d => d.Value).Take(this.choose).ToList();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string chooseText = (this.choose == this.numberDice) ? string.Empty : "k" + this.choose;
            return string.Format(FormatDiceTermText, this.numberDice, this.sides, chooseText);
        }
    }
}
