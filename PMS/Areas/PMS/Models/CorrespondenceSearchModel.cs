using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class CorrespondenceSearchModel
    {
        public string ProjectCode { set; get; }
        public int? Department { set; get; }
        public string LetterNo { set; get; }
        public string Keywords { set; get; }
        public int CorrespondenceType { get; set; }
    }
}