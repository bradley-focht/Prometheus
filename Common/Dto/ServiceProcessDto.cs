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
        [Display(Order = 1)]
        [Required(ErrorMessage = "Process name is required")]
        public string Name { get; set; }

		[Display(Order = 2)]
		public string Owner { get; set; }
        /// <summary>
        /// key business benefits
        /// </summary>
        [AllowHtml]
		[Display(Order = 4)]
        public string Benefits { get; set; }
        /// <summary>
        /// Area of continuous improvement
        /// </summary>
        [AllowHtml]
		[Display(Order=5)]
        public string Improvements { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        [AllowHtml]
		[Display(Order=3)]
        public string Description { get; set; }
        
        
    }
}
