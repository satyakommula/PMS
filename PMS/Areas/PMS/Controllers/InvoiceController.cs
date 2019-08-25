using PMS.Areas.PMS.Models;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.PMS.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IFilterService _filterService;
        // GET: PMS/Invoice
        public ActionResult InvoiceData()
        {
            List<FilterEntity> listFilters = new List<FilterEntity>();
            listFilters.Add(new FilterEntity { FilterCode = "1", FilterName = "TS1", FilterType = "TypeOfInvoice" });
            listFilters.Add(new FilterEntity { FilterCode = "2", FilterName = "TS2", FilterType = "TypeOfInvoice" });
            ViewBag.Filters = listFilters;
            return View( new tblInvoiceData());
        }

        public ActionResult ConsultantsInvoice()
        {
            return View(new tblInvoiceConsultant());
        }
    }
}