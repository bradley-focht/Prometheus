using System;

namespace Common.Dto
{
	/// <summary>
	/// Document that a User has attached to a Service that can be requested through Prometheus
	/// </summary>
	public interface IServiceDocumentDto
	{
		/// <summary>
		/// original extension 
		/// </summary>
		string FileExtension { get; set; }

		/// <summary>
		/// Original file name that was used when file was uploaded
		/// </summary>
		string Filename { get; set; }
		int Id { get; set; }

		/// <summary>
		/// ID of the Service that this Document is for
		/// </summary>
		int ServiceId { get; set; }

		/// <summary>
		/// the replacement name used in the file system
		/// </summary>
		Guid StorageNameGuid { get; set; }

		/// <summary>
		/// date uploaded to the system
		/// </summary>
		DateTime UploadDate { get; set; }

		/// <summary>
		/// sid of who did the uploading
		/// </summary>
		string Uploader { get; set; }

		/// <summary>
		/// Save MIME type once so it doesn't have to be generated later
		/// </summary>
		string MimeType { get; set; }
	}
}