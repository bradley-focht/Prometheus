using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
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
        /// <param name="id">selected option Id</param>
        /// <returns></returns>
        public ActionResult Begin(int id)
        {
            ServiceRequestModel model = new ServiceRequestModel {ServiceRequest = new ServiceRequestDto {ServiceOptionId = id} };   //start new SR
            _ps = InterfaceFactory.CreatePortfolioService(dummyId);

            model.Package = ServicePackageHelper.GetPackage(_ps, id);
            model.CurrentIndex = -1;            /* index for info tab */
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

            
            return RedirectToAction("Index", "ServiceRequestApproval");
        }


        /// <summary>
        /// Save the info portion of a service request.
        /// </summary>
        /// <param name="serviceRequest"></param>
        /// <param name="submit">submit buttn id</param>
        /// <returns></returns>
        [HttpPost]
	    public ActionResult SaveInfo(ServiceRequestInfoReturnModel serviceRequest, int submit)
        {
            ServiceRequestModel model = new ServiceRequestModel();      //data to be sent to next view
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save Service Request due to invalid data";
                return View("ServiceRequest", model);
            }
            // data ok from here on
            ServiceRequestDto request = new ServiceRequestDto
            {
                Id = serviceRequest.Id,
                RequestedByUserId = int.Parse(Session["Id"].ToString()),
                Comments = serviceRequest.Comments,
                Officeuse = serviceRequest.OfficeUse,
                SubmissionDate = DateTime.Now,
                CreationDate = DateTime.Now,
                ServiceOptionId = serviceRequest.ServiceOptionId,
                RequestedForDate = serviceRequest.RequestedDate           
            };

            _ps = InterfaceFactory.CreatePortfolioService(dummyId);
            
            model.CurrentIndex = 0;
            try
            {
                request = (ServiceRequestDto) _ps.ModifyServiceRequest(request, request.Id > 0? EntityModification.Update : EntityModification.Create);
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save service request, error: {exception.Message}";
                model.CurrentIndex = -1;
                model.ServiceRequest = request;
                return View("ServiceRequest", model);
            }
            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = "Successfully saved Service Request";
            if (submit >= 9999)
            {
                //TODO: Change state after saving
                return RedirectToAction("Index", "ServiceRequestApproval");
            }

            return RedirectToAction("Form", new {id = request.Id, index = submit});
        }


        /// <summary>
        /// Save data from SR forms
        /// </summary>
        /// <param name="form"></param>
        /// <param name="submit">submit button clicked</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveForm(ServiceRequestFormReturnModel form, int submit)
        {
            if (form.Options != null)
            {
                TempData["MessageType"] = WebMessageType.Success;
                TempData["Message"] = "Successfully saved changes to Service Request";
            }
            if (submit >= 9999)
            {
                //TODO: Change state after saving
                return RedirectToAction("Index", "ServiceRequestApproval");
            }
            return RedirectToAction("Form", new {id = form.Id, index = submit});
        }

        /// <summary>
        /// View a tab in the SR form
        /// </summary>
        /// <param name="id">service request id</param>
        /// <param name="index">package index</param>
        /// <param name="mode">display mode</param>
        /// <returns></returns>
        public ActionResult Form(int id, int index, ServiceRequestMode mode = ServiceRequestMode.Selection)
        {
            _ps = InterfaceFactory.CreatePortfolioService(dummyId);
            ServiceRequestModel model = new ServiceRequestModel {CurrentIndex = index, ServiceRequestId = id, Mode = mode};
            
            try
            {
                model.ServiceRequest = _ps.GetServiceRequest(id);       //get db info
                model.Package = ServicePackageHelper.GetPackage(_ps, model.ServiceRequest.ServiceOptionId);
                if (index >= 0)
                {
                    model.OptionCategory = model.Package.ServiceOptionCategoryTags.ElementAt(index).ServiceOptionCategory;
                }
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