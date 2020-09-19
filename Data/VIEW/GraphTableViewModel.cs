using DGMLD3.Data.RDMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.VIEW
{
    public class GraphTableViewModel
    {
        public List<Graph> Graphs { get; set; }
        public string SearchString { get; set; }
    }
}
