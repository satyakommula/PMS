using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models.Response
{
    public class SearchReportsResponseModel
    {
        public int Id { get; set; }
        public System.Guid ReportId { get; set; }
        public string ProjectCode { get; set; }
        public string Department { get; set; }
        public string ReportNo { get; set; }
        public string ReportDate { get; set; }

        public string ReportTitle { get; set; }
        public string FilePath { get; set; }
        public string Download { get; set; }
        // public string OtherDownload { get; set; }
    }
}