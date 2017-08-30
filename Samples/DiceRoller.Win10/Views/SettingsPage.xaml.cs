﻿// <copyright file="SettingsPage.xaml.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

using DiceRoller.Win10.Services;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace DiceRoller.Win10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage()
        {
            this.InitializeComponent();

            this.DataContext = this;
        }

        /// <summary>
        /// Gets the app settings for this page.
        /// </summary>
        public AppSettingsService Settings { get; } = AppServices.Instance.AppSettingsService;

        /// <summary>
        /// Click handler to clear the current results list.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ClearResultsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Settings.ClearResultsList = true;
        }
    }
}