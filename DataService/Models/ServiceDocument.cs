using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Models
{
    public class ServiceDocument : IServiceDocument
    {
        //PK
        [Key]
        public int Id { get; set; }
        //FK
        
        public int ServiceId { get; set; }

        public string Filename { get; set; }
        public string FileExtension { get; set; }
        public Guid StorageNameGuid { get; set; }
        public DateTime? UploadDate { get; set; }
        public string Uploader { get; set; }
    }
}
