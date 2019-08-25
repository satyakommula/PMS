using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMS.Areas.PMS.Models.Response;
using PMS.Repository;

namespace PMS.Services
{
    public class ProjectService : IProjectService
    {

        private IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public List<ProjectSelectionModel> GetProjectsForSelection()
        {
            return _projectRepository.GetProjectsForSelection();
        }
    }
}