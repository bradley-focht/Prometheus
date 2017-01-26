namespace DataService.Models
{
	public interface IServiceRequestOptionTextInput : IUserCreatedEntity, IServiceRequestOptionInput
	{
		int Id { get; set; }
		int TextInputId { get; set; }

		TextInput TextInput { get; set; }
	}
}