// <copyright file="HomeController.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace DiceRoller.Web.Controllers
{
    /// <summary>
    /// Controller class for Home operations
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets data for the Index operation.
        /// </summary>
        /// <returns>Main home view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Gets data for the About operation.
        /// </summary>
        /// <returns>About view.</returns>
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// Gets data for the Contact operation.
        /// </summary>
        /// <returns>Contact view.</returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>
        /// Gets data for the Error operation.
        /// </summary>
        /// <returns>Error view.</returns>
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
