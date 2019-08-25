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
    public class CorrespondenceController : Controller
    {
        private readonly IFilterService _filterService;
        private readonly ICorrespondenceService _correspondenceService;

        public CorrespondenceController(IFilterService filterService, ICorrespondenceService correspondenceService)
        {
            _filterService = filterService;
            _correspondenceService = correspondenceService;
        }

        // GET: PMS/Correspondence
        public ActionResult CorrespondenceData(int? id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();

            tblCorrespondence correspondanceData = null;
            if(id!=null)
            {
                correspondanceData = _correspondenceService.Get(id.Value);
            }
            else
            {
                correspondanceData = new tblCorrespondence(); 
            }

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Projectcode1", FilterType = "ProjectCode" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Projectcode2", FilterType = "ProjectCode" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Dep1", FilterType = "Department" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Dep2", FilterType = "Department" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Type1", FilterType = "Type" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Type2", FilterType = "Type" });
            ViewBag.Filters = listFilters;
            return View(correspondanceData);
        }

        [HttpPost]

        public JsonResult SaveCorrespondence()
        {
            int result = 0;
            tblCorrespondence correspondenceData = JsonConvert.DeserializeObject<tblCorrespondence>(Request["correspondenceData"]);

            if (Request.Files.Count > 0)
            {
                string folderPath = ConfigurationManager.AppSettings["CorrespondencePath"];
                string fileName = Request.Files[0].FileName;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //if (!string.IsNullOrEmpty(correspondenceData.CVCompletePath))
                //{
                //    System.IO.File.Delete(correspondenceData.CVCompletePath);
                //}
                if (!System.IO.File.Exists(folderPath + fileName))
                {
                    Request.Files[0].SaveAs(folderPath + fileName);
                    correspondenceData.FilePath = folderPath + fileName;
                    correspondenceData.FileName = fileName;
                }
                else
                {
                    fileName ="1_" + Request.Files[0].FileName;
                    if (!System.IO.File.Exists(folderPath + fileName))
                    {
                        fileName = DateTime.Now.ToString("ddMMyyy") + "_" + Request.Files[0].FileName;

                    }
                    Request.Files[0].SaveAs(folderPath + fileName);
                     correspondenceData.FilePath = folderPath + fileName;
                    correspondenceData.FileName = fileName;
                }
            }
            if (correspondenceData.Id == 0)
            {
                result = _correspondenceService.Insert(correspondenceData);
            }
            else
            {
                result = _correspondenceService.Update(correspondenceData);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchCorrespondence()
        {
            //List<FilterEntity> listFilters = new List<FilterEntity>();
            List<FilterEntity> listFilters = _filterService.GetFilters();

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Des1", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Des2", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Dep1", FilterType = "Department" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Dep2", FilterType = "Department" });
            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Engineering", FilterType = "Qualification" });

            ViewBag.Filters = listFilters;

            return View(new CorrespondenceSearchModel());
        }
        [HttpPost]

        public JsonResult SearchCorrespondence(CorrespondenceSearchModel correspondenceSearchModel)
        {
            var result = _correspondenceService.SearchCorrespondence(correspondenceSearchModel);
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