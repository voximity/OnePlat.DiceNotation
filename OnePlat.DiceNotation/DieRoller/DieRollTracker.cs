// <copyright file="DieRollTracker.cs" company="DarthPedro">
// Copyright (c) 2017 DarthPedro. All rights reserved.
// </copyright>

//-----------------------------------------------------------------------
// Assembly         : OnePlat.DiceNotation
// Author           : DarthPedro
// Created          : 8/30/2017
//
// Last Modified By : DarthPedro
// Last Modified On : 8/31/2017
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            // check input values first
            if (dieSides < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(dieSides));
            }

            if (result < -1 || result > dieSides)
            {
                throw new ArgumentOutOfRangeException(nameof(result));
            }

            if (dieRoller == null)
            {
                throw new ArgumentNullException(nameof(dieRoller));
            }

            if (typeof(IDieRoller).GetTypeInfo().IsAssignableFrom(dieRoller.GetTypeInfo()) == false)
            {
                throw new ArgumentException(nameof(dieRoller));
            }

            // create tracking data entry
            DateTime timestamp = DateTime.Now;
            DieTrackingData entry = new DieTrackingData
            {
                Id = Guid.NewGuid(),
                RollerType = dieRoller.Name,
                DieSides = dieSides.ToString(),
                Result = result,
                Timpstamp = DateTime.Now
            };

            // save it to list
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

        /// <inheritdoc/>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this.rollData, Formatting.Indented);
        }

        /// <inheritdoc/>
        public void LoadFromJson(string jsonText)
        {
            object obj = JsonConvert.DeserializeObject(jsonText, typeof(List<DieTrackingData>));
            this.rollData = obj as List<DieTrackingData>;
        }

        /// <inheritdoc/>
        public IList<AggregateDieTrackingData> GetFrequencyDataView()
        {
            IList<AggregateDieTrackingData> results = new List<AggregateDieTrackingData>();
            var dieTypes = this.GetTrackingData().Select(d => d.RollerType).Distinct();

            // first go through all of the different DieRoller types
            foreach (string t in dieTypes)
            {
                var dieSides = this.GetTrackingData(t).Select(d => d.DieSides).Distinct();

                // then go through all of the different die sides rolled for each roller type
                foreach (string s in dieSides)
                {
                    var diceBySides = this.GetTrackingData(t, s);
                    var dieResults = diceBySides.Select(d => d.Result).Distinct();
                    float total = diceBySides.Count;

                    // finally go through all fo the results rolled for each die side
                    foreach (int r in dieResults)
                    {
                        int count = diceBySides.Count(d => d.Result == r);
                        AggregateDieTrackingData aggCount = new AggregateDieTrackingData
                        {
                            RollerType = t,
                            DieSides = s,
                            Result = r,
                            Count = count,
                            Percentage = (float)Math.Round((count / total) * 100, 1)
                        };

                        // add that data into a view for each roller type, sides, result combination
                        results.Add(aggCount);
                    }
                }
            }

            return results;
        }
    }
}
