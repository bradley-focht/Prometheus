namespace Common.Dto
{
	public interface IServiceTagDto : IServicePackageTag
	{
		int ServiceId { get; set; }
		IServiceDto Service { get; set; }
	}
}
