using AirBnbChartJSTest.Models;
using AirBnbFakeDatabase.Database;
using AirBnbFakeDatabase.Services;
using System.Collections.Generic;

namespace AirBnbChartJSTest.Services
{
    public class ChartsService
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ListingService _listingService;

        public ChartsService()
        {
            _fakeDatabase = new FakeDatabase();
            _listingService = new ListingService();
        }

        public List<DataPoint> GetListingsPerNeighbourhoodDataPoints()
        {
            var listings = _fakeDatabase.Listings;
            var amountOfListingsPerNeighbourhood = _listingService.GetBarChartData(listings);

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var listing in amountOfListingsPerNeighbourhood)
            {
                DataPoint datapoint = new DataPoint(listing.NeighbourhoodName, listing.AmountOfListings);
                dataPoints.Add(datapoint);
            }

            return dataPoints;
        }
    }
}
