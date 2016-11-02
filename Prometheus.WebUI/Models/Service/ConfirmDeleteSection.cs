namespace Prometheus.WebUI.Models.Service
{
    public class ConfirmDeleteSection
    {
        public ConfirmDeleteSection()
        {
            
        }

        public ConfirmDeleteSection(int id, string friendlyName, string section, string action, string service)
        {
            Id = id;
            Section = section;
            Action = action;
            FriendlyName = friendlyName;
            Service = service;
        }

        public string FriendlyName { get; set; }
        public int Id { get; set; }
        public string Section { get; set; }
        public string Action { get; set; }
        public string Service { get; set; }
        public int Serviceid { get; set; }
    }
}