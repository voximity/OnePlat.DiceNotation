// <copyright file="ConstantDieRoller.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/7/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/24/2017
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
    /// Die roller that always returns a constant value.
    /// Useful for testing and providing non-random rolls.
    /// </summary>
    public class ConstantDieRoller : IDieRoller
    {
        private const int DefaultRollValue = 1;
        private int constantRollValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantDieRoller"/> class.
        /// </summary>
        /// <param name="rollValue">Constant roll value to use.</param>
        public ConstantDieRoller(int rollValue = DefaultRollValue)
        {
            this.constantRollValue = rollValue;
        }

        /// <inheritdoc/>
        public int Roll(int sides, int? factor = null)
        {
            return this.constantRollValue;
        }
    }
}
