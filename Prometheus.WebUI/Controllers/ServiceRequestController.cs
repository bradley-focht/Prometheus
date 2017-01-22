using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Prometheus.WebUI.Models.ServiceRequest;
using Prometheus.WebUI.Models.Shared;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceRequestController : Controller
    {
        // GET: ServiceRequest
        public ActionResult BeginRequest(int id=0)
        {
            ServiceRequestModel model = new ServiceRequestModel();
            IEnumerable<Tuple<int, string>> titles = new List<Tuple<int, string>> {new Tuple<int, string>(0, "User Account"), new Tuple<int, string>(1, "Hardware"), new Tuple<int, string>(2, "Software"), new Tuple<int, string>(3, "Network Access")};
            model.Titles = titles;
            model.CurrentIndex = -1;
            return View("ServiceRequest", model);
        }


	    public ActionResult SubmitNewServiceRequest(ServiceRequest serviceRequest)
	    {

			return View(serviceRequest);
		}

	}
}