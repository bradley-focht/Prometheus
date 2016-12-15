using System;
using System.ComponentModel.DataAnnotations;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class SwotActivityItemModel
    {
        public int ServiceId { get; set; }
        public ISwotActivityDto SwotActivity { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get { return SwotActivity.Name; }
            set { SwotActivity.Name = Name; }
        }
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date
        {
            get { return SwotActivity.Date;  }
            set { SwotActivity.Date = Date; }
        }

    }
}