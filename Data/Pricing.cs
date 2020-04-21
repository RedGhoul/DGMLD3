using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data
{
    public class Pricing
    {
        public int id { get; set; }
        public string planName { get; set; }
        public double price { get; set; }
        public bool active { get; set; }
    }
}
