// <copyright file="RollController.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Web.ViewModels;
using OnePlat.DiceNotation;
using OnePlat.DiceNotation.DiceTerms;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DiceRoller.Mvc.Controllers
{
    /// <summary>
    /// Controller class that handles die roll operations.
    /// </summary>
    public class RollController : Controller
    {
        /// <summary>
        /// Default view of die roll results.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            IList<DiceResult> results = new List<DiceResult>();
            List<TermResult> rolls = new List<TermResult> { new TermResult { Scalar = 1, Value = 15, Type = "DiceTerm.d20" } };

            results.Add(new DiceResult("d20", rolls, "RandomDieRoller", new DiceConfiguration()));

            return this.View(results);
        }

        /// <summary>
        /// Creates a die roll from dice expression.
        /// GET: Roll/Create
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            return this.View(new DiceRollerViewModel());
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
                // TODO: Add insert logic here
                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }
    }
}
