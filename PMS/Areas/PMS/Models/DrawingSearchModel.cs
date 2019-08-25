using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class DrawingSearchModel
    {
        public string ProjectCode { get; set; }
        public string DrawingNo { get; set; }
        public int Department { get; set; }
        public string DrawingTitle { get; set; }
    }
}