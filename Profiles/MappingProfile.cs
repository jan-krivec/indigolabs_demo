using AutoMapper;
using indigolabs_demo.DTOs;
using indigolabs_demo.Models;

namespace indigolabs_demo.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CityTemperatureModel, CityTemperatureDTO>();
            CreateMap<CityTemperatureModel, CityAvgTemperatureDTO>();
        }
    }
}
