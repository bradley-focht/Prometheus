using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	/// <summary>
	/// A PowerShell script that can execute within Prometheus
	/// </summary>
	public class ScriptDto : IScriptDto
	{
		[HiddenInput]
		public int Id { get; set; }

		/// <summary>
		/// General name for the script file
		/// </summary>
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		/// <summary>
		/// Description of Script functionality
		/// </summary>
		[AllowHtml]
		public string Description { get; set; }

		/// <summary>
		/// Version number of the script
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// The replacement name used in the file system
		/// </summary>
		[Display(Name = "Script File")]
		public Guid ScriptFile { get; set; }

		/// <summary>
		/// Media type of Script
		/// </summary>
		public string MimeType { get; set; }

		/// <summary>
		/// Date when file was uploaded
		/// </summary>
		public DateTime? UploadDate { get; set; }
		/*
		 * don't forget to annotate
		 */


		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
	}
}
