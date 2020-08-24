using CountryDashboardServer.Components.Models;
using CountryDashboardServer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDashboardServer.Interfaces
{
    public interface ICountryService
    {
        Task<Response<List<Item>>> GetAllCountries();
        Task<Response<Countries>> AddCountry(Guid Id,string Country, string IsoCode);
        Task<Response<Countries>> DeleteCountry(string IsoCode);
    }
}
