using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prometheus.Domain.Abstract;
using Prometheus.WebUI.Models;

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
			List<IServiceBundle> portfolioItems = new List<IServiceBundle>();

						var portfolioItem = new IServiceBundle();
			portfolioItem.Description = "some new service";
			portfolioItem.Name = "Workplace services";
			portfolioItem.Id = 1;
			portfolioItem.Measures = "A B C";
			

			portfolioItems.Add(portfolioItem);

            return View(portfolioItems);
        }

		/// <summary>
		/// Save updates to records or save new records if no id
		/// </summary>
		/// <param name="ServiceBundleId"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Save(IService service)
		{
			ServiceBundleModel model = new ServiceBundleModel();
			model.ServiceBundles = new List<KeyValuePair<int, string>>();
			model.CurrentServiceBundle = new IServiceBundle() { Id = 0 };


			return View(model);
		}

		/// <summary>
		/// Returns the Service Portfolio Editor with a model with id = 0;
		/// </summary>
		/// <returns></returns>
		public ActionResult Add()
		{
			var newServiceBundle = new IServiceBundle();
			newServiceBundle.Id = 0;

			return View("Editor", newServiceBundle);
		}



		public ActionResult Retrieve(int id = 0)
		{
			ServiceBundleModel model = new ServiceBundleModel();
			model.ServiceBundles = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(1, "Hip Replacement") };

			model.CurrentServiceBundle = new IServiceBundle() { Id = id, Name = "i'm a test!", Description=null, BusinessValue=null, Measures="not now" };

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