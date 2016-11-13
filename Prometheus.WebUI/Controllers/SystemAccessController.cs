using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
    public class SystemAccessController : Controller
    {
        // GET: SystemAccess
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionsAndRoles()
        {
            return View();
        }

        public ActionResult UserAccess()
        {
            return View();
        }

        /// <summary>
        /// Setup the partial View for searching AD accounts
        ///    
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchAdAccount(string searchString)
        {
            return View("UserAccess");
        }

        public ActionResult SearchAdAccountUtility()
        {
            return PartialView("PartialViews/SearchAdAccountUtility");
        }
    }
}