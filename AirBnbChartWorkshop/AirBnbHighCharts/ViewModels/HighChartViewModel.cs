using AirBnbHighCharts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnbHighCharts.ViewModels
{
    public class HighChartViewModel
    {
        public IEnumerable<AirBnbFakeDatabase.Models.AmountOfListingsPerNeighbourhood> AmountOfListingsPerNeighbourhood { get; set; }

        public IEnumerable<int> GetAmountOfListings()
        {
            return AmountOfListingsPerNeighbourhood.Select(listing => listing.AmountOfListings);
        }

        public IEnumerable<string> GetNeighbourhoods()
        {
            return AmountOfListingsPerNeighbourhood.Select(listing => listing.NeighbourhoodName);
        }

        public IEnumerable<PieChartSlice> GetPieChartData()
        {
            return AmountOfListingsPerNeighbourhood.Select(listing => new PieChartSlice
            {
                Name = listing.NeighbourhoodName,
                y = listing.AmountOfListings
            });
        }
    }
}
