using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prometheus.WebUI.Models.UserAccount;

namespace Prometheus.WebUI.Controllers
{
    public class UserAccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Login Authorization and builds Cookie
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

        public ActionResult Logout()
        {
            return View("Index");
        }
    }
}