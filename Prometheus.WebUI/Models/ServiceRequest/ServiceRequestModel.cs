using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequest
{
    public class ServiceRequestModel
    {
        [HiddenInput]
        public int ServiceRequestId { get; set; }
        [HiddenInput]
        public int InitialOptionId { get; set; }

        /// <summary>
        /// who is making the request
        /// </summary>
        [Required(ErrorMessage = "Requestor is required")]
        public string Requestor { get; set; }
        /// <summary>
        /// who is this request for
        /// </summary>
        [Required(ErrorMessage = "Requestor is required")]
        public IEnumerable<string> Requestees { get; set; }

        [Required(ErrorMessage = "Requested date is required")]
        public DateTime RequestedDate { get; set; }

        public string Comments { get; set; }
        public string OfficeUseComments { get; set; }

        public ServiceOptionCategoryDto OptionCategory { get; set; }
        //SR UI

        /// <summary>
        /// index, title
        /// </summary>
        public IServiceRequestPackageDto Package { get; set; }

        public IServiceRequestDto ServiceRequest { get; set; }

        public int CurrentIndex { get; set; }

    }
}