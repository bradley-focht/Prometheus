namespace Prometheus.WebUI.Models.Service
{
    public class ConfirmDeleteSectionItemModel
    {
        public ConfirmDeleteSectionItemModel()
        {
        }

        public ConfirmDeleteSectionItemModel(int id, string friendlyName, string deleteAction, string service, string section)
        {
            Id = id;
            DeleteAction = deleteAction;
            FriendlyName = friendlyName;
            Service = service;
        }

        /// <summary>
        /// Item's name to be displayed in delete confirmation message
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// Item's id to be used for delete action 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Controller action method that will be called
        /// </summary>
        public string DeleteAction { get; set; }
        /// <summary>
        /// Friendly service name to be displayed
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// Service's Id for reference in back links
        /// </summary>
        public int ServiceId { get; set; }
        /// <summary>
        /// For back link to last section viewed
        /// </summary>
        public string Section { get; set; }
    }
}