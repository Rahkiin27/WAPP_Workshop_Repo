# Charts met HighCharts
In deze workshop gaan we een barchart, piechart en linechart toevoegen d.m.v. HighCharts.

1. Clone de workshop repo: [https://github.com/TheLegend27NL/WAPP_Workshop_Repo](https://github.com/TheLegend27NL/WAPP_Workshop_Repo)
2. Open de solution met VS2019
3. Maak een nieuw .NET CORE 3.0 MVC project in dezelfde solution aan genaamd: `"HighCharts"`
4. Installeer de Newtonsoft.Json NuGet package. (Staat als tweede wanneer de "Manage NuGet Packages" pagina op het "Browse" tabblad wordt geopend)

5. Open _Layout.cshtml en plak het volgende boven ```</head>```:
```html
  <script src="https://code.highcharts.com/highcharts.js"></script>
  <script src="https://code.highcharts.com/modules/series-label.js"></script>
  <script src="https://code.highcharts.com/modules/exporting.js"></script>
  <script src="https://code.highcharts.com/modules/export-data.js"></script>
```

6. Maak een map `"ViewModels"` aan en creeër het bestand `"HighChartViewModel.cs"` en plak de volgende code:
```c#
using AirBnbFakeDatabase.Models;
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
                Y = listing.AmountOfListings
            });
        }
    }
}
```

Het idee van deze viewmodel is dat er straks in de views deze data kan worden opgevraagd.

7. Maak een nieuwe `"Model"` in de `"Models"` folder genaamd `"PieChartSlice.cs"` en plak de volgende code:

```c#
namespace AirBnbHighCharts.Models
{
    public class PieChartSlice
    {
        public string Name { get; set; }

        public int Y { get; set; }
    }
}
```
De reden dat specifiek voor een `"PieChart"` een model moet worden aangemaakt, is omdat een PieChart de data in een iets ander format verwacht.


8. Open de `"HomeController"` en voeg de volgende code toe. Druk daarna eventueel op `"ALT+ENTER"` om de usings goed te zetten:
```c#
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirBnbHighCharts.Models;
using AirBnbFakeDatabase.Database;
using AirBnbFakeDatabase.Services;
using AirBnbHighCharts.ViewModels;

namespace AirBnbHighCharts.Controllers
{
    public class HomeController : Controller
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ListingService _listingService;

        public HomeController()
        {
            _fakeDatabase = FakeDatabase.Instance;
            _listingService = new ListingService();
        }

        public IActionResult Index()
        {
            var listings = _fakeDatabase.Listings;
            var amountOfListingsPerNeighbourhood = _listingService.GetBarChartData(listings);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult BarChart()
        {
            var listings = _fakeDatabase.Listings;
            var amountOfListingsPerNeighbourhood = _listingService.GetBarChartData(listings);

            var HighChartsViewModel = new HighChartViewModel
            {
                AmountOfListingsPerNeighbourhood = amountOfListingsPerNeighbourhood
            };

            return View(HighChartsViewModel);
        }

        public IActionResult PieChart()
        {
            var listings = _fakeDatabase.Listings;
            var amountOfListingsPerNeighbourhood = _listingService.GetBarChartData(listings);

            var HighChartsViewModel = new HighChartViewModel
            {
                AmountOfListingsPerNeighbourhood = amountOfListingsPerNeighbourhood
            };

            return View(HighChartsViewModel);
        }

        public IActionResult LineChart()
        {
            var listings = _fakeDatabase.Listings;
            var amountOfListingsPerNeighbourhood = _listingService.GetBarChartData(listings);

            var HighChartsViewModel = new HighChartViewModel
            {
                AmountOfListingsPerNeighbourhood = amountOfListingsPerNeighbourhood
            };

            return View(HighChartsViewModel);
        }
    }
}
```

9. Open de `"_Layout.cshtml"` in de `"View -> shared"` folder en voeg de volgende nav-items toe:
```html
<li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="BarChart">BarChart</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="PieChart">PieChart</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="LineChart">LineChart</a>
                        </li>
```

10. 
## PieChart
Maak een nieuwe `"Razor View"` aan in de `"Home"` map genaamd `"PieChart.cshtml"` en plak daar de volgende code in:
```c#
@model AirBnbHighCharts.ViewModels.HighChartViewModel;
@using Newtonsoft.Json;
@using Newtonsoft.Json.Serialization;

<div id="container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>

<script>
    var data = @Html.Raw(JsonConvert.SerializeObject(@Model.GetPieChartData(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));

    Highcharts.chart('container', {
    chart: {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    },
    title: {
        text: 'Listings per neighbourhood'
    },
    tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                }
            }
        }
    },
    series: [{
        name: 'Neighbourhoods',
        colorByPoint: true,
        data: data,
    }]
});
</script>
```
Hier zie je dus dat de viewmodel wordt aangeroepen met de methodes:

- `"GetPieChartData()"`

*Tip: De volgende code:*
```csharp
@Html.Raw(JsonConvert.SerializeObject(@Model.GetPieChartData(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));
```
*Is om een "`C#`" object om te zetten naar een "`Javascript JSON`" object*

## BarChart
Maak een nieuwe `"Razor View"` aan in de `"Home"` map genaamd `"BarChart.cshtml"` en plak daar de volgende code in:
```c#
@model AirBnbHighCharts.ViewModels.HighChartViewModel;
@using Newtonsoft.Json;
@using Newtonsoft.Json.Serialization;

<div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

<script>
    var neighbourhoods = @Html.Raw(JsonConvert.SerializeObject(@Model.GetNeighbourhoods(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));


    var listings = @Html.Raw(JsonConvert.SerializeObject(@Model.GetAmountOfListings(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));


    Highcharts.chart('container', {
    chart: {
        type: 'column'
    },
    title: {
        text: 'Listings per Neighbourhood'
    },
    subtitle: {
        text: 'Listings from Amsterdam'
    },
    xAxis: {
        categories: neighbourhoods,
        crosshair: true
    },
    yAxis: {
        min: 0,
        title: {
            text: 'Listings'
        }
    },
    tooltip: {
        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
        footerFormat: '</table>',
        shared: true,
        useHTML: true
    },
    plotOptions: {
        column: {
            pointPadding: 0.2,
            borderWidth: 0
        }
    },
    series: [{
        name: 'Neighbourhoods',
        data: listings
    }]
});
</script>
```
Hier zie je dus dat de `"ViewModel"` wordt aangeroepen met de methodes:

- `"GetNeighbourhoods()"`
- `"GetAmountOfListings()"`

*Tip: De volgende code:*
```csharp
@Html.Raw(JsonConvert.SerializeObject(@Model.GetPieChartData(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));
```
*Is om een "`C#`" object om te zetten naar een "`Javascript JSON`" object*

## LineChart
Maak een nieuwe Razor View aan in de `"Home"` map genaamd `"BarChart.cshtml"` en plak daar de volgende code in:
```c#
@model AirBnbHighCharts.ViewModels.HighChartViewModel;
@using Newtonsoft.Json;
@using Newtonsoft.Json.Serialization;

<div id="container"></div>

<script>
    var listings = @Html.Raw(JsonConvert.SerializeObject(@Model.GetAmountOfListings(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));

    var neighbourhoods = @Html.Raw(JsonConvert.SerializeObject(@Model.GetNeighbourhoods(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));

    Highcharts.chart('container', {

    title: {
        text: 'Average Price of beds'
    },

    subtitle: {
        text: 'Data from AirBnB'
    },
        xAxis: {
            categories: neighbourhoods
    },

    yAxis: {
        title: {
            text: 'Price'
        }
    },
    legend: {
        layout: 'vertical',
        align: 'right',
        verticalAlign: 'middle'
    },

    plotOptions: {
        series: {
            label: {
                connectorAllowed: false
            }
        }
    },

        series: [{
            name: 'Beds per neighbourhood',
            data: listings
        }],

    responsive: {
        rules: [{
            condition: {
                maxWidth: 500
            },
            chartOptions: {
                legend: {
                    layout: 'horizontal',
                    align: 'center',
                    verticalAlign: 'bottom'
                }
            }
        }]
    }

});
</script>
```
Hier zie je dus dat de viewmodel wordt aangeroepen met de methodes:

- `"GetNeighbourhoods()"`
- `"GetAmountOfListings()"`

*Tip: De volgende code:*
```csharp
@Html.Raw(JsonConvert.SerializeObject(@Model.GetPieChartData(),
         new JsonSerializerSettings
         {
             ContractResolver = new CamelCasePropertyNamesContractResolver()
         }));
```
*Is om een "`C#`" object om te zetten naar een "`Javascript JSON`" object*

11. Start je applicatie en navigeer naar de pagina via de navbar.
Als het goed is zie je nu een barchart met de gegeven data.