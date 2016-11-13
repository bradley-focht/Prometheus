using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceDocument : IServiceDocument
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		//FK

		public int ServiceId { get; set; }

		public string Filename { get; set; }
		public string FileExtension { get; set; }
		public Guid StorageNameGuid { get; set; }
		public DateTime? UploadDate { get; set; }
		public string Uploader { get; set; }
		#region Navigation Properties
		public virtual Service Service { get; set; }
		#endregion
	}
}
