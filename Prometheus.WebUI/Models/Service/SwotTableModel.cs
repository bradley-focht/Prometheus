using System.Collections.Generic;
using Common.Dto;


namespace Prometheus.WebUI.Models.Service
{
    public class SwotTableModel
    {
        public int ServiceId { get; set; }


        public ICollection<IServiceSwotDto> Strengths { get; set; }
        public ICollection<IServiceSwotDto> Weaknesses { get; set; }
        public ICollection<IServiceSwotDto> Opportunities { get; set; }
        public ICollection<IServiceSwotDto> Threats { get; set; }
        

    }
}