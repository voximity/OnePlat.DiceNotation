// <copyright file="ConstantRollerCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation.CommandLine
// Author           : DarthPedro
// Created          : 8/11/2017
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
    /// Command class that parses the constant roller option and converts it
    /// to the appropriate value for die roller.
    /// </summary>
    public class ConstantRollerCommand : ICommand
    {
        private MainViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantRollerCommand"/> class.
        /// </summary>
        /// <param name="viewModel">Associated view model</param>
        public ConstantRollerCommand(MainViewModel viewModel)
        {
            this.vm = viewModel;
        }

        /// <inheritdoc/>
        public string[] Operations => new string[] { "-c" };

        /// <inheritdoc/>
        public int Order { get; } = 20;

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
                string constant = parameter as string;
                this.vm.ConstantRollerValue = int.Parse(constant);

                return true;
            }
            catch (Exception ex)
            {
                this.vm.DisplayText += "Error parsing the constant die roller option. The format should be -c:X, where X is the constant value.\r\n";
                this.vm.DisplayText += string.Format("Exception thrown {0} - {1}\r\n", ex.GetType().Name, ex.Message);
                return false;
            }
        }
    }
}
