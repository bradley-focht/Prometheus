using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums.Permissions;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;

namespace UserManager.Controllers
{
	public class PermissionController : IPermissionController
	{
		public bool UserHasPermission<T>(int userId, T permission)
		{
			if (!IsPermissionEnum(permission))
				throw new ArgumentException("permission must be an enum");

			if (permission is ApproveServiceRequest)
			{
				//This double cast though
				return UserCanApproveServiceRequest(userId, (ApproveServiceRequest)(object)permission);
			}

			if (permission is BusinessCatalog)
			{
				return UserCanAccessBusinessCatalog(userId, (BusinessCatalog)(object)permission);
			}

			if (permission is RolePermissionAdustment)
			{
				return UserCanAccessRolePermissions(userId, (RolePermissionAdustment)(object)permission);
			}

			if (permission is ServiceDetails)
			{
				return UserCanAccessServiceDetails(userId, (ServiceDetails)(object)permission);
			}

			if (permission is ServiceRequestSubmission)
			{
				return UserCanSubmitRequests(userId, (ServiceRequestSubmission)(object)permission);
			}

			if (permission is SupportCatalog)
			{
				return UserCanAccessSupportCatalog(userId, (SupportCatalog)(object)permission);
			}

			if (permission is UserRoleAssignment)
			{
				return UserCanAssignRoles(userId, (UserRoleAssignment)(object)permission);
			}

			if (permission is ServicePortfolio)
			{
				return UserCanAccessServicePortfolio(userId, (ServicePortfolio)(object)permission);
			}

			if (permission is ServiceCatalogMaintenance)
			{
				return UserCanMaintainServiceCatalog(userId, (ServiceCatalogMaintenance)(object)permission);
			}

			if (permission is ScriptAccess)
			{
				return UserCanAccessScripts(userId, (ScriptAccess)(object)permission);
			}

			if (permission is FulfillmentAccess)
			{
				return UserCanFulfillServiceRequests(userId, (FulfillmentAccess)(object)permission);
			}

			if (permission is ApiAccess)
			{
				return UserCanAccessApi(userId, (ApiAccess)(object)permission);
			}

			return false;
		}

		private bool UserCanAccessApi(int userId, ApiAccess permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ApiAccess >= permission);
		}

		private bool UserCanFulfillServiceRequests(int userId, FulfillmentAccess permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.FulfillmentAccess >= permission);
		}

		private bool UserCanAccessScripts(int userId, ScriptAccess permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ScriptAccess >= permission);
		}

		private bool UserCanMaintainServiceCatalog(int userId, ServiceCatalogMaintenance permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServiceCatalogMaintenanceAccess >= permission);
		}

		private bool UserCanAccessServicePortfolio(int userId, ServicePortfolio permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServicePortfolioAccess >= permission);
		}

		private bool UserCanAssignRoles(int userId, UserRoleAssignment permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.UserRoleAssignmentAccess >= permission);
		}

		private bool UserCanAccessSupportCatalog(int userId, SupportCatalog permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.SupportCatalogAccess >= permission);
		}

		private bool UserCanSubmitRequests(int userId, ServiceRequestSubmission permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServiceRequestSubmissionAccess >= permission);
		}

		private bool UserCanAccessServiceDetails(int userId, ServiceDetails permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServiceDetailsAccess >= permission);
		}

		private bool UserCanAccessRolePermissions(int userId, RolePermissionAdustment permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.RolePermissionAdjustmentAccess >= permission);
		}

		private bool UserCanAccessBusinessCatalog(int userId, BusinessCatalog permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.BusinessCatalogAccess >= permission);
		}

		private bool UserCanApproveServiceRequest(int userId, ApproveServiceRequest permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ApproveServiceRequestAccess >= permission);
		}

		private bool IsPermissionEnum<T>(T en)
		{
			return typeof(T).IsEnum &&
				   (en is ApproveServiceRequest || en is BusinessCatalog || en is RolePermissionAdustment || en is ServiceDetails
					|| en is ServiceRequestSubmission || en is SupportCatalog || en is UserRoleAssignment || en is ServicePortfolio
					|| en is ServiceCatalogMaintenance || en is ApiAccess || en is ScriptAccess || en is FulfillmentAccess);
		}

		private IEnumerable<IRoleDto> GetUserRoles(int userId)
		{
			using (var context = new PrometheusContext())
			{
				var user = context.Users.Find(userId);
				if (user == null)
					throw new EntityNotFoundException("", typeof(User), userId);

				foreach (var userRole in user.Roles)
				{
					yield return ManualMapper.MapRoleToDto(userRole);
				}
			}
		}
	}
}
