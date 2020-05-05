using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.RDMS
{
    public class Node
    {
        public int Id { get; set; }
        public string NodeId { get; set; }
        public string group { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public int GraphId { get; set; }
        public Graph Graph { get; set; }
    }
}
