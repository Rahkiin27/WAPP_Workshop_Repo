using AirBnbChartWorkshop.Models.ViewModels;
using AirBnbFakeDatabase.Models;
using AirBnbFakeDatabase.Services;
using System.Collections.Generic;

namespace AirBnbChartWorkshop.Services
{
    public class DevExtremeService
    {
        private readonly ListingService _listingService;

        public DevExtremeService()
        {
            _listingService = new ListingService();
        }

        public DevExtremeViewModel GetDevExtremeViewModel(IEnumerable<Listing> listings) 
        {
            return new DevExtremeViewModel
            {
                BarChartData = _listingService.GetBarChartData(listings),
                LineChartData = _listingService.GetLineChartData(listings, 50),
                PieChartData = _listingService.GetBarChartData(listings)
            };
        }
    }
}
