// <copyright file="CovidData.cs" company="ASE#">
//     Copyright (c) ASE#. All rights reserved.
// </copyright>

namespace Management.DomainModels
{
    using System;

    /// <summary>
    /// class <c>CovidData</c> provide the cumulated COVID-19 confirmed cases and  
    /// cumulated COVID-19 death cases of a country/state
    /// </summary>
    public class CovidData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CovidData"/> class.
        /// </summary>
        /// <param name="confirmedCases">The confirmed COVID-19 cases of a country/state.</param>
        /// <param name="deathCases">The COVID-19 death cases of a country/state.</param>
        public CovidData(int? confirmedCases, int? deathCases)
        {
            this.ConfirmedCases = confirmedCases ?? throw new ArgumentNullException(nameof(confirmedCases));
            this.DeathCases = deathCases ?? throw new ArgumentNullException(nameof(deathCases));
        }

        /// <summary>
        /// Gets the cumulated confirmed COVID-19 cases of a country/state
        /// </summary>
        public int ConfirmedCases { get; }

        /// <summary>
        /// Gets the cumulated COVID-19 death cases of a country/state
        /// </summary>
        public int DeathCases { get; }
    }
}
