// <copyright file="IExpressionTerm.cs" company="DarthPedro">
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
    /// Interface for all terms in our dice notation expression.
    /// </summary>
    public interface IExpressionTerm
    {
        /// <summary>
        /// Calculates the result for this term using the specified die roller.
        /// </summary>
        /// <param name="dieRoller">IDieRoller to use for calculation</param>
        /// <returns>List of results for this expression term</returns>
        IReadOnlyList<TermResult> CalculateResults(IDieRoller dieRoller);
    }
}
