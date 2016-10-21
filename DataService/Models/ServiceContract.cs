using System;

namespace DataService.Models
{
    public class ServiceContract : IServiceContract
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ContractNumber { get; set; }
        public string ServiceProvider { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
