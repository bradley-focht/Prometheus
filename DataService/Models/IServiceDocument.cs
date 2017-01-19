using System;

namespace DataService.Models
{
	public interface IServiceDocument : IUserCreatedEntity
	{
		string FileExtension { get; set; }
		string Filename { get; set; }
		string MimeType { get; set; }
		int Id { get; set; }
		int ServiceId { get; set; }
		Guid StorageNameGuid { get; set; }
		DateTime UploadDate { get; set; }
		string Uploader { get; set; }

		Service Service { get; set; }
	}
}