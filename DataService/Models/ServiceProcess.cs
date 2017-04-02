using System;

namespace DataService.Models
{
	public class ServiceProcess : IServiceProcess
	{
		public int Id { get; set; }
		public int ServiceId { get; set; }

		/// <summary>
		/// Process name, should be unique in the service
		/// </summary>
		public string Name { get; set; }
		public string Owner { get; set; }
		public string Description { get; set; }

		/// <summary>
		/// key business benefits
		/// </summary>
		public string Benefits { get; set; }

		/// <summary>
		/// Area of continuous improvement
		/// </summary>
		public string Improvements { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}
