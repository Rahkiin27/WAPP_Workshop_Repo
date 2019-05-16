using AirBnbChartWorkshop.Models.ViewModels;
using AirBnbFakeDatabase.Models;
using AirBnbFakeDatabase.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
                BarChartData_AmountPerNeighbourhood = _listingService.GetBarChartData(listings)
            };
        }
    }
}
