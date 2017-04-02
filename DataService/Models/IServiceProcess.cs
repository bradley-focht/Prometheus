using System;

namespace DataService.Models
{
	public interface IServiceProcess
	{
		/// <summary>
		/// key business benefits
		/// </summary>
		string Benefits { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int Id { get; set; }

		/// <summary>
		/// Area of continuous improvement
		/// </summary>
		string Improvements { get; set; }

		/// <summary>
		/// Process name, should be unique in the service
		/// </summary>
		string Name { get; set; }
		string Description { get; set; }
		int ServiceId { get; set; }
		string Owner { get; set; }
	}
}
