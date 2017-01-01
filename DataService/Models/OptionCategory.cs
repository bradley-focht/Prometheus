using System.Collections.Generic;

namespace DataService.Models
{
    public class OptionCategory : IOptionCategory
    {
        public int Popularity { get; set; }
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string BusinessValue { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
    }
}
