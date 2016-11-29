using System.Web.Mvc;
using Prometheus.WebUI.Models.SystemAccess;


namespace Prometheus.WebUI.Controllers
{
    //[Authorize]
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
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchAdAccount(string searchString)
        {
            AdSearchResultsModel model = new AdSearchResultsModel();
            UserManager.UserManager um = new UserManager.UserManager();
            model.SearchResults = um.SearchUsers(searchString);
            
            return View("UserAccess", model);
        }

    }
}