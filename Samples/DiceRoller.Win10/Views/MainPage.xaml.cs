// <copyright file="MainPage.xaml.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Win10.Services;
using DiceRoller.Win10.ViewModels;
using DiceRoller.Win10.Views;
using OnePlat.DiceNotation;
using OnePlat.DiceNotation.DieRoller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
namespace DiceRoller.Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Members
        private AppSettingsService appSettings = AppServices.Instance.AppSettingsService;
        private IDice diceService = AppServices.Instance.DiceService;
        private IDieRollTracker diceFrequencyTracker = AppServices.Instance.DiceFrequencyTracker;
        private IDieRoller dieRoller = new RandomDieRoller(AppServices.Instance.DiceFrequencyTracker);
        private TextFileService fileService = AppServices.Instance.FileService;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            this.InitializeData();
            this.DataContext = this;

            // set control default values.
            this.DiceTypeCombobox.SelectedIndex = 5;
            this.DiceExpresssionGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of the dice types to use in this page.
        /// </summary>
        public List<DiceType> DiceTypes { get; private set; }

        /// <summary>
        /// Gets the list for rolled dice results.
        /// </summary>
        public ObservableCollection<DiceResult> DiceRollResults { get; } = new ObservableCollection<DiceResult>();
        #endregion

        #region Helper methods

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // when focus comes back to this page, then reset any app settings that may have
            // changed,
            if (this.appSettings != null)
            {
                if (this.ViewToggleButton.IsChecked != this.appSettings.UseDiceExpressionEditor)
                {
                    // if the dice roll view has changed since last view, then reset the UI.
                    this.ViewToggleButton.IsChecked = this.appSettings.UseDiceExpressionEditor;
                    this.ViewToggleButton_Click(this, null);
                }

                if (this.appSettings.ClearResultsList == true)
                {
                    // if user wanted results list cleared, then clear the list and reset the setting.
                    this.DiceRollResults.Clear();
                    this.appSettings.ClearResultsList = false;
                }

                // set the dice configuration.
                this.diceService.Configuration.DefaultDieSides = this.appSettings.DefaultDiceSides;
                this.diceService.Configuration.HasBoundedResult = !this.appSettings.UseUnboundedResults;
            }

            // setup a time to save die frequency data every 5 minutes.
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += this.Timer_Tick;
            timer.Interval = new TimeSpan(0, 5, 0);
            timer.Start();
        }

        /// <inheritdoc/>
        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            string jsonText = await this.diceFrequencyTracker.ToJsonAsync();
            await this.fileService.WriteFileAsync(Constants.DieFrequencyDataFilename, jsonText);

            base.OnNavigatingFrom(e);
        }

        /// <summary>
        /// Initializes the data elements for this page.
        /// </summary>
        private void InitializeData()
        {
            this.DiceTypes = new List<DiceType>
            {
                new DiceType { DisplayText = "d4", DiceSides = 4, ImageUri = "ms-appx:///Assets/Dice4.png" },
                new DiceType { DisplayText = "d6", DiceSides = 6, ImageUri = "ms-appx:///Assets/Dice6.png" },
                new DiceType { DisplayText = "d8", DiceSides = 8, ImageUri = "ms-appx:///Assets/Dice8.png" },
                new DiceType { DisplayText = "d10", DiceSides = 10, ImageUri = "ms-appx:///Assets/Dice10.png" },
                new DiceType { DisplayText = "d12", DiceSides = 12, ImageUri = "ms-appx:///Assets/Dice12.png" },
                new DiceType { DisplayText = "d20", DiceSides = 20, ImageUri = "ms-appx:///Assets/Dice20.png" },
                new DiceType { DisplayText = "d%", DiceSides = 100, ImageUri = "ms-appx:///Assets/Dice100.png" },
            };
        }
        #endregion

        #region Event handlers

        /// <summary>
        /// Click handler for the Roll button for basic dice definition.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void RollButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // first clear any expression in the dice service
            this.diceService.Clear();

            // setup the dice expression
            DiceType diceType = (DiceType)this.DiceTypeCombobox.SelectedItem;
            this.diceService.Dice(diceType.DiceSides, (int)this.DiceNumberNumeric.Value);
            this.diceService.Constant((int)this.DiceModifierNumeric.Value);

            // roll the dice and save the results
            DiceResult result = this.diceService.Roll(this.dieRoller);
            this.DiceRollResults.Insert(0, result);
        }

        /// <summary>
        /// Click handler for the Roll button to handle dice expression rolls.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private async void RollExpressionButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                // roll the dice (based on dice expression string) and save the results.
                DiceResult result = this.diceService.Roll(this.DiceExpressionTextbox.Text, this.dieRoller);
                this.DiceRollResults.Insert(0, result);
            }
            catch (Exception ex)
            {
                // if there's an error in parsing the expression string, show an error message.
                string message = "There was a error parsing the dice expression: " +
                                 this.DiceExpressionTextbox.Text +
                                 "\r\nException Text: " + ex.Message +
                                 "\r\nPlease correct the expression and try again.";

                MessageDialog dialog = new MessageDialog(message, "Dice Parsing Error");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Click handler toggle which dice roller view to show.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ViewToggleButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.ViewToggleButton.IsChecked.Value)
            {
                this.DiceRollGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.DiceExpresssionGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                this.DiceRollGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.DiceExpresssionGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Click handler for Settings page navigation.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void SettingsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        /// <summary>
        /// Click handler for Frequency stats page navigation.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FrequencyStatsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FrequencyStatsPage));
        }

        /// <summary>
        /// Key handler for DiceExpression textbox to handle processing of Enter key.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void DiceExpressionTextbox_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                this.RollExpressionButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Event handler for Tick event on timer.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private async void Timer_Tick(object sender, object e)
        {
            string jsonText = await this.diceFrequencyTracker.ToJsonAsync();
            await this.fileService.WriteFileAsync(Constants.DieFrequencyDataFilename, jsonText);
        }
        #endregion
    }
}