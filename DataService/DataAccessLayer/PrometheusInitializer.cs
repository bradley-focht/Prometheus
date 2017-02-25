﻿using System.Collections.Generic;
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
			AddItilLifecycleStatus(context);

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
						LifecycleStatus = ((from c in context.LifecycleStatuses where c.Name == "Operational" select c).First())
					},
					new Service
					{
						Name = "Second Service",LifecycleStatus = ((from c in context.LifecycleStatuses where c.Name == "Operational" select c).First())
					}
				}
			});
			context.SaveChanges();

		}

		private void AddItilLifecycleStatus(PrometheusContext context)
		{
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Requirements", CatalogVisible = false, Position = 1});
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Defined", CatalogVisible = false, Position = 2 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Analyzed", CatalogVisible = false, Position = 3 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Approved", CatalogVisible = false, Position = 4 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Chartered", CatalogVisible = false, Position = 5 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Designed", CatalogVisible = false, Position = 6 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Developed", CatalogVisible = false, Position = 7 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Built", CatalogVisible = false, Position = 8 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Released", CatalogVisible = false, Position = 9 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Operational", CatalogVisible = true, Position = 10 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Test", CatalogVisible = true, Position = 11 });
			context.LifecycleStatuses.Add(new LifecycleStatus { Name = "Retired", CatalogVisible = false, Position = 9 });
			context.SaveChanges();
		}

		private void AddAdministrator(PrometheusContext context)
		{
			var admin = new User()
			{
				Name = "Administrator"
			};

			admin.Roles = new List<Role>();
			admin.Roles.Add(context.Roles.First(x => x.Name == "Administrator"));
			
			context.Users.Add(admin);
			context.SaveChanges();
		}

		private void AddGuestUser(PrometheusContext context)
		{
			var guest = new User()
			{
				Name = "Guest"
			};
			guest.Roles = new List<Role>();
			guest.Roles.Add(context.Roles.First(x=>x.Name == "Guest"));
			context.Users.Add(guest);
			context.SaveChanges();
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
