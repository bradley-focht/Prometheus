using Common.Enums;

namespace Common.Dto
{
	public class ServiceRequestUserInputDto : IServiceRequestUserInputDto
	{
		public int Id { get; set; }
		public int ServiceRequestId { get; set; }
		public int InputId { get; set; }
		public UserInputType UserInputType { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
}