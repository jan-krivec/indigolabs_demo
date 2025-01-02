using indigolabs_demo.Models;

namespace indigolabs_demo.Services
{
    public interface IFileWatcherService
    {
        Dictionary<string, CityTemperatureModel> GetCityDictionary();
    }
}
