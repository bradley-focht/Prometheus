using System.Collections.Generic;

namespace Prometheus.WebUI.Models
{
	/// <summary>
	/// For use with the _ItemListView Partial View
	/// Each item in the list will have an action link on the same controller to the action/id
	/// </summary>
	public class LinkListModel
	{
		public LinkListModel() { }

		public LinkListModel(IEnumerable<KeyValuePair<int, string>> listItems, int selectedItemId, string actionName) 
		{
			ListItems = listItems;
			SelectedItemId = selectedItemId;
			ActionName = ActionName;
		}

		public IEnumerable<KeyValuePair<int, string>> ListItems { get; set; }
		public int SelectedItemId { get; set; }
		public string ActionName { get; set; }
	}
}