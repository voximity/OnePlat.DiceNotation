// <copyright file="DiceTerm.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/8/2017
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
    /// Dice expression term that represents dice roll values.
    /// </summary>
    public class DiceTerm : IExpressionTerm
    {
        #region Members
        private const string DiceFormatResultType = "{0}.d{1}";
        private const string DiceFormatDiceTermText = "{0}d{1}{2}";
        private const string FormatDiceMultiplyTermText = "{0}d{1}{2}x{3}";
        private const string FormatDiceDivideTermText = "{0}d{1}{2}/{3}";
        private const int MaxRerollsAllowed = 1000;

        private int numberDice;
        private int sides;
        private double scalar;
        private int? choose;
        private int? exploding;
        #endregion

        #region Constuctor

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceTerm"/> class.
        /// </summary>
        /// <param name="numberDice">Number of dice in the expression</param>
        /// <param name="sides">Type of die based on number of sides</param>
        /// <param name="scalar">Scalar multiplier to dice term</param>
        /// <param name="choose">How many dice to use (value should be between 1 and number of dice)</param>
        /// <param name="exploding">Exploding threshold for dice re-rolls</param>
        public DiceTerm(int numberDice, int sides, double scalar = 1, int? choose = null, int? exploding = null)
        {
            if (numberDice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberDice), "Number of dice must be greater than 0.");
            }

            if (sides < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(sides), "Dice sides must be greater than 1.");
            }

            if (scalar == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(scalar), "Scalar multiplier cannot be 0.");
            }

            if (choose == 0 || choose > numberDice || choose < -numberDice)
            {
                throw new ArgumentOutOfRangeException(nameof(choose), "Choose must be greater than 0 and less than number of dice.");
            }

            if (exploding <= 0 || exploding > sides)
            {
                throw new ArgumentOutOfRangeException(nameof(exploding), "Exploding threshold must be greater than 0 and less than die faces.");
            }

            this.numberDice = numberDice;
            this.sides = sides;
            this.scalar = scalar;
            this.choose = choose;
            this.exploding = exploding;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the format string for ResultTerm type.
        /// </summary>
        protected string FormatResultType { get; set; } = DiceFormatResultType;

        /// <summary>
        /// Gets or sets the format string for die operator display text.
        /// </summary>
        protected string FormatDiceTermText { get; set; } = DiceFormatDiceTermText;
        #endregion

        #region IExpressionTerm methods

        /// <inheritdoc/>
        public IReadOnlyList<TermResult> CalculateResults(IDieRoller dieRoller)
        {
            // ensure we have a die roller.
            if (dieRoller == null)
            {
                throw new ArgumentNullException(nameof(dieRoller));
            }

            List<TermResult> results = new List<TermResult>();
            string termType = string.Format(this.FormatResultType, this.GetType().Name, this.sides);
            int rerolls = 0;

            // go through the number of dice and roll each one, saving them as term results.
            for (int i = 0; i < this.numberDice + rerolls; i++)
            {
                int value = this.RollTerm(dieRoller, this.sides);
                if (this.exploding != null && value >= this.exploding)
                {
                    if (rerolls > MaxRerollsAllowed)
                    {
                        throw new OverflowException("Rolling dice past the maximum allowed number of rerolls.");
                    }

                    rerolls++;
                }

                results.Add(new TermResult
                {
                    Scalar = this.scalar,
                    Value = value,
                    Type = termType
                });
            }

            // order by their value (high to low) and only take the amount specified in choose.
            int tempChoose = this.choose ?? results.Count;
            var ordered = (tempChoose > 0) ? results.OrderByDescending(d => d.Value).ToList() : results.OrderBy(d => d.Value).ToList();
            for (int i = Math.Abs(tempChoose); i < ordered.Count(); i++)
            {
                ordered[i].AppliesToResultCalculation = false;
            }

            return results;
        }
        #endregion

        #region Helper methods

        /// <inheritdoc/>
        public override string ToString()
        {
            string variableText = (this.choose == null || this.choose == this.numberDice) ? string.Empty : "k" + this.choose;
            variableText += (this.exploding == null) ? string.Empty : "!" + this.exploding;
            string result;

            if (this.scalar == 1)
            {
                result = string.Format(this.FormatDiceTermText, this.numberDice, this.sides, variableText);
            }
            else if (this.scalar == -1)
            {
                result = string.Format(this.FormatDiceTermText, -this.numberDice, this.sides, variableText);
            }
            else if (this.scalar > 1)
            {
                result = string.Format(FormatDiceMultiplyTermText, this.numberDice, this.sides, variableText, this.scalar);
            }
            else
            {
                result = string.Format(FormatDiceDivideTermText, this.numberDice, this.sides, variableText, (int)(1 / this.scalar));
            }

            return result;
        }

        /// <summary>
        /// Performs the actual dice rolls for this term.
        /// </summary>
        /// <param name="dieRoller">IDieRoller to use</param>
        /// <param name="sides">Number of sides to roll</param>
        /// <returns>Returns rolled value.</returns>
        protected virtual int RollTerm(IDieRoller dieRoller, int sides)
        {
            return dieRoller.Roll(sides);
        }
        #endregion
    }
}
