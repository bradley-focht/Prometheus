using Prometheus.Domain.Abstract;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Service
{
	public class LifecycleModel
	{
		public LifecycleModel() { }
		public LifecycleModel(ILifecycleStatus currentSelection)
		{
			CurrentSelection = currentSelection;
		}
		public LifecycleModel(ILifecycleStatus currentSelection, IEnumerable<KeyValuePair<int, string>> lifecycleStatuses) : this(currentSelection)
		{		
			lifecycleStatuses = LifecycleStatuses;
		}


		public ILifecycleStatus CurrentSelection { get; set; }
		public IEnumerable<KeyValuePair<int, string>> LifecycleStatuses { get; set; }
	}
}