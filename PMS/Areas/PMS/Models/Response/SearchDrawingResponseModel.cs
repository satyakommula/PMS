using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models.Response
{
    public class SearchDrawingResponseModel
    {
        public int Id { get; set; }
        public System.Guid DrawingId { get; set; }
        public string ProjectCode { get; set; }
        public string Department { get; set; }
        public string DrawingNo { get; set; }
        public string DrawingDate { get; set; }

        public string DrawingTitle { get; set; }
        public string FilePath { get; set; }
        public string Download { get; set; }
        // public string OtherDownload { get; set; }
    }
}