using System.Web.Mvc;
using Prometheus.WebUI.Models.UserAccount;

namespace Prometheus.WebUI.Controllers
{
    public class UserAccountController : Controller
    {
        /// <summary>
        /// Index page is the login page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Login Authorization and builds session cookie
        /// </summary>
        /// <param name="uAccount"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccountModel uAccount)
        {
            if (!ModelState.IsValid)
                return View("Index");


            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Destroys the session
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            return View("Index");
        }
    }
}