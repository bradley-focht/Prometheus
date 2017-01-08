using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
    public class ServiceDocumentDto : IServiceDocumentDto
    {
        [HiddenInput]
        public int Id { get; set; }
        public int ServiceId { get; set; }

        /// <summary>
        /// Original file name that was used when file was uploaded
        /// </summary>
        [Required(ErrorMessage = "Filename is required")]
        public string Filename { get; set; }
        /// <summary>
        /// original extension 
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// the replacement name used in the file system
        /// </summary>
        public Guid StorageNameGuid { get; set; }
        
        /// <summary>
        /// date uploaded to the system
        /// </summary>
        public DateTime? UploadDate { get; set; }
        /// <summary>
        /// sid of who did the uploading
        /// </summary>
        public string Uploader { get; set; }

		/// <summary>
		/// Save MIME type once so it doesn't have to be generated later
		/// </summary>
	    public string MimeType { get; set; }
    }
}
