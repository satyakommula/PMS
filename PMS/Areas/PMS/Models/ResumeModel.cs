using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class ResumeModel
    {

        public string ResumeDate { set; get; }
        public string Designation { set; get; }
        public string Department { set; get; }
        public string ReceivedThrough { set; get; }
        public string Name { set; get; }
        public string DOB { set; get; }
        public string Age { set; get; }
        public List<EducationModel> EducationQualification { set; get; }
        public List<EducationModel> AdditionalQualification { set; get; }
        public string Keywords { set; get; }
        public string ExpYears { set; get; }
        public string ExpMonths { set; get; }
        public string PresentCompany { set; get; }
        public string Position { set; get; }
        public string PlaceOfWorking { set; get; }
        public string NoOfCompanies { set; get; }
        public string PresentSalary { set; get; }
        public string ExpectedSalary { set; get; }
        public string FatherName { set; get; }
        public string Address1 { set; get; }
        public string Address2 { set; get; }
        public string Mobile { set; get; }
        public string Mobile1 { set; get; }
        public string Email { set; get; }

        public HttpPostedFileBase CVPath { set; get; }
        public string CVCompletePath { set; get; }
    }
}