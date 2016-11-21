using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prometheus.WebUI.Models.SystemAccess;

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
            return View(new AdSearchResultsModel());
        }

        /// <summary>
        /// Setup the partial View for searching AD accounts
        ///    
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchAdAccount(string searchString)
        {
            AdSearchResultsModel model = new AdSearchResultsModel();

            model.SearchResults = new List<Tuple<string, string>> { new Tuple<string, string>("abc", "John Doe")};
            return View("UserAccess", model);
        }

    }
}