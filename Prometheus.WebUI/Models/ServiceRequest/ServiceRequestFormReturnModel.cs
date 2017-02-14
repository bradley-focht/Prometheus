﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceRequest
{
    public class ServiceRequestFormReturnModel
    {
        public int ServiceOptionId { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public int Index { get; set; }
        public ICollection<int> Options { get; set; }

	    public ServiceRequestMode Mode { get; set; }


	}
}