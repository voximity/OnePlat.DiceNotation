// <copyright file="WaitForInputCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/10/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/10/2017
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
using System;
using System.Collections.Generic;
using System.Text;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command that waits for user input to console.
    /// </summary>
    public class WaitForInputCommand
    {
        /// <summary>
        /// Executes the specified command
        /// </summary>
        public void Execute()
        {
            Console.WriteLine();
            Console.Write("Press enter to complete command.");
            Console.ReadLine();
        }
    }
}
