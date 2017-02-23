using System;
using System.IO;

namespace DataService.Models
{
	public interface IScript : IUserCreatedEntity
	{
		int Id { get; set; }
	    string Name { get; set; }
	    string Description { get; set; }
	    string Version { get; set; }
        string Filename { get; set; }
        Guid ScriptFile { get; set; }
        string MimeType { get; set; }
        DateTime UploadDate { get; set; }
	}
}