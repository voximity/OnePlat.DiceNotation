// <copyright file="HomeController.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using System.Web.Mvc;

namespace DiceRoller.Mvc.Controllers
{
    /// <summary>
    /// Home controller class.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Main index method
        /// </summary>
        /// <returns>Resulting view.</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// About method
        /// </summary>
        /// <returns>Resulting view.</returns>
        public ActionResult About()
        {
            this.ViewBag.Message = "d20 Dice Roller";

            return this.View();
        }

        /// <summary>
        /// Contact method
        /// </summary>
        /// <returns>Resulting view.</returns>
        public ActionResult Contact()
        {
            this.ViewBag.Message = "OnePlat.DiceNotation contact information.";

            return this.View();
        }
    }
}