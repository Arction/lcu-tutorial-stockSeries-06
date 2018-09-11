# Presenting Financial Data with StockSeries

This tutorial explains how to load financial data from a CSV file and display it using *LightningChart StockSeries*. Stock series are used to visualize stock exchange data in candlestick and stock bars formats. The tutorial assumes that you have created a new chart with *LightningChart* on a WinForms or WPF application. If not, please follow our [Simple 2D Chart](https://www.arction.com/tutorials/#/lcu_tutorial_simple2Dchart_01) on creating an application. 

![screenshot](./assets/stockSeries_screenshot.png)
#####  1. Store references to default axes for a quick access and configure X axis to display values as dates.  
```csharp
// store reference to default axisX and configure
var axisX = chart.ViewXY.XAxes[0];
axisX.Title.Text = "Date";
axisX.ValueType = AxisValueType.DateTime;
axisX.LabelsAngle = 90;
axisX.MajorDiv = 24 * 60 * 60; //Major Div is one day in seconds

// store reference to default axisX and configure
var axisY = chart.ViewXY.YAxes[0];
axisY.Title.Text = "Price";
```

##### 2. Create a new StockSeries to hold the stock information and configure it
```csharp
var stockSeries = new StockSeries(
    chart.ViewXY,
    axisX,
    axisY
);

chart.ViewXY.StockSeries.Add(stockSeries);
stockSeries.Style = StockStyle.CandleStick;
stockSeries.FillBorder.Width = 1;
stockSeries.Title.Text = "Example Inc.";
```

##### 3. Load the data.

Load the data from a CSV file into the series data points using `series.LoadFromCSV(string fileName, SeparatorCSV separator)`. The data has to be organized in columns in the following order:
    
|   Date   |   Open   |   Close   |   High   |   Low   |  Value  |  Transaction|
| -------- |:--------:|:---------:|:--------:|:-------:|:-------:|-----------:|
| DateTime |  double  |   double  |  double  | double  |   int   |   double   |

Series values can be written into a file using `series.SaveToCSV`, which is a pair function for LoadFromCSV.

```csharp
stockSeries.LoadFromCSV("../../../data/data.csv", SeparatorCSV.Semicolon);
// Create a reference to the loaded data points.
var stockData = stockSeries.DataPoints;
```

##### 4. Prepare data for line series which matches closed values.
```csharp
var closeData = new SeriesPoint[stockData.Length];
for (var i = 0; i < stockData.Length; i++)
{
    closeData[i] = new SeriesPoint()
    {
        X = axisX.DateTimeToAxisValue(stockData[i].Date),
        Y = stockData[i].Close
    };
}

// Add PointLineSeries to show the dynamic in closed values on Stock Exchange.
var lineSeries = new PointLineSeries();
lineSeries.Title.Text = "Example Inc.";
lineSeries.Points = closeData;
chart.ViewXY.PointLineSeries.Add(lineSeries);
```

##### 5. Call ZoomToFit to scale axes to match data in the series.
```csharp
//Auto-scale X and Y axes.
chart.ViewXY.ZoomToFit();
```
