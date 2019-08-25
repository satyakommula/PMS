using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
   public interface ICorrespondenceService
    {
        int Insert(tblCorrespondence correspondenceData);
        int Update(tblCorrespondence correspondenceData);
        List<SearchCorrespondenceModel> SearchCorrespondence(CorrespondenceSearchModel searchData);
        List<tblCorrespondence> GetAll();
        tblCorrespondence Get(int id);
    }
}
