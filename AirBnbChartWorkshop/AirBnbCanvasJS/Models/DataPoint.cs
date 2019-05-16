using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnbCanvasJS.Models
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
