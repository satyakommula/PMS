using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Repository
{
    public class ProjectRepository : IProjectRepository
    {
      public  List<ProjectSelectionModel> GetProjectsForSelection()
        {
            try
            {
                using (var dbContext = new PMSEntities())
                {
                 return   dbContext.tblProjectMasters.Select(x => new ProjectSelectionModel (){ ProjectCode=x.PROJECTCODE, ProjectName=x.PROJECTNAME }).ToList();
                }
                
            }
            catch (Exception ex)
            {
            }
            return null;

        }
    }
}