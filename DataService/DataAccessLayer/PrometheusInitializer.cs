using System.Collections.Generic;
using System.Linq;
using Common.Enums.Permissions;
using DataService.Models;

namespace DataService.DataAccessLayer
{
	//Adjust the inherited object to match desired database behavior
	//https://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application
	public class PrometheusInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PrometheusContext>
	{
		protected override void Seed(PrometheusContext context)
		{
			SeedDefaultPermissions(context);
			AddGuestUser(context);
			AddAdministrator(context);

			//Populate Users
			var users = new List<User>
			{
				new User()
			};
			foreach (var user in users)
			{
				context.Users.Add(user);
			}
			context.SaveChanges();

			//Add a sample service bundle with services and service request options
			context.ServiceBundles.Add(new ServiceBundle
			{
				Name = "First Service Bundle Name",
				Services = new List<Service>
				{
					new Service
					{
						Name = "First Service",
						LifecycleStatus = new LifecycleStatus()
						{
							CatalogVisible = true,
							Name = "Operational"
						}/*, TODO: Sean - sorry but the null references these create are a bit of an issue
						ServiceRequestOptions = new List<ServiceOption>
						{
							new ServiceOption(),
							new ServiceOption()
						} */
					},
					new Service
					{
						Name = "Second Service",LifecycleStatus = new LifecycleStatus()
						{
							Name = "Chartered"
						}/*,
						ServiceRequestOptions = new List<ServiceOption>
						{
							new ServiceOption(),
							new ServiceOption()
						} */
					}
				}
			});
			context.SaveChanges();

		}

		private void AddAdministrator(PrometheusContext context)
		{
			var admin = new User()
			{
				Name = "Administrator"
			};

			admin.Roles.Add(context.Roles.First(x => x.Name == "Administrator"));
			context.Users.Add(admin);
		}

		private void AddGuestUser(PrometheusContext context)
		{
			var guest = new User()
			{
				Name = "Guest"
			};

			guest.Roles.Add(context.Roles.First(x => x.Name == "Guest"));
			context.Users.Add(guest);
		}

		public void SeedDefaultPermissions(PrometheusContext context)
		{
			context.Roles.AddRange(new List<Role>()
			{
				new Role()
				{
					Name = "Administrator",
					ApproveServiceRequestAccess = ApproveServiceRequest.ApproveAnyRequests,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.CanViewCatalog,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.CanAdustRolePermissions,
					ServiceDetailsAccess = ServiceDetails.CanEditServiceDetails,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					UserRoleAssignmentAccess = UserRoleAssignment.CanAssignRoles,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.CanEdit,
					ServicePortfolioAccess = ServicePortfolio.CanEdit
				},
				new Role()
				{
					Name = "Service Manager",
					ApproveServiceRequestAccess = ApproveServiceRequest.NoAccess,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					SupportCatalogAccess = SupportCatalog.CanViewCatalog,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					UserRoleAssignmentAccess = UserRoleAssignment.CanAssignRoles,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceDetailsAccess = ServiceDetails.CanEditServiceDetails,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.CanEdit,
					ServicePortfolioAccess = ServicePortfolio.CanEdit
				},
				new Role()
				{
					Name = "Account Executive",
					ApproveServiceRequestAccess = ApproveServiceRequest.ApproveAnyRequests,
					UserRoleAssignmentAccess = UserRoleAssignment.CanViewRoles,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.NoAccess,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.NoAccess,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.CanView
				},
				new Role()
				{
					Name = "Service Owner",
					ApproveServiceRequestAccess = ApproveServiceRequest.NoAccess,
					UserRoleAssignmentAccess = UserRoleAssignment.CanViewRoles,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.CanViewServiceDetails,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.CanViewCatalog,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.CanView
				},new Role()
				{
					Name = "External Executive",
					ApproveServiceRequestAccess = ApproveServiceRequest.NoAccess,
					UserRoleAssignmentAccess = UserRoleAssignment.CanViewRoles,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.NoAccess,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.NoAccess,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.CanView
				},new Role()
				{
					Name = "Internal Executive",
					ApproveServiceRequestAccess = ApproveServiceRequest.NoAccess,
					UserRoleAssignmentAccess = UserRoleAssignment.CanViewRoles,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.CanViewServiceDetails,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.NoAccess,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.CanView
				},new Role()
				{
					Name = "Support Staff",
					ApproveServiceRequestAccess = ApproveServiceRequest.NoAccess,
					UserRoleAssignmentAccess = UserRoleAssignment.CanViewRoles,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.NoAccess,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.CanViewCatalog,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.NoAccess
				},new Role()
				{
					Name = "Approver",
					ApproveServiceRequestAccess = ApproveServiceRequest.ApproveMinistryRequests,
					UserRoleAssignmentAccess = UserRoleAssignment.CanViewRoles,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.NoAccess,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.NoAccess,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.NoAccess
				},new Role()
				{
					Name = "Authorized User",
					ApproveServiceRequestAccess = ApproveServiceRequest.ApproveBasicRequests,
					UserRoleAssignmentAccess = UserRoleAssignment.CanViewRoles,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.NoAccess,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.NoAccess,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.CanSubmitRequests,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.NoAccess
				},new Role()
				{
					Name = "Guest",
					ApproveServiceRequestAccess = ApproveServiceRequest.NoAccess,
					UserRoleAssignmentAccess = UserRoleAssignment.NoAccess,
					RolePermissionAdjustmentAccess = RolePermissionAdustment.NoAccess,
					ServiceDetailsAccess = ServiceDetails.NoAccess,
					BusinessCatalogAccess = BusinessCatalog.CanViewCatalog,
					SupportCatalogAccess = SupportCatalog.NoAccess,
					ServiceRequestSubmissionAccess = ServiceRequestSubmission.NoAccess,
					ServiceCatalogMaintenanceAccess = ServiceCatalogMaintenance.NoAccess,
					ServicePortfolioAccess = ServicePortfolio.NoAccess
				},
			});
			context.SaveChanges();
		}
	}
}
