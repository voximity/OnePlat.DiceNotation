// <copyright file="SetVerboseCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotatoin.CommandLine
// Author           : DarthPedro
// Created          : 8/13/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/15/2017
//-----------------------------------------------------------------------
// <summary>
//       This project is licensed under the MS-PL license.
//
//       OnePlat.DiceNotation.CommandLine is an open source project that parses,
//  evalutes, and rolls dice that conform to the defined Dice notiation. This
//  command line tool is for testing purposes.
//  This notation is usable in any form of random dice games and role-playing
//  games like D&D and d20.
// </summary>
//-----------------------------------------------------------------------
using OnePlat.DiceNotation.CommandLine.Core;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command that turns on verbose output.
    /// </summary>
    public class SetVerboseCommand : ICommand
    {
        private MainViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetVerboseCommand"/> class.
        /// </summary>
        /// <param name="viewModel">Associated view model</param>
        public SetVerboseCommand(MainViewModel viewModel)
        {
            this.vm = viewModel;
        }

        /// <inheritdoc/>
        public string[] Operations => new string[] { "-v" };

        /// <inheritdoc/>
        public int Order { get; } = 10;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool Execute(object parameter)
        {
            this.vm.UseVerboseOutput = true;
            return true;
        }
    }
}
