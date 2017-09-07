// <copyright file="RandomDieRoller.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/7/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 9/5/2017
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

namespace OnePlat.DiceNotation.DieRoller
{
    /// <summary>
    /// Die roller that rolls a random number between 1 and number of sides.
    /// This uses the .NET framework random number generator (to its a
    /// pseudo-random number).
    /// </summary>
    public class RandomDieRoller : RandomDieRollerBase
    {
        private static readonly Random DefaultRandomGenerator = new Random();
        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDieRoller"/> class.
        /// Uses the default static random number generator.
        /// </summary>
        /// <param name="tracker">Tracking service to remember die rolls</param>
        public RandomDieRoller(IDieRollTracker tracker = null)
            : this(DefaultRandomGenerator, tracker)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDieRoller"/> class.
        /// </summary>
        /// <param name="random">Random number generator to use</param>
        /// <param name="tracker">Tracking service to remember die rolls</param>
        public RandomDieRoller(Random random, IDieRollTracker tracker)
            : base(tracker)
        {
            this.random = random;
        }

        /// <inheritdoc/>
        protected override int GetNextRandom(int sides)
        {
            return this.random.Next(sides) + 1;
        }
    }
}
