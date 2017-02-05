using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
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
            ServiceRequestModel model = new ServiceRequestModel {InitialOptionId = id};
            _ps = InterfaceFactory.CreatePortfolioService(dummyId);

            IServiceRequestPackageDto package = _ps.GetServiceRequestPackagesForServiceOption(id).FirstOrDefault();
            

            model.Package = package;
            model.CurrentIndex = -1;
            return View("ServiceRequest", model);
        }


        /// <summary>
        /// Cancel an already started SR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CancelRequest(int id = 0)
        {
            //hell if i know what to do right now...

            
            return View();
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
            // data ok from here on
            ServiceRequestDto request = new ServiceRequestDto
            {
                RequestedByUserId = dummyId,
                Comments = serviceRequest.Comments,
                Officeuse = serviceRequest.OfficeUse,
                SubmissionDate = serviceRequest.RequiredDate,
                CreationDate = DateTime.Now
            };

            _ps = InterfaceFactory.CreatePortfolioService(dummyId);
            model.Package = _ps.GetServiceRequestPackagesForServiceOption(serviceRequest.InitialOptionId).FirstOrDefault();
            model.CurrentIndex = 0;
            try
            {
                request = (ServiceRequestDto) _ps.ModifyServiceRequest(request, EntityModification.Create);
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save service request, error: {exception.Message}";
                model.CurrentIndex = -1;
                return View("ServiceRequest", model);
            }

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = "Successfully created Service Request";


            return RedirectToAction("Form", new {id = request.Id, index = 0});
        }

        /// <summary>
        /// View a tab in the SR form
        /// </summary>
        /// <param name="id">service request id</param>
        /// <param name="index">package index</param>
        /// <param name="selectedOptionId">optional id selected on the index</param>
        /// <returns></returns>
        public ActionResult Form(int id, int index, int selectedOptionId=0)
        {
            _ps = InterfaceFactory.CreatePortfolioService(dummyId);
            ServiceRequestModel model = new ServiceRequestModel {CurrentIndex = index, ServiceRequestId = id};
            try
            {
                model.ServiceRequest = _ps.GetServiceRequest(id);       //get db info
                model.Package = _ps.GetServiceRequestPackage(1);
                model.OptionCategory =_ps.GetServiceOptionCategory(model.Package.ServiceOptionCategories.ElementAt(index).Id);
            }
            catch(Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to retrieve service request information, error: {exception.Message}";
                return View("ServiceRequest", model);
            }

            
            return View("ServiceRequest", model);
        }
    }
}