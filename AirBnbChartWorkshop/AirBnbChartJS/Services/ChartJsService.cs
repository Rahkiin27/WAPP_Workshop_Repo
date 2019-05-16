using AirBnbChartJS.Models;
using AirBnbFakeDatabase.Models;
using AirBnbFakeDatabase.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AirBnbChartJS.Services
{
    public class ChartJsService
    {
        private static ChartJsService _chartJsService;
        public static ChartJsService Instance
        {
            get
            {
                if (_chartJsService == null)
                    _chartJsService = new ChartJsService();

                return _chartJsService;
            }
        }

        private const int BAR_CHART = 0;
        private const int LINE_CHART = 1;
        private const int PIE_CHART = 2;

        private readonly ListingService _listingService;
        private readonly Random _random;

        private IEnumerable<Color> _selectedColors;

        public ChartJsService()
        {
            _listingService = new ListingService();
            _random = new Random();
        }

        public ChartViewModel GetChartViewModel(IEnumerable<Listing> listings, int chartId)
        {
            string type = string.Empty;
            switch (chartId)
            {
                case BAR_CHART:
                    type = "bar";
                    break;
                case LINE_CHART:
                    type = "line";
                    break;
                case PIE_CHART:
                    type = "pie";
                    break;
                default:
                    break;
            }

            var data = _listingService.GetBarChartData(listings);

            if (_selectedColors == null || _selectedColors.Count() < data.Count())
                _selectedColors = data.Select(d => GetRandomColor()).ToList();

            return new ChartViewModel
            {
                Label = "Amount of Listings per Neighbourhood",
                Type = type,
                XLabels = data.Select(x => x.NeighbourhoodName),
                YValues = data.Select(x => x.AmountOfListings),
                Colors = _selectedColors
            };
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
        }
    }
}
