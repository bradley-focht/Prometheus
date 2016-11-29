using System;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceSectionModel
    {
        public string Section { get; set; }
        public IServiceDto Service;

        /// <summary>
        /// Use either the Id for an int or Guid, whichever is relavent to the section item
        /// </summary>
        public int SectionItemId { get; set; }
        public Guid SectionItemGuid { get; set; }
    }
}