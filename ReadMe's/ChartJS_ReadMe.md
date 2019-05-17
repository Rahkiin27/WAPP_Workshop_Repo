# Charts met ChartJS
In deze workshop gaan we een barchart, piechart en linechart toevoegen d.m.v. ChartJS.

1. Clone de workshop repo: [https://github.com/TheLegend27NL/WAPP_Workshop_Repo](https://github.com/TheLegend27NL/WAPP_Workshop_Repo)
2. Open de solution met VS2019
3. Maak een nieuw .NET CORE 3.0 MVC project in dezelfde solution aan genaamd: `"AirBnbChartJSTest"`
4. Installeer de Newtonsoft.Json NuGet package. (Staat als tweede wanneer de "Manage NuGet Packages" pagina op het "Browse" tabblad wordt geopend)

5. Open _Layout.cshtml en plak het volgende boven "</head>":
```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.bundle.min.js"></script>
<script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
```

6. Maak in de map `"Models"` een nieuw bestand met de naam `"ChartViewModel.cs"` en plak de volgende code in dit bestand:
```c#
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AirBnbChartJSTest.Models
{
    public class ChartViewModel
    {
        public string Label { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> XLabels { get; set; }
        public IEnumerable<int> YValues { get; set; }
        public IEnumerable<Color> Colors { get; set; }

        public IEnumerable<string> HtmlColors
        {
            get
            {
                return Colors.Select(c => { return $"rgba({c.R}, {c.G}, {c.B}, {c.A})"; });
            }
        }

        public IEnumerable<string> HtmlBorderColors
        {
            get
            {
                return Colors.Select(c => { return "rgba(0, 0, 0, 255)"; });
            }
        }
    }
}
```
7. Maak een nieuwe folder aan genaamd `"Services"` en voeg hier een nieuwe class toe genaamd `"ChartJsService.cs"`. Plak hier vervolgens de volgende code in: (Het "Listing" object wordt rood gemarkeerd. Ga op deze regel staan en druk op ALT+ENTER om een reference naar de "AirBnbFakeDatabase" te maken.)
```c#
using AirBnbChartJSTest.Models;
using AirBnbFakeDatabase.Models;
using AirBnbFakeDatabase.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AirBnbChartJSTest.Services
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
```

8. Open de HomeController en voeg de volgende code toe. Druk daarna op ALT+ENTER om de usings goed te zetten:
```c#
private readonly FakeDatabase _fakeDatabase;
private readonly ChartJsService _chartJsService;

public HomeController()
{
    _fakeDatabase = FakeDatabase.Instance;
    _chartJsService = ChartJsService.Instance;
}

public IActionResult Chart(int id)
{
    var listings = _fakeDatabase.Listings;
    var viewModel = _chartJsService.GetChartViewModel(listings, id);
    return View(viewModel);
}
```

9. Open de _Layout pagina en voeg de volgende nav-items toe:
```html
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Chart" asp-route-id="0">Bar Chart</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Chart" asp-route-id="1">Line Chart</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Chart" asp-route-id="2">Pie Chart</a>
</li>
```

10. Maak een nieuwe Razor View aan in de `"Home"` map genaamd `"Chart.cshtml"` en plak daar de volgende code in:
```c#
@model AirBnbChartJSTest.Models.ChartViewModel
@using Newtonsoft.Json
@{
    ViewData["Title"] = "Home Page";
}

<canvas id="myChart" width="800" height="400"></canvas>

<script type="text/javascript">
    var label = @Html.Raw(JsonConvert.SerializeObject(Model.Label));
    var labels = @Html.Raw(JsonConvert.SerializeObject(Model.XLabels));
    var data = @Html.Raw(JsonConvert.SerializeObject(Model.YValues));
    var colors = @Html.Raw(JsonConvert.SerializeObject(Model.HtmlColors));
    var borderColors = @Html.Raw(JsonConvert.SerializeObject(Model.HtmlBorderColors));

    var ctx = document.getElementById('myChart');
    var mixedChart = new Chart(ctx, {
        type: '@Html.Raw(Model.Type)',
        data: {
            datasets: [{
                label: '@Html.Raw(Model.Label)',
                data: data,
                backgroundColor: colors,
                borderColor: borderColors
            }],
            labels: labels
        }
});
</script>  
```

11. Druk op F5, en je bent klaar!