using indigolabs_demo.DTOs;
using indigolabs_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace indigolabs_demo.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityTemperatureController : ControllerBase
    {

        private ICityTemperatureService _cityTemperatureService;

        public CityTemperatureController(ICityTemperatureService cityTemperatureService)
        {
            _cityTemperatureService = cityTemperatureService;
        }

        [HttpGet]
        public IActionResult GetCityTemperature()
        {

            try
            {
                List<CityTemperatureDTO> cityTemperatureList = _cityTemperatureService.GetCityTemperatureList();
                return Ok(cityTemperatureList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("filterList")]
        public IActionResult FilterCityTemperature(CityTemperatureFilterDTO? filter)
        {

            try
            {
                List<CityAvgTemperatureDTO> cityTemperatureList = _cityTemperatureService.FilterCityTemperatureList(filter);
                return Ok(cityTemperatureList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{cityName}")]
        public IActionResult GetCityTemperature(string cityName)
        {

            try
            {
                CityTemperatureDTO cityTemperature = _cityTemperatureService.GetCityTemperature(cityName);
                return Ok(cityTemperature);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
