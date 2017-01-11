using Common.Dto;
namespace Prometheus.WebUI.Models.Service
{
    public class ServiceModel
    {
        public IServiceDto Service { get; set; }
        public string SelectedSection { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
	    public int ServiceId => Service.Id;
    }
}