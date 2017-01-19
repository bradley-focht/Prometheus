using System;

namespace Common.Dto
{
    public interface IServiceDocumentDto
    {
        string FileExtension { get; set; }
        string Filename { get; set; }
        int Id { get; set; }
        int ServiceId { get; set; }
        Guid StorageNameGuid { get; set; }
        DateTime UploadDate { get; set; }
        string Uploader { get; set; }
		string MimeType { get; set; }
    }
}