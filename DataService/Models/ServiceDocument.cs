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
		/// <summary>
		/// ID of the Service that this Document is for
		/// </summary>
		public int ServiceId { get; set; }

		/// <summary>
		/// Store the mime type so no need to generate it later
		/// </summary>
		public string MimeType { get; set; }

		/// <summary>
		/// Original file name that was used when file was uploaded
		/// </summary>
		public string Filename { get; set; }

		/// <summary>
		/// File ext does not change but filename may
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
		#region Navigation Properties
		public virtual Service Service { get; set; }
		#endregion

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
	}
}
