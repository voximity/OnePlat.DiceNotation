// <copyright file="IDieRoller.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/7/2017
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
namespace OnePlat.DiceNotation.DieRoller
{
    /// <summary>
    /// Interface defining the random die roller.
    /// </summary>
    public interface IDieRoller
    {
        /// <summary>
        /// Rolls the die with the specified number of sides.
        /// </summary>
        /// <param name="sides">Number of sides on the die (also its max value).</param>
        /// <returns>Random value between 1 and sides</returns>
        int Roll(int sides);
    }
}
