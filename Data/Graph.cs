using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data
{
    public class Graph
    {
        public int Id { get; set; }
        public ICollection<Node> Nodes { get; set; }
        public ICollection<Link> Links { get; set; }
        public string Name { get; set; }
    }
}
