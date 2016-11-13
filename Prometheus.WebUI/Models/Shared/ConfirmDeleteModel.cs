namespace Prometheus.WebUI.Models.Shared
{
    /// <summary>
    /// Used to send data to the generic confirm delete partial view
    /// </summary>
	public class ConfirmDeleteModel
	{      
		public int Id { get; set; }
		public string Name { get; set; }
	    public string ControllerName { get; set; }
		public string DeleteAction { get; set; }
		public string CancelAction { get; set;  }
	}
}