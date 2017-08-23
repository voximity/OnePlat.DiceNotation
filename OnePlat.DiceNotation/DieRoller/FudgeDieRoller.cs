// <copyright file="FudgeDieRoller.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/23/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/23/2017
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
using System;
using System.Diagnostics;

namespace OnePlat.DiceNotation.DieRoller
{
    /// <summary>
    /// Die roller for fudge/fate dice
    /// </summary>
    public class FudgeDieRoller : IDieRoller
    {
        private const int DieSides = 6;
        private static readonly Random RandomGenerator = new Random();

        /// <inheritdoc/>
        public int Roll(int sides)
        {
            // fudge dice roll three values: -1, 0, 1
            int rand = RandomGenerator.Next(6);
            int result = (rand / 2) - 1;
            Debug.WriteLine("Fudge die roll - raw:{0}, calculation:{1}", rand, result);
            return result;
        }
    }
}
