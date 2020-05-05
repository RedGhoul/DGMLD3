using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.DTO
{
    public class GraphDto
    {
        public ICollection<GraphNodeDTO> Nodes { get; set; }
        public ICollection<GraphLinkDTO> Links { get; set; }
    }
}
