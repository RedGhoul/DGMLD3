using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.RDMS
{
    public class Link
    {
        public int Id { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public int GraphId { get; set; }
        public Graph Graph { get; set; }
    }
}
