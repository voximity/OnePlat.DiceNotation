// <copyright file="DiceSettingsViewModel.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Mvc.Services;
using OnePlat.DiceNotation.DieRoller;
using System.Collections.Generic;
using System.Linq;

namespace DiceRoller.Mvc.ViewModels
{
    /// <summary>
    /// View model for the dice settings
    /// </summary>
    public class DiceSettingsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceSettingsViewModel"/> class.
        /// </summary>
        public DiceSettingsViewModel()
        {
            this.DieRollerTypes = new List<DieRollerType>
            {
                new DieRollerType { DisplayText = "Pseudo Random [default]", Type = typeof(RandomDieRoller).ToString() },
                new DieRollerType { DisplayText = "Secure Random", Type = typeof(SecureRandomDieRoller).ToString() },
                new DieRollerType { DisplayText = "MathNet Random", Type = typeof(MathNetDieRoller).ToString() },
            };

            this.SelectedDieRollerType = this.DieRollerTypes.First(t => t.Type == this.Settings.CurrentDieRollerType);
        }

        #region Properties

        /// <summary>
        /// Gets the app settings service.
        /// </summary>
        public AppSettingsService Settings { get; } = AppServices.Instance.AppSettingsService;

        /// <summary>
        /// Gets the list of die rollers that can be used by the app.
        /// </summary>
        public List<DieRollerType> DieRollerTypes { get; private set; }

        /// <summary>
        /// Gets or sets the selected die roller types.
        /// </summary>
        public DieRollerType SelectedDieRollerType { get; set; }
        #endregion

        #region Commands

        /// <summary>
        /// Saves the settings changes.
        /// </summary>
        public void SaveSettingsCommand()
        {
        }
        #endregion
    }
}