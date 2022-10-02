using CountryProject.Exceptions;
using CountryProject.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CountryProject.Services
{
    public class CountryService : ICountryService
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "CacheKey";
        private readonly IConfiguration _config;

        public CountryService(IMemoryCache cache, IConfiguration config)
        {
            _cache = cache;
            _config = config;
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = RetrieveFromCache();
            if (countries == null)
            {
                using HttpClient _httpClient = new HttpClient();
                var url = $"{_config["CountryBaseUrl"]}/all";
                var result = await _httpClient.GetAsync(url);
                var res = await result.Content.ReadAsStringAsync();

                if (!result.IsSuccessStatusCode)
                {
                    throw new NotSuccessfulException("An Error Occured");
                }
                countries = JsonConvert.DeserializeObject<List<CountryModel>>(res);
                SaveToCache(countries);
            }
            return countries;
        }

        public async Task<CountryModel> GetCountryById(int id)
        {
            //get country by Id
            var countries = RetrieveFromCache();
            var country = new CountryModel();
            if (countries == null)
            {
                using HttpClient _httpClient = new HttpClient();
                var url = $"{_config["CountryBaseUrl"]}/all";
                var result = await _httpClient.GetAsync(url);
                var res = await result.Content.ReadAsStringAsync();

                if (!result.IsSuccessStatusCode)
                {
                    throw new NotSuccessfulException("An Error Occured");
                }
                countries = JsonConvert.DeserializeObject<List<CountryModel>>(res);
                SaveToCache(countries);
            }

            country = countries.FirstOrDefault(x => x.Id == id);

            //get all neighboring countries
            if (country.Borders != null)
            {
                country.Neighbors = await GetCountryByCodes(country.Borders);
            }
            return country;
        }


        public async Task<List<CountryModel>> GetCountryByCodes(List<string> codes)
        {
            using HttpClient _httpClient = new HttpClient();
            var url = $"{_config["CountryBaseUrl"]}/alpha?codes={string.Join(',', codes)}";
            var result = await _httpClient.GetAsync(url);
            var res = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                throw new NotSuccessfulException("An Error Occured");
            }
            return JsonConvert.DeserializeObject<List<CountryModel>>(res);
        }

        #region Helpers
        public void SaveToCache(List<CountryModel> data)
        {
            var index = 1;
            data = data.OrderBy(x => x.Name.Common)
                       .Select(x =>
                       {
                           x.Id = index++;
                           return x;
                       }).ToList();
            _cache.Set(CacheKey, data, DateTimeOffset.Now.AddMinutes(5));
        }

        public List<CountryModel> RetrieveFromCache()
        {
            var res = _cache.Get<List<CountryModel>>(CacheKey);
            return res;
        }


        #endregion
    }
}
