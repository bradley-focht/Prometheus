using System;
using System.Web.Mvc;
using System.Web.Security;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.UserAccount;
using UserManager;

namespace Prometheus.WebUI.Controllers
{
	public class UserAccountController : PrometheusController
	{
		/// <summary>
		/// user manager functions for security
		/// </summary>
		private readonly IUserManager _userManager;

		public UserAccountController(IUserManager userManager)
		{
			_userManager = userManager;
		}

		/// <summary>
		/// Index page is the login page
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(string returnUrl)
		{
			return View(new UserAccountModel { ReturnUrl = returnUrl });
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
				return RedirectToAction("Index", "UserAccount", new UserAccountModel { ReturnUrl = userLogin.ReturnUrl });

			//validate login, create session cookie

			IUserDto user;
			try
			{
				user = (UserDto)_userManager.Login(userLogin.Username, userLogin.Password);    //get the user object
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Login failure, error: {exception.Message}";
				return RedirectToAction("Index", "UserAccount", new UserAccountModel { ReturnUrl = userLogin.ReturnUrl });
			}
			if (user != null && user.Id > 0)
			{
				FormsAuthentication.SetAuthCookie(user.Name, true);                             //enter data in session cookie

				Session["DisplayName"] = user.Name;
				Session["Guid"] = user.AdGuid;
				Session["Id"] = user.Id;
				Session["DepartmentId"] = user.Department.Id;
				Session["DepartmentName"] = user.Department.Name;

				if (string.IsNullOrEmpty(userLogin.ReturnUrl))
					return RedirectToAction("Index", "Home");
				return Redirect(userLogin.ReturnUrl);
			}

			TempData["MessageType"] = WebMessageType.Failure;
			TempData["Message"] = "Failed to login, please check username and password.";

			return RedirectToAction("Index", "UserAccount", new UserAccountModel { ReturnUrl = userLogin.ReturnUrl });
		}

		/// <summary>
		/// Login guest account
		/// </summary>
		/// <param name="returnUrl">if entry point is not login page</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult LoginGuest(string returnUrl)
		{
			FormsAuthentication.SetAuthCookie("Guest", true);
			Session["DisplayName"] = "Guest";
			Session["Guid"] = Guid.Empty;
			Session["Id"] = _userManager.GuestId;
			Session["DepartmentId"] = 0;
			Session["DepartmentName"] = null;
			if (string.IsNullOrEmpty(returnUrl))
				return RedirectToAction("Index", "Home");
			return Redirect(returnUrl);
		}

		/// <summary>
		/// Login admin
		/// </summary>
		/// <param name="returnUrl">if entry point is not login page</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult LoginAdmin(string returnUrl)
		{
			FormsAuthentication.SetAuthCookie("Admin", true);
			Session["DisplayName"] = "Administrator";
			Session["Guid"] = Guid.Empty;
			Session["Id"] = _userManager.AdministratorId;
			Session["DepartmentId"] = 0;
			Session["DepartmentName"] = null;
			if (string.IsNullOrEmpty(returnUrl))
				return RedirectToAction("Index", "Home");
			return Redirect(returnUrl);
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