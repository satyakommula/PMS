using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models.Response
{
    public class SearchResumeResponseModel
    {
        public int Id { get; set; }
        public System.Guid ResumeId { get; set; }
        public Nullable<System.DateTime> ResumeDate { get; set; }
        public string Name { get; set; }
        public string Keywords { get; set; }
        public Nullable<int> ExpYears { get; set; }
        public Nullable<int> ExpMonths { get; set; }
        public Nullable<decimal> PresentSalary { get; set; }
        public Nullable<decimal> ExpectedSalary { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CVCompletePath { get; set; }
        public string Download { get; set; }
    }
}