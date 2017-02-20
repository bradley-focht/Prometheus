using System;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ScriptDto : IScriptDto
	{
		[HiddenInput]
		public int Id { get; set; }

		/*
		 * don't forget to annotate
		 */


		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
	}
}
