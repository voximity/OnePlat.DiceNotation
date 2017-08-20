// <copyright file="ConstantTerm.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.Mvvm.Core
// Author           : DarthPedro
// Created          : 8/8/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/20/2017
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
using System.Collections.Generic;

namespace OnePlat.DiceNotation.DiceTerms
{
    /// <summary>
    /// Dice expression term that represents a constant value.
    /// </summary>
    public class ConstantTerm : IExpressionTerm
    {
        private int constant = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantTerm"/> class.
        /// </summary>
        /// <param name="constant">Constant value for this term.</param>
        public ConstantTerm(int constant)
        {
            this.constant = constant;
        }

        /// <inheritdoc/>
        public IReadOnlyList<TermResult> CalculateResults(IDieRoller dieRoller)
        {
            List<TermResult> results = new List<TermResult>
            {
                new TermResult { Scalar = 1, Value = this.constant, Type = this.GetType().Name }
            };

            return results.AsReadOnly();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.constant.ToString();
        }
    }
}
