//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class tblOutwardWO
    {
        public int Id { get; set; }
        public System.Guid WOId { get; set; }
        public string BranchCode { get; set; }
        public string BranchLocation { get; set; }
        public Nullable<int> Division { get; set; }
        public Nullable<int> ProjectType { get; set; }
        public Nullable<int> TypeofService { get; set; }
        public string RelatedProjectCode { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.Guid> VendorId { get; set; }
        public string VendorCode { get; set; }
        public string WorkOrderNo { get; set; }
        public Nullable<System.DateTime> WorkOrderDate { get; set; }
        public string DescriptionofWO { get; set; }
        public string WorkOrderValue { get; set; }
        public string WOFilePath { get; set; }
        public string WOFileName { get; set; }
        public string VendorName1 { get; set; }
        public string VendorName2 { get; set; }
        public string VendorName3 { get; set; }
        public string VendorFilePath1 { get; set; }
        public string VendorFilePath2 { get; set; }
        public string VendorFilePath3 { get; set; }
        public string VendorFileName1 { get; set; }
        public string VendorFileName2 { get; set; }
        public string VendorFileName3 { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<System.DateTimeOffset> CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTimeOffset> UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        [NotMapped]
        public bool IsWorkOrderFileAttached { get; set; }
        [NotMapped]
        public bool IsVendor1FileAttached { get; set; }
        [NotMapped]
        public bool IsVendor2FileAttached { get; set; }
        [NotMapped]
        public bool IsVendor3FileAttached { get; set; }
        [NotMapped]
        public HttpPostedFileBase WorkOrderFile { set; get; }
        [NotMapped]
        public HttpPostedFileBase Vendor1File { set; get; }
        [NotMapped]
        public HttpPostedFileBase Vendor2File { set; get; }
        [NotMapped]
        public HttpPostedFileBase Vendor3File { set; get; }
    }
}