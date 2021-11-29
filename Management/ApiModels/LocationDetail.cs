namespace Management.ApiModels
{
    /// <summary>
    /// Representation of a location detail (air quality, weather, and covid)
    /// </summary>
    public class LocationDetail
    {
        /// <summary>
        /// Gets or sets the location name
        /// </summary>
        public string LocationName { get; set; } = "DemoName";

        /// <summary>
        /// Gets or sets the air quality score
        /// </summary>
        public int AirScore { get; set; } = 90;

        /// <summary>
        /// Gets or sets the covid score
        /// </summary>
        public int CovidScore { get; set; } = 60;

        /// <summary>
        /// Gets or sets the weather score
        /// </summary>
        public int WheatherScore { get; set; } = 80;
    }
}
