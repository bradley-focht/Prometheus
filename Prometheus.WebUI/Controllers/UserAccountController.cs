using System;
using System.Web.Mvc;
using System.Web.Security;
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
	        IUserDto user;
	        try
	        {
		        user = (UserDto) um.Login(userLogin.Username, userLogin.Password);
	        }
	        catch (Exception exception)
	        {
		        TempData["MessageType"] = WebMessageType.Failure;
		        TempData["Message"] = $"Login failure, error: {exception.Message}";
				return RedirectToAction("Index", "Home");
			}
	        if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, true);
               
                Session["DisplayName"] = user.Name;
                Session["Id"] = user.Id;

                return RedirectToAction("Index", "Home");
            }

            TempData["MessageType"] = WebMessageType.Failure;
            TempData["Message"] = "Failed to login, please check username and password.";

            return RedirectToAction("Index", "UserAccount");
        }

        /// <summary>
        /// Login as Guest
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LoginGuest()
        {
            FormsAuthentication.SetAuthCookie("Guest", true);
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
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index", "UserAccount");
        }
    }
}