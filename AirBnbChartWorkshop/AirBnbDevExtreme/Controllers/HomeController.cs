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
        private readonly DevExtremeService _devExtremeService;

        public HomeController()
        {
            _fakeDatabase = new FakeDatabase();
            _devExtremeService = new DevExtremeService();
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
            var listings = _fakeDatabase.Listings;

            return View(_devExtremeService.GetDevExtremeViewModel(listings));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
