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
    public class ReportsController : Controller
    {
        private readonly IFilterService _filterService;
        private IProjectService _projectService;
        private IReportsService _reportsService;

        public ReportsController(IFilterService filterService,
            IProjectService projectService,
            IReportsService reportsService)
        {
            _filterService = filterService;
            _projectService = projectService;
            _reportsService = reportsService;
        }

        // GET: PMS/Reports
        public ActionResult ReportsData(int? id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();
            ViewBag.Filters = listFilters;
            tblReport data = null;
            if (id == null)
            {
                data = new tblReport();
            }
            else
            {
                data = _reportsService.Get(id.Value);
            }

            return View(data);
        }

        [ValidateAntiForgeryToken()]
        [HttpPost]
        public JsonResult SaveReport()
        {
            int result = 0;
            tblReport data = JsonConvert.DeserializeObject<tblReport>(Request["data"]);

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string folderPath = ConfigurationManager.AppSettings["ReportsPath"];
                string fileName = "";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (data.IsFileAttached)
                {
                    fileName = Request.Files[0].FileName;
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
                        fileName = "1_" + Request.Files[0].FileName;
                        if (!System.IO.File.Exists(folderPath + fileName))
                        {
                            fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[0].FileName;
                        }
                        Request.Files[0].SaveAs(folderPath + fileName);
                        data.FilePath = folderPath + fileName;
                        data.FileName = fileName;
                    }
                }
                if (data.IsOtherFileAttached)
                {
                    int index = 1;
                    if (!data.IsOtherFileAttached)
                    {
                        index = 0;
                    }
                    fileName = Request.Files[index].FileName;

                    if (!string.IsNullOrEmpty(data.OtherFilePath) && data.IsOtherFileAttached)
                    {
                        System.IO.File.Delete(data.OtherFilePath);
                    }
                    if (!System.IO.File.Exists(folderPath + fileName))
                    {
                        Request.Files[index].SaveAs(folderPath + fileName);
                        data.OtherFilePath = folderPath + fileName;
                        data.OtherFileName = fileName;
                    }
                    else
                    {
                        fileName = "1_" + Request.Files[index].FileName;
                        if (!System.IO.File.Exists(folderPath + fileName))
                        {
                            fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[index].FileName;
                        }
                        Request.Files[index].SaveAs(folderPath + fileName);
                        data.OtherFilePath = folderPath + fileName;
                        data.OtherFileName = fileName;
                    }
                }
            }
            if (data.Id == 0)
            {
                result = _reportsService.Insert(data);
            }
            else
            {
                result = _reportsService.Update(data);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchReports()
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();
            ViewBag.Filters = listFilters;
            return View(new ReportsSearchModel());
        }

        [HttpPost]

        public JsonResult SearchReports(ReportsSearchModel searchModel)
        {
            var result = _reportsService.Search(searchModel);
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