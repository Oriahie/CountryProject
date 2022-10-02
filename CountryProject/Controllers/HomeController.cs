using CountryProject.Models;
using CountryProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CountryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICountryService _countryService;

        public HomeController(ILogger<HomeController> logger, 
                              ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Country()
        {
            var countries = await _countryService.GetCountries();
            return View(countries);
        }

        [HttpGet("api/country")]
        public async Task<IActionResult> GetCountries()
        {
            var date = DateTime.Now;
            var countries = await _countryService.GetCountries();
            _logger.LogInformation($"Calling Get All Countries TimeStamp : {date} , Duration : {DateTime.Now.Subtract(date)}");
            return Ok(new
            {
                Data = countries
            });
        }

        [HttpGet("api/country/{id}")]
        public async Task<IActionResult> GetCountryById([Required]int id)
        {
            var date = DateTime.Now;
            var country = await _countryService.GetCountryById(id);
            _logger.LogInformation($"Calling Get Country By Id : {id} TimeStamp : {date} , Duration : {DateTime.Now.Subtract(date)}");
            return Ok(new
            {
                Data = country
            });
        }
    }
}
