// <copyright file="SetDefaultNumberDieSidesCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation.CommandLine
// Author           : DarthPedro
// Created          : 8/24/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/24/2017
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
    /// Command class to handle setting a different default value for number
    /// of sides used by a set of dice.
    /// </summary>
    public class SetDefaultNumberDieSidesCommand : ICommand
    {
        private MainViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetDefaultNumberDieSidesCommand"/> class.
        /// </summary>
        /// <param name="viewModel">Associated view model</param>
        public SetDefaultNumberDieSidesCommand(MainViewModel viewModel)
        {
            this.vm = viewModel;
        }

        /// <inheritdoc/>
        public string[] Operations => new string[] { "-s" };

        /// <inheritdoc/>
        public int Order { get; set; } = 22;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool Execute(object parameter)
        {
            try
            {
                string sides = parameter as string;
                this.vm.DefaultDieSides = int.Parse(sides);

                return true;
            }
            catch (Exception ex)
            {
                this.vm.DisplayText += "Error parsing the default die sides option. The format should be -s:X, where X is the default value.\r\n";
                this.vm.DisplayText += string.Format("Exception thrown {0} - {1}\r\n", ex.GetType().Name, ex.Message);
                return false;
            }
        }
    }
}
