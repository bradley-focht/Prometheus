using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceFulfillmentEngineWebJob.Api.Models.Enums;
using ServiceFulfillmentEngineWebJob.EntityFramework.Models.Enums;

namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models
{
	/// <summary>
	/// Executable Scripts
	/// </summary>
	public class Script : IScript /*: ICreatedEntity */
	{
		/// <summary>
		/// An Id of sorts
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public ServiceRequestAction Action { get; set; }

		/// <summary>
		/// Friendly Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Set the relative execution order if multiple scripts apply
		/// </summary>
		public Priority Priority { get; set; }

		/// <summary>
		/// Script file name
		/// </summary>
		public string FileName { get; set; }
		/// <summary>
		/// Service Request Option Code applies to
		/// </summary>
		public string ApplicableCode { get; set; }
	}
}
