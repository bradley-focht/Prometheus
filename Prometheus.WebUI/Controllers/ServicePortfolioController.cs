using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataService.Models;
using Prometheus.WebUI.Models.ServicePortfolio;

namespace Prometheus.WebUI.Controllers
{
    public class ServicePortfolioController : Controller
    {
        /// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public ActionResult Index()
        {
			//temp code please remove on implementation
			var portfolioItems = new List<IServiceBundle>();


			portfolioItems.Add(new ServiceBundle() {Id = Guid.NewGuid(), Name = "Workplace Services", Description = "some new service" });


            return View(portfolioItems);
        }
		/// <summary>
        /// 
        /// </summary>
        /// <param name="serviceBundle"></param>
        /// <returns></returns>
		[HttpPost]
		public ActionResult Save(ServiceBundle serviceBundle)           
		{

			return RedirectToAction("Show");
		}

		/// <summary>
		/// Returns the Service Portfolio Editor with a model with id = 0;
		/// </summary>
		/// <returns></returns>
		public ActionResult Add()
		{
            ServiceBundleModel model = new ServiceBundleModel(new ServiceBundle());

            return View(model);
		}


        /// <summary>
        /// Show the initial service portfolio editor and if an item is selected, otherwise 
        ///   currentSelection is null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Show(Guid? id = null)
		{
            ServiceBundleModel model = new ServiceBundleModel(new ServiceBundle());
            
			return View(model);
		}

		/// <summary>
		/// Last chance before deleting a record
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDelete(int id)
		{

			return null;
		}


		/// <summary>
		/// Delete a service bundle
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Delete(int id)
		{

			return null;
		}

    }
}