// <copyright file="DiceType.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

namespace DiceRoller.Win10.ViewModels
{
    /// <summary>
    /// Data class for holding dice type information for selectors.
    /// </summary>
    public class DiceType
    {
        /// <summary>
        /// Gets or sets the display text for the type of dice.
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets the image uri for the type of dice.
        /// </summary>
        public string ImageUri { get; set; }

        /// <summary>
        /// Gets or sets the number of sides for the type of dice.
        /// </summary>
        public int DiceSides { get; set; }
    }
}
