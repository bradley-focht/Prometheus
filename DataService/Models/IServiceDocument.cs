using System;

namespace DataService.Models
{
    public interface IServiceDocument
    {
        string FileExtension { get; set; }
        string Filename { get; set; }
        int Id { get; set; }
        int ServiceId { get; set; }
        Guid StorageNameGuid { get; set; }
        DateTime? UploadDate { get; set; }
        string Uploader { get; set; }
    }
}