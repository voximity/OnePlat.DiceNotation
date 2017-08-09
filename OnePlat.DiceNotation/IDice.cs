// <copyright file="IDice.cs" company="DarthPedro">
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

namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Interface that defines operations on dice.
    /// </summary>
    public interface IDice
    {
        /// <summary>
        /// Creates a DiceTerm with specified values for this dice expression.
        /// </summary>
        /// <param name="sides">sides of die</param>
        /// <param name="numberDice">number of dice</param>
        /// <param name="choose">choose how many results to return</param>
        /// <returns>IDice representing the current terms.</returns>
        IDice Dice(int sides, int numberDice = 1, int? choose = null);

        /// <summary>
        /// Creates a ConstantTerm with the specified value for this dice expression.
        /// </summary>
        /// <param name="constant">Constant value</param>
        /// <returns>IDice representing the current terms.</returns>
        IDice Constant(int constant);

        /// <summary>
        /// Creates the expression terms from the parsed text string.
        /// </summary>
        /// <param name="expression">Expression string to parse</param>
        /// <returns>IDice representing the parsed terms.</returns>
        IDice Parse(string expression);

        /// <summary>
        /// Rolls the dice for all of the terms in this expression.
        /// </summary>
        /// <param name="dieRoller">Die roller to use in calculations</param>
        /// <returns>Dice results</returns>
        DiceResult Roll(IDieRoller dieRoller);
    }
}
