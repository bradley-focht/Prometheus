using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceRequestOption : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceOptionId { get; set; }
		int ServiceRequestId { get; set; }

		int ApproverUserId { get; set; }
		int RequestedByUserId { get; set; }

		ServiceOption ServiceOption { get; set; }
		ServiceRequest ServiceRequest { get; set; }
		ICollection<ServiceRequestOptionScriptedSelectionInput> ServiceRequestOptionScriptedSelectionInputs { get; set; }
		ICollection<ServiceRequestOptionSelectionInput> ServiceRequestOptionSelectionInputs { get; set; }
		ICollection<ServiceRequestOptionTextInput> ServiceRequestOptionTextInputs { get; set; }
	}
}