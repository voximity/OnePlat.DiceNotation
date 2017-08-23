// <copyright file="IDice.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/8/2017
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
using OnePlat.DiceNotation.DieRoller;

namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Interface that defines operations on dice.
    /// </summary>
    public interface IDice
    {
        /// <summary>
        /// Gets the current configuration for this set of Dice.
        /// </summary>
        DiceConfiguration Configuration { get; }

        /// <summary>
        /// Creates a DiceTerm with specified values for this dice expression.
        /// </summary>
        /// <param name="sides">sides of die</param>
        /// <param name="numberDice">number of dice</param>
        /// <param name="scalar">scalar multiplier</param>
        /// <param name="choose">choose how many results to return</param>
        /// <param name="exploding">Exploding threshold for dice re-rolls</param>
        /// <returns>IDice representing the current terms.</returns>
        IDice Dice(int sides, int numberDice = 1, double scalar = 1, int? choose = null, int? exploding = null);

        /// <summary>
        /// Creates a ConstantTerm with the specified value for this dice expression.
        /// </summary>
        /// <param name="constant">Constant value</param>
        /// <returns>IDice representing the current terms.</returns>
        IDice Constant(int constant);

        /// <summary>
        /// Concatenates the terms of another Dice expression into this Dice expression.
        /// </summary>
        /// <param name="otherDice">Other IDice terms to concatentate</param>
        /// <returns>IDice representing the current terms.</returns>
        IDice Concat(IDice otherDice);

        /// <summary>
        /// Rolls the dice for all of the terms in this expression.
        /// </summary>
        /// <param name="dieRoller">Die roller to use in calculations</param>
        /// <returns>Dice results</returns>
        DiceResult Roll(IDieRoller dieRoller);

        /// <summary>
        /// Rolls the dice for the dice expression as a string.
        /// </summary>
        /// <param name="expression">Expression string to parse</param>
        /// <param name="dieRoller">Die roller to use in calculations</param>
        /// <returns>Dice results</returns>
        DiceResult Roll(string expression, IDieRoller dieRoller);
    }
}
