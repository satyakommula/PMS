using PMS.Areas.PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Repository
{
   public interface IFilterRepository
    {
        List<FilterEntity> GetFilters();
    }
}
