using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using PMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Services
{
    public class BGsService : IBGsService
    {
        private readonly IBGsRepository _bgsRepository;
        public BGsService(IBGsRepository bgsRepository)
        {
            _bgsRepository = bgsRepository;
        }
        public int Insert(tblBGsData data)
        {
            return _bgsRepository.Insert(data);
        }

        public int Update(tblBGsData data)
        {
            return _bgsRepository.Update(data);
        }

        public List<SearchBGsResponseModel> Search(BGsSearchModel searchData)
        {
            return _bgsRepository.Search(searchData);
        }

        public List<tblBGsData> GetAll()
        {
            return _bgsRepository.GetAll();
        }

        public tblBGsData Get(int id)
        {
            return _bgsRepository.Get(id);
        }
    }
}