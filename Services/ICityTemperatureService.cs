using indigolabs_demo.DTOs;

namespace indigolabs_demo.Services
{
    public interface ICityTemperatureService
    {
        List<CityAvgTemperatureDTO> FilterCityTemperatureList(CityTemperatureFilterDTO? filter);
        CityTemperatureDTO GetCityTemperature(string cityName);
        List<CityTemperatureDTO> GetCityTemperatureList();
    }
}
