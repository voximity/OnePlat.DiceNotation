﻿// <copyright file="App.xaml.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>
using DiceRoller.Win10.Services;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DiceRoller.Win10
{
    // todo: Replace dll reference in sample with 1.0.4 Nuget package after it is posted.

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            // load any cached dice frequency data.
            AppServices appServices = AppServices.Instance;
            string jsonText = await appServices.FileService.ReadFileAsync(Constants.DieFrequencyDataFilename);
            await appServices.DiceFrequencyTracker.LoadFromJsonAsync(jsonText);

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                // Register a global back event handler. This can be registered on a per-page-bases if you only have a subset of your pages
                // that needs to handle back or if you want to do page-specific logic before deciding to navigate back on those pages.
                SystemNavigationManager.GetForCurrentView().BackRequested += this.App_BackRequested;

                // enable the titlebar back button.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when a user issues a global back on the device.
        /// If the app has no in-app back stack left for the current view/frame the user may be navigated away
        /// back to the previous app in the system's app back stack or to the start screen.
        /// In windowed mode on desktop there is no system app back stack and the user will stay in the app even when the in-app back stack is depleted.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                return;
            }

            // If we can go back and the event has not already been handled, do so.
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            AppServices services = AppServices.Instance;
            string jsonText = await services.DiceFrequencyTracker.ToJsonAsync();
            await services.FileService.WriteFileAsync(Constants.DieFrequencyDataFilename, jsonText);

            deferral.Complete();
        }
    }
}
