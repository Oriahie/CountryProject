using CountryProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountryProject.Services
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetCountries();
        Task<CountryModel> GetCountryById(int id);
        Task<List<CountryModel>> GetCountryByCodes(List<string> codes);
    }
}
