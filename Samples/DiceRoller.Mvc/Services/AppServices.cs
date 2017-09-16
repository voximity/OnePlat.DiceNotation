// <copyright file="AppServices.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using OnePlat.DiceNotation;
using OnePlat.DiceNotation.DieRoller;
using System.Web.SessionState;

namespace DiceRoller.Mvc.Services
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
            this.DiceFrequencyTracker.TrackerDataLimit = this.AppSettingsService.CachedTrackerDataLimit;
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
        private IDieRollTracker diceTracker = new DieRollTracker();
        private DieRollerFactory dieRollerFactory = new DieRollerFactory();
        private AppSettingsService appSettings;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the dice service.
        /// </summary>
        public IDice DiceService
        {
            get { return this.diceService; }
        }

        /// <summary>
        /// Gets the die roll tracker service.
        /// </summary>
        public IDieRollTracker DiceFrequencyTracker
        {
            get { return this.diceTracker; }
        }

        /// <summary>
        /// Gets the die roller factory service.
        /// </summary>
        public DieRollerFactory DieRollerFactory
        {
            get { return this.dieRollerFactory; }
        }

        /// <summary>
        /// Gets the app settings service.
        /// </summary>
        public AppSettingsService AppSettingsService
        {
            get { return this.appSettings; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServices"/> class.
        /// </summary>
        /// <param name="session">Session state to use for settings</param>
        public void Initialize(HttpSessionState session)
        {
            this.appSettings = new AppSettingsService(session);
        }
    }
}
