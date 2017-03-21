using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using ServiceFulfillmentEngineWebJob.EntityFramework.Models;

namespace ServiceFulfillmentEngineWebJob.EntityFramework.DataAccessLayer
{
	public class ServiceFulfillmentEngineContext : DbContext
	{
		private const int NullUserId = 0;
		public ServiceFulfillmentEngineContext() : base("ServiceFulfillmentEngineContext")
		{

		}

		public DbSet<RequestFulfillment> RequestFulfillments { get; set; }
		public DbSet<RequestOptionFulfillment> RequestOptionFulfillments { get; set; }

		/// <summary>
		/// Add or remove EF conventions in this function
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Tables in DB will be named as User, ServiceBundle, etc. instead of Users, ServiceBundles, etc.
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Override SaveChanges to give a better error message when there is an issue saving to the database.
		/// http://stackoverflow.com/questions/15820505/dbentityvalidationexception-how-can-i-easily-tell-what-caused-the-error
		/// </summary>
		/// <returns></returns>
		public int SaveChanges(int userId = NullUserId)
		{
			try
			{
				AddMetadata(userId);
				return base.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				// Retrieve the error messages as a list of strings.
				var errorMessages = ex.EntityValidationErrors
					.SelectMany(x => x.ValidationErrors)
					.Select(x => x.ErrorMessage);

				// Join the list to a single string.
				var fullErrorMessage = string.Join("; ", errorMessages);

				// Combine the original exception message with the new one.
				var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

				// Throw a new DbEntityValidationException with the improved exception message.
				throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
			}
		}

		/// <summary>
		/// Sets the metadata fields such as the updated / created date and user
		/// </summary>
		/// <param name="userId"></param>
		private void AddMetadata(int userId)
		{
			var createdEntitiesChanged = ChangeTracker.Entries().Where(x => x.Entity is ICreatedEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

			foreach (var entity in createdEntitiesChanged)
			{
				if (entity.State == EntityState.Added)
				{
					((ICreatedEntity)entity.Entity).DateCreated = DateTime.UtcNow;
					((ICreatedEntity)entity.Entity).CreatedByUserId = userId;
				}

				((ICreatedEntity)entity.Entity).DateUpdated = DateTime.UtcNow;
				((ICreatedEntity)entity.Entity).UpdatedByUserId = userId;
			}
		}
	}
}