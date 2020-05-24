using DGMLD3.Data.RDMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.VIEW
{
    public class GraphTableViewModel
    {
        public GraphTableViewModel()
        {
            NameSortParm = "name_asc";
            GraphNameSortParm = "g_name_desc";
            DateSortParm = "date_desc";
            IsPublicParm = "ispublic_desc";
            CurrentSort = "";
            CurrentFilter = "";
        }
        public PaginatedList<Graph> PageList { get; set; }
        public string NameSortParm { get; set; }
        public string GraphNameSortParm { get; set; }
        public string DateSortParm { get; set; }
        public string IsPublicParm { get; set; }
        public string SearchString { get; set; }

        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }
    }
}
