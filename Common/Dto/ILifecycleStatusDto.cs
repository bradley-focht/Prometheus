namespace Common.Dto
{
	public interface ILifecycleStatusDto
	{
		bool CatalogVisible { get; set; }
		string Comment { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		int Position { get; set; }
	}
}