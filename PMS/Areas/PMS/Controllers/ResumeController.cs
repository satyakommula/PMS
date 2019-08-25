using PMS.Areas.PMS.Models;
using PMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Services;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using PMS.Adapter;
using PMS.Utilities;

namespace PMS.Areas.PMS.Controllers
{
    public class ResumeController : Controller
    {

        private readonly IResumeService _resumeService;
        private readonly IFilterService _filterService;

        public ResumeController(IResumeService resumeService, IFilterService filterService)
        {
            _resumeService = resumeService;
            _filterService = filterService;
        }
        // GET: PMS/Resume
        public ActionResult Index(int? id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Des1", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Des2", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Dep1", FilterType = "Department" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Dep2", FilterType = "Department" });
            ViewBag.Filters = listFilters;
            tblResumeData resumeData = null;
            if (id == null)
            {
                resumeData = new tblResumeData();
            }
            else
            {
                resumeData = _resumeService.Get(id.Value);

            //    string mimeType = MimeTypeMap.GetMimeType(Path.GetExtension(resumeData.CVPath).ToLower());
            //    byte[] bytes = System.IO.File.ReadAllBytes(resumeData.CVCompletePath);
            //    resumeData.File = (HttpPostedFileBase)new
            //HttpPostedFileBaseCustom(new MemoryStream(bytes), mimeType, resumeData.CVPath);

            }


            return View(resumeData);
        }

        [HttpGet]
        public PartialViewResult Qualifications()
        {
            return PartialView("_Qualification", new tblEducation() { isDelete=true});
        }

        [HttpGet]
        public PartialViewResult AdditionalQualifications()
        {
            return PartialView("_AdditionalQualifications", new tblEducation() { isDelete = true });
        }

        [HttpPost]

        public JsonResult SaveResume()
        {
            int result = 0;
            tblResumeData resumeData = JsonConvert.DeserializeObject<tblResumeData>(Request["resumeData"]);

            if (Request.Files.Count > 0)
            {
                string folderPath = ConfigurationManager.AppSettings["ResumesPath"];
                string fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_" + Request.Files[0].FileName;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                if (!string.IsNullOrEmpty(resumeData.CVCompletePath))
                {
                    System.IO.File.Delete(resumeData.CVCompletePath);
                }
                if (!System.IO.File.Exists(folderPath + fileName))
                {
                    Request.Files[0].SaveAs(folderPath + fileName);
                    resumeData.CVCompletePath = folderPath + fileName;
                    resumeData.CVPath = fileName;
                }
                else
                {
                    fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[0].FileName;
                    Request.Files[0].SaveAs(folderPath + fileName);
                    resumeData.CVCompletePath = folderPath + fileName;
                    resumeData.CVPath =  fileName;
                }
            }
                if (resumeData.Id == 0)
                {
                    result = _resumeService.Insert(resumeData);
                }
                else
                {
                    result = _resumeService.Update(resumeData);
                }
            
           
            return Json(result, JsonRequestBehavior.AllowGet); 
        }


        public ActionResult SearchResume()
        {
            //List<FilterEntity> listFilters = new List<FilterEntity>();
            List<FilterEntity> listFilters = _filterService.GetFilters();

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Des1", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Des2", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Dep1", FilterType = "Department" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Dep2", FilterType = "Department" });

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Engineering", FilterType = "Qualification"  });


            ViewBag.Filters = listFilters;


            return View(new ResumeSearchModel());
        }
        [HttpPost]

        public JsonResult SearchResume(ResumeSearchModel resumeSearchModel)
        {
            var result = _resumeService.SearchResume(resumeSearchModel);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string fileName = Path.GetFileName(file);
           string mimeType= MimeTypeMap.GetMimeType(Path.GetExtension(file).ToLower());
            return File(file, mimeType, fileName);
        }

        public  ActionResult Edit(int id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Des1", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Des2", FilterType = "Designation" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Dep1", FilterType = "Department" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Dep2", FilterType = "Department" });
            ViewBag.Filters = listFilters;
            var resumeData = _resumeService.Get(id);
            return View("Index.cshtml",resumeData);
          
        }
    }
}