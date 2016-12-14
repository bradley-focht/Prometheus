using System.ComponentModel.DataAnnotations;
using DataService.Models;

namespace Prometheus.WebUI.Models.Service
{
    public class SwotActivityItemModel
    {
        public int ServiceId { get; set; }
        public ISwotActivity SwotActivity { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get { return SwotActivity.Name; }
            set { SwotActivity.Name = Name; }
        }

    }
}