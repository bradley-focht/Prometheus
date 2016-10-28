namespace Common.Dto
{
    public interface IServiceWorkUnitDto
    {
        string Contact { get; set; }
        int Id { get; set; }
        string Responsibilities { get; set; }
        int ServiceId { get; set; }
        string WorkUnit { get; set; }
    }
}