using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class OptionsTableModel
    {
        public List<ICatalogable> Options { get; set; }
        public int ServiceId { get; set; }

    }
}