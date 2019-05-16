using AirBnbFakeDatabase.Models;
using System.Collections.Generic;

namespace AirBnbChartWorkshop.Models.ViewModels
{
    public class DevExtremeViewModel
    {
        public IEnumerable<AmountOfListingsPerNeighbourhood> BarChartData { get; set; }
        public IEnumerable<AverageAmountOfBedsPerPriceRange> LineChartData { get; set; }
        public IEnumerable<AmountOfListingsPerNeighbourhood> PieChartData { get; set; }
    }
}
