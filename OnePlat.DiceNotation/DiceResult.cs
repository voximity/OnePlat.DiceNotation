// <copyright file="DiceResult.cs" company="DarthPedro">
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
using OnePlat.DiceNotation.DiceTerms;
using System.Collections.Generic;
using System.Linq;

namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Class the represents the results for any dice expression calculation.
    /// </summary>
    public class DiceResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceResult"/> class.
        /// </summary>
        /// <param name="results">List for results for each term in dice expression</param>
        /// <param name="rollerUsed">Define die roller used to get the results</param>
        public DiceResult(IEnumerable<TermResult> results, string rollerUsed)
        {
            this.DieRollerUsed = rollerUsed;
            this.Results = results.ToList();
            this.Value = results.Sum(r => r.Value * r.Scalar);
        }

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
