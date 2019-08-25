using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Repository
{
   public interface IVendorRepository
    {
        int Insert(tblVendorData data);
        int Update(tblVendorData data);
    //    List<SearchBGsResponseModel> Search(BGsSearchModel searchData);
        List<tblVendorData> GetAll();
        tblVendorData Get(int id);
    }
}
