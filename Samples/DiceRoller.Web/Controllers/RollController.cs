// <copyright file="RollController.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiceRoller.Web.Controllers
{
    /// <summary>
    /// Controller for dice rolls
    /// </summary>
    public class RollController : Controller
    {
        /// <summary>
        /// GET: Roll
        /// </summary>
        /// <returns>Main view</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// POST: Roll/Edit/5
        /// </summary>
        /// <param name="collection">Form properties</param>
        /// <returns>Main view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }
    }
}