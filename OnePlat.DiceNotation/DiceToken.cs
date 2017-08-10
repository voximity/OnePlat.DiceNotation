// <copyright file="DiceToken.cs" company="DarthPedro">
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
namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Token accumulator class for DiceParser.
    /// </summary>
    internal class DiceToken
    {
        /// <summary>
        /// Gets or sets the Constant value.
        /// </summary>
        public string Constant { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scalar multiplier.
        /// </summary>
        public int Scalar { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of dice in the expression.
        /// </summary>
        public int NumberDice { get; set; }

        /// <summary>
        /// Gets or sets the dice sides in the expression.
        /// </summary>
        public int Sides { get; set; }

        /// <summary>
        /// Gets or sets the choose operator in the expression.
        /// </summary>
        public int? Choose { get; set; }
    }
}
