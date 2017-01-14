using System.Web.Mvc;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequest;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceRequestController : Controller
    {
	    private int userId = 1;
        // GET: ServiceRequest
        public ActionResult BeginOptionRequest(int id)
        {
	        var ps = InterfaceFactory.CreatePortfolioService(userId);

	        var option = ps.GetServiceOption(id);

			ServiceRequestModel model = new ServiceRequestModel
			{
				Option = option
			};

            return View(model);
        }


	    public ActionResult SubmitNewServiceRequest(FormCollection data)
	    {
		    return View();
	    }

	}
}