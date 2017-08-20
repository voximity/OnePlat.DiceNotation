// <copyright file="MainView.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation.CommandLine
// Author           : DarthPedro
// Created          : 8/13/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/20/2017
//-----------------------------------------------------------------------
// <summary>
//       This project is licensed under the MIT license.
//
//       OnePlat.DiceNotation.CommandLine is an open source project that parses,
//  evalutes, and rolls dice that conform to the defined Dice notiation. This
//  command line tool is for testing purposes.
//  This notation is usable in any form of random dice games and role-playing
//  games like D&D and d20.
// </summary>
//-----------------------------------------------------------------------
using OnePlat.DiceNotation.CommandLine.Core;
using System;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Main view class for this application.
    /// </summary>
    public class MainView : IView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            this.DataContext = new MainViewModel();
        }

        /// <inheritdoc/>
        public IViewModel DataContext { get; set; }

        /// <inheritdoc/>
        public void Update()
        {
            MainViewModel vm = this.DataContext as MainViewModel;
            if (vm != null)
            {
                Console.WriteLine(vm.DisplayText);
            }
        }
    }
}
