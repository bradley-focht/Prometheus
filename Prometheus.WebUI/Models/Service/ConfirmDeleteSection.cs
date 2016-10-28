namespace Prometheus.WebUI.Models.Service
{
    public class ConfirmDeleteSection
    {
        public ConfirmDeleteSection(int id, string friendlyName, string section, string action)
        {
            Id = id;
            Section = section;
            Action = action;
            FriendlyName = friendlyName;
        }

        public string FriendlyName { get; set; }
        public int Id { get; set; }
        public string Section { get; set; }
        public string Action { get; set; }
    }
}