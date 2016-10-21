namespace DataService.Models
{
    public interface IServiceWorkUnit
    {
        int Id { get; set; }
        string Manager { get; set; }
        string Responsibilities { get; set; }
        int ServiceId { get; set; }
        string WorkUnit { get; set; }
    }
}