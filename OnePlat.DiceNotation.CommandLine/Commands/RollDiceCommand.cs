// <copyright file="RollDiceCommand.cs" company="DarthPedro">
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
using OnePlat.DiceNotation.Converters;
using OnePlat.DiceNotation.DiceTerms;
using System;
using System.Text;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Command class to handle to roll dice.
    /// </summary>
    public class RollDiceCommand : ICommand
    {
        private MainViewModel vm;
        private DiceResultConverter diceConverter = new DiceResultConverter();
        private TermResultListConverter listConverter = new TermResultListConverter();

        /// <summary>
        /// Initializes a new instance of the <see cref="RollDiceCommand"/> class.
        /// </summary>
        /// <param name="viewModel">Associated view model</param>
        public RollDiceCommand(MainViewModel viewModel)
        {
            this.vm = viewModel;
        }

        /// <inheritdoc/>
        public string[] Operations { get; } = new string[] { "-r", "-roll" };

        /// <inheritdoc/>
        public int Order { get; } = 100;

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
                IDice dice = new Dice();
                dice.Configuration.HasBoundedResult = this.vm.HasBoundedResult;
                dice.Configuration.DefaultDieSides = this.vm.DefaultDieSides ?? 6;

                DiceResult result = dice.Roll(parameter as string, this.vm.DieRoller);

                if (this.vm.UseVerboseOutput)
                {
                    this.vm.DisplayText += this.VerboseRollDisplay(dice, result);
                }
                else
                {
                    this.vm.DisplayText += string.Format(
                        "DiceRoll => {0} [dice: {1}]",
                        this.diceConverter.Convert(result, typeof(string), null, "default"),
                        this.listConverter.Convert(result.Results, typeof(string), null, "default"));
                }

                return true;
            }
            catch (Exception ex)
            {
                this.vm.DisplayText += string.Format("Error: could not parse dice notation for {0}.\r\n", parameter);
                this.vm.DisplayText += string.Format("Exception thrown: {0} - {1}\r\n", ex.GetType(), ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Display the dice roll results in verbose format.
        /// Helpful in debugging dice expression strings.
        /// </summary>
        /// <param name="dice">Dice expression used</param>
        /// <param name="result">Dice results to display</param>
        /// <returns>Resulting display text.</returns>
        private string VerboseRollDisplay(IDice dice, DiceResult result)
        {
            StringBuilder output = new StringBuilder();

            output.AppendFormat(
               "DiceRoll => {0}",
                this.diceConverter.Convert(result, typeof(string), null, "default"));

            output.AppendLine("===============================================");
            output.AppendFormat("  DiceResult.DieRollerUsed: {0}\r\n", result.DieRollerUsed);
            output.AppendFormat("  DiceResult.NumberTerms: {0}\r\n", result.Results.Count);
            output.AppendLine("  Terms list:");
            output.AppendLine("  ---------------------------");

            foreach (TermResult term in result.Results)
            {
                output.AppendFormat("    TermResult.Type: {0}\r\n", term.Type);
                output.AppendFormat("    TermResult.IncludeInResult: {0}\r\n", term.AppliesToResultCalculation);
                output.AppendFormat("    TermResult.Scalar: {0}\r\n", term.Scalar);
                output.AppendFormat("    TermResult.Value: {0}\r\n", term.Value);
                output.AppendLine();
            }

            output.AppendLine("  ---------------------------");
            output.AppendFormat("  Total Roll: {0}\r\n", result.Value);

            return output.ToString();
        }
    }
}
