using System.Web.Mvc;
using Common.Dto;
using Prometheus.WebUI.Helpers;
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
        /// <param name="userLogin">Contains user credentials</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccountModel userLogin)
        {
            if (!ModelState.IsValid)
                return View("Index");

            //validate login, create session cookie
            var um = new global::UserManager.UserManager();     /*this is weird, sean why did you name it like this? */
            var user = (UserDto)um.Login(userLogin.Username, userLogin.Password);
            if (user != null)
            {
                Session["Displayname"] = user.Name;
                Session["Id"] = user.Id;
                Session["Roles"] = user.Role;

                return RedirectToAction("Index", "Home");
            }

            TempData["MessageType"] = WebMessageType.Failure;
            TempData["Message"] = "Login failed. If you do not know your username or password then contact your IT Service Desk";

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Login as Guest
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LoginGuest()
        {
            Session["DisplayName"] = "Guest";
            Session["Id"] = 0;
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