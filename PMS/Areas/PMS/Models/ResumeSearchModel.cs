using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class ResumeSearchModel
    {
        public string Name { set; get; }
        public int? Years { set; get; }
        public string Skills { set; get; }
        public int? Qualification { set; get; }
        public int? Designation { set; get; }
        public int? Department { set; get; }

    }
}