// <copyright file="HelpCommand.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/10/2017
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
using System;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command class to show help information for this tool.
    /// </summary>
    public class HelpCommand
    {
        /// <summary>
        /// Executes the specified command
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("=== OnePlat.DiceNotation Command Line Tool (1.0.0) ===");
            Console.WriteLine("Usage: dice [dice-expression] [common-options]");
            Console.WriteLine();
            Console.WriteLine("Common options:");
            Console.WriteLine("  -v        Enable verbose output");
            Console.WriteLine("  -cX       Enable using constant die roller, with value of X. Helpful in debugging dice notation expressions");
            Console.WriteLine("  -h | -?   Show help");
            Console.WriteLine();
            Console.WriteLine("Dice expressions:");
            Console.WriteLine("  Dice notations that conform to the following forms:");
            Console.WriteLine("    XdY [-] [+] [x] [/] N    X dice with Y-sides operated on by (-, +, x, /) with number N");
            Console.WriteLine("    XdY [-] [+] AdB          The first roll of X dice with Y-sides is operated on by (-, +) with a second roll of A dice with B-sides.");
            Console.WriteLine();
            Console.WriteLine("  Example: 3d6+2, d20, 3+1d8, 3d6+1d8, 2d4x10, 4d6k3, ...");
            Console.WriteLine();
            Console.WriteLine("  Note: If you wish to use spaces in the dice expression, then place the expression within quotes (ex: dice \"3d6 + 2\").");
        }
    }
}
