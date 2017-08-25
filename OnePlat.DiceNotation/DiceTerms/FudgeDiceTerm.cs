// <copyright file="FudgeDiceTerm.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/23/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/24/2017
//-----------------------------------------------------------------------
// <summary>
//       This project is licensed under the MIT license.
//
//       OnePlat.DiceNotation is an open source project that parses,
//  evalutes, and rolls dice that conform to the defined Dice notiation.
//  This notation is usable in any form of random dice games and role-playing
//  games like D&D and d20.
// </summary>
//-----------------------------------------------------------------------
using OnePlat.DiceNotation.DieRoller;

namespace OnePlat.DiceNotation.DiceTerms
{
    /// <summary>
    /// Fudge dice expression term that represents dice roll values.
    /// </summary>
    public class FudgeDiceTerm : DiceTerm
    {
        #region Members
        private const string FudgeFormatResultType = "{0}.dF";
        private const string FudgeFormatDiceTermText = "{0}f{2}";
        private const int FudgeNumberSides = 3;
        private const int FudgeFactor = -2;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FudgeDiceTerm"/> class.
        /// </summary>
        /// <param name="numberDice">Number of dice in the expression</param>
        /// <param name="choose">How many dice to use (value should be between 1 and number of dice)</param>
        public FudgeDiceTerm(int numberDice, int? choose = null)
            : base(numberDice, FudgeNumberSides, 1, choose)
        {
            this.FormatResultType = FudgeFormatResultType;
            this.FormatDiceTermText = FudgeFormatDiceTermText;
        }

        /// <inheritdoc/>
        protected override int RollTerm(IDieRoller dieRoller, int sides)
        {
            return dieRoller.Roll(sides, FudgeFactor);
        }
    }
}
