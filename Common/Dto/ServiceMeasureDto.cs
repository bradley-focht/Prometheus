namespace Common.Dto
{ 
	public class ServiceMeasureDto : IServiceMeasureDto
	{
		public int Id { get; set; }
		public int ServiceId { get; set; }
		public string Method { get; set; }
		public string Outcome { get; set; }
	}
}
