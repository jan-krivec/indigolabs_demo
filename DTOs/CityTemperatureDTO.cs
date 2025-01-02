using System.ComponentModel.DataAnnotations;
using indigolabs_demo.Converters;
using System.Text.Json.Serialization;

namespace indigolabs_demo.DTOs
{
    public class CityTemperatureDTO : CityAvgTemperatureDTO
    {
        [JsonConverter(typeof(DoubleFormatConverter))]
        public double MaxTemperature { get; set; }
        [JsonConverter(typeof(DoubleFormatConverter))]
        public double MinTemperature { get; set; }
    }
}
