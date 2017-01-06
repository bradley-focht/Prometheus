using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceRequestController : Controller
    {
        // GET: ServiceRequest
        public ActionResult BeginOptionRequest(int id)
        {
            return View();
        }
    }
}