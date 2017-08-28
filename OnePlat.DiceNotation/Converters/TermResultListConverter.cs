// <copyright file="TermResultListConverter.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/28/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/28/2017
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnePlat.DiceNotation.Converters
{
    /// <summary>
    /// Value converter to change a list of TermResults into a display string.
    /// </summary>
    public class TermResultListConverter
    {
        /// <summary>
        /// Converts the TermResults into a string respresentation.
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">target type of the conversion</param>
        /// <param name="parameter">converter parameter</param>
        /// <param name="language">language</param>
        /// <returns>Converted value.</returns>
        public virtual object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(string))
            {
                throw new ArgumentException("Unexpected type passed to converter.", nameof(targetType));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            List<TermResult> list = value as List<TermResult>;
            if (list == null)
            {
                throw new ArgumentException("Object not of type List<TermResult>.", nameof(value));
            }

            return this.DiceRollsToString(list);
        }

        /// <summary>
        /// Converts back from string representation to a list of TermResults.
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">target type of the conversion</param>
        /// <param name="parameter">converter parameter</param>
        /// <param name="language">language</param>
        /// <returns>Converted value.</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts the DiceResult to a string representation of a list of
        /// dice roll results.
        /// </summary>
        /// <param name="results">DiceResult to use</param>
        /// <returns>string represenation of DiceResult dice rolls</returns>
        private string DiceRollsToString(List<TermResult> results)
        {
            var list = from r in results
                       where r.Type.Contains(nameof(DiceTerm))
                       select r;

            string res = string.Empty;
            foreach (TermResult item in list)
            {
                if (item.AppliesToResultCalculation)
                {
                    res += item.Value.ToString() + ", ";
                }
                else
                {
                    res += item.Value.ToString() + "*, ";
                }
            }

            return res.Trim().TrimEnd(',');
        }
    }
}
