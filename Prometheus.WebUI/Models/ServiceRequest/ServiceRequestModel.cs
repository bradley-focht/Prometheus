using System;
using System.Collections;
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
                return 0;   //by default return an impossible option
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
		/// To display the contents
		/// </summary>
        public IServiceOptionCategoryDto OptionCategory { get; set; }

	    public IInputGroupDto UserInputs { get; set; }

        /// <summary>
        /// index, title
        /// </summary>
        public IServiceRequestPackageDto Package { get; set; }

        public IServiceRequestDto ServiceRequest { get; set; }

        public int CurrentIndex { get; set; }

		/// <summary>
		/// return a list of all user inputs
		/// </summary>
	    public IEnumerable<IUserInput> UserInputList
	    {
		    get
		    {
				List<IUserInput> inputs = new List<IUserInput>();
			    if (UserInputs != null)
			    {
				    if (UserInputs.ScriptedSelectionInputs != null)
						inputs.AddRange(from s in UserInputs.ScriptedSelectionInputs select (IUserInput)s);
					if (UserInputs.TextInputs != null)
						inputs.AddRange(from s in UserInputs.TextInputs select (IUserInput)s);
					if (UserInputs.SelectionInputs != null)
						inputs.AddRange(from s in UserInputs.SelectionInputs select (IUserInput)s);
				}
				return inputs;
			}
	    }
    }
}