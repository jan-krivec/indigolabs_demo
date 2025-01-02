using indigolabs_demo.DTOs;

namespace indigolabs_demo.Services
{
    public interface ICityTemperatureService
    {
        List<CityTemperatureDTO> GetCityTemperatureList();
        List<CityAvgTemperatureDTO> FilterCityTemperatureList(CityTemperatureFilterDTO? filter);

        CityTemperatureDTO GetCityTemperature(string cityName);
    }
}
