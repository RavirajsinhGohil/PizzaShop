using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Controllers;

[Route("GetData")]
public class GetDataController : Controller
{
        private readonly SchemaTestContext _dbo;
        // private readonly IGetDataRepository _getDataRepository;

        public GetDataController(SchemaTestContext dbo)
        {
            _dbo = dbo;
        }

        [HttpGet("GetCountries")]
        public IActionResult GetCountries()
        {

            var countries = _dbo.Countries
                .Select(c => new
                {
                    countryId = c.Countryid,
                    countryName = c.Countryname
                })
                .ToList();

            return Json(countries);
        }

        [HttpGet("GetStates")]
        public IActionResult GetStates(int countryId)
        {
            var states = _dbo.States
                .Where(s => s.Countryid == countryId)
                .Select(s => new
                {
                    stateId = s.Stateid,
                    stateName = s.Statename
                })
                .ToList();

            return Json(states); 
        }

        [HttpGet("GetCities")]
        public IActionResult GetCities(int stateId)
        {
            var cities = _dbo.Cities
                .Where(c => c.Stateid == stateId)
                .Select(c => new
                {
                    cityId = c.Cityid,
                    cityName = c.Cityname
                })
                .ToList();

            return Json(cities); 
        }
}
