using AirBnbFakeDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirBnbFakeDatabase.Services
{
    public class ListingService
    {
        public IEnumerable<AmountOfListingsPerNeighbourhood> GetBarChartData(IEnumerable<Listing> listings)
        {
            return listings
                .GroupBy(l => l.Neighbourhood)
                .Select(l =>
                {
                    return new AmountOfListingsPerNeighbourhood
                    {
                        NeighbourhoodName = l.Key,
                        AmountOfListings = l.Sum(item => 1)
                    };
                });
        }
    }
}
