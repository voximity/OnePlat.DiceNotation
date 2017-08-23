// <copyright file="DiceConfiguration.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/21/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/22/2017
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
namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Class to encapsulate dice configuration which allows users to customize
    /// some of the default behavior of the DiceNotation system.
    /// </summary>
    public class DiceConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether these dice have their results bounded to 1 or greater.
        /// </summary>
        public bool HasBoundedResult { get; set; } = true;

        /// <summary>
        /// Gets or sets the value for the bounded minimum
        /// </summary>
        public int BoundedResultMinimum { get; set; } = 1;

        /// <summary>
        /// Gets or sets the defualt sides of dice to use when it's omitted from dice notation.
        /// </summary>
        public int DefaultDieSides { get; set; } = 6;
    }
}
