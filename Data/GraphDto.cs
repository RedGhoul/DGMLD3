using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data
{
    public class GraphDto
    {
        public ICollection<GraphNode> Nodes { get; set; }
        public ICollection<GraphLink> Links { get; set; }
    }
}
