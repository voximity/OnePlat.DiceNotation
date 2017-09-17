// <copyright file="DiceRollerViewModel.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Mvc.Services;
using OnePlat.DiceNotation;
using OnePlat.DiceNotation.DieRoller;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiceRoller.Web.ViewModels
{
    /// <summary>
    /// View model for the dice roller pages.
    /// </summary>
    public class DiceRollerViewModel
    {
        #region Members
        private IDice dice;
        private IDieRoller dieRoller;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceRollerViewModel"/> class.
        /// </summary>
        public DiceRollerViewModel()
        {
            AppServices services = AppServices.Instance;
            this.dice = services.DiceService;
            string rollerType = services.AppSettingsService.CurrentDieRollerType;
            this.dieRoller = services.DieRollerFactory.GetDieRoller(rollerType, services.DiceFrequencyTracker);
        }

        #region Properties

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
        #endregion

        #region Commands

        /// <summary>
        /// Command method to roll the dice and save the results.
        /// </summary>
        /// <param name="expression">String expression to roll.</param>
        public void RollCommand(string expression)
        {
            this.DiceExpression = expression;
            DiceResult result = this.dice.Roll(this.DiceExpression, this.dieRoller);
            this.RollResults.Insert(0, result);
        }
        #endregion
    }
}
