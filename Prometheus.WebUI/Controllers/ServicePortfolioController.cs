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
        // GET: ServicePortfolio
        public ActionResult Index()
        {
			//temp code please remove on implementation
			List<IServiceBundle> portfolioItems = new List<IServiceBundle>();

						var portfolioItem = new IServiceBundle();
			portfolioItem.Description = "some new service";
			portfolioItem.Name = "Workplace services";
			portfolioItem.id = 1;
			portfolioItem.Measures = "A B C";
			

			portfolioItems.Add(portfolioItem);

            return View(portfolioItems);
        }

		/// <summary>
		/// Used for the Service Portfolio Editor, default view with no item selected for editing
		/// </summary>
		/// <param name="ServiceBundleId"></param>
		/// <returns></returns>
		public ActionResult Show(int id = 0)
		{
			ServiceBundleModel model = new ServiceBundleModel();
			model.ServiceBundles = new List<KeyValuePair<int, string>>();
			model.CurrentServiceBundle = new IServiceBundle() { id = 0 };


			return View(model);
		}



		public ActionResult Edit(int id)
		{
			var serviceBundle = new IServiceBundle();

			return View(serviceBundle);
		}


    }
}