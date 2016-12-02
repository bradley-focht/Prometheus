using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
    public class ServiceProcessDto : IServiceProcessDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        /// <summary>
        /// Process name, should be unique in the service
        /// </summary>
        [Required(ErrorMessage = "Process name is required")]
        public string Name { get; set; }
        /// <summary>
        /// key business benefits
        /// </summary>
        [AllowHtml]
        public string Benefits { get; set; }
        /// <summary>
        /// Area of continuous improvement
        /// </summary>
        [AllowHtml]
        public string Improvements { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        
        
    }
}
