using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public class OptionCategory : IOptionCategory
    {
        public int Popularity { get; set; }
        public int Id { get; set; }
        public int ServiceId { get; set; }
	    public string Features { get; set; }
	    public string Benefits { get; set; }
	    public string Support { get; set; }
	    public string Description { get; set; }
	    public string BusinessValue { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
	    public DateTime? DateCreated { get; set; }
	    public DateTime? DateUpdated { get; set; }
	    public int CreatedByUserId { get; set; }
	    public int UpdatedByUserId { get; set; }
    }
}
