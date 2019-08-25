using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class ReportsSearchModel
    {
        public string ProjectCode { get; set; }
        public string ReportNo { get; set; }
        public int Department { get; set; }
        public string ReportTitle { get; set; }
    }
}