// <copyright file="DieRollerType.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

namespace DiceRoller.Mvc.ViewModels
{
    /// <summary>
    /// View model class for roller type display.
    /// </summary>
    public class DieRollerType
    {
        /// <summary>
        /// Gets or sets the display text for the type of die roller.
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets the type of die roller.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Represents this class in string format.
        /// </summary>
        /// <returns>Text</returns>
        public override string ToString()
        {
            return this.DisplayText;
        }
    }
}
