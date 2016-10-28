using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
    public class ServiceWorkUnitDto : IServiceWorkUnitDto
    {
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public int ServiceId { get; set; }

        //the title of a team in the company
        [Display(Name = "Work Unit")]
        public string WorkUnit { get; set; }
        
        //a manager or someone's name to contact
        public string Contact { get; set; }
        
        //a list of things that this team does to support this service
        [AllowHtml]
        public string Responsibilities { get; set; }
    }
}
