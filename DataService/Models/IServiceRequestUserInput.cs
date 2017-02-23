using Common.Enums;

namespace DataService.Models
{
	public interface IServiceRequestUserInput:IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceRequestId { get; set; }
		int InputId { get; set; }
		UserInputType UserInputType { get; set; }
		string Name { get; set; }
		string Value { get; set; }
		ServiceRequest ServiceRequest { get; set; }
	}
}
