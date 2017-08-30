// <copyright file="AppServices.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using OnePlat.DiceNotation;

namespace DiceRoller.Win10.Services
{
    /// <summary>
    /// Class that provides all of the services used by this app.
    /// </summary>
    public class AppServices
    {
        #region Singleton pattern

        private static volatile AppServices instance = new AppServices();

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServices"/> class.
        /// Hides the constructor so that only this class can create an instance.
        /// Needed to implement the singleton pattern.
        /// </summary>
        private AppServices()
        {
        }

        /// <summary>
        /// Gets the single instance of this class.
        /// Needed to implement the singleton pattern.
        /// </summary>
        public static AppServices Instance
        {
            get { return instance; }
        }
        #endregion

        #region Members
        private IDice diceService = new Dice();
        private AppSettingsService appSettings = new AppSettingsService();
        #endregion

        /// <summary>
        /// Gets the dice service.
        /// </summary>
        public IDice DiceService
        {
            get { return this.diceService; }
        }

        /// <summary>
        /// Gets the app settings service.
        /// </summary>
        public AppSettingsService AppSettingsService
        {
            get { return this.appSettings; }
        }
    }
}
