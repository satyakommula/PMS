using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
   public interface IUserService
    {
        int Insert(tblUser data);
        int Update(tblUser data);
        tblUser Get(int id);
        tblUser Get(string email);
    }
}
