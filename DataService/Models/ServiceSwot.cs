using System.Collections.Generic;

namespace DataService.Models
{
    class ServiceSwot
    {
        public int Id { get; set; }
        //this is either Strength, Weakness, Opportunity, or Threat
        // might be best as an enum?
        public string Type { get; set; }
   
        public string Description { get; set; }

        public IEnumerable<ISwotActivity> SwotActivities { get; set; }
    }
}
