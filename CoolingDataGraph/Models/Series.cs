using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingDataGraph.Models
{
    public class Series
    {
        public Series()
        {
            Data = new List<double?>();
        }
        
        public string Name { get; set; }
        public List<double?> Data { get; set; }
        public string Color { get; set; }
        public int? PointStart { get; set; }
        public bool Annomaly { get; set; }
    }
}
