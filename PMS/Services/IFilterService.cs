using PMS.Areas.PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
  public  interface IFilterService
    {
        List<FilterEntity> GetFilters();

    }
}
