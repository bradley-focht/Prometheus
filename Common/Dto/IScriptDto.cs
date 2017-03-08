using System;

namespace Common.Dto
{
	public interface IScriptDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }
        Guid ScriptFile { get; set; }
        string MimeType { get; set; }
        DateTime? UploadDate { get; set; }
	}
}
