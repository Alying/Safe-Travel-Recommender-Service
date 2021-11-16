// <copyright file="AirQualityData.cs" company="ASE#">
//     Copyright (c) ASE#. All rights reserved.
// </copyright>

namespace Management.DomainModels
{
    using System;

    /// <summary>
    /// class <c>AirQualityData</c> provide the cumulated COVID-19 confirmed cases and  
    /// cumulated COVID-19 death cases of a country/state
    /// </summary>
    public class AirQualityData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirQualityData"/> class.
        /// </summary>
        /// <param name="aqi">The Air Quality Index(AQI) value based on US EPA standard of a city.</param>
        /// <param name="humidity">The humidity percentage of a city.</param>
        /// <param name="windSpeed">The wind speed of a city.</param>
        public AirQualityData(int? aqi, double? humidity, double? windSpeed)
        {
            this.AirQualityIndex = aqi ?? throw new ArgumentNullException(nameof(aqi));
            this.Humidity = humidity ?? throw new ArgumentNullException(nameof(humidity));
            this.WindSpeed = windSpeed ?? throw new ArgumentNullException(nameof(windSpeed));
        }

        /// <summary>
        /// Gets the Air Quality Index(AQI) value based on US EPA standard
        /// </summary>
        public int AirQualityIndex { get; }     

        /// <summary>
        /// Gets the humidity percentage
        /// </summary>
        public double Humidity { get; }

        /// <summary>
        /// Gets the wind speed in meters per second
        /// </summary>
        public double WindSpeed { get; }
    }
}
