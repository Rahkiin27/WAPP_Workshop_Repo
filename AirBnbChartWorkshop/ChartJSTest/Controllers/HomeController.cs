using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChartJSTest.Models;
using AirBnbFakeDatabase.Database;
using AirBnbChartJSTest.Services;

namespace ChartJSTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ChartJsService _chartJsService;

        public HomeController()
        {
            _fakeDatabase = FakeDatabase.Instance;
            _chartJsService = ChartJsService.Instance;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chart(int id)
        {
            var listings = _fakeDatabase.Listings;
            var viewModel = _chartJsService.GetChartViewModel(listings, id);
            return View(viewModel);
        }
    }
}
