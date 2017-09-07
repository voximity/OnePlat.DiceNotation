// <copyright file="RandomDieRollerBase.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 9/5/2017
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
namespace OnePlat.DiceNotation.DieRoller
{
    /// <summary>
    /// Die roller base class that rolls a random number between 1 and number of sides.
    /// This base class is used by all random rollers to implement standard IDieRoller behavior.
    /// </summary>
    public abstract class RandomDieRollerBase : IDieRoller
    {
        private IDieRollTracker tracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDieRollerBase"/> class.
        /// Uses the default static random number generator.
        /// </summary>
        /// <param name="tracker">Tracking service to remember die rolls</param>
        public RandomDieRollerBase(IDieRollTracker tracker = null)
        {
            this.tracker = tracker;
        }

        /// <inheritdoc/>
        public int Roll(int sides, int? factor = null)
        {
            // roll the actual random value
            int result = this.GetNextRandom(sides);
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

        /// <summary>
        /// Method that calculates the actual next random number.
        /// Abstract method that must be implemented by each random roller.
        /// </summary>
        /// <param name="sides">Number of sides on the die (also its max value).</param>
        /// <returns>Random value between 1 and sides</returns>
        protected abstract int GetNextRandom(int sides);
    }
}
