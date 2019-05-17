namespace AirBnbChartJSTest.Models
{
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
}