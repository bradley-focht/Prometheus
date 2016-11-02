using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
    public class UserAccountController : Controller
    {
        public ActionResult LoginScreen()
        {
            return View();
        }
    }
}