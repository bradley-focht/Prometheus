namespace DataService.Models
{
	public interface IServiceRequestOptionScriptedSelectionInput : IUserCreatedEntity, IServiceRequestOptionInput
	{
		int Id { get; set; }

		int ScriptedSelectionInputId { get; set; }

		ScriptedSelectionInput ScriptedSelectionInput { get; set; }
	}
}