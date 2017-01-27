using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;
using Ninject.Activation;

namespace Prometheus.WebUI.Models.ServiceRequest
{
    public class ServiceRequestModel
    {
        public int ServiceRequestId { get; set; }
        public string Requestor { get; set; }
        public IEnumerable<string> Requestees { get; set; }
        public string Comments { get; set; }
        public string OfficeUseComments { get; set; }


        //SR UI

        /// <summary>
        /// index, title
        /// </summary>
        public ServiceRequestPackageDto Package { get; set; }

        public int CurrentIndex { get; set; }

    }
}