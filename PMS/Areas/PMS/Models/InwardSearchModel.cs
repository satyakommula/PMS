using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class InwardSearchModel
    {
        public string ProjectCode { get; set; }
        public string WorkOrderNo { get; set; }
        public string BranchCode { get; set; }
        public string ClientName { get; set; }
    }
}