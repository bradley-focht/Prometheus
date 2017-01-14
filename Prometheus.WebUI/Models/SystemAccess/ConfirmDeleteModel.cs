namespace Prometheus.WebUI.Models.SystemAccess
{
	public class ConfirmDeleteModel
	{
		/// <summary>
		/// Item's name to be displayed in delete confirmation message
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Item's id to be used for delete action 
		/// </summary>
		public int Id { get; set; }

		public string DeleteAction { get; set; }
		/// <summary>
		/// Friendly service name to be displayed
		/// </summary>
		public string ReturnAction { get; set; }

	}
}
