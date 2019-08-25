using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models.Response
{
    public class SearchCorrespondenceModel
    {
        public int Id { get; set; }
        public System.Guid CorrespondenceId { get; set; }
        public string CorrespondenceType { get; set; }
        public string ProjectCode { get; set; }
        public Nullable<int> Department { get; set; }
        public string Subject { get; set; }
        public string LetterNo { get; set; }
        public Nullable<System.DateTimeOffset> LetterDate { get; set; }
        public string FilePath { get; set; }
        public string Download { get; set; }
    }
}