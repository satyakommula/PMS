using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models.Response
{
    public class ClientDataModel
    {
        public string ClientName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
}