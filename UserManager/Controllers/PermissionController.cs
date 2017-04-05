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
		/// <summary>
		/// Determines whether or not the supplied userID corresponds with a user that has access to the permission provided.
		/// 
		/// example use:
		//*		bool canViewServiceDetails = UserHasPermission<ServiceDetails>(userId, ServiceDetails.CanViewServiceDetails);
		/// </summary>
		/// <typeparam name="T">Permission type</typeparam>
		/// <param name="userId">ID for user to check</param>
		/// <param name="permission">Permission level to ensure user has</param>
		/// <returns></returns>
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

			if (permission is RolePermissionAdjustment)
			{
				return UserCanAccessRolePermissions(userId, (RolePermissionAdjustment)(object)permission);
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

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAccessApi(int userId, ApiAccess permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ApiAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanFulfillServiceRequests(int userId, FulfillmentAccess permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.FulfillmentAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAccessScripts(int userId, ScriptAccess permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ScriptAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanMaintainServiceCatalog(int userId, ServiceCatalogMaintenance permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServiceCatalogMaintenanceAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAccessServicePortfolio(int userId, ServicePortfolio permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServicePortfolioAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAssignRoles(int userId, UserRoleAssignment permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.UserRoleAssignmentAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAccessSupportCatalog(int userId, SupportCatalog permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.SupportCatalogAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanSubmitRequests(int userId, ServiceRequestSubmission permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServiceRequestSubmissionAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAccessServiceDetails(int userId, ServiceDetails permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ServiceDetailsAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAccessRolePermissions(int userId, RolePermissionAdjustment permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.RolePermissionAdjustmentAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanAccessBusinessCatalog(int userId, BusinessCatalog permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.BusinessCatalogAccess >= permission);
		}

		/// <summary>
		/// Checks if the User with ID provided has equal or higher permission than the permission provided
		/// </summary>
		/// <param name="userId">ID of User to check permission for</param>
		/// <param name="permission">Permission to check that User has</param>
		/// <returns></returns>
		private bool UserCanApproveServiceRequest(int userId, ApproveServiceRequest permission)
		{
			var userRoles = GetUserRoles(userId);
			return userRoles.Any(role => role.ApproveServiceRequestAccess >= permission);
		}

		/// <summary>
		/// Determines is the object passed in is an enum as well as if it is one of the defined permissions
		/// </summary>
		/// <typeparam name="T">Type to check</typeparam>
		/// <param name="en">Object to check type of</param>
		/// <returns></returns>
		private bool IsPermissionEnum<T>(T en)
		{
			return typeof(T).IsEnum &&
				   (en is ApproveServiceRequest || en is BusinessCatalog || en is RolePermissionAdjustment || en is ServiceDetails
					|| en is ServiceRequestSubmission || en is SupportCatalog || en is UserRoleAssignment || en is ServicePortfolio
					|| en is ServiceCatalogMaintenance || en is ApiAccess || en is ScriptAccess || en is FulfillmentAccess);
		}

		/// <summary>
		/// Retrieves all of the Roles attributed the the User with the ID supplied
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
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
