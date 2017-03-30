using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	/// <summary>
	/// Service Controller to View model
	/// </summary>
	public class ServiceRequestModel
	{
		[HiddenInput]
		public int ServiceRequestId { get; set; }

		/// <summary>
		/// Display Mode
		/// </summary>
		[HiddenInput]
		public ServiceRequestMode Mode { get; set; }

		/// <summary>
		/// Originally selected option to start the SR
		/// </summary>
		[HiddenInput]
		public int ServiceOptionId
		{
			get
			{		
				if (ServiceRequest?.ServiceOptionId != null) return (int) ServiceRequest.ServiceOptionId;
				return 0; //by default return an impossible option
			}
		}

		/// <summary>
		/// who is making the request
		/// </summary>
		[Required(ErrorMessage = "Requestor is required")]
		public int Requestor => ServiceRequest.RequestedByUserId;

		/// <summary>
		/// who is this request for
		/// </summary>
		[Required(ErrorMessage = "At least one requestee is required")]
		public IEnumerable<string> Requestees { get; set; }
		/// <summary>
		/// Used for display purposes only
		/// </summary>
		public IEnumerable<string> RequesteeDisplayNames { get; set; }
		/// <summary>
		/// Used for display purposes only
		/// </summary>
		public string RequestorDisplayName { get; set; }
		/// <summary>
		/// Requested Date
		/// </summary>
		[Required(ErrorMessage = "Requested date is required")]
		public DateTime RequestedDate => ServiceRequest.RequestedForDate;

		/// <summary>
		/// Department Queue to which SR will be submitted
		/// </summary>
		[Required(ErrorMessage = "Approval Department is required")]
		public int DepartmentId => ServiceRequest.DepartmentId;

		public string Comments => ServiceRequest.Comments;
		public string OfficeUse => ServiceRequest.Officeuse;

		/// <summary>
		/// To display the list of options
		/// </summary>
		public List<IServiceOptionCategoryDto> OptionCategories { get; set; }

		/// <summary>
		/// Display the list of inputs
		/// </summary>
		public List<ServiceOptionTag> UserInputs { get; set; }

		/// <summary>
		/// Available packages to choose from
		/// </summary>
		public IServiceRequestPackageDto NewPackage { get; set; }
		public IServiceRequestPackageDto ChangePackage { get; set; }
		public IServiceRequestPackageDto RemovePackage { get; set; }

		public IServiceRequestPackageDto InUsePackage
		{
			get
			{
				switch (SelectedAction)
				{
					case ServiceRequestAction.New:
						return NewPackage;
						case ServiceRequestAction.Change:
						return ChangePackage;
						case ServiceRequestAction.Remove:
						return RemovePackage;
					default:
						return null;
				}
			}
		}

		public IEnumerable<IServicePackageTag> GetPackageTags(ServiceRequestAction action)
		{
			List<IServicePackageTag> tags = new List<IServicePackageTag>();
			switch (action)
			{
				case ServiceRequestAction.New:
					if (NewPackage != null)
					{
						if (NewPackage.ServiceOptionCategoryTags!= null)
							tags.AddRange(from o in NewPackage.ServiceOptionCategoryTags select o);
						if (NewPackage.ServiceTags != null)
							tags.AddRange(from o in NewPackage.ServiceTags select o);
						return tags.OrderBy(t => t.Order);
					}
					return null;
				case ServiceRequestAction.Change:
					if (ChangePackage != null)
					{
						if (ChangePackage.ServiceOptionCategoryTags != null)
							tags.AddRange(from o in ChangePackage.ServiceOptionCategoryTags select o);
						if (ChangePackage.ServiceTags != null)
							tags.AddRange(from o in ChangePackage.ServiceTags select o);
						return tags.OrderBy(t => t.Order);
					}
					return null;
				case ServiceRequestAction.Remove:
					if (RemovePackage != null)
					{
						if (RemovePackage.ServiceOptionCategoryTags != null)
							tags.AddRange(from o in RemovePackage.ServiceOptionCategoryTags select o);
						if (RemovePackage.ServiceTags != null)
							tags.AddRange(from o in RemovePackage.ServiceTags select o);
						return tags.OrderBy(t => t.Order);
					}
					return null;
			}

			return null;
		}
		/// <summary>
		/// One action must be selected
		/// </summary>
		public ServiceRequestAction SelectedAction { get; set; }
		/// <summary>
		/// Display the SR
		/// </summary>
		public IServiceRequestDto ServiceRequest { get; set; }

		/// <summary>
		/// Service Request Approval
		/// </summary>
		public IApprovalDto Approval { get; set; }
		/// <summary>
		/// Currently selected index 
		/// </summary>
		public int CurrentIndex { get; set; }

		/// <summary>
		/// Return all user input data
		/// </summary>
		/// <returns></returns>
		public List<IUserInput> GetUserInputList()
		{
			{
				List<IUserInput> inputList = new List<IUserInput>();
				if (UserInputs != null)
				{
					foreach (var option in UserInputs)
					{
						inputList.AddRange(option.UserInputs.UserInputs);
					}
					inputList = inputList.GroupBy(m => m.DisplayName).Select(g=>g.First()).ToList();	//keep unique only
					return inputList;
				}
				return inputList;
			}
		}

		/// <summary>
		/// Avoid null pointer exceptions in razor
		/// </summary>
		/// <returns></returns>
		public List<ServiceOptionTag> GetOptions()
		{
			if (UserInputs == null)
				return new List<ServiceOptionTag>();
			
			return UserInputs;
		}
	
		/// <summary>
		/// calculate the up front price
		/// </summary>
		public double PriceUpFront
		{
			get
			{
				if (ServiceRequest.ServiceRequestOptions == null)
					return 0d;
				double price = 0;
				foreach (var option in ServiceRequest.ServiceRequestOptions)
				{
					price += option.Quantity*option.ServiceOption.PriceUpFront;
				}
					return price;
			}
		}
		/// <summary>
		/// Calculate the monthly price
		/// </summary>
		public double PriceMonthly
		{
			get
			{
				if (ServiceRequest.ServiceRequestOptions == null)
					return 0d;
				double price = 0;
				foreach (var option in ServiceRequest.ServiceRequestOptions)
				{
					price += option.Quantity * option.ServiceOption.PriceMonthly;
				}
				return price;
			}
		}

		/// <summary>
		/// If UI should allow SR form to b 
		/// </summary>
		public bool CanEdit { get; set; }
	}
}