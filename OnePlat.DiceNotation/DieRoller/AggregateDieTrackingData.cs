// <copyright file="AggregateDieTrackingData.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/31/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/31/2017
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
    /// Data class to hold aggregate tracking data count per roll result.
    /// </summary>
    public class AggregateDieTrackingData
    {
        /// <summary>
        /// Gets or sets the die roller type used in roll
        /// </summary>
        public string RollerType { get; set; }

        /// <summary>
        /// Gets or sets the number of side on the die
        /// </summary>
        public string DieSides { get; set; }

        /// <summary>
        /// Gets or sets the result of the roll
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Gets or sets the instance count of this roll result.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the percentage of counts of this roll result.
        /// </summary>
        public float Percentage { get; set; }
    }
}
