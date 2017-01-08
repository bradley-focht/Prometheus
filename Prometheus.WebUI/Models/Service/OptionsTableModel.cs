using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class OptionsTableModel
    {
        public List<IOffering> Options { get; set; }
        public int ServiceId { get; set; }

		/// <summary>
		/// Used for net pw calculations in table view
		/// </summary>
	    public double i { get; set; }
	    public int n { get; set; }

		/// <summary>
		/// Pagination 
		/// </summary>
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }

	}
}