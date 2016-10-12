using DataService.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataService.DataAccessLayer
{
	public class PrometheusContext : DbContext
	{
		public PrometheusContext() : base("PrometheusContext")
		{

		}

		public DbSet<ServiceBundle> ServiceBundles { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<ServiceRequestOption> ServiceRequestOptions { get; set; }

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Add or remove EF conventions in this function
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Tables in DB will be named as User, ServiceBundle, etc. instead of Users, ServiceBundles, etc.
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
