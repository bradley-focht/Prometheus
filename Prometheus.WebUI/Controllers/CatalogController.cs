using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult BusinessCatalog()
        {
            return View();
        }

        public ActionResult SupportCatalog()
        {
            return View();
        }
    }
}