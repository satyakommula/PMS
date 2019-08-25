using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
   public interface IReportsService
    {
        int Insert(tblReport data);
        int Update(tblReport data);
        List<SearchReportsResponseModel> Search(ReportsSearchModel searchData);
        List<tblReport> GetAll();
        tblReport Get(int id);
    }
}
