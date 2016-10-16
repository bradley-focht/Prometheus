using System;

namespace Prometheus.WebUI.Models.Shared
{
	public class ConfirmDeleteModel
	{
	    public ConfirmDeleteModel(int id, string name)
	    {
            Id = id;
	        Name = name;
	    }

	    public ConfirmDeleteModel(int id, string name, string deleteAction, string cancelAction, string controllerName) : this(id, name)
	    {
	        DeleteAction = deleteAction;
	        CancelAction = cancelAction;
	        ControllerName = controllerName;
	    }
        
		public int Id { get; set; }
		public string Name { get; set; }
	    public string ControllerName { get; set; }
		public string DeleteAction { get; set; }
		public string CancelAction { get; set;  }
		public string CancelArgs { get; set; }
	}
}