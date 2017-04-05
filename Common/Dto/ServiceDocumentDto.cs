using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	/// <summary>
	/// Document that a User has attached to a Service that can be requested through Prometheus
	/// </summary>
	public class ServiceDocumentDto : IServiceDocumentDto
	{
		[HiddenInput]
		public int Id { get; set; }

		/// <summary>
		/// ID of the Service that this Document is for
		/// </summary>
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
		public DateTime UploadDate { get; set; }

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
