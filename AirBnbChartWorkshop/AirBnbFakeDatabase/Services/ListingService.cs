using AirBnbFakeDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace AirBnbFakeDatabase.Services
{
    public class ListingService
    {
        public IEnumerable<AmountPerNeighbourhood> GetBarChartData(IEnumerable<Listing> listings)
        {
            var neighbourhoods = new Dictionary<string, int>();

            var output = listings
                .GroupBy(l => l.Neighbourhood)
                .Select(l =>
                {
                    return new AmountPerNeighbourhood
                    {
                        NeighbourhoodName = l.Key,
                        AmountOfListings = l.Sum(item => 1)
                    };
                });

            return output;
        }
    }
}
