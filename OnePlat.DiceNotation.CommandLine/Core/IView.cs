// <copyright file="IView.cs" company="DarthPedro">
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
namespace OnePlat.DiceNotation.CommandLine.Core
{
    /// <summary>
    /// Interface for the View definition.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets or sets the data context for this view.
        /// </summary>
        IViewModel DataContext { get; set; }

        /// <summary>
        /// Updates the view with its current view model information.
        /// </summary>
        void Update();
    }
}
