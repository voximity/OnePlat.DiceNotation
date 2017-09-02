// <copyright file="SettingsPage.xaml.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Win10.Services;
using OnePlat.DiceNotation.DieRoller;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace DiceRoller.Win10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        #region Members
        private const int DataLimitFactor = 1000;
        private IDieRollTracker frequencyTracker = AppServices.Instance.DiceFrequencyTracker;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage()
        {
            this.InitializeComponent();

            this.DataContext = this;

            this.DiceFrequencyLimitSlider.Value = this.frequencyTracker.TrackerDataLimit / DataLimitFactor;
        }

        /// <summary>
        /// Gets the app settings for this page.
        /// </summary>
        public AppSettingsService Settings { get; } = AppServices.Instance.AppSettingsService;

        #region Event handlers

        /// <summary>
        /// Click handler to clear the current results list.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ClearResultsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Settings.ClearResultsList = true;
        }

        /// <summary>
        /// Click handler to clear the dice frequency data.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ClearFrequencyButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AppServices.Instance.DiceFrequencyTracker.Clear();
        }

        /// <summary>
        /// Event handler for frequency data limit slider changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void DiceFrequencyLimitSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (e.OldValue != 0)
            {
                this.frequencyTracker.TrackerDataLimit = (int)this.DiceFrequencyLimitSlider.Value * DataLimitFactor;
            }
        }
        #endregion
    }
}
