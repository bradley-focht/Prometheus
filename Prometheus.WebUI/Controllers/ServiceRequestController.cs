using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.ServiceRequest;
using RequestService.Controllers;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
	/// <summary>
	/// MVC Service Requeset Controller
	/// </summary>
	[Authorize]
	public class ServiceRequestController : PrometheusController
	{
		private readonly IPortfolioService _ps;
		private readonly IServiceRequestController _srController;
		private IServiceRequestOptionController _rs;

		public ServiceRequestController()
		{
			_ps = InterfaceFactory.CreatePortfolioService();
			_srController = InterfaceFactory.CreateServiceRequestController();
		}

		/// <summary>
		/// Begin a new Service Request
		/// </summary>
		/// <param name="id">selected option Id</param>
		/// <param name="serviceRequestAction"></param>
		/// <returns></returns>
		public ActionResult Begin(int id, ServiceRequestAction serviceRequestAction = ServiceRequestAction.New)
		{
			ServiceRequestModel model = new ServiceRequestModel { ServiceRequest = new ServiceRequestDto { ServiceOptionId = id }, SelectedAction = serviceRequestAction};   //start new SR
			
			model.NewPackage = ServicePackageHelper.GetPackage(UserId, _ps, id, ServiceRequestAction.New);
			model.ChangePackage = ServicePackageHelper.GetPackage(UserId, _ps, id, ServiceRequestAction.Change);
			model.RemovePackage = ServicePackageHelper.GetPackage(UserId, _ps, id, ServiceRequestAction.Remove);

			//add package only
			if (model.NewPackage != null && model.ChangePackage == null  && model.RemovePackage == null)
			{
				model.SelectedAction = ServiceRequestAction.New;
			} //change package only
			else if (model.NewPackage == null && model.ChangePackage != null && model.RemovePackage == null)
			{
				model.SelectedAction = ServiceRequestAction.Change;
			} //remove package only
			else if (model.NewPackage == null && model.ChangePackage == null && model.RemovePackage != null)
			{
				model.SelectedAction = ServiceRequestAction.Remove;
			}

			model.CurrentIndex = -1;            /* index for info tab */
			return View("ServiceRequest", model);
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
			if (submit == 9999 && form.Id == 0)
			{
				return RedirectToAction("Index", "ServiceRequestApproval");
			}
			ServiceRequestModel model = new ServiceRequestModel();      //data to be sent to next view
			if (!ModelState.IsValid)									//server side validation
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save Service Request due to invalid data";
				return View("ServiceRequest", model);
			}
			// data ok from here on
			ServiceRequestDto request = new ServiceRequestDto	//need an adapter
			{
				Id = form.Id,			
				RequestedByUserId = int.Parse(Session["Id"].ToString()),
				Comments = form.Comments,
				Action = form.Action,
				Officeuse = form.OfficeUse,
				SubmissionDate = DateTime.Now,
				CreationDate = DateTime.Now,
				ServiceOptionId = form.ServiceOptionId,
				RequestedForDate = form.RequestedDate,
				DepartmentId = form.DepartmentId
			};

			model.CurrentIndex = 0;
			try
			{
				request = (ServiceRequestDto)_srController.ModifyServiceRequest(UserId, request, request.Id > 0 ? EntityModification.Update : EntityModification.Create);

				//make sr name & save it
				if (request.ServiceOptionId != null)
					request.Name = $"{request.Action.ToString().Substring(0, 3)}_{_ps.GetServiceOptionCategory(UserId, _ps.GetServiceOption(UserId, (int)request.ServiceOptionId).ServiceOptionCategoryId).Code}_{request.Id}";
				_srController.ModifyServiceRequest(UserId, request, request.Id > 0 ? EntityModification.Update : EntityModification.Create);
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
			if (submit >= 99999)
			{
				return RedirectToAction("ConfirmServiceRequestStateChange", "ServiceRequestApproval", new { id = form.Id, nextState = ServiceRequestState.Cancelled });
			}
			if (submit >= 9999)
			{
				return RedirectToAction("ConfirmServiceRequestStateChange", "ServiceRequestApproval", new { id = form.Id, nextState = ServiceRequestState.Submitted });
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
			_rs = InterfaceFactory.CreateServiceRequestOptionController();
			/* STEP ONE - Get the Service Package and SR */
			ServiceRequestModel model = new ServiceRequestModel //used to hold all the data until redirecting
			{
				CurrentIndex = submit,
				ServiceRequestId = form.Id,
				Mode = ServiceRequestMode.Selection
			};
			try
			{
				model.ServiceRequest = _srController.GetServiceRequest(UserId, form.Id); //SR
				if (model.ServiceRequest.Action == ServiceRequestAction.New)			//Package
				{
					model.NewPackage = ServicePackageHelper.GetPackage(UserId, _ps, model.ServiceRequest.ServiceOptionId,
						ServiceRequestAction.New); //package
					model.SelectedAction = ServiceRequestAction.New;	//new package
					if (model.NewPackage == null)
					{
						model.NewPackage = ServicePackageHelper.GetPackage(UserId, _ps, model.ServiceRequest.ServiceOptionId);
					}
				}
				else if (model.ServiceRequest.Action == ServiceRequestAction.Remove)
				{
					model.RemovePackage = ServicePackageHelper.GetPackage(UserId, _ps, model.ServiceRequest.ServiceOptionId,
						ServiceRequestAction.Remove);
						model.SelectedAction = ServiceRequestAction.Remove;	//remove package
					if (model.NewPackage == null && model.RemovePackage == null)
					{
						model.NewPackage = ServicePackageHelper.GetPackage(UserId, _ps, model.ServiceRequest.ServiceOptionId);
					}
				}
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service request information, error: {exception.Message}";
				model.CurrentIndex = -1; //either the SR does not exist or the option does not exist
				return View("ServiceRequest", model);
			}

			/* STEP TWO - figure out what category to work with */
			IServiceOptionCategoryDto currentCategory = null;
			if (model.ServiceRequest.Action == ServiceRequestAction.New)
			{
				if (model.NewPackage != null)
					currentCategory = _ps.GetServiceOptionCategory(UserId,
							model.NewPackage.ServiceOptionCategoryTags.ToArray()[form.CurrentIndex].ServiceOptionCategory.Id);
			}
			else if (model.ServiceRequest.Action == ServiceRequestAction.Change)
			{
				if (model.ChangePackage != null)
					currentCategory = _ps.GetServiceOptionCategory(UserId,
							model.ChangePackage.ServiceOptionCategoryTags.ToArray()[form.CurrentIndex].ServiceOptionCategory.Id);
			}
			else if (model.ServiceRequest.Action == ServiceRequestAction.Remove)
			{
				if (model.RemovePackage != null)
					currentCategory = _ps.GetServiceOptionCategory(UserId,
							model.RemovePackage.ServiceOptionCategoryTags.ToArray()[form.CurrentIndex].ServiceOptionCategory.Id);
			}

			/* STEP THREE - add/remove options in the SR */
			{
				ICollection<IServiceRequestOptionDto> formOptions;
				if (form.Options == null)
				{
					formOptions = new List<IServiceRequestOptionDto>();
				} //need to avoid a null pointer exception here
				else
				{
					formOptions = form.GetServiceRequestOptions().ToList();
				}
				if (model.ServiceRequest.ServiceRequestOptions == null)
				{
					model.ServiceRequest.ServiceRequestOptions = new List<IServiceRequestOptionDto>();
				}

				try
				{
					if (currentCategory != null)
						foreach (var option in currentCategory.ServiceOptions)                                                          /* sighhhhh */
						{
							var formDto = (from o in formOptions where o.ServiceOptionId == option.Id select o).FirstOrDefault();
							var srDto = (from o in model.ServiceRequest.ServiceRequestOptions where o.ServiceOptionId == option.Id select o).FirstOrDefault();

							if (formDto != null && srDto == null)       //add condition
							{
								_rs.ModifyServiceRequestOption(UserId, (from o in formOptions where o.ServiceOptionId == option.Id select o).First(), EntityModification.Create);
							}
							else if (formDto == null && srDto != null) //remove condition
							{
								_rs.ModifyServiceRequestOption(UserId, (from o in model.ServiceRequest.ServiceRequestOptions where o.ServiceOptionId == option.Id select o).First(), EntityModification.Delete);
							}
							else if (formDto != null /* && srDto != null */)    //update condition
							{
								_rs.ModifyServiceRequestOption(UserId, srDto, EntityModification.Delete);
								_rs.ModifyServiceRequestOption(UserId, formDto, EntityModification.Create);
							}                                                                                                       /* done \*/
						}
					model.ServiceRequest = _srController.GetServiceRequest(UserId, form.Id); // refresh the data in the model now
				}
				catch (Exception exception)     //what, a problem?
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"Failed to retrieve service request information, error: {exception.Message}";
					model.CurrentIndex = -1; //either the SR does not exist or the option does not exist
					return View("ServiceRequest", model);
				}
			}
			/* STEP FOUR - add/remove user input data */
			//update and add
			IServiceRequestUserInputController userInputController = InterfaceFactory.CreateServiceRequestUserInputController();
			if (form.UserInput != null)
			{
				List<ServiceRequestUserInputDto> userDataList = (from u in form.UserInput
																 where u.Value != null
																 select new ServiceRequestUserInputDto
																 { Id = u.Id, Name = u.Name, UserInputType = u.UserInputType,
																	 ServiceRequestId = form.Id, Value = u.Value, InputId = u.InputId
																 }).ToList();
				foreach (var userData in userDataList)
				{
					try
					{
						userInputController.ModifyServiceRequestUserInput(UserId, userData, userData.Id > 0 ? EntityModification.Update : EntityModification.Create);
					}
					catch (Exception exception)
					{
						TempData["MessageType"] = WebMessageType.Failure;
						TempData["Message"] = $"Failed to save user input data, error: {exception.Message}";
						return View("ServiceRequest", model);
					}
				}
			}
			ICollection<UserInputTag> userInputs;
			userInputs = form.UserInput == null ? new List<UserInputTag>() : (from u in form.UserInput select new UserInputTag { UserInput = u, Required = false }).ToList();

			var options = from o in model.ServiceRequest.ServiceRequestOptions select new ServiceOptionDto { Id = o.ServiceOptionId };
			var requiredInputs = _ps.GetInputsForServiceOptions(UserId, options);
			//clean up - figure out what can be removed
			foreach (var userData in userInputs)
			{
				switch (userData.UserInput.UserInputType)
				{
					case UserInputType.ScriptedSelection:
						foreach (var input in (from s in requiredInputs.UserInputs where s is IScriptedSelectionInputDto select s))
						{
							if (userData.UserInput.UserInputType == UserInputType.ScriptedSelection & userData.UserInput.InputId == input.Id)
							{
								userData.Required = true;
							}
						}
						break;
					case UserInputType.Selection:
						foreach (var input in (from s in requiredInputs.UserInputs where s is ISelectionInputDto select s))
						{
							if (userData.UserInput.UserInputType == UserInputType.Selection & userData.UserInput.InputId == input.Id)
							{
								userData.Required = true;
							}
						}
						break;
					default:
						foreach (var input in (from s in requiredInputs.UserInputs where s is ITextInputDto select s))
						{
							if (userData.UserInput.UserInputType == UserInputType.Text & userData.UserInput.InputId == input.Id)
							{
								userData.Required = true;
							}
						}
						break;
				}
			}

			//do the removals
			foreach (var userData in userInputs)
			{
				if (!userData.Required && userData.UserInput.Id > 0) //avoid the new
				{
					userInputController.ModifyServiceRequestUserInput(UserId, new ServiceRequestUserInputDto { Id = userData.UserInput.Id }, EntityModification.Delete);
				}
			}

			/* STEP FIVE - navigation */

			model.CurrentIndex = submit;
			if (submit >= 99999)
			{
				return RedirectToAction("ConfirmServiceRequestStateChange", "ServiceRequestApproval", new { id = form.Id, nextState = ServiceRequestState.Cancelled });
			}
			if (submit >= 9999)
			{
				return RedirectToAction("ConfirmServiceRequestStateChange", "ServiceRequestApproval", new { id = form.Id, nextState = ServiceRequestState.Submitted });
			}

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
			ServiceRequestModel model = new ServiceRequestModel { CurrentIndex = index, ServiceRequestId = id, Mode = mode };

			/* STEP ONE - get SR, get package, and enough  */
			try
			{
				model.ServiceRequest = _srController.GetServiceRequest(UserId, id);       //get db info
				model.NewPackage = ServicePackageHelper.GetPackage(UserId, _ps, model.ServiceRequest.ServiceOptionId, model.ServiceRequest.Action) ??
				                   ServicePackageHelper.GetPackage(UserId, _ps, model.ServiceRequest.ServiceOptionId);
				//deal with no package by making one
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service request information, error: {exception.Message}";
				return View("ServiceRequest", model);
			}

			/* STEP TWO - get any user inputs & associate with the option */
			List<ServiceOptionTag> optionInputList = new List<ServiceOptionTag>();
			if (index < 0) { /*not much to do here, eh... */ }
			else if (index < model.NewPackage.ServiceOptionCategoryTags.Count && index < 999)
			{
				model.OptionCategory = model.NewPackage.ServiceOptionCategoryTags.ElementAt(index).ServiceOptionCategory;
				foreach (var option in model.OptionCategory.ServiceOptions)
				{
					IEnumerable<IUserInput> inputs = null;	//collect relavent inputs
					if (model.ServiceRequest.Action == ServiceRequestAction.New)	//new
					{
						inputs = from i in _ps.GetInputsForServiceOptions(UserId, new List<IServiceOptionDto> {option}).UserInputs
							where i.AvailableOnAdd select i;
					}
					else if (model.ServiceRequest.Action == ServiceRequestAction.Change)	//change
					{
						inputs = from i in _ps.GetInputsForServiceOptions(UserId, new List<IServiceOptionDto> { option }).UserInputs
								 where i.AvailableOnChange select i;
					}
					else if (model.ServiceRequest.Action == ServiceRequestAction.Remove)		//remove
					{
						inputs = from j in _ps.GetInputsForServiceOptions(UserId, new List<IServiceOptionDto> {option}).UserInputs
							where j.AvailableOnRemove
							select j;
					}
					if (inputs != null) //reduce chances of nulls getting to razor views
					{
						optionInputList.Add(new ServiceOptionTag { ServiceOption = option, UserInputs = UserInputHelper.MakeInputGroupDto(inputs) });			
					}
				}
			}

			//for each SR option in the SR get the option {monthly price, up front price }
			if (model.ServiceRequest?.ServiceRequestOptions != null)
			{
				foreach (var option in model.ServiceRequest.ServiceRequestOptions)
				{
					option.ServiceOption = _ps.GetServiceOption(UserId, option.ServiceOptionId);
				}
			}
			model.UserInputs = optionInputList;
			return View("ServiceRequest", model);
		}
	}
}