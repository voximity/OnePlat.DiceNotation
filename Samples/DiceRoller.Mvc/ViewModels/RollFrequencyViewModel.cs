// <copyright file="RollFrequencyViewModel.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Mvc.Services;
using OnePlat.DiceNotation.DieRoller;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Mvc.ViewModels
{
    /// <summary>
    /// View model class for the roll frequency views.
    /// </summary>
    public class RollFrequencyViewModel
    {
        #region Members
        private IDieRollTracker frequencyTracker = AppServices.Instance.DiceFrequencyTracker;
        private IList<AggregateDieTrackingData> frequencyData;
        #endregion

        #region

        /// <summary>
        /// Gets the list of types of rollers in the frequency data.
        /// </summary>
        [Display(Name = "Die Roller Types")]
        public List<string> RollerTypes
        {
            get { return this.frequencyData.Select(d => d.RollerType).Distinct().ToList(); }
        }

        /// <summary>
        /// Gets the list of various dice rolled int he frequency data.
        /// </summary>
        [Display(Name = "Number Dice Sides")]
        public List<string> DiceSides
        {
            get { return this.frequencyData.Select(d => d.DieSides).Distinct().ToList(); }
        }

        /// <summary>
        /// Gets or sets the selected roller type.
        /// </summary>
        public string SelectedRollerType { get; set; }

        /// <summary>
        /// Gets or sets the selected dice sides.
        /// </summary>
        public string SelectedDiceSides { get; set; }

        /// <summary>
        /// Gets the list of items for this page.
        /// </summary>
        public List<AggregateDieTrackingData> FrequencyData { get; private set; } = new List<AggregateDieTrackingData>();
        #endregion

        #region Commands

        /// <summary>
        /// Updates the frequency data based on the filters selected
        /// on view model.
        /// </summary>
        public void CalculateFrequencyDataCommand()
        {
            var list = from d in this.frequencyData
                       where d.RollerType == this.SelectedRollerType && d.DieSides == this.SelectedDiceSides
                       select d;

            this.FrequencyData = list.ToList();
        }
        #endregion

        /// <summary>
        /// Called when navigated to this page.
        /// </summary>
        /// <returns>Task</returns>
        public async Task Initialize()
        {
            this.frequencyData = await this.frequencyTracker.GetFrequencyDataViewAsync();

            if (this.RollerTypes.Count == 1)
            {
                this.SelectedRollerType = this.RollerTypes.First();
            }
        }
    }
}