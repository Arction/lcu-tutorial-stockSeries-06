// ------------------------------------------------------------------------------------------------------
// LightningChart® example code: StockSeries chart.
//
// If you need any assistance, or notice error in this example code, please contact support@arction.com. 
//
// Permission to use this code in your application comes with LightningChart® license. 
//
// http://arction.com/ | support@arction.com | sales@arction.com
//
// © Arction Ltd 2009-2018. All rights reserved.  
// ------------------------------------------------------------------------------------------------------
using System.Windows;
using System.Windows.Media;

// Arction namespaces 
using Arction.Wpf.SemibindableCharting; // LightningChartUltimate and general types

namespace StockSeriesTutorial_WPF_SB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            chart.Title.Text = "Stock Series";

            // 1. Configure X and Y axis

            // X axis configuration
            axisX.Title.Text = "Date";
            axisX.ValueType = AxisValueType.DateTime;
            axisX.LabelsAngle = 90;
            axisX.MajorDiv = 24 * 60 * 60; //Major Div is one day in seconds

            // Y axis configuration
            axisY.Title.Text = "Price";

            // 2. Configure the stock plot
            stockSeries.Style = StockStyle.CandleStick;
            stockSeries.FillBorder.Width = 1;
            stockSeries.Title.Text = "Example Inc.";

            // 3. Load data from a CSV file into series data points.
            /*
             * For StockData, the data has to be organized in columns in the following order:
             * Column 0: Date (DateTime)
             * Column 1: Open (double)
             * Column 2: Close (double)
             * Column 3: High (double)
             * Column 4: Low (double)
             * Column 5: Volume (int)
             * Column 6: Transaction (double)
             * 
             * Series values can be written into a file using series.SaveToCSV,
             * which is a pair function for LoadFromCSV.
             */
            stockSeries.LoadFromCSV("../../../data/data.csv", SeparatorCSV.Semicolon);

            // 4. Create a reference to the loaded data points.
            var stockData = stockSeries.DataPoints;

            // 5. Prepare data for line-series, which matches closed values.
            var closeData = new SeriesPoint[stockData.Length];
            for (var i = 0; i < stockData.Length; i++)
            {
                closeData[i] = new SeriesPoint()
                {
                    X = axisX.DateTimeToAxisValue(stockData[i].Date),
                    Y = stockData[i].Close
                };
            }

            // 6. Set lineSeries to show the dynamic in closed values on Stock Exchange.
            lineSeries.Title.Text = "Example Inc.";
            lineSeries.Points = closeData;

            // Auto-scale X and Y axes.
            chart.ViewXY.ZoomToFit();

            #region Hidden polishing
            CustomizeChart(chart);
            #endregion
        }

        private void CustomizeChart(LightningChartUltimate chart)
        {
            chart.ChartBackground.Color = Color.FromArgb(255, 30, 30, 30);
            chart.ChartBackground.GradientFill = GradientFill.Solid;
            chart.ViewXY.GraphBackground.Color = Color.FromArgb(255, 20, 20, 20);
            chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            chart.Title.Color = Color.FromArgb(255, 249, 202, 3);
            chart.Title.MouseHighlight = MouseOverHighlight.None;

            foreach (var yAxis in chart.ViewXY.YAxes)
            {
                yAxis.Title.Color = Color.FromArgb(255, 249, 202, 3);
                yAxis.Title.MouseHighlight = MouseOverHighlight.None;
                yAxis.MajorGrid.Color = Color.FromArgb(35, 255, 255, 255);
                yAxis.MajorGrid.Pattern = LinePattern.Solid;
                yAxis.MinorDivTickStyle.Visible = false;
            }

            foreach (var xAxis in chart.ViewXY.XAxes)
            {
                xAxis.Title.Color = Color.FromArgb(255, 249, 202, 3);
                xAxis.Title.MouseHighlight = MouseOverHighlight.None;
                xAxis.MajorGrid.Color = Color.FromArgb(35, 255, 255, 255);
                xAxis.MajorGrid.Pattern = LinePattern.Solid;
                xAxis.MinorDivTickStyle.Visible = false;
            }
        }
    }
}