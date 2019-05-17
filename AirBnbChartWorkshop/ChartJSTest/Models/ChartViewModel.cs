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