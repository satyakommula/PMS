using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class ProjectModel
    {
       
        public string BranchCode { set; get; }
        public string BranchLocation { set; get; }
        public string Division { set; get; }
        public string ProjectType { set; get; }
        public string TypeOfService { set; get; }
        public string ProjectName { set; get; }
        public string WorkOrderNo { set; get; }
        public string WorkOrderDate { set; get; }
        public string WorkOrderValue { set; get; }
        public HttpPostedFileBase WorkOrderPath { set; get; }

        public string ClientName { set; get; }
        public string ClientAddress1 { set; get; }
        public string ClientAddress2 { set; get; }
        public string ContactPerson1 { set; get; }
        public string ContactPerson2 { set; get; }
        public string ContactNumber1 { set; get; }
        public string ContactNumber2 { set; get; }
        public string EmailId1 { set; get; }
        public string EmailId2 { set; get; }


        public string EmployerName { set; get; }
        public string EmployerAddress1 { set; get; }
        public string EmployerAddress2 { set; get; }
        public string AuthorityEngineer { set; get; }
        public string AuthorityAddress1 { set; get; }
        public string AuthorityAddress2 { set; get; }


        public string ScopeofService { set; get; }
        public string Carriageway { set; get; }
        public string EnterLaneWidth { set; get; }
        public string EnterLaneWidth1 { set; get; }

        public string ServiceRoadlength { set; get; }
        public string PavementType { set; get; }
        public string ModeofImplementation { set; get; }


    }
}