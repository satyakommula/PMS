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
    public class DrawingController : Controller
    {
        private readonly IFilterService _filterService;
        private readonly IDrawingService _drawingService;
        private IProjectService _projectService;
        public DrawingController(IFilterService filterService, 
            IDrawingService drawingService,
            IProjectService projectService)
        {
            _filterService = filterService;
            _drawingService = drawingService;
            _projectService = projectService;
        }
        // GET: PMS/DrawingEntry
        public ActionResult DrawingEntry(int? id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();
            ViewBag.Filters = listFilters;
            tblDrawing drawingData = null;
            if (id == null)
            {
                drawingData = new tblDrawing();
            }
            else
            {
                drawingData = _drawingService.Get(id.Value);
            }

            return View(drawingData);
        }

        [HttpPost]
        public JsonResult SaveDrawing()
        {
            int result = 0;
            tblDrawing data = JsonConvert.DeserializeObject<tblDrawing>(Request["data"]);

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string folderPath = ConfigurationManager.AppSettings["DrawingsPath"];
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
                if(data.IsOtherFileAttached)
                {
                    int index = 1;
                    if(!data.IsOtherFileAttached)
                    {
                        index = 0;
                    }
                    fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_" + Request.Files[index].FileName;

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
                        fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[index].FileName;
                        Request.Files[index].SaveAs(folderPath + fileName);
                        data.OtherFilePath = folderPath + fileName;
                        data.OtherFileName = fileName;
                    }
                }
            }
            if (data.Id == 0)
            {
                result = _drawingService.Insert(data);
            }
            else
            {
                result = _drawingService.Update(data);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchDrawing()
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();
            ViewBag.Filters = listFilters;
            return View(new DrawingSearchModel());
        }

        [HttpPost]

        public JsonResult SearchDrawing(DrawingSearchModel searchModel)
        {
            var result = _drawingService.Search(searchModel);
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