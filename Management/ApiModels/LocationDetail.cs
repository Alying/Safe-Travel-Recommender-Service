using System;
using System.Collections.Generic;
using System.Text;

namespace Management.ApiModels
{
    public class LocationDetail
    {
        public string LocationName { get; set; } = "DemoName";

        public int AirScore { get; set; } = 90;

        public int CovidScore { get; set; } = 60;

        public int WheatherScore { get; set; } = 80;
    }
}
