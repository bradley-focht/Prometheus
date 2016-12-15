using System;
using System.ComponentModel.DataAnnotations;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class SwotActivityItemModel
    {
        public SwotActivityItemModel(SwotActivityDto activity)
        {
            SwotActivity = activity;
        }
        public ISwotActivityDto SwotActivity { get; set; }

        //used for title / backlinks
        public string Action { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public int SwotId
        {
            get { return SwotActivity.ServiceSwotId;  }
            set { SwotActivity.ServiceSwotId = SwotId; }
        }
        public string SwotName { get; set; }


    }
}