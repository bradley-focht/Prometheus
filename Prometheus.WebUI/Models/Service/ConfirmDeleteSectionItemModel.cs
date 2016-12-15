namespace Prometheus.WebUI.Models.Service
{
    public class ConfirmDeleteSectionItemModel
    {
        /// <summary>
        /// Item's name to be displayed in delete confirmation message
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Item's id to be used for delete action 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Items with a deeper hierarchy may need a second level of Id
        /// </summary>
        public int AltId { get; set; }
        public string AltName { get; set; }
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