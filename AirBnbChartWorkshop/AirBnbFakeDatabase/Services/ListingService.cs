using AirBnbFakeDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace AirBnbFakeDatabase.Services
{
    public class ListingService
    {
        public IEnumerable<AmountPerNeighbourhood> GetBarChartData(IEnumerable<Listing> listings)
        {
            var output = new List<AmountPerNeighbourhood>();
            var neighbourhoods = new Dictionary<string, int>();

            foreach (var listing in listings)
            {
                if (!neighbourhoods.Keys.Contains(listing.Neighbourhood))
                {
                    neighbourhoods.Add(listing.Neighbourhood, 1);
                }
                else
                {
                    neighbourhoods[listing.Neighbourhood]++;
                }
            }

            foreach (var kvp in neighbourhoods)
            {
                output.Add(new AmountPerNeighbourhood
                {
                    NeighbourhoodName = kvp.Key,
                    AmountOfListings = kvp.Value
                });
            }

            return output;
        }
    }
}
