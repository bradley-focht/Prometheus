using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	/// <summary>
	/// A PowerShell script that can execute within Prometheus
	/// </summary>
	public class Script : IScript
	{
		/// <summary>
		/// Primary Key
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		#region Fields
		/// <summary>
		/// General name for the script file
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Description of Script functionality
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Version number of the script
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// The replacement name used in the file system
		/// </summary>
		public Guid ScriptFile { get; set; }

		/// <summary>
		/// Media type of Script
		/// </summary>
		public string MimeType { get; set; }

		/// <summary>
		/// Date when file was uploaded
		/// </summary>
		public DateTime? UploadDate { get; set; }

		#endregion

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

	}
}
