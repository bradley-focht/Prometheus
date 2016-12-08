using System.Collections.Generic;
using Common.Dto;


namespace Prometheus.WebUI.Models.Service
{
    public class SwotTableModel
    {
        public int ServiceId { get; set; }


        public IEnumerable<IServiceSwotDto> Strengths { get; set; }
        public IEnumerable<IServiceSwotDto> Weaknesses { get; set; }
        public IEnumerable<IServiceSwotDto> Opportunities { get; set; }
        public IEnumerable<IServiceSwotDto> Threats { get; set; }
        

    }
}