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

        public IEnumerable<AverageAmountOfBedsPerPriceRange> GetLineChartData(IEnumerable<Listing> listings, int priceRangeSize)
        {
            return listings
                .GroupBy(l => GetPriceRange(l.Price))
                .OrderBy(l => l.Key)
                .Select(l =>
                {
                    int priceRange = GetPriceRange(l.Max(item => item.Price)) * priceRangeSize;
                    string priceRangeString = $"${priceRange - priceRangeSize} - ${priceRange}";
                    return new AverageAmountOfBedsPerPriceRange
                    {
                        PriceRange = priceRangeString,
                        AverageAmountOfBeds = l.Average(item => item.Beds)
                    };
                });

            int GetPriceRange(double price) => (int)Math.Ceiling(price / priceRangeSize);
        }
    }
}
