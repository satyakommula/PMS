using Newtonsoft.Json;
using PMS.Adapter;
using PMS.Areas.PMS.Models;
using PMS.Repository;
using PMS.Services;
using PMS.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.PMS.Controllers
{
    public class WorkOrderController : Controller
    {

        private IProjectService _projectService;
        private readonly IFilterService _filterService;
        private readonly IWorkOrderService _workOrderService;
        public WorkOrderController(IProjectService projectService, IFilterService filterService,
            IWorkOrderService workOrderService)
        {

            _projectService = projectService;
            _filterService = filterService;
            _workOrderService = workOrderService;
        }
        // GET: PMS/WorkOrder
        public ActionResult InwardWO(int? id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Div1", FilterType = "Division" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Div2", FilterType = "Division" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Project Type 1", FilterType = "ProjectType" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Project Type 2", FilterType = "ProjectType" });
            //listFilters.Add(new FilterEntity { FilterCode = "111", FilterName = "TOS1", FilterType = "TypeofService" });
            //listFilters.Add(new FilterEntity { FilterCode = "222", FilterName = "TOS2", FilterType = "TypeofService" });
            tblInwardWO inwardWO = null;
            if (id != null)
            {
                inwardWO = _workOrderService.Get(id.Value);
            }
            else
            {
                inwardWO = new tblInwardWO();
            }
            ViewBag.Filters = listFilters;

            return View(new tblInwardWO());
        }
        public ActionResult OutwardWO(int? id)
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();

            //listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Div1", FilterType = "TypeOfOwnership" });
            //listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Div2", FilterType = "TypeOfOwnership" });
            //listFilters.Add(new FilterEntity { FilterCode = "11", FilterName = "Project Type 1", FilterType = "ProjectType" });
            //listFilters.Add(new FilterEntity { FilterCode = "22", FilterName = "Project Type 2", FilterType = "ProjectType" });
            //listFilters.Add(new FilterEntity { FilterCode = "111", FilterName = "TOS1", FilterType = "TypeOfOwnership" });
            //listFilters.Add(new FilterEntity { FilterCode = "222", FilterName = "TOS2", FilterType = "TypeOfOwnership" });


            ViewBag.Filters = listFilters;
            return View(new tblOutwardWO());
        }



        [HttpGet]
        public JsonResult GetClients()
        {
            return Json(_workOrderService.getCleints(), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public JsonResult SaveIWWorkOrder()
        {
            int result = 0;
            tblInwardWO inwardData = JsonConvert.DeserializeObject<tblInwardWO>(Request["inwardData"]);

            if (Request.Files.Count > 0)
            {
                string folderPath = ConfigurationManager.AppSettings["InwardsPath"];
                string fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_" + Request.Files[0].FileName;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                if (!string.IsNullOrEmpty(inwardData.WOPath))
                {
                    System.IO.File.Delete(inwardData.WOPath);
                }
                if (!System.IO.File.Exists(folderPath + fileName))
                {
                    Request.Files[0].SaveAs(folderPath + fileName);
                    inwardData.WOPath = folderPath + fileName;
                }
                else
                {
                    fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[0].FileName;
                    Request.Files[0].SaveAs(folderPath + fileName);
                    inwardData.WOPath = folderPath + fileName;
                }
            }
            if (inwardData.Id == 0)
            {
                result = _workOrderService.Insert(inwardData);
            }
            else
            {
                result = _workOrderService.Update(inwardData);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveOutwardData()
        {
            int result = 0;

            try
            {
                tblOutwardWO data = JsonConvert.DeserializeObject<tblOutwardWO>(Request["outwardData"]);
                if (Request.Files.Count > 0)
                {
                    string folderPath = ConfigurationManager.AppSettings["OutwardsPath"];
                    string selectedFileName = Request.Files[0].FileName.Split(';')[0];
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string fileName = "";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    if (data.IsWorkOrderFileAttached)
                    {
                        fileName = Request.Files[0].FileName;
                        if (!string.IsNullOrEmpty(data.WOFilePath) && data.IsWorkOrderFileAttached)
                        {
                            System.IO.File.Delete(data.WOFilePath);
                        }
                        if (!System.IO.File.Exists(folderPath + fileName))
                        {
                            Request.Files[0].SaveAs(folderPath + fileName);
                            data.WOFilePath = folderPath + fileName;
                            data.WOFileName = fileName;
                        }
                        else
                        {
                            fileName = "1_" + Request.Files[0].FileName;
                            if (!System.IO.File.Exists(folderPath + fileName))
                            {
                                fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[0].FileName;
                            }
                            Request.Files[0].SaveAs(folderPath + fileName);
                            data.WOFilePath = folderPath + fileName;
                            data.WOFileName = fileName;
                        }
                    }
                    if (data.IsVendor1FileAttached)
                    {
                        int index = 1;
                        if (!data.IsWorkOrderFileAttached)
                        {
                            index = 0;
                        }
                        fileName = Request.Files[index].FileName;

                        if (!string.IsNullOrEmpty(data.VendorFilePath1) && data.IsVendor1FileAttached)
                        {
                            System.IO.File.Delete(data.VendorFilePath1);
                        }
                        if (!System.IO.File.Exists(folderPath + fileName))
                        {
                            Request.Files[index].SaveAs(folderPath + fileName);
                            data.VendorFilePath1 = folderPath + fileName;
                            data.VendorFileName1 = fileName;
                        }
                        else
                        {
                            fileName = "1_" + Request.Files[index].FileName;
                            if (!System.IO.File.Exists(folderPath + fileName))
                            {
                                fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_1_" + Request.Files[index].FileName;
                            }
                            Request.Files[index].SaveAs(folderPath + fileName);
                            data.VendorFilePath1 = folderPath + fileName;
                            data.VendorFileName1 = fileName;
                        }
                    }

                    if (data.IsVendor2FileAttached)
                    {
                        int index = 2;
                        if (!data.IsVendor1FileAttached)
                        {
                            index = index - 1;
                        }
                        if (!data.IsWorkOrderFileAttached)
                        {
                            index = index - 1;
                        }
                        fileName = Request.Files[index].FileName;

                        if (!string.IsNullOrEmpty(data.VendorFilePath2) && data.IsVendor2FileAttached)
                        {
                            System.IO.File.Delete(data.VendorFilePath2);
                        }
                        if (!System.IO.File.Exists(folderPath + fileName))
                        {
                            Request.Files[index].SaveAs(folderPath + fileName);
                            data.VendorFilePath2 = folderPath + fileName;
                            data.VendorFileName2 = fileName;
                        }
                        else
                        {
                            fileName = "1_" + Request.Files[index].FileName;
                            if (!System.IO.File.Exists(folderPath + fileName))
                            {
                                fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_2_" + Request.Files[index].FileName;
                            }
                            Request.Files[index].SaveAs(folderPath + fileName);
                            data.VendorFilePath2 = folderPath + fileName;
                            data.VendorFileName2 = fileName;
                        }
                    }

                    if (data.IsVendor3FileAttached)
                    {
                        int index = 3;
                        if (!data.IsVendor2FileAttached)
                        {
                            index = index - 1;
                        }
                        if (!data.IsVendor1FileAttached)
                        {
                            index = index - 1;
                        }
                        if (!data.IsWorkOrderFileAttached)
                        {
                            index = index - 1;
                        }
                        fileName = Request.Files[index].FileName;

                        if (!string.IsNullOrEmpty(data.VendorFilePath3) && data.IsVendor3FileAttached)
                        {
                            System.IO.File.Delete(data.VendorFilePath3);
                        }
                        if (!System.IO.File.Exists(folderPath + fileName))
                        {
                            Request.Files[index].SaveAs(folderPath + fileName);
                            data.VendorFilePath3 = folderPath + fileName;
                            data.VendorFileName3 = fileName;
                        }
                        else
                        {
                            fileName = "1_" + Request.Files[index].FileName;
                            if (!System.IO.File.Exists(folderPath + fileName))
                            {
                                fileName = DateTime.Now.ToString("ddMMyyyHHmmsss") + "_3_" + Request.Files[index].FileName;
                            }
                            Request.Files[index].SaveAs(folderPath + fileName);
                            data.VendorFilePath3 = folderPath + fileName;
                            data.VendorFileName3 = fileName;
                        }
                    }
                }
                if (data.Id == 0)
                {
                    result = _workOrderService.SaveOutward(data);
                }
                else
                {
                    result = _workOrderService.UpdateOutward(data);
                }
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchInward()
        {
            List<FilterEntity> listFilters = _filterService.GetFilters();
            ViewBag.Filters = listFilters;
            return View(new InwardSearchModel());
        }

        [HttpPost]

        public JsonResult SearchInward(InwardSearchModel searchModel)
        {
            var result = _workOrderService.SearchInward(searchModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            //string fileName = Path.GetFileName(file);
            //string mimeType = MimeTypeMap.GetMimeType(Path.GetExtension(file).ToLower());
            //return File(file, mimeType, fileName);

            return DownloadAll(file);
        }

        [HttpGet]
        public virtual ActionResult DownloadAll(string file)
        {
            List<ExcelRowData> listData = new List<ExcelRowData>();
            List<string> rowdata = new List<string>();
            rowdata.Add("Data1");
            rowdata.Add("Data2");
            rowdata.Add("Data3");
            rowdata.Add("Data4");
            rowdata.Add("Data5");
            listData.Add(new ExcelRowData() {Fields= rowdata });
            List<string> headerList = new List<string>();
            headerList.Add("Header1");
            headerList.Add("Header2");
            headerList.Add("Header3");
            headerList.Add("Header4");
            headerList.Add("Header5");
            string downloadName = "Download.zip";
            byte[] result= CommonFunctions.Instance.WriteToFile(file, new int[]{4},null,null,null, listData, 5, headerList,out downloadName);

            return File(result, "application / zip", downloadName);
           // return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Test.xlsx");
        }
    }
}