using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Repository
{
   public interface IBGsRepository
    {
        int Insert(tblBGsData data);
        int Update(tblBGsData data);
        List<SearchBGsResponseModel> Search(BGsSearchModel searchData);
        List<tblBGsData> GetAll();
        tblBGsData Get(int id);
    }
}
