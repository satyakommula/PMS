using PMS.Areas.PMS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
   public interface IProjectService
    {
        List<ProjectSelectionModel> GetProjectsForSelection();
    }
}
