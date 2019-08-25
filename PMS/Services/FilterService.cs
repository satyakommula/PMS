using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMS.Areas.PMS.Models;
using PMS.Repository;

namespace PMS.Services
{
    public class FilterService : IFilterService
    {
        private IFilterRepository _filterRepository;
        public FilterService(IFilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }
        public List<FilterEntity> GetFilters()
        {
            List<FilterEntity> filters = null;
            if (HttpContext.Current == null ||
    HttpContext.Current.Session == null ||
    HttpContext.Current.Session["filters"] == null)
            {
                filters = _filterRepository.GetFilters();
                HttpContext.Current.Session["filters"] = filters;
            }
            else
            {
                filters = (List<FilterEntity>)HttpContext.Current.Session["filters"];
            }
            return filters;
        }
    }
}