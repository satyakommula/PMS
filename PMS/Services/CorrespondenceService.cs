using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using PMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Services
{
    public class CorrespondenceService : ICorrespondenceService
    {
        private readonly ICorrespondenceRepository _correspondenceRepository;
        public CorrespondenceService(ICorrespondenceRepository correspondenceRepository)
        {
            _correspondenceRepository = correspondenceRepository;
        }
        public int Insert(tblCorrespondence correspondenceData)
        {
            return _correspondenceRepository.Insert(correspondenceData);
        }

        public int Update(tblCorrespondence correspondenceData)
        {
            return _correspondenceRepository.Update(correspondenceData);
        }

        public List<SearchCorrespondenceModel> SearchCorrespondence(CorrespondenceSearchModel searchData)
        {
            return _correspondenceRepository.SearchCorrespondence(searchData);
        }

        public List<tblCorrespondence> GetAll()
        {
            return _correspondenceRepository.GetAll();
        }

        public tblCorrespondence Get(int id)
        {
            return _correspondenceRepository.Get(id);
        }
    }
}