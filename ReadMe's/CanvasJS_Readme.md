

# Charts met canvasJS
In deze workshop gaan we een barchart, piechart en linechart toevoegen d.m.v. canvasJS.

1. Clone de workshop repo: [https://github.com/TheLegend27NL/WAPP_Workshop_Repo](https://github.com/TheLegend27NL/WAPP_Workshop_Repo)
2. Maak een nieuw .NET CORE 3.0 MVC project in dezelfde solution aan.
3. Voeg een referentie toe naar de AirBnBFakeDatabase class library. Dit doe je door op jouw eigen project rechter muis te doen op dependencies -> Add reference en vervolgens AirBnbFakeDatabse aanvinken
4. Open in project "AirBnbCanvasJs" het bestand `"Views/Shared/_Layout.cshtml"` en plaats de volgende regel tussen de <head></head> tags:
```c#
<script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
```
en voeg de volgende nav-items toe:
```c#
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
5. Maak in de map `"Models"` een nieuw bestand met de naam `"DataPoint.cs"` en plak de volgende code:
```c#
public class DataPoint
    {
        public DataPoint(string label, double y)
        {
            this.label = label;
            this.y = y;
        }

        public string label { get; set; }
        public double y { get; set; }
    }
```
6. Maak een nieuwe map genaamd `"Services"` en voeg hier een nieuwe class toe met de naam `"ChartsService.cs"`. Plak hier vervolgens de volgende code:
```c#
public class ChartsService
{
    private readonly FakeDatabase _fakeDatabase;
    private readonly ListingService _listingService;

    public ChartsService()
    {
        _fakeDatabase = new FakeDatabase();
        _listingService = new ListingService();
    }

    public List<DataPoint> GetListingsPerNeighbourhoodDataPoints()
    {
        var listings = _fakeDatabase.Listings;
        var amountOfListingsPerNeighbourhood = _listingService.GetBarChartData(listings);

        List<DataPoint> dataPoints = new List<DataPoint>();
        foreach (var listing in amountOfListingsPerNeighbourhood)
        {
            DataPoint datapoint = new DataPoint(listing.NeighbourhoodName, listing.AmountOfListings);
            dataPoints.Add(datapoint);
        }

        return dataPoints;
    }
}
```

## Barchart toevoegen
7. Open de Homecontroller en voeg de volgende code toe:
```c#
private readonly ChartsService _chartsService;

public HomeController()
{
    _chartsService = new ChartsService();
}
```

```c#
public IActionResult Barchart()
{
    var dataPoints = _chartsService.GetListingsPerNeighbourhoodDataPoints();
    ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

    return View();
}
```
8. Maak in de map `"Views/Home"` een nieuw bestand met de naam `Barchart.cshtml` en plak de volgende code:
```c#
@{
    ViewData["Title"] = "Barchart";
}

<div id="chartContainer"></div>

<script type="text/javascript">
    window.onload = function () {
		var chart = new CanvasJS.Chart("chartContainer", {
			theme: "light2",
			animationEnabled: true,
			title: {
				text: "Barchart"
			},
			data: [
			{
				type: "column",
				dataPoints: @Html.Raw(ViewBag.DataPoints)
			}
			]
		});
        chart.render();
    };
</script>
```
9.  Run het project en ga via het menu naar de barchart

## Piechart toevoegen
10. Open de Homecontroller en voeg de volgende code toe:
```c#
public IActionResult Piechart()
{
	var dataPoints = _chartsService.GetListingsPerNeighbourhoodDataPoints();
	ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

	return View();
}
```
11. Maak in de map `"Views/Home"` een nieuw bestand met de naam `Piechart.cshtml` en plak de volgende code:
```c#
@{
    ViewData["Title"] = "Piechart";
}

<div id="chartContainer"></div>

<script type="text/javascript">
    window.onload = function () {
		var chart = new CanvasJS.Chart("chartContainer", {
			theme: "light2",
			animationEnabled: true,
			title: {
				text: "Piechart"
			},
			data: [
			{
				type: "pie",
				dataPoints: @Html.Raw(ViewBag.DataPoints)
			}
			]
		});
        chart.render();
    };
</script>
```
12.  Run het project en ga via het menu naar de piechart

## Linechart toevoegen
13. Open de Homecontroller en voeg de volgende code toe:
```c#
public IActionResult Linechart()
{
	var dataPoints = _chartsService.GetListingsPerNeighbourhoodDataPoints();
	ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

	return View();
}
```
14. Maak in de map `"Views/Home"` een nieuw bestand met de naam `Linechart.cshtml` en plak de volgende code:
```c#
@{
    ViewData["Title"] = "Linechart";
}

<div id="chartContainer"></div>

<script type="text/javascript">
    window.onload = function () {
		var chart = new CanvasJS.Chart("chartContainer", {
			theme: "light2",
			animationEnabled: true,
			title: {
				text: "Linechart"
			},
			data: [
			{
				type: "line",
				dataPoints: @Html.Raw(ViewBag.DataPoints)
			}
			]
		});
        chart.render();
    };
</script>
```
15.  Run het project en ga via het menu naar de linechart
