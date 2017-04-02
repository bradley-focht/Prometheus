using System;

namespace DataService.Models
{
	/// <summary>
	/// A PowerShell script that can execute within Prometheus
	/// </summary>
	public interface IScript : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// General name for the script file
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Description of Script functionality
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Version number of the script
		/// </summary>
		string Version { get; set; }

		/// <summary>
		/// The replacement name used in the file system
		/// </summary>
		Guid ScriptFile { get; set; }

		/// <summary>
		/// Media type of Script
		/// </summary>
		string MimeType { get; set; }

		/// <summary>
		/// Date when file was uploaded
		/// </summary>
		DateTime? UploadDate { get; set; }
	}
}