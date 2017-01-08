namespace Common.Dto
{
    public interface ICatalogPublishable
    {
        int Id { get; set; }
		int Popularity { get; set; }
        string Name { get; set; }
		string BusinessValue { get; set; }
    }
}
