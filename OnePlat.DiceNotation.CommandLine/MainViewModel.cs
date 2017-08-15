// <copyright file="MainViewModel.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation.CommandLine
// Author           : DarthPedro
// Created          : 8/13/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/15/2017
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
using OnePlat.DiceNotation.CommandLine.Core;
using OnePlat.DiceNotation.DieRoller;

namespace OnePlat.DiceNotation.CommandLine
{
    /// <summary>
    /// Main view model for this application.
    /// </summary>
    public class MainViewModel : IViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            this.HelpCommand = new HelpCommand(this);
            this.SetVerboseCommand = new SetVerboseCommand(this);
            this.ConstantRollerCommand = new ConstantRollerCommand(this);
            this.SetUnboundResultCommand = new SetUnboundedResultCommand(this);
            this.RollDiceCommand = new RollDiceCommand(this);
        }

        #region Properties

        /// <inheritdoc/>
        public ICommand DefaultCommand
        {
            get { return this.RollDiceCommand; }
        }

        /// <summary>
        /// Gets or sets the display text for this view model.
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use verbose output.
        /// </summary>
        public bool UseVerboseOutput { get; set; } = false;

        /// <summary>
        /// Gets or sets the value for a ConstantDieRoller.
        /// </summary>
        public int? ConstantRollerValue { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to use a bounded result in DiceResults.
        /// </summary>
        public bool HasBoundedResult { get; set; } = true;

        /// <summary>
        /// Gets the die roller to use for this view model based on options.
        /// </summary>
        public IDieRoller DieRoller
        {
            get
            {
                IDieRoller roller;
                if (this.ConstantRollerValue == null)
                {
                    roller = new RandomDieRoller();
                }
                else
                {
                    roller = new ConstantDieRoller(this.ConstantRollerValue.Value);
                }

                return roller;
            }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Gets the help command.
        /// </summary>
        public ICommand HelpCommand { get; }

        /// <summary>
        /// Gets the verbose command.
        /// </summary>
        public ICommand SetVerboseCommand { get; }

        /// <summary>
        /// Gets the ConstantDieRoller command.
        /// </summary>
        public ICommand ConstantRollerCommand { get; }

        /// <summary>
        /// Gets the SetUnboundedResult command
        /// </summary>
        public ICommand SetUnboundResultCommand { get; }

        /// <summary>
        /// Gets the RollDice command.
        /// </summary>
        public ICommand RollDiceCommand { get; }
        #endregion
    }
}
