using System;
using System.Web.Mvc;
using System.Web.Security;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.UserAccount;
using UserManager;

namespace Prometheus.WebUI.Controllers
{
    public class UserAccountController : Controller
    {
        /// <summary>
        /// user manager functions for security
        /// </summary>
	    private readonly IUserManager _userManager;

        public UserAccountController()
	    {
		    _userManager = InterfaceFactory.CreateUserManagerService();
	    }

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
        //[ValidateAntiForgeryToken]
        public ActionResult Login(UserAccountModel userLogin)
        {
            if (!ModelState.IsValid)
                return View("Index");

            //validate login, create session cookie
            
	        IUserDto user;
	        try
	        {
		        user = (UserDto) _userManager.Login(userLogin.Username, userLogin.Password);    //get the user object
	        }
	        catch (Exception exception)
	        {
		        TempData["MessageType"] = WebMessageType.Failure;
		        TempData["Message"] = $"Login failure, error: {exception.Message}";
				return RedirectToAction("Index", "UserAccount");
			}
	        if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, true);                             //enter data in session cookie
               
                Session["DisplayName"] = user.Name;
                Session["Guid"] = user.AdGuid;
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
            Session["Id"] = 1;
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