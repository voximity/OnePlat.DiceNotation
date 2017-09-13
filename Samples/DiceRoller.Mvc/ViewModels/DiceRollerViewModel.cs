// <copyright file="DiceRollerViewModel.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using OnePlat.DiceNotation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiceRoller.Web.ViewModels
{
    /// <summary>
    /// View model for the dice roller pages.
    /// </summary>
    public class DiceRollerViewModel
    {
        /// <summary>
        /// Gets or sets the dice expression text.
        /// </summary>
        [Display(Name = "Dice Expression")]
        [Required]
        public string DiceExpression { get; set; }

        /// <summary>
        /// Gets or sets the list of roll results.
        /// </summary>
        public IList<DiceResult> RollResults { get; set; } = new List<DiceResult>();
    }
}
