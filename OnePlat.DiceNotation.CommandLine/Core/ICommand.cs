// <copyright file="ICommand.cs" company="DarthPedro">
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
    /// Interface for commands in this application.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the list of operation identifiers for this command.
        /// </summary>
        string[] Operations { get; }

        /// <summary>
        /// Gets the relative order of this command operation.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Executes the given command.
        /// </summary>
        /// <param name="parameter">Parameter passed for execution</param>
        /// <returns>true means continue command processing; false means terminat command processing.</returns>
        bool Execute(object parameter);

        /// <summary>
        /// Tests whether the command can execute based on parameter.
        /// </summary>
        /// <param name="parameter">Parameter passed for test</param>
        /// <returns>true means the command can execute; false means it cannot.</returns>
        bool CanExecute(object parameter);
    }
}
