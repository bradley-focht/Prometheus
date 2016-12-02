namespace Common.Dto
{
    public interface IServiceProcessDto : ICreatedEntityDto
    {
        int Id { get; set; }
        int ServiceId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Benefits { get; set; }
        string Improvements { get; set; }

    }
}