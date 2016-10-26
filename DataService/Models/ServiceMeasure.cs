namespace DataService.Models
{
	public class ServiceMeasure : IServiceMeasure
	{
		public int Id { get; set; }
		public int ServiceId { get; set; }
		public string Method { get; set; }
		public string Outcome { get; set; }
	}
}
