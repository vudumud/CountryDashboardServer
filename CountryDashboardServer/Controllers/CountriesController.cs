using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryDashboardServer.Components.Models;
using CountryDashboardServer.Interfaces;
using CountryDashboardServer.Shared;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace CountryDashboardServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        public ICountryService countryService;
        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCounty(string Country, string IsoCode)
        {
            var contry = await countryService.AddCountry(Guid.NewGuid(), Country, IsoCode);
            return Ok(contry);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await countryService.GetAllCountries();            
            return Ok(countries);
        }        
        [HttpDelete]
        public async Task<IActionResult> DeleteCountry(string IsoCode)
        {
            var contry = await countryService.DeleteCountry(IsoCode);
            return Ok(contry);
        }
    }
}
