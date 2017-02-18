namespace Common.Dto
{
	public interface IServiceRequestOptionInputDto
	{
		int ServiceRequestOptionId { get; set; }

		string Value { get; set; }

		IServiceRequestOptionDto ServiceRequestOption { get; set; }
	}
}
