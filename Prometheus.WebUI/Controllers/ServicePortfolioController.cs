using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prometheus.Domain.Abstract;


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
		/// Used for the Service Portfolio Editor
		/// </summary>
		/// <param name="ServiceBundleId"></param>
		/// <returns></returns>
		public ActionResult Edit(int id = 0)
		{
			return View();
		}



    }
}