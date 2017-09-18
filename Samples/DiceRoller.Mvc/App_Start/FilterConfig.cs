// <copyright file="FilterConfig.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using System.Web.Mvc;

namespace DiceRoller.Mvc
{
    /// <summary>
    /// Class to configure the filters in this application.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the global filters for this application.
        /// Defaults only to global error handler.
        /// </summary>
        /// <param name="filters">Filter collection to register with.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
