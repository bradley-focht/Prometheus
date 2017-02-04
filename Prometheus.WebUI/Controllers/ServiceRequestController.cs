using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequest;
using Prometheus.WebUI.Models.Shared;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceRequestController : Controller
    {
        private IPortfolioService _ps;
        private int dummyId = 0;
        /// <summary>
        /// Begin a new Service Request
        /// </summary>
        /// <param name="id">option selected Id</param>
        /// <returns></returns>
        public ActionResult Begin(int id = 0)
        {
            ServiceRequestModel model = new ServiceRequestModel();
            _ps = InterfaceFactory.CreatePortfolioService(dummyId);

            IServiceRequestPackageDto package = _ps.GetServiceRequestPackageForServiceOption(id);

            model.Package = package;
            model.CurrentIndex = -1;
            return View("ServiceRequest", model);
        }

        /// <summary>
        /// Save the info portion of a service request.
        /// </summary>
        /// <param name="serviceRequest"></param>
        /// <returns></returns>
	    public ActionResult SaveInfo(ServiceRequest serviceRequest)
        {
            ServiceRequestModel model = new ServiceRequestModel();
            if (serviceRequest.Id != 0 || !ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save Service Request due to invalid data";

                return View("ServiceRequest", model);
            }
            model.Package = new ServiceRequestPackageDto();
            model.Package.ServiceOptionCategories = new List<IServiceOptionCategoryDto>
                {
                    new ServiceOptionCategoryDto
                    {
                        Id = 1, Name = "User Accounts", ServiceOptions = new List<IServiceOptionDto>
                        {
                            new ServiceOptionDto {Id = 1, Name = "User Account"}
                        }
                    },
                    new ServiceOptionCategoryDto
                    {
                        Id = 2, Name="Desktops", ServiceOptions = new List<IServiceOptionDto>
                        {
                            new ServiceOptionDto {Id = 2, Name="Standard Desktop" },
                            new ServiceOptionDto {Id = 3, Name="Executive Desktop" }
                        }
                    }
                };
            model.CurrentIndex = 0;

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = "Successfully created Service Request";


            return View("ServiceRequest", model);
        }

    }
}