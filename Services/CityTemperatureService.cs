using AutoMapper;
using indigolabs_demo.DTOs;
using indigolabs_demo.Models;

namespace indigolabs_demo.Services
{
    public class CityTemperatureService : ICityTemperatureService
    {
        private IFileWatcherService _fileWatcherService;
        private readonly IMapper _mapper;

        public CityTemperatureService(IFileWatcherService fileWatcherService, IMapper mapper)
        {
            _fileWatcherService = fileWatcherService;
            _mapper = mapper;
        }

        private Dictionary<string, CityTemperatureModel> GetCityDictionary()
        {
            return _fileWatcherService.GetCityDictionary();
        }

        private List<CityTemperatureModel> GetCityTemperatureDTOList()
        {
            return GetCityDictionary().Values.ToList();
        }

        public List<CityTemperatureDTO> GetCityTemperatureList()
        {
            try
            {
                List<CityTemperatureModel> cityTemperatureList = GetCityTemperatureDTOList();
                return cityTemperatureList.Select(cityTemperature => _mapper.Map<CityTemperatureDTO>(cityTemperature)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CityAvgTemperatureDTO> FilterCityTemperatureList(CityTemperatureFilterDTO? filter)
        {
            try
            {
                List<CityTemperatureModel> cityTemperatureList = GetCityTemperatureDTOList();

                if (filter != null)
                {
                    if (filter.LessThan != null)
                    {
                        cityTemperatureList = cityTemperatureList.FindAll(cityTemperature => cityTemperature.AvgTemperature < filter.LessThan);
                    }
                    if (filter.GreaterThan != null)
                    {
                        cityTemperatureList = cityTemperatureList.FindAll(cityTemperature => cityTemperature.AvgTemperature > filter.GreaterThan);
                    }
                }
                return cityTemperatureList.Select(cityTemperature => _mapper.Map<CityAvgTemperatureDTO>(cityTemperature)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CityTemperatureDTO GetCityTemperature(string cityName)
        {
            try
            {
                Dictionary<string, CityTemperatureModel> cityDictionary = GetCityDictionary();
                CityTemperatureModel cityTemperature = cityDictionary[cityName];
                return _mapper.Map<CityTemperatureDTO>(cityTemperature);
            }
            catch
            {
                throw new Exception("City not found!");
            }
        }
    }
}
