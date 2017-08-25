// <copyright file="DiceResult.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
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
using OnePlat.DiceNotation.DiceTerms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Class the represents the results for any dice expression calculation.
    /// </summary>
    public class DiceResult
    {
        private const string FudgeDiceIdentifier = "f";

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceResult"/> class.
        /// </summary>
        /// <param name="expression">dice expression rolled</param>
        /// <param name="results">List for results for each term in dice expression</param>
        /// <param name="rollerUsed">Define die roller used to get the results</param>
        /// <param name="config">Dice config to tell whether this result will be bounded or unbounded</param>
        public DiceResult(string expression, List<TermResult> results, string rollerUsed, DiceConfiguration config)
            : this(expression, results.Sum(r => (int)Math.Round(r.AppliesToResultCalculation ? r.Value * r.Scalar : 0)), results, rollerUsed, config)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceResult"/> class.
        /// </summary>
        /// <param name="expression">dice expression rolled</param>
        /// <param name="value">calculated value of the results</param>
        /// <param name="results">List for results for each term in dice expression</param>
        /// <param name="rollerUsed">Define die roller used to get the results</param>
        /// <param name="config">Dice config to tell whether this result will be bounded or unbounded</param>
        public DiceResult(string expression, int value, List<TermResult> results, string rollerUsed, DiceConfiguration config)
        {
            this.DiceExpression = expression;
            this.DieRollerUsed = rollerUsed;
            this.Results = results.ToList();

            bool boundedResult = expression.Contains(FudgeDiceIdentifier) ? false : config.HasBoundedResult;
            this.Value = boundedResult ? Math.Max(value, config.BoundedResultMinimum) : value;
        }

        /// <summary>
        /// Gets the dice expression that was evaluated.
        /// </summary>
        public string DiceExpression { get; private set; }

        /// <summary>
        /// Gets the die roller used to generate this result.
        /// </summary>
        public string DieRollerUsed { get; private set; }

        /// <summary>
        /// Gets the results list for all of the terms in this dice expression.
        /// </summary>
        public IReadOnlyList<TermResult> Results { get; private set; }

        /// <summary>
        /// Gets the numeric value for this result.
        /// </summary>
        public int Value { get; private set; }
    }
}
