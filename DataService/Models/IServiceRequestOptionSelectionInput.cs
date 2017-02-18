namespace DataService.Models
{
	public interface IServiceRequestOptionSelectionInput : IUserCreatedEntity, IServiceRequestOptionInput
	{
		int Id { get; set; }

		int SelectionInputId { get; set; }

		SelectionInput SelectionInput { get; set; }
	}
}