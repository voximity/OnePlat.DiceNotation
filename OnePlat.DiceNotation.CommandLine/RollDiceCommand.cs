// <copyright file="RollDiceCommand.cs" company="DarthPedro">
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
using OnePlat.DiceNotation.DieRoller;
using System;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command class to handle to roll dice.
    /// </summary>
    public class RollDiceCommand
    {
        /// <summary>
        /// Executes the specified command
        /// </summary>
        /// <param name="parameter">parameter to use in the command</param>
        public void Execute(string parameter)
        {
            try
            {
                IDice dice = new Dice().Parse(parameter);
                DiceResult result = dice.Roll(new RandomDieRoller());

                Console.WriteLine("DiceRoll[{0}] => {1}", dice.ToString(), result.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: could not parse dice notation for {0}.", parameter);
                Console.WriteLine("Exception thrown: {0} - {1}", ex.GetType(), ex.Message);
            }
        }
    }
}
