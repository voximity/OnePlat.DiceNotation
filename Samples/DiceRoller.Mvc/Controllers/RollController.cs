// <copyright file="RollController.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Mvc.ViewModels;
using DiceRoller.Web.ViewModels;
using OnePlat.DiceNotation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DiceRoller.Mvc.Controllers
{
    /// <summary>
    /// Controller class that handles die roll operations.
    /// </summary>
    public class RollController : Controller
    {
        #region Members
        private const string CurrentDiceResultsListSessionKey = "CurrentDiceResultsList";
        private const string DiceExpressionFormKey = "DiceExpression";
        private DiceRollerViewModel vmDiceRoller = new DiceRollerViewModel();
        private RollFrequencyViewModel vmRollFrequency = new RollFrequencyViewModel();
        private DiceSettingsViewModel vmDiceSettings = new DiceSettingsViewModel();
        #endregion

        /// <summary>
        /// Default view of die roll results.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            this.vmDiceRoller.RollResults = this.Session[CurrentDiceResultsListSessionKey] as IList<DiceResult> ?? new List<DiceResult>();

            return this.View(this.vmDiceRoller.RollResults);
        }

        /// <summary>
        /// Creates a die roll from dice expression.
        /// GET: Roll/Create
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            this.vmDiceRoller.RollResults = this.Session[CurrentDiceResultsListSessionKey] as IList<DiceResult> ?? new List<DiceResult>();

            return this.View(this.vmDiceRoller);
        }

        /// <summary>
        /// Creates a die roll from dice expression.
        /// POST: Roll/Create
        /// </summary>
        /// <param name="collection">Forms collection.</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                this.vmDiceRoller.RollResults = this.Session[CurrentDiceResultsListSessionKey] as IList<DiceResult> ?? new List<DiceResult>();
                this.vmDiceRoller.RollCommand(collection[DiceExpressionFormKey]);
                this.Session[CurrentDiceResultsListSessionKey] = this.vmDiceRoller.RollResults;

                return this.View(this.vmDiceRoller);
            }
            catch
            {
                return this.View(this.vmDiceRoller);
            }
        }

        /// <summary>
        /// Show die roll stats view.
        /// GET: Roll/Stats
        /// </summary>
        /// <returns>View</returns>
        public async Task<ActionResult> Stats()
        {
            await this.vmRollFrequency.Initialize();
            return this.View(this.vmRollFrequency);
        }

        /// <summary>
        /// Show die roll stats view for specified filters.
        /// POST: Roll/Stats
        /// </summary>
        /// <param name="collection">Forms collection.</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> Stats(FormCollection collection)
        {
            try
            {
                await this.vmRollFrequency.Initialize();
                this.vmRollFrequency.SelectedRollerType = collection["SelectedRollerType"];
                this.vmRollFrequency.SelectedDiceSides = collection["SelectedDiceSides"];

                this.vmRollFrequency.CalculateFrequencyDataCommand();

                return this.View(this.vmRollFrequency);
            }
            catch
            {
                return this.View(this.vmRollFrequency);
            }
        }

        /// <summary>
        /// Show the dice settings view.
        /// GET: Roll/Settings
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Settings()
        {
            return this.View(this.vmDiceSettings);
        }

        /// <summary>
        /// Show the dice settings view
        /// POST: Roll/Settings
        /// </summary>
        /// <param name="collection">Forms collection.</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Settings(FormCollection collection)
        {
            try
            {
                this.vmDiceSettings.SaveSettingsCommand();

                return this.View(this.vmDiceSettings);
            }
            catch
            {
                return this.View(this.vmDiceSettings);
            }
        }
    }
}
