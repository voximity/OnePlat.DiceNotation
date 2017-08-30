// <copyright file="DieRollTracker.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/30/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/30/2017
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
using System.Collections.Generic;
using System.Linq;

namespace OnePlat.DiceNotation.DieRoller
{
    /// <summary>
    /// Class for in-memory statistical tracking service for die rolls.
    /// </summary>
    public class DieRollTracker : IDieRollTracker
    {
        private List<DieTrackingData> rollData = new List<DieTrackingData>();

        /// <inheritdoc/>
        public void AddDieRoll(int dieSides, int result, Type dieRoller)
        {
            DateTime timestamp = DateTime.Now;
            DieTrackingData entry = new DieTrackingData
            {
                Id = Guid.NewGuid(),
                RollerType = dieRoller.Name,
                DieSides = dieSides.ToString(),
                Result = result,
                Timpstamp = DateTime.Now
            };

            this.rollData.Add(entry);
        }

        /// <inheritdoc/>
        public IList<DieTrackingData> GetTrackingData(string dieType = null, string dieSides = null)
        {
            IEnumerable<DieTrackingData> result = this.rollData;

            if (!string.IsNullOrEmpty(dieType))
            {
                result = result.Where(e => e.RollerType == dieType);
            }

            if (!string.IsNullOrEmpty(dieSides))
            {
                result = result.Where(e => e.DieSides == dieSides);
            }

            return result.OrderBy(e => e.DieSides).ThenBy(e => e.Result).ToList();
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.rollData.Clear();
        }
    }
}
