using Common.Dto;
using Prometheus.WebUI.Models.ServicePortfolio;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;
using System;
using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
	public class ServicePortfolioController : Controller
	{
		//TODO: Brad change this to user ID
		private const int DummyUserId = 0;

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			/* create interface to service portfolio */
			var sps = new PortfolioService(DummyUserId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

			var portfolioBundles = sps.GetServiceBundles();
			/* IEnumerable<IServiceBundleDto> portfolioBundles = new List<IServiceBundleDto> {new ServiceBundleDto
			{
				Name = "Employee Productivity Services",
				Description = "Enable secure, anytime, anywhere, stable work capabilities and access to required information to meet personal computing requirements and increase customer satisfaction",
				BusinessValue = "This service will provide you with <ul><li>Increased employee productivity</li><li>Value created through enterprise procurement with standard offerings in order to reduce cost</li></ul>",
				Services = new List<IServiceDto> {new ServiceDto { Name = "Identity and Access Management"}, new ServiceDto { Name="Hardware Services"} },
				Measures = "Customer satisfaction surveys, Customer reports"
			} };
			*/
			return View(portfolioBundles);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="serviceBundle"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Save(ServiceBundleDto serviceBundle)
		{
			var sps = new PortfolioService(DummyUserId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
			serviceBundle.Id = 0;
			sps.SaveServiceBundle(serviceBundle);
			TempData["messageType"] = "success";
			TempData["message"] = "Service bundle saved successfully";
			return RedirectToAction("Show");
		}

		/// <summary>
		/// Returns the Service Portfolio Editor with a model with id = 0;
		/// </summary>
		/// <returns></returns>
		public ActionResult Add()
		{
			ServiceBundleModel model = new ServiceBundleModel(new ServiceBundleDto());

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
			ServiceBundleModel model = new ServiceBundleModel(new ServiceBundleDto());

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