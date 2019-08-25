using PMS.Areas.PMS.Models;
using PMS.Areas.PMS.Models.Response;
using PMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        public ResumeService(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }
        public int Insert(tblResumeData resumeData)
        {

            


            return _resumeRepository.Insert(resumeData);
        }

        public int Update(tblResumeData resumeData)
        {




            return _resumeRepository.Update(resumeData);
        }

        public List<SearchResumeResponseModel> SearchResume(ResumeSearchModel searchData)
        {
            return _resumeRepository.SearchResume(searchData);
        }

        public List<tblResumeData> GetAll()
        {
            return _resumeRepository.GetAll();
        }

        public tblResumeData Get(int id)
        {
            return _resumeRepository.Get(id);
        }
    }
}