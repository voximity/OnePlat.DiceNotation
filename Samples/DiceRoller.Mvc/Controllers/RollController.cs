// <copyright file="RollController.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Web.ViewModels;
using OnePlat.DiceNotation;
using System.Collections.Generic;
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
        private DiceRollerViewModel vm = new DiceRollerViewModel();
        #endregion

        /// <summary>
        /// Default view of die roll results.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            this.vm.RollResults = this.Session[CurrentDiceResultsListSessionKey] as IList<DiceResult> ?? new List<DiceResult>();

            return this.View(this.vm.RollResults);
        }

        /// <summary>
        /// Creates a die roll from dice expression.
        /// GET: Roll/Create
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            this.vm.RollResults = this.Session[CurrentDiceResultsListSessionKey] as IList<DiceResult> ?? new List<DiceResult>();

            return this.View(this.vm);
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
                this.vm.RollResults = this.Session[CurrentDiceResultsListSessionKey] as IList<DiceResult> ?? new List<DiceResult>();
                this.vm.RollCommand(collection[DiceExpressionFormKey]);
                this.Session[CurrentDiceResultsListSessionKey] = this.vm.RollResults;

                return this.View(this.vm);
            }
            catch
            {
                return this.View(this.vm);
            }
        }
    }
}
