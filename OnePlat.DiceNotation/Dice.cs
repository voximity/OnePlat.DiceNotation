// <copyright file="Dice.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/8/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/22/2017
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
using OnePlat.DiceNotation.DiceTerms;
using OnePlat.DiceNotation.DieRoller;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnePlat.DiceNotation
{
    /// <summary>
    /// Dice class for manipulation of variable die rolls and modifiers.
    /// </summary>
    public class Dice : IDice
    {
        private IList<IExpressionTerm> terms = new List<IExpressionTerm>();
        private DiceParser parser = new DiceParser();

        /// <summary>
        /// Initializes a new instance of the <see cref="Dice"/> class.
        /// </summary>
        public Dice()
        {
        }

        /// <inheritdoc/>
        public DiceConfiguration Configuration { get; } = new DiceConfiguration();

        /// <inheritdoc/>
        public IDice Constant(int constant)
        {
            // do not add a constant term if it's 0.
            if (constant != 0)
            {
                this.terms.Add(new ConstantTerm(constant));
            }

            return this;
        }

        /// <inheritdoc/>
        IDice IDice.Dice(int sides, int numberDice, double scalar, int? choose, int? exploding)
        {
            this.terms.Add(new DiceTerm(numberDice, sides, scalar, choose, exploding));
            return this;
        }

        /// <inheritdoc/>
        public IDice Concat(IDice otherDice)
        {
            Dice other = (Dice)otherDice;
            if (other == null)
            {
                throw new ArgumentNullException(nameof(otherDice));
            }

            foreach (IExpressionTerm term in ((Dice)otherDice).terms)
            {
                this.terms.Add(term);
            }

            return this;
        }

        /// <inheritdoc/>
        public DiceResult Roll(string expression, IDieRoller dieRoller)
        {
            return this.parser.Parse(expression, this.Configuration, dieRoller);
        }

        /// <inheritdoc/>
        public DiceResult Roll(IDieRoller dieRoller)
        {
            List<TermResult> termResults = this.terms.SelectMany(t => t.CalculateResults(dieRoller)).ToList();
            return new DiceResult(this.ToString(), termResults, dieRoller.GetType().ToString(), this.Configuration);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Join("+", this.terms).Replace("+-", "-");
        }
    }
}
