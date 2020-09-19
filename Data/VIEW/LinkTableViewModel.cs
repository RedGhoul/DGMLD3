using DGMLD3.Data.RDMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.VIEW
{
    public class LinkTableViewModel
    {
        public string GraphName { get; set; }
        public string GraphLink { get; set; }
        public List<Link> Links { get; set; }
        public string SearchString { get; set; } = "";
    }
}
