// <copyright file="Program.cs" company="DarthPedro">
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

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that gets called on program launch.
        /// </summary>
        /// <param name="args">Command line arguments to this CLI</param>
        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                // if there's only one argument, attempt to parse and roll the dice.
                new RollDiceCommand().Execute(args[0]);
            }
            else
            {
                // if command line doesn't conform to expected types, then show help.
                new HelpCommand().Execute();
            }

#if DEBUG
            // when in debug mode, wait for user to press enter to complete command.
            new WaitForInputCommand().Execute();
#endif
        }
    }
}