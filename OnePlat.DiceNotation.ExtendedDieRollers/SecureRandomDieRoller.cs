// <copyright file="SecureRandomDieRoller.cs" company="DarthPedro">
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
using System;
using System.Security.Cryptography;

namespace OnePlat.DiceNotation.DieRoller
{
    /// <summary>
    /// Die roller class that user Cryptography API to generate more verifiable
    /// secure random numbers.
    /// </summary>
    public class SecureRandomDieRoller : IDieRoller
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private IDieRollTracker tracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureRandomDieRoller"/> class.
        /// </summary>
        /// <param name="tracker">Die roll tracker to use; default to no tracker</param>
        public SecureRandomDieRoller(IDieRollTracker tracker = null)
        {
            this.tracker = tracker;
        }

        /// <inheritdoc/>
        public int Roll(int sides, int? factor = 0)
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
        /// Calculates the next random number using the Cryptography API.
        /// </summary>
        /// <param name="sides">Number of die sides to roll</param>
        /// <returns>Random value between 1 and number of sides (inclusive).</returns>
        private int GetNextRandom(int sides)
        {
            if (sides < 2)
            {
                throw new ArgumentOutOfRangeException("sides");
            }

            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(randomNumber);
            }
            while (!this.IsFairRoll(randomNumber[0], sides));

            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % sides) + 1);
        }

        private bool IsFairRoll(byte roll, int sides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = byte.MaxValue / sides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < sides * fullSetsOfValues;
        }
    }
}
