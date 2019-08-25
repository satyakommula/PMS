using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models.Response
{
    public class SearchBGsResponseModel
    {
        public int Id { get; set; }
        public System.Guid BGsId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string BGNo { get; set; }
        public string WONo { get; set; }
        public Nullable<System.DateTimeOffset> BGDate { get; set; }
        public Nullable<System.DateTimeOffset> WODate { get; set; }
        public string FilePath { get; set; }
        public string Download { get; set; }
    }
}