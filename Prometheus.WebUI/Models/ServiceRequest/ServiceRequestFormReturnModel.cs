using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceRequest
{
	/// <summary>
	/// For model binder to return a posted object
	/// </summary>
	public class ServiceRequestFormReturnModel
	{
		/// <summary>
		/// Service Request ID
		/// </summary>
		[Required]
		public int Id { get; set; }

		/// <summary>
		/// What options were selected
		/// </summary>
		public ICollection<int> Options { get; set; }
		/// <summary>
		/// How many were requested, returned in the same order as Options
		/// </summary>
		public ICollection<int> Quantity { get; set; }

        /// <summary>
        /// Index of package that was just submitted
        /// </summary>
	    public int CurrentIndex { get; set; }
	}
}