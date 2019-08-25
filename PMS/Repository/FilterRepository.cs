using PMS.Areas.PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Repository
{
    public class FilterRepository: IFilterRepository
    {

     public   List<FilterEntity> GetFilters()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                    return dbContext.tblFilters.Select(x => new FilterEntity() { FilterCode = x.FilterCode, FilterName = x.FilterName,FilterType=x.FilterType }).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return new List<FilterEntity>();

        }
    }
}