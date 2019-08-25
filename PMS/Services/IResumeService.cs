using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
   public interface IResumeService
    {
        int Insert(tblResumeData resumeData);
        int Update(tblResumeData resumeData);
        List<SearchResumeResponseModel> SearchResume(ResumeSearchModel searchData);
        List<tblResumeData> GetAll();
        tblResumeData Get(int id);
    }
}
