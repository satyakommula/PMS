using PMS.Areas.PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.PMS.Controllers
{
    public class VendorController : Controller
    {
        // GET: PMS/VendorRegistration
        public ActionResult VendorRegistration()
        {
            List<FilterEntity> listFilters = new List<FilterEntity>();

            listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "Ownership1", FilterType = "TypeOfOwnership" });
            listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "Ownership2", FilterType = "TypeOfOwnership" });


            ViewBag.Filters = listFilters;
            return View(new tblVendorData());
        }

        public ActionResult VendorAllocation()
        {
            List<FilterEntity> listFilters = new List<FilterEntity>();

            listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "InHouse", FilterType = "TypeOfHouse" });
            listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "OutHouse", FilterType = "TypeOfHouse" });


            ViewBag.Filters = listFilters;
            return View(new tblVendorAllocation());
        }
    }
}