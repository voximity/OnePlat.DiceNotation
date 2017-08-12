// <copyright file="ConstantRollerCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/11/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/11/2017
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
namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command class that parses the constant roller option and converts it
    /// to the appropriate value for die roller.
    /// </summary>
    public class ConstantRollerCommand
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="parameter">Parameter to process</param>
        /// <returns>int value to constant die roller</returns>
        public int Execute(string parameter)
        {
            string value = parameter.Substring(Program.OptionConstantDieRoller.Length);
            return int.Parse(value);
        }
    }
}
