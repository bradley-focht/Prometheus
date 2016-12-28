﻿using DataService.Models;
using System.Collections.Generic;
using Common.Enums.Permissions;

namespace DataService.DataAccessLayer
{
	//Adjust the inherited object to match desired database behavior
	//https://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application
	public class PrometheusInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PrometheusContext>
	{
		protected override void Seed(PrometheusContext context)
		{
			//Populate Roles
			var roles = new List<Role>
			{
				new Role {Name = "Admin"},
				new Role {Name = "Manager"},
				new Role {Name = "User"},
				new Role {Name = "Peasant"}
			};
			foreach (var role in roles)
			{
				context.Roles.Add(role);
			}
			context.SaveChanges();

			//Populate Users
			var users = new List<User>
			{
				new User{RoleId = roles[0].Id},
				new User{RoleId = roles[1].Id},
				new User{RoleId = roles[2].Id},
				new User{RoleId = roles[3].Id},
			};
			foreach (var user in users)
			{
				context.Users.Add(user);
			}
			context.SaveChanges(1);

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
							Name = "Operational"
						},
						ServiceRequestOptions = new List<ServiceOption>
						{
							new ServiceOption(),
							new ServiceOption()
						}
					},
					new Service
					{
						Name = "Second Service",LifecycleStatus = new LifecycleStatus()
						{
							Name = "Chartered"
						},
						ServiceRequestOptions = new List<ServiceOption>
						{
							new ServiceOption(),
							new ServiceOption()
						}
					}
				}
			});
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
					UserRoleAssignmentAccess = UserRoleAssignment.CanAssignRoles
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
					ServiceDetailsAccess = ServiceDetails.CanEditServiceDetails
				}
			});
		}
	}
}
