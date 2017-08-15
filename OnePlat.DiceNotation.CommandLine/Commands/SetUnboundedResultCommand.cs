// <copyright file="SetUnboundedResultCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation.CommandLine
// Author           : DarthPedro
// Created          : 8/15/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/15/2017
//-----------------------------------------------------------------------
// <summary>
//       This project is licensed under the MS-PL license.
//
//       OnePlat.DiceNotation is an open source project that parses,
//  evalutes, and rolls dice that conform to the defined Dice notiation.
//  This notation is usable in any form of random dice games and role-playing
//  games like D&D and d20.
// </summary>
//-----------------------------------------------------------------------
using OnePlat.DiceNotation.CommandLine.Core;
using System;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command class for setting dice to unbounded results.
    /// (not bound to 1 as a minimum result)
    /// </summary>
    public class SetUnboundedResultCommand : ICommand
    {
        private MainViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetUnboundedResultCommand"/> class.
        /// </summary>
        /// <param name="viewModel">Associated view model</param>
        public SetUnboundedResultCommand(MainViewModel viewModel)
        {
            this.vm = viewModel;
        }

        /// <inheritdoc/>
        public string[] Operations => new string[] { "-u" };

        /// <inheritdoc/>
        public int Order { get; set; } = 21;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool Execute(object parameter)
        {
            this.vm.HasBoundedResult = false;
            return true;
        }
    }
}
