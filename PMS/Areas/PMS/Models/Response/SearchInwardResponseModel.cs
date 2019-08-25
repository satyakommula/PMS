using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models.Response
{
    public class SearchInwardResponseModel
    {
        public int Id { get; set; }
        public System.Guid WOId { get; set; }
        public string ProjectCode { get; set; }
        public string BranchCode { get; set; }
        public string WorkOrderNo { get; set; }
        public string ClientName { get; set; }

        public string FilePath { get; set; }
        public string Download { get; set; }
        // public string OtherDownload { get; set; }
    }
}