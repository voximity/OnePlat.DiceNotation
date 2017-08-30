﻿// <copyright file="AppSettingsService.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using Windows.Storage;

namespace DiceRoller.Win10.Services
{
    /// <summary>
    /// Service class for encapsulating application settings.
    /// </summary>
    public class AppSettingsService
    {
        #region Members
        private const string KeyUseDiceExpressionEditor = "UseDiceExpressionEditorKey";
        private const bool DefaultUseDiceExpressionEditor = false;
        private const string KeyUseUnboundedResults = "UseUnboundedResultsKey";
        private const bool DefaultUseUnboundedResults = false;
        private const string KeyDefaultDiceSides = "DefaultDiceSidesKey";
        private const int DefaultDiceSidesDefault = 6;
        private const string KeyClearResultsList = "ClearResultsListKey";
        private const bool DefaultClearResultsList = false;

        private ApplicationDataContainer appSettings = ApplicationData.Current.RoamingSettings;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to use the dice expression editor by default.
        /// </summary>
        public bool UseDiceExpressionEditor
        {
            get
            {
                return this.GetOrDefaultValue<bool>(KeyUseDiceExpressionEditor, DefaultUseDiceExpressionEditor);
            }

            set
            {
                this.appSettings.Values[KeyUseDiceExpressionEditor] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow unbounded dice results.
        /// </summary>
        public bool UseUnboundedResults
        {
            get
            {
                return this.GetOrDefaultValue<bool>(KeyUseUnboundedResults, DefaultUseUnboundedResults);
            }

            set
            {
                this.appSettings.Values[KeyUseUnboundedResults] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to use for default dice sides, when dice sides are not
        /// specified in dice expressions.
        /// </summary>
        public int DefaultDiceSides
        {
            get
            {
                return this.GetOrDefaultValue<int>(KeyDefaultDiceSides, DefaultDiceSidesDefault);
            }

            set
            {
                this.appSettings.Values[KeyDefaultDiceSides] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the results list should be cleared.
        /// </summary>
        public bool ClearResultsList
        {
            get
            {
                return this.GetOrDefaultValue<bool>(KeyClearResultsList, DefaultClearResultsList);
            }

            set
            {
                this.appSettings.Values[KeyClearResultsList] = value;
            }
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
            this.appSettings.Values.TryGetValue(key, out object value);
            if (value == null)
            {
                value = defaultValue;
            }

            return (T)value;
        }
        #endregion
    }
}