using CountryDashboardServer.Components.Database;
using CountryDashboardServer.Components.Models;
using CountryDashboardServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CountryDashboardServer.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CountryDashboardServer.Components.Services
{
    public class CountryService : ICountryService
    {
        private readonly DbContexts dbContexts;
        public CountryService(DbContexts dbContexts)
        {
            this.dbContexts = dbContexts;
        }

        public async Task<Response<Countries>> AddCountry(Guid Id, string Country, string IsoCode)
        {
            var listCountryIsoCodes =  dbContexts.Countries.Select(c => c.IsoCode).ToList();
            var existingCountry = await dbContexts.Countries.FirstOrDefaultAsync(c => c.IsoCode == IsoCode.ToString());
            if (existingCountry != null)
                {
                    return new Response<Countries>
                    {
                        Message = "The country already exist",
                        Model = existingCountry,
                        Successful = false
                    };
                }
                else if(listCountryIsoCodes.Count == 10)
                {
                    return new Response<Countries>
                    {
                        Message = "The number of countries exceed the limit of 10",
                        Successful = false
                    };
                }
                var newCountry = new Countries()
                {
                    ID = Id,
                    Country = Country,
                    IsoCode = IsoCode
                };
                dbContexts.Add(newCountry);
                dbContexts.SaveChanges();

                return new Response<Countries>
                {
                    Message = "Successful",
                    Model = newCountry,
                    Successful = true
                };
                        
        }        
        public async Task<Response<List<Item>>> GetAllCountries()
        {
            var ItemList = new List<Item>();
            var countries = dbContexts.Countries.ToList();
            var invalidResponse = new InvalidResponse();
            foreach (var country in countries)
            {                
                var uri = $"http://api.worldbank.org/v2/country/{country.IsoCode}?format=json";                
                using (HttpResponseMessage response = await ApiHelper.client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            var CountriesList = await response.Content.ReadAsAsync<List<object>>();
                            if(CountriesList.Count == 1)                     
                                 invalidResponse = JsonConvert.DeserializeObject<InvalidResponse>(CountriesList[0].ToString().Substring(0));
                          
                            var PagesDetails = JsonConvert.DeserializeObject<PagesDetails>(CountriesList[0].ToString().Substring(0));
                            List<Item> Items =  JsonConvert.DeserializeObject<List<Item>>(CountriesList[1].ToString().Substring(0));
                            Items.FirstOrDefault().PagesDetails = PagesDetails;
                            ItemList.Add(Items.FirstOrDefault());
                        }
                        catch(Exception)
                        {
                            return new Response<List<Item>>()
                            {
                                Message = $"{country.IsoCode.ToUpper()} {invalidResponse.Message.Select(c=>c.Value.ToString()).FirstOrDefault()}",
                                Successful = false
                            };
                        }
                    }
                }
            }
            return new Response<List<Item>>() 
            { 
                Message = "Sucessfully retieved countries",
                Model = ItemList,
                Successful = true
            };                           
        }
        public async Task<Response<Countries>> DeleteCountry(string IsoCode)
        {
            var country = await dbContexts.Countries.FirstOrDefaultAsync(c => c.IsoCode == IsoCode.ToString());
            if (country == null)
            {
                return new Response<Countries>
                {
                    Message = $"Country with IsoCode {IsoCode} does not exist in the database",
                    Successful = false
                };
            }
            var deleted = dbContexts.Countries.Remove(country);
            dbContexts.SaveChanges();
            return new Response<Countries>
            {
                Message = $"Sucessfully deleted {country.Country}",
                Model = country,
                Successful = true
            };
        }
    }
}
