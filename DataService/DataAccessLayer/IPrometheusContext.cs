using System.Data.Entity;
using DataService.Models;

namespace DataService.DataAccessLayer
{
	public interface IPrometheusContext
	{
		DbSet<LifecycleStatus> LifecycleStatuses { get; set; }
		DbSet<Role> Roles { get; set; }
		DbSet<ServiceBundle> ServiceBundles { get; set; }
		DbSet<ServiceRequestOption> ServiceRequestOptions { get; set; }
		DbSet<Service> Services { get; set; }
		DbSet<User> Users { get; set; }

		int SaveChanges(int userId = 0);
	}
}