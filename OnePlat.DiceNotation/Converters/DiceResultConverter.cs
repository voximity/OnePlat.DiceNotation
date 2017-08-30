// <copyright file="DiceResultConverter.cs" company="DarthPedro">
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
using System;

namespace OnePlat.DiceNotation.Converters
{
    /// <summary>
    /// Value converter to change a DiceResult into a display string.
    /// </summary>
    public class DiceResultConverter
    {
        /// <summary>
        /// Converts the DiceResult into a string respresentation.
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

            DiceResult dr = value as DiceResult;
            if (dr == null)
            {
                throw new ArgumentException("Object not of type DiceResult.", nameof(value));
            }

            return string.Format("{0} ({1})", dr.Value, dr.DiceExpression);
        }

        /// <summary>
        /// Converts back from string representation to a DiceResult.
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">target type of the conversion</param>
        /// <param name="parameter">converter parameter</param>
        /// <param name="language">language</param>
        /// <returns>Converted value.</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // the reverse conversion is not supported.
            throw new NotSupportedException();
        }
    }
}
