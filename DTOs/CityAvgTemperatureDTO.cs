using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using indigolabs_demo.Converters;

namespace indigolabs_demo.DTOs
{
    public class CityAvgTemperatureDTO
    {
        public string Name { get; set; } = string.Empty;
        [JsonConverter(typeof(DoubleFormatConverter))]
        public double AvgTemperature { get; set; } = 0;
    }
}
