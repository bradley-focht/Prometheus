using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
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
				if (ServiceRequest.ServiceOptionId != null) return (int) ServiceRequest.ServiceOptionId;
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
		[Required(ErrorMessage = "At least one rrequestee is required")]
		public IEnumerable<string> Requestees { get; set; }

		/// <summary>
		/// Requested Date
		/// </summary>
		[Required(ErrorMessage = "Requested date is required")]
		public DateTime RequestedDate => ServiceRequest.RequestedForDate;

		public string Comments => ServiceRequest.Comments;
		public string OfficeUse => ServiceRequest.Officeuse;

		/// <summary>
		/// To display the list of options
		/// </summary>
		public IServiceOptionCategoryDto OptionCategory { get; set; }

		/// <summary>
		/// Display the list of inputs
		/// </summary>
		public List<ServiceOptionTag> UserInputs { get; set; }

		/// <summary>
		/// index, title
		/// </summary>
		public IServiceRequestPackageDto Package { get; set; }

		/// <summary>
		/// Display the SR
		/// </summary>
		public IServiceRequestDto ServiceRequest { get; set; }

		/// <summary>
		/// Currently selected index 
		/// </summary>
		public int CurrentIndex { get; set; }

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
	}
}