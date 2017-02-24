namespace DataService.Models
{
	public interface IServiceRequestOption : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceOptionId { get; set; }
		int ServiceRequestId { get; set; }
		int Quantity { get; set; }

		int ApproverUserId { get; set; }
		int RequestedByUserId { get; set; }

		ServiceOption ServiceOption { get; set; }
		ServiceRequest ServiceRequest { get; set; }
	}
}