using System;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ScriptDto : IScriptDto
	{
		[HiddenInput]
		public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Filename { get; set; }
        public Guid ScriptFile { get; set; }
        public string MimeType { get; set; }
        public DateTime UploadDate { get; set; }
		/*
		 * don't forget to annotate
		 */


		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
	}
}
