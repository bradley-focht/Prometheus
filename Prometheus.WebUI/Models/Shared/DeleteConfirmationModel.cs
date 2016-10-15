/// <summary>
/// Class is used in conjuction with the DeleteConfirmation Partial View
///  intended only to give information to display a friendly confirmation and
///  where to send the delete from
/// </summary>
namespace Prometheus.WebUI.Models
{
	public class DeleteConfirmationModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DeleteAction { get; set; }
		public string CancelAction { get; set;  }
		public string CancelArgs { get; set; }
	}
}