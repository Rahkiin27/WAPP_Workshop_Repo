using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirBnbHighCharts.Models;
using AirBnbFakeDatabase.Database;
using AirBnbFakeDatabase.Services;

namespace AirBnbHighCharts.Controllers
{
    public class HomeController : Controller
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ListingService _listingService;

        public HomeController()
        {
            _fakeDatabase = new FakeDatabase();
            _listingService = new ListingService();
        }

        public IActionResult Index()
        {
            var listings = _fakeDatabase.Listings;
            var amountOfListingsPerNeighbourhood = _listingService.GetBarChartData(listings);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
