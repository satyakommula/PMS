using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using PMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;
        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }
        public int Insert(tblReport data)
        {
            return _reportsRepository.Insert(data);
        }

        public int Update(tblReport data)
        {
            return _reportsRepository.Update(data);
        }

        public List<SearchReportsResponseModel> Search(ReportsSearchModel searchData)
        {
            return _reportsRepository.Search(searchData);
        }

        public List<tblReport> GetAll()
        {
            return _reportsRepository.GetAll();
        }

        public tblReport Get(int id)
        {
            return _reportsRepository.Get(id);
        }
    }
}