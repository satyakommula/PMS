using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class VendorModel
    {
        public string NameOfVendor { set; get; }
        public string Address { set; get; }
        public string City { set; get; }
        public string Country { set; get; }
        public string TelephoneNo { set; get; }
        public string Website { set; get; }
        public string TypeOfOwnership { set; get; }
        public string state { set; get; }
        public string PinNo { set; get; }
        public string FaxNo { set; get; }
        public string EmailId { set; get; }

        public string AuthorisedRepresentative { set; get; }
        public string Designation1 { set; get; }
        public string Designation2 { set; get; }
        public string MobileNo1 { set; get; }
        public string MobileNo2 { set; get; }
        public string EmailId1 { set; get; }
        public string EmailId2 { set; get; }
        public string AlternateContactPerson { set; get; }

        public string ServicesOfVendor { set; get; }

        public string VendorNameasperBankRecords { set; get; }
        public string BankName { set; get; }
        public string Branch { set; get; }
        public string IFSCCode { set; get; }
        public string AccountNo { set; get; }

        public string PanNo { set; get; }
        public string MSMENo { set; get; }
        public string GSTINNo { set; get; }
        public string CINNo { set; get; }
        [NotMapped]
        public bool PanCard { set; get; }
        [NotMapped]
        public bool CertificateofIncorporation { set; get; }
        [NotMapped]
        public bool CompanyProfile { set; get; }
        [NotMapped]
        public bool GSTCertificate { set; get; }
        [NotMapped]
        public bool Cancelledheque { set; get; }
        [NotMapped]
        public bool MSME { set; get; }
        [NotMapped]
        public HttpPostedFileBase AttachPanCard { set; get; }
        [NotMapped]
        public HttpPostedFileBase AttachCertificateofIncorporation { set; get; }
        [NotMapped]
        public HttpPostedFileBase AttachCompanyProfile { set; get; }
        [NotMapped]
        public HttpPostedFileBase AttachGSTCertificate { set; get; }
        [NotMapped]
        public HttpPostedFileBase AttachCancelledheque { set; get; }
        [NotMapped]
        public HttpPostedFileBase AttachMSME { set; get; }
        [NotMapped]
        public HttpPostedFileBase FileAttach { set; get; }
    }
}