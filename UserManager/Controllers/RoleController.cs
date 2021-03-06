﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using Common.Enums.Permissions;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;

namespace UserManager.Controllers
{
	public class RoleController : EntityController<IRoleDto>, IRoleController
	{
		private readonly IPermissionController _permissionController;

		public RoleController(IPermissionController permissionController)
		{
			_permissionController = permissionController;
		}

		/// <summary>
		/// Modifies the Role in the database
		/// </summary>
		/// <param name="performingUserId">User ID for the user perfomring the modification</param>
		/// <param name="roleDto"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns></returns>
		public IRoleDto ModifyRole(int performingUserId, IRoleDto roleDto, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, roleDto, modification);
		}

		protected override IRoleDto Create(int performingUserId, IRoleDto roleDto)
		{
			using (var context = new PrometheusContext())
			{
				var role = context.Roles.Find(roleDto.Id);
				if (role != null)
				{
					throw new InvalidOperationException(string.Format("Role with ID {0} already exists.", roleDto.Id));
				}
				var savedRole = context.Roles.Add(ManualMapper.MapDtoToRole(roleDto));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapRoleToDto(savedRole);
			}
		}

		protected override IRoleDto Update(int performingUserId, IRoleDto roleDto)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.Roles.Any(x => x.Id == roleDto.Id))
				{
					throw new InvalidOperationException(string.Format("Role with ID {0} cannot be updated since it does not exist.",
						roleDto.Id));
				}
				var updatedRole = ManualMapper.MapDtoToRole(roleDto);
				context.Roles.Attach(updatedRole);
				context.Entry(updatedRole).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapRoleToDto(updatedRole);
			}
		}

		protected override IRoleDto Delete(int performingUserId, IRoleDto roleDto)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.Roles.Find(roleDto.Id);
				context.Roles.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}

		/// <summary>
		/// Adds a role to all provided users
		/// </summary>
		/// <param name="performingUserId">User adding the roles to users</param>
		/// <param name="roleDto">Role to add</param>
		/// <param name="users">Users having the role added to them</param>
		/// <returns>All Users with the Role added</returns>
		public IEnumerable<IUserDto> AddRoleToUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users)
		{
			if (_permissionController.UserHasPermission(performingUserId, UserRoleAssignment.CanAssignRoles))
			{
				using (var context = new PrometheusContext())
				{
					var role = context.Roles.Find(roleDto.Id);
					if (role == null)
						throw new EntityNotFoundException("Role to be added to users does not exist.", typeof(Role), roleDto.Id);

					var usersAdjusted = new List<User>();
					foreach (var userDto in users)
					{
						var user = context.Users.Find(userDto.Id);
						if (user == null)
							throw new EntityNotFoundException("Cannot add role to user.", typeof(User), userDto.Id);
						if (user.Roles == null)
						{
							user.Roles = new List<Role>();
						}

						if (!user.Roles.Contains(role))
						{
							context.Users.Attach(user);
							user.Roles.Add(role);
							context.Entry(user).State = EntityState.Modified;
							usersAdjusted.Add(user);
						}
					}
					context.SaveChanges(performingUserId);
					var usersAdjustedDtos = new List<IUserDto>();
					foreach (var user in usersAdjusted)
					{
						usersAdjustedDtos.Add(ManualMapper.MapUserToDto(user));
					}
					return usersAdjustedDtos;
				}
			}
			throw new PermissionException("Roles not added to the users specified.", performingUserId,
				RolePermissionAdjustment.CanAdjustRolePermissions);
		}

		/// <summary>
		/// Removes a role from all provided users if they had the role to begin with
		/// </summary>
		/// <param name="performingUserId">User removing the roles from users</param>
		/// <param name="roleDto">Role to remove</param>
		/// <param name="users">Users having the role removed from them</param>
		/// <returns>All Users with the role removed</returns>
		public IEnumerable<IUserDto> RemoveRoleFromUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users)
		{
			if (_permissionController.UserHasPermission(performingUserId, UserRoleAssignment.CanAssignRoles))
			{
				using (var context = new PrometheusContext())
				{
					var role = context.Roles.Find(roleDto.Id);
					if (role == null)
						throw new EntityNotFoundException("Role to be removed from users does not exist.", typeof(Role),
							roleDto.Id);

					var usersAdjusted = new List<User>();
					foreach (var userDto in users)
					{
						var user = context.Users.Find(userDto.Id);
						if (user == null)
							throw new EntityNotFoundException("Cannot remove role from user.", typeof(User), userDto.Id);

						if (user.Roles.Contains(role))
						{
							context.Users.Attach(user);
							user.Roles.Remove(role);
							context.Entry(user).State = EntityState.Modified;
							usersAdjusted.Add(user);
						}
					}
					context.SaveChanges(performingUserId);

					var usersAdjustedDtos = new List<IUserDto>();
					foreach (var user in usersAdjusted)
					{
						usersAdjustedDtos.Add(ManualMapper.MapUserToDto(user));
					}
					return usersAdjustedDtos;
				}
			}
			else
			{
				throw new PermissionException("Roles not removed from the users specified.", performingUserId,
					RolePermissionAdjustment.CanAdjustRolePermissions);
			}
		}

		/// <summary>
		/// Get all available roles
		/// </summary>
		/// <param name="performingUserId">user requesting the action</param>
		/// <returns></returns>
		public IEnumerable<IRoleDto> GetRoles(int performingUserId)
		{
			if (_permissionController.UserHasPermission(performingUserId, UserRoleAssignment.CanViewRoles))
			{
				using (var context = new PrometheusContext())
				{
					var roles = context.Roles;
					foreach (var role in roles)
					{
						yield return ManualMapper.MapRoleToDto(role);
					}
				}
			}
		}

		/// <summary>
		/// Retrieve a single role
		/// </summary>
		/// <param name="performingUserId">user making hte request</param>
		/// <param name="roleId">role to retrieve</param>
		/// <returns></returns>
		public IRoleDto GetRole(int performingUserId, int roleId)
		{
			if (_permissionController.UserHasPermission(performingUserId, UserRoleAssignment.CanViewRoles))
			{
				using (var context = new PrometheusContext())
				{
					return ManualMapper.MapRoleToDto(context.Roles.FirstOrDefault(r => r.Id == roleId));
				}
			}
			return null;
		}

		protected override bool UserHasPermissionToModify(int performingUserId, IRoleDto role, EntityModification modification, out object permission)
		{
			permission = RolePermissionAdjustment.CanAdjustRolePermissions;
			return _permissionController.UserHasPermission(performingUserId, (RolePermissionAdjustment)permission);
		}
	}
}