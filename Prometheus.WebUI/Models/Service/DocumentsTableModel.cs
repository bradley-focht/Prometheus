using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
	public class DocumentsTableModel
	{
		public List<IServiceDocumentDto> Documents { get; set; }
		public int ServiceId { get; set; }

		/// <summary>
		/// Pagination 
		/// </summary>
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
	}
}