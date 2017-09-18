// <copyright file="RouteConfig.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using System.Web.Mvc;
using System.Web.Routing;

namespace DiceRoller.Mvc
{
    /// <summary>
    /// Class that holds this app's route configuration.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers which route maps are available for this application.
        /// </summary>
        /// <param name="routes">Routes collection to register with.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
