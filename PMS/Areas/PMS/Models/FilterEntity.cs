using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class FilterEntity
    {
        public string FilterCode { set; get; }
        public string FilterName { set; get; }
        public string FilterType { set; get; }
        public bool FilterEnable { set; get; }
    }
}