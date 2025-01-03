﻿namespace indigolabs_demo.Models
{
    public class CityTemperatureModel
    {
        public string Name { get; set; } = string.Empty;
        public double AvgTemperature { get; set; } = 0;
        public double? MaxTemperature { get; set; }
        public double? MinTemperature { get; set; }
        public double SumTemperature { get; set; } = 0;
        public int Count { get; set; } = 0;
    }
}
