// <copyright file="Recommendation.cs" company="ASE#">
//     Copyright (c) ASE#. All rights reserved.
// </copyright>

namespace Management.DomainModels
{
    using System;

    /// <summary>
    /// class <c>Recommendation</c> provides the summary of a city's weather,  
    /// minimum temperature, and maximum temperature
    /// </summary>
    public class Recommendation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recommendation"/> class.
        /// </summary>
        /// <param name="location">The location (country, state) inquired by the user.</param>
        /// <param name="userId">The user id of the user.</param>
        /// <param name="covid">The COVID-19 data of a country/state.</param>
        /// <param name="weather">The weather data of a city.</param>
        /// <param name="airQuality">The air quality data of a city.</param>
        public Recommendation(Location location, UserId userId, CovidData covid, WeatherData weather, AirQualityData airQuality)
        {
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            this.CovidData = covid ?? throw new ArgumentNullException(nameof(covid));
            this.WeatherData = weather ?? throw new ArgumentNullException(nameof(weather));
            this.AirQualityData = airQuality ?? throw new ArgumentNullException(nameof(airQuality));
        }

        /// <summary>
        /// Gets the location (country, state)
        /// </summary>
        public Location Location { get; }

        /// <summary>
        /// Gets the user id 
        /// </summary>
        public UserId UserId { get; }

        /// <summary>
        /// Gets the COVID-19 data 
        /// </summary>
        public CovidData CovidData { get; }

        /// <summary>
        /// Gets the weather data
        /// </summary>
        public WeatherData WeatherData { get; }

        /// <summary>
        /// Gets the air quality data
        /// </summary>
        public AirQualityData AirQualityData { get; }
    }
}
