using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGMLD3.Data.VIEW
{
    public class UploadViewModel
    {
        public IFormFile file { get; set; }
        public string DGML_Type_ID { get; set; }
        public IEnumerable<SelectListItem> DGML_Types { get; set; }
        public string ErrorMsg { get; set; }

        public string GraphName { get; set; }
        public UploadViewModel()
        {
            this.DGML_Types = new List<SelectListItem>()
            {
                new SelectListItem("CODE-MAP","CODE"),
                new SelectListItem("DB-MAP","DB")
            };
        }
    }
}
