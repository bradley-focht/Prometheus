using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.ServiceRequestMaintenance;
using Prometheus.WebUI.Models.Shared;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceRequestMaintenanceController : Controller
    {
	    private int _dummyId = 0;

	    // GET: ServiceRequestMaintenance
        public ActionResult Index()
        {
	 			return View();
        }

		/// <summary>
		/// Generate a link list of service names with filtering control
		/// </summary>
		/// <param name="catalog"></param>
		/// <param name="id"></param>
		/// <returns></returns>
	    public PartialViewResult GetServiceNames(string catalog = "Both", int id = 0)
	    {
		    var rs = InterfaceFactory.CreateCatalogController(_dummyId);
		    var services = new List<Tuple<int, string>>();
			if (catalog == "Business" || catalog == "Both")
		    {
			    services.AddRange(from s in rs.BusinessCatalog
				    select new Tuple<int, string>(s.Id, s.Name));
		    }

		    if (catalog == "Support" || catalog == "Both")
		    {
				services.AddRange(from s in rs.SupportCatalog
								  select new Tuple<int, string>(s.Id, s.Name));
			}
			LinkListModel model = new LinkListModel {SelectedItemId = id, ListItems = services};
		    return PartialView(model);
	    }

	    public ActionResult ShowServiceOptions(int id)
	    {
		    var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
		    var service = ps.GetService(id);

		    return View("Index", service);
	    }

		/// <summary>
		/// Show a service option and editor functions
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
	    public ActionResult ShowServiceOption(int id)
		{
			ServiceRequestOptionModel model = new ServiceRequestOptionModel();

			model.Option = InterfaceFactory.CreatePortfolioService(_dummyId).GetServiceOption(id);
			
			return View(model);
		}

		/// <summary>
		/// Returns View to add form of corresponding type
		/// </summary>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
	    public ActionResult AddUserInput(UserInputTypes type, int id)
	    {
		    return View(new AddUserInputModel { InputType = type, OptionId = id });
	    }

	}
}