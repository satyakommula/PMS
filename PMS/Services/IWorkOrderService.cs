using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
  public  interface IWorkOrderService
    {
        List<ClientDataModel> getCleints();
        int Insert(tblInwardWO inwardData);
        int Update(tblInwardWO inwardData);
        int SaveOutward(tblOutwardWO outWardData);
        int UpdateOutward(tblOutwardWO outWardData);
        List<SearchInwardResponseModel> SearchInward(InwardSearchModel searchData);
        List<tblInwardWO> GetAll();
        tblInwardWO Get(int id);
    }
}
