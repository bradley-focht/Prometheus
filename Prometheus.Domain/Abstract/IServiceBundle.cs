using System.Collections.Generic;
/// <summary>
/// This is just a place holder for use in the UI- delete when no longer needed
/// </summary>
namespace Prometheus.Domain.Abstract
{
	public class IServiceBundle
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string BusinessValue { get; set; }
		public string Measures { get; set; }
		public IEnumerable<IService> Services { get; set; }
	}
}
