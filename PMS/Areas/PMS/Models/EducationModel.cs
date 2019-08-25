using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.PMS.Models
{
    public class EducationModel
    {
        public string Qualification { set; get; }
        public string University { set; get; }
        public string Year { set; get; }
        public string Course { set; get; }
        public string Specialisation { set; get; }

        private List<FilterEntity> _listQualifications { set; get; }
        public List<FilterEntity> ListQualifications {


            get {
                if(_listQualifications==null)
                {
                    _listQualifications = new List<FilterEntity>();
                    _listQualifications.Add(new FilterEntity { FilterCode="1",FilterName="Engineering"});

                }
                return _listQualifications;
            } }

        public bool isDelete { set; get; }
    }
}