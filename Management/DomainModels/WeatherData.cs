// <copyright file="WeatherData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Management.DomainModels
{
    using System;

    /// <summary>
    /// Provides the summary of a city's weather, minimum temperature, and maximum temperature.
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherData"/> class.
        /// </summary>
        /// <param name="weatherSummary">The summary of weather of a city.</param>
        /// <param name="minTemp">The minimum temperature in Fahrenheit of a city.</param>
        /// <param name="maxTemp">The maximum temperature in Fahrenheit of a city.</param>
        public WeatherData(string weatherSummary, double? minTemp, double? maxTemp)
        {
            this.WeatherSummary = weatherSummary ?? throw new ArgumentNullException(nameof(weatherSummary));
            this.MinTemperature = minTemp ?? throw new ArgumentNullException(nameof(minTemp));
            this.MaxTemperature = maxTemp ?? throw new ArgumentNullException(nameof(maxTemp));
        }

        /// <summary>
        /// Gets the summary of today's weather.
        /// </summary>
        public string WeatherSummary { get; }

        /// <summary>
        /// Gets the minimum temperature in Fahrenheit.
        /// </summary>
        public double MinTemperature { get; }

        /// <summary>
        /// Gets the maximum temperature in Fahrenheit.
        /// </summary>
        public double MaxTemperature { get; }
    }
}
