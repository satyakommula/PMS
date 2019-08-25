using PMS.Areas.PMS.Models;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.PMS.Controllers
{
    public class ProjectMasterController : Controller
    {
        private IProjectService _projectService;

        public ProjectMasterController(IProjectService projectService)
        {
            _projectService = projectService;

        }

        // GET: PMS/ProjectMaster
        public ActionResult Index()
        {

            ProjectModel _ProjectModel = new ProjectModel();
            List<FilterEntity> listFilters = new List<FilterEntity>();

            listFilters.Add(new FilterEntity { FilterCode = "B1", FilterName = "Engineering", FilterType = "" });
            listFilters.Add(new FilterEntity { FilterCode = "B2", FilterName = "IT", FilterType = "" });
            ViewBag.Filters = listFilters;
            return View(_ProjectModel);
        }

        [HttpPost]
        public ActionResult Index(ProjectModel projectModel)
        {
            return null;
        }


        [HttpGet]
        public JsonResult GetProjects()
        {
            return Json(_projectService.GetProjectsForSelection(), JsonRequestBehavior.AllowGet);
        }
    }

}