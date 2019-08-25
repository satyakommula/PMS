using Newtonsoft.Json;
using PMS.Adapter;
using PMS.Areas.PMS.Models;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.PMS.Controllers
{
    public class BGsController : Controller
    {
        private readonly IFilterService _filterService;
        private readonly IBGsService _bgsService;
        private IProjectService _projectService;
        public BGsController(IFilterService filterService,
            IBGsService bgsService,
            IProjectService projectService)
        {
            _filterService = filterService;
            _bgsService = bgsService;
            _projectService = projectService;
        }
        // GET: PMS/BGs
        public ActionResult BGsData(int? id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();
            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "pc1", FilterType = "ProjectCode" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "pc2", FilterType = "ProjectCode" });
            listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "t1", FilterType = "TypeofBG" });
            listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "t2", FilterType = "TypeofBG" });
            ViewBag.Filters = listFilters;
            tblBGsData bgsData = null;
            if (id == null)
            {
                bgsData = new tblBGsData();
            }
            else
            {
                bgsData = _bgsService.Get(id.Value);
            }
            return View(bgsData);
        }

        [HttpPost]
        public JsonResult SaveBGs()
        {
            int result = 0;
            tblBGsData data = JsonConvert.DeserializeObject<tblBGsData>(Request["data"]);

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string folderPath = ConfigurationManager.AppSettings["BGsPath"];
                string fileName = "";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (data.IsFileAttached)
                {
                    fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_" + Request.Files[0].FileName;
                    if (!string.IsNullOrEmpty(data.FilePath) && data.IsFileAttached)
                    {
                        System.IO.File.Delete(data.FilePath);
                    }
                    if (!System.IO.File.Exists(folderPath + fileName))
                    {
                        Request.Files[0].SaveAs(folderPath + fileName);
                        data.FilePath = folderPath + fileName;
                        data.FileName = fileName;
                    }
                    else
                    {
                        fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[0].FileName;
                        Request.Files[0].SaveAs(folderPath + fileName);
                        data.FilePath = folderPath + fileName;
                        data.FileName = fileName;
                    }
                }
            }
            if (data.Id == 0)
            {
                result = _bgsService.Insert(data);
            }
            else
            {
                result = _bgsService.Update(data);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchBGs()
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();
            ViewBag.Filters = listFilters;
            return View(new BGsSearchModel());
        }

        [HttpPost]

        public JsonResult SearchBGs(BGsSearchModel searchModel)
        {
            var result = _bgsService.Search(searchModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string fileName = Path.GetFileName(file);
            string mimeType = MimeTypeMap.GetMimeType(Path.GetExtension(file).ToLower());
            return File(file, mimeType, fileName);
        }
    }
}