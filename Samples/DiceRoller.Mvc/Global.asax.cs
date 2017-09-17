// <copyright file="Global.asax.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Mvc.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DiceRoller.Mvc
{
    /// <summary>
    /// The application class for MVC.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Event handler for Start of the application.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // initialize global application services.
            AppServices.Instance.Initialize();
        }
    }
}
