// <copyright file="TermResult.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/8/2017
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
namespace OnePlat.DiceNotation.DiceTerms
{
    /// <summary>
    /// The resulting role for a single term in dice notation.
    /// </summary>
    public class TermResult
    {
        /// <summary>
        /// Gets or sets the scalar multiplier of this result.
        /// </summary>
        public double Scalar { get; set; }

        /// <summary>
        /// Gets or sets the result value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the term
        /// </summary>
        public string Type { get; set; }
    }
}
