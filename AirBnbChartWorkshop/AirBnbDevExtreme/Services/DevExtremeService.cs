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
            var lineChartData = _listingService.GetLineChartData(listings, 100);

            return new DevExtremeViewModel
            {
                BarChartData_AmountPerNeighbourhood = _listingService.GetBarChartData(listings)
            };
        }
    }
}
