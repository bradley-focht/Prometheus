using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
using Prometheus.WebUI.Models.ServiceRequest;
using RequestService.Controllers;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceRequestController : Controller
	{
		private IPortfolioService _ps;
		private IServiceRequestOptionController _rs;
		private int dummyId = 0;

		public ServiceRequestController()
		{
			//na atm
		}

		/// <summary>
		/// Begin a new Service Request
		/// </summary>
		/// <param name="id">selected option Id</param>
		/// <returns></returns>
		public ActionResult Begin(int id)
		{
			ServiceRequestModel model = new ServiceRequestModel { ServiceRequest = new ServiceRequestDto { ServiceOptionId = id } };   //start new SR
			_ps = InterfaceFactory.CreatePortfolioService(dummyId);

			model.Package = ServicePackageHelper.GetPackage(_ps, id);
			model.CurrentIndex = -1;            /* index for info tab */
			return View("ServiceRequest", model);
		}


		/// <summary>
		/// Cancel an already started SR before it has been submitted for approval
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult CancelRequest(int id = 0)
		{
			if (id == 0)
				return RedirectToAction("Index", "ServiceRequestApproval");

			_ps = InterfaceFactory.CreatePortfolioService(dummyId);
			_ps.ModifyServiceRequest(new ServiceRequestDto {Id = id}, EntityModification.Delete);
			return RedirectToAction("Index", "ServiceRequestApproval");
		}


		/// <summary>
		/// Save the info portion of a service request.
		/// </summary>
		/// <param name="form"></param>
		/// <param name="submit">submit buttn id</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveInfo(ServiceRequestInfoReturnModel form, int submit)
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
				Id = form.Id,
				RequestedByUserId = int.Parse(Session["Id"].ToString()),
				Comments = form.Comments,
				Officeuse = form.OfficeUse,
				SubmissionDate = DateTime.Now,
				CreationDate = DateTime.Now,
				ServiceOptionId = form.ServiceOptionId,
				RequestedForDate = form.RequestedDate
			};

			_ps = InterfaceFactory.CreatePortfolioService(dummyId);

			model.CurrentIndex = 0;
			try
			{
				request = (ServiceRequestDto)_ps.ModifyServiceRequest(request, request.Id > 0 ? EntityModification.Update : EntityModification.Create);
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

			return RedirectToAction("Form", new { id = request.Id, index = submit });
		}


		/// <summary>
		/// Save data from SR form  Select Options Mode
		/// </summary>
		/// <param name="form">all submitted data</param>
		/// <param name="submit">submit button clicked</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveFormSelection(ServiceRequestFormReturnModel form, int submit)
		{
			_ps = InterfaceFactory.CreatePortfolioService(dummyId);
			_rs = InterfaceFactory.CreateServiceRequestOptionController(dummyId);
			/* STEP ONE - Get the Service Package and SR */
			ServiceRequestModel model = new ServiceRequestModel //used to hold all the data until redirecting
			{
				CurrentIndex = submit,
				ServiceRequestId = form.Id,
				Mode = ServiceRequestMode.Selection
			};
			try
			{
				model.ServiceRequest = _ps.GetServiceRequest(form.Id); //SR
				model.Package = ServicePackageHelper.GetPackage(_ps, model.ServiceRequest.ServiceOptionId); //Package
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service request information, error: {exception.Message}";
				model.CurrentIndex = -1; //either the SR does not exist or the option does not exist
				return View("ServiceRequest", model);
			}

			/* STEP TWO - figure out what category to work with */
			var currentCategory =
				_ps.GetServiceOptionCategory(
					model.Package.ServiceOptionCategoryTags.ToArray()[form.CurrentIndex].ServiceOptionCategory.Id);

			/* STEP THREE - add/remove options in the SR */
			{
				ICollection<ServiceRequestOptionDto> formOptions;
				if (form.Options == null) { formOptions = new List<ServiceRequestOptionDto>(); }    //need to avoid a null pointer exception here
				else { formOptions = form.GetServiceRequestOptions().ToList(); }
				if (model.ServiceRequest.ServiceRequestOptions == null)
				{ model.ServiceRequest.ServiceRequestOptions = new List<IServiceRequestOptionDto>(); }
				try
				{
					foreach (var option in currentCategory.ServiceOptions)                                                          /* sighhhhh */
					{
						var formDto = (from o in formOptions where o.ServiceOptionId == option.Id select o).FirstOrDefault();
						var srDto = (from o in model.ServiceRequest.ServiceRequestOptions where o.ServiceOptionId == option.Id select o).FirstOrDefault();

						if (formDto != null && srDto == null)		//add condition
						{
							_rs.ModifyServiceRequestOption((from o in formOptions where o.ServiceOptionId == option.Id select o).First(), EntityModification.Create);
						}
						else if (formDto == null && srDto != null) //remove condition
						{
							_rs.ModifyServiceRequestOption((from o in model.ServiceRequest.ServiceRequestOptions where o.ServiceOptionId == option.Id select o).First(), EntityModification.Delete);
						}
						else if (formDto != null /* && srDto != null */)	//update condition
						{
							_rs.ModifyServiceRequestOption(srDto, EntityModification.Delete);
							_rs.ModifyServiceRequestOption(formDto, EntityModification.Create);
						}                                                                                                       /* done \*/
					}
				}
				catch (Exception exception)		//what, a problem?
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"Failed to retrieve service request information, error: {exception.Message}";
					model.CurrentIndex = -1; //either the SR does not exist or the option does not exist
					return View("ServiceRequest", model);
				}
			}
			/* STEP FOUR - add/remove user input data */

			/* STEP FIVE - navigation */
			if (submit >= 9999)
			{
				//TODO: Change state after saving
				return RedirectToAction("Index", "ServiceRequestApproval");
			}
			model.CurrentIndex = submit;

			model.Mode = ServiceRequestMode.Selection;
			return RedirectToAction("Form", new { id = model.ServiceRequestId, index = model.CurrentIndex, mode = model.Mode });
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
			ServiceRequestModel model = new ServiceRequestModel { CurrentIndex = index, ServiceRequestId = id, Mode = mode };

			/* STEP ONE - get SR and get package */
			try
			{
				model.ServiceRequest = _ps.GetServiceRequest(id);       //get db info
				model.Package = ServicePackageHelper.GetPackage(_ps, model.ServiceRequest.ServiceOptionId);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service request information, error: {exception.Message}";
				return View("ServiceRequest", model);
			}
			/* STEP TWO - get any user inputs & associate with the option */
			List<ServiceOptionTag> optionInputList = new List<ServiceOptionTag>();
			if (index < 0)
			{
				//not much to do here, eh...
			}
			else if (index < model.Package.ServiceOptionCategoryTags.Count && index < 999)
			{
				model.OptionCategory = model.Package.ServiceOptionCategoryTags.ElementAt(index).ServiceOptionCategory;
				foreach (var option in model.OptionCategory.ServiceOptions)
				{
					optionInputList.Add(new ServiceOptionTag
					{
						ServiceOption = option,
						UserInputs = _ps.GetInputsForServiceOptions(new List<IServiceOptionDto> { option })
					});
				}
			}

			model.UserInputs = optionInputList;

			return View("ServiceRequest", model);
		}
	}
}