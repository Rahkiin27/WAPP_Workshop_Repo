using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AirBnbChartWorkshop.Models;
using AirBnbChartWorkshop.Services;
using AirBnbFakeDatabase.Database;

namespace AirBnbChartWorkshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly FakeDatabase _fakeDatabase;

        public HomeController()
        {
            _fakeDatabase = new FakeDatabase();
        }

        public IActionResult Index()
        {
            return View(_fakeDatabase.Listings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DevExtreme()
        {
            var service = new DevExtremeService();
            var listings = _fakeDatabase.Listings;

            return View(service.GetDevExtremeViewModel(listings));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult HighCharts()
        {
            var highChartViewModel = new ChartViewModel
            {
                ListingData = GetListingsWithAmount()
            };

            return View(highChartViewModel);
        }

        private Dictionary<string, int> GetListingsWithAmount()
        {
            var listings = _fakeDatabase.Listings;
            var output = new Dictionary<string, int>();

            foreach (var listing in listings)
            {
                if (!output.Keys.Contains(listing.Neighbourhood))
                {
                    output.Add(listing.Neighbourhood, 1);
                }
                else
                {
                    output[listing.Neighbourhood]++;
                }
            }

            return output;
        }
    }
}
