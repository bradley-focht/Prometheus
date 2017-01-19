using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class ServiceDocument : IServiceDocument
	{
		//PK
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }

		
		//FK
		public int ServiceId { get; set; }
		/// <summary>
		/// Store the mime type so no need to generate it later
		/// </summary>
		public string MimeType { get; set; }
		public string Filename { get; set; }
		/// <summary>
		/// File ext does not change but filename may
		/// </summary>
		public string FileExtension { get; set; }
		public Guid StorageNameGuid { get; set; }
		public DateTime UploadDate { get; set; }
		public string Uploader { get; set; }
		#region Navigation Properties
		public virtual Service Service { get; set; }
		#endregion

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
	}
}
