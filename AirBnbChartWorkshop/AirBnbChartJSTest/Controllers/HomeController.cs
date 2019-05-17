using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirBnbChartJSTest.Models;
using AirBnbChartJSTest.Services;
using Newtonsoft.Json;

namespace AirBnbChartJSTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChartsService _chartsService;

        public HomeController()
        {
            _chartsService = new ChartsService();
        }

        public IActionResult Barchart()
        {
            var dataPoints = _chartsService.GetListingsPerNeighbourhoodDataPoints();
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }

        public IActionResult Piechart()
        {
            var dataPoints = _chartsService.GetListingsPerNeighbourhoodDataPoints();
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }

        public IActionResult Linechart()
        {
            var dataPoints = _chartsService.GetListingsPerNeighbourhoodDataPoints();
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }

        public IActionResult Index()
        {
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
