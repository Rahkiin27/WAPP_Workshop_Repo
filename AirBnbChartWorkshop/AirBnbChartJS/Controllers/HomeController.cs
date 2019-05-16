using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AirBnbChartJS.Models;
using AirBnbFakeDatabase.Database;
using AirBnbChartJS.Services;

namespace AirBnbChartJS.Controllers
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

        public IActionResult Chart(int id)
        {
            var listings = _fakeDatabase.Listings;
            var viewModel = _chartJsService.GetChartViewModel(listings, id);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
