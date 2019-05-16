using AirBnbFakeDatabase.Models;
using System.Collections.Generic;

namespace AirBnbChartWorkshop.Models.ViewModels
{
    public class DevExtremeViewModel
    {
        public IEnumerable<AmountOfListingsPerNeighbourhood> BarChartData_AmountPerNeighbourhood { get; set; }
    }
}
