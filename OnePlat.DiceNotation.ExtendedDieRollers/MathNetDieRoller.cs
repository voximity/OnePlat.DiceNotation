// <copyright file="MathNetDieRoller.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 9/3/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 9/3/2017
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
using MathNet.Numerics.Random;
using System;

namespace OnePlat.DiceNotation.DieRoller
{
    /// <summary>
    /// Die roller class that uses MathNet numerics library to generate random numbers.
    /// </summary>
    public class MathNetDieRoller : IDieRoller
    {
        private static RandomSource randomSource;
        private IDieRollTracker tracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathNetDieRoller"/> class.
        /// </summary>
        public MathNetDieRoller()
            : this(new MersenneTwister(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathNetDieRoller"/> class.
        /// </summary>
        /// <param name="source">Random source to use</param>
        /// <param name="tracker">Die roll tracker to use; null means don't track roll data</param>
        public MathNetDieRoller(RandomSource source, IDieRollTracker tracker = null)
        {
            randomSource = source ?? throw new ArgumentNullException(nameof(source));
            this.tracker = tracker;
        }

        /// <inheritdoc/>
        public int Roll(int sides, int? factor = null)
        {
            if (sides < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(sides));
            }

            // roll the actual random value
            int result = randomSource.Next(0, sides) + 1;
            if (factor != null)
            {
                result += factor.Value;
            }

            // if the user provided a roll tracker, then use it
            if (this.tracker != null)
            {
                this.tracker.AddDieRoll(sides, result, this.GetType());
            }

            return result;
        }
    }
}
