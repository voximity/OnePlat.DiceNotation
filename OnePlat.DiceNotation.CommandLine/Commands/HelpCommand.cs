// <copyright file="HelpCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation.CommandLine
// Author           : DarthPedro
// Created          : 8/10/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/28/2017
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
using System.Text;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command class to show help information for this tool.
    /// </summary>
    public class HelpCommand : ICommand
    {
        private MainViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/> class.
        /// </summary>
        /// <param name="viewModel">Associated view model</param>
        public HelpCommand(MainViewModel viewModel = null)
        {
            this.vm = viewModel;
        }

        /// <inheritdoc/>
        public string[] Operations => new string[] { "-h", "-?" };

        /// <inheritdoc/>
        public int Order { get; } = 1;

        /// <inheritdoc/>
        public bool CanExecute(object parameter = null)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool Execute(object parameter = null)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("=== OnePlat.DiceNotation Command Line Tool (1.0.4) ===");
            output.AppendLine("Usage: dice [dice-expression] [common-options]");
            output.AppendLine();
            output.AppendLine("Common options:");
            output.AppendLine("  -v        Enable verbose output");
            output.AppendLine("  -c:X       Enable using constant die roller, with value of X. Helpful in debugging dice notation expressions.");
            output.AppendLine("  -s:X       Sets using default number of dice sides, with value of X.");
            output.AppendLine("  -h | -?   Show help");
            output.AppendLine();
            output.AppendLine("Dice expressions:");
            output.AppendLine("  Dice notations that conform to the following forms:");
            output.AppendLine("    XdY [-] [+] [x] [/] N    X dice with Y-sides operated on by (-, +, x, /) with number N");
            output.AppendLine("    XdY [-] [+] AdB          The first roll of X dice with Y-sides is operated on by (-, +) with a second roll of A dice with B-sides.");
            output.AppendLine("    Xf [-] [+] [x] [/] N     X Fudge/FATE dice operated on by (-, +, x, /) with number N");
            output.AppendLine();
            output.AppendLine("  Example: 3d6+2, d20, 3+1d8, 3d6+1d8, 2d4x10, 4d6k3, ...");
            output.AppendLine();
            output.AppendLine("  Note: If you wish to use spaces in the dice expression, then place the expression within quotes (ex: dice \"3d6 + 2\").");

            this.vm.DisplayText += output.ToString();

            return false;
        }
    }
}
