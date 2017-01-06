namespace Common.Dto
{
    public interface ICatalogable
    {
        int Id { get; set; }
        int ServiceId { get; set; }
		int Popularity { get; set; }
        string Name { get; set; }
        string BusinessValue { get; set; }
    }
}
