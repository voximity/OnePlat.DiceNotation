// <copyright file="Program.cs" company="DarthPedro">
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
using System.Collections.Generic;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Main program class.
    /// </summary>
    public class Program
    {
        private const string OptionHelp = "-h";
        private const string OptionVerbose = "-v";

        /// <summary>
        /// Main method that gets called on program launch.
        /// </summary>
        /// <param name="args">Command line arguments to this CLI</param>
        public static void Main(string[] args)
        {
            // first process command line options.
            List<string> parameters = new List<string>(args);
            bool verbose = false;

            if (parameters.Contains(OptionHelp))
            {
                // if command line contains request for help, then show help.
                new HelpCommand().Execute();
                return;
            }

            if (parameters.Contains(OptionVerbose))
            {
                // enable verbose display of results.
                verbose = true;
                parameters.Remove(OptionVerbose);
            }

            // then process commands and parameters.
            if (parameters.Count == 1)
            {
                // if there's only one argument, attempt to parse and roll the dice.
                new RollDiceCommand(verbose).Execute(args[0]);
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