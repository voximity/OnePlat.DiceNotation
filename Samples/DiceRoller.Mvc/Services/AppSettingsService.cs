// <copyright file="AppSettingsService.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DiceRoller.Mvc.Services
{
    /// <summary>
    /// Service class for encapsulating application settings.
    /// </summary>
    [Serializable]
    public class AppSettingsService : Dictionary<string, object>
    {
        #region Members
        private const string KeyUseUnboundedResults = "UseUnboundedResultsKey";
        private const bool DefaultUseUnboundedResults = false;
        private const string KeyDefaultDiceSides = "DefaultDiceSidesKey";
        private const int DefaultDiceSidesDefault = 6;
        private const string KeyCachedTrackerDataLimit = "CachedTrackerDataLimitKey";
        private const int DefaultCachedTrackerDataLimit = 250000;
        private const string KeyCurrentDieRoller = "CurrentDieRollerKey";
        private const string DefaultCurrentDieRoller = "OnePlat.DiceNotation.DieRoller.RandomDieRoller";
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsService"/> class.
        /// </summary>
        public AppSettingsService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsService"/> class.
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Stream context</param>
        protected AppSettingsService(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to allow unbounded dice results.
        /// </summary>
        public bool UseUnboundedResults
        {
            get { return this.GetOrDefaultValue<bool>(KeyUseUnboundedResults, DefaultUseUnboundedResults); }
            set { this[KeyUseUnboundedResults] = value; }
        }

        /// <summary>
        /// Gets or sets the value to use for default dice sides, when dice sides are not
        /// specified in dice expressions.
        /// </summary>
        public int DefaultDiceSides
        {
            get { return this.GetOrDefaultValue<int>(KeyDefaultDiceSides, DefaultDiceSidesDefault); }
            set { this[KeyDefaultDiceSides] = value; }
        }

        /// <summary>
        /// Gets or sets cached tracker limit in the app.
        /// </summary>
        public int CachedTrackerDataLimit
        {
            get { return this.GetOrDefaultValue<int>(KeyCachedTrackerDataLimit, DefaultCachedTrackerDataLimit); }
            set { this[KeyCachedTrackerDataLimit] = value; }
        }

        /// <summary>
        /// Gets or sets the type of current die roller used in the app.
        /// </summary>
        public string CurrentDieRollerType
        {
            get { return this.GetOrDefaultValue<string>(KeyCurrentDieRoller, DefaultCurrentDieRoller); }
            set { this[KeyCurrentDieRoller] = value; }
        }
        #endregion

        #region Helper methods

        /// <summary>
        /// Gets the value for the specified field from the roaming application settings.
        /// If that key doesn't exist in the settings, then it returns the default value.
        /// </summary>
        /// <typeparam name="T">Type of return and default values</typeparam>
        /// <param name="key">Key to find the setting</param>
        /// <param name="defaultValue">Default value for the setting</param>
        /// <returns>Returns the app settings value or the default.</returns>
        private T GetOrDefaultValue<T>(string key, T defaultValue)
        {
            this.TryGetValue(key, out object value);
            if (value == null)
            {
                value = defaultValue;
            }

            return (T)value;
        }
        #endregion
    }
}
