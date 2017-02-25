﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;

namespace UserManager.Controllers
{
	public class UserController : EntityController<IUserDto>, IUserController
	{

		public IUserDto ModifyUser(int performingUserId, IUserDto userDto, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, userDto, modification);
		}

		public IEnumerable<IUserDto> GetUsers(int performingUserId)
		{
			//TODO: Sean -  need to check permissions...
			{

				using (var context = new PrometheusContext())
				{
					var users = context.Users;
					foreach (var user in users)
					{
						yield return ManualMapper.MapUserToDto(user);
					}
				}
			}
		}



		public IUserDto GetUser(int performingUserId, int userId)
		{
			//TODO: Sean - need to do permissions stuff here

			using (var context = new PrometheusContext())
			{
				var user = (from u in context.Users
							where u.Id == userId
							select u).FirstOrDefault();

				return ManualMapper.MapUserToDto(user);				//will return null if user not found
			}
		}

		private int _guestId;
		public int GuestId
		{
			get
			{
				if (_guestId == 0)
				{
					using (var context = new PrometheusContext())
					{
						var guest = context.Users.FirstOrDefault(x => x.Name == "Guest");
						if (guest != null)
							_guestId = guest.Id;
					}
				}
				return _guestId;
			}
		}

		private int _administratorId;
		public int AdministratorId
		{
			get
			{
				if (_administratorId == 0)
				{
					using (var context = new PrometheusContext())
					{
						var administrator = context.Users.FirstOrDefault(x => x.Name == "Administrator");
						if (administrator != null)
							_administratorId = administrator.Id;
					}
				}
				return _administratorId;
			}
		}

		protected override IUserDto Create(int performingUserId, IUserDto userDto)
		{
			using (var context = new PrometheusContext())
			{
				var user = context.Users.Find(userDto.Id);
				if (user != null)
				{
					throw new InvalidOperationException(string.Format("User with ID {0} already exists.", userDto.Id));
				}
				var savedUser = context.Users.Add(ManualMapper.MapDtoToUser(userDto));
				context.SaveChanges();
				return ManualMapper.MapUserToDto(savedUser);
			}
		}

		protected override IUserDto Update(int performingUserId, IUserDto userDto)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.Users.Any(x => x.Id == userDto.Id))
				{
					throw new InvalidOperationException(string.Format("User with ID {0} cannot be updated since it does not exist.", userDto.Id));
				}
				var updatedUser = ManualMapper.MapDtoToUser(userDto);
				context.Users.Attach(updatedUser);
				context.Entry(updatedUser).State = EntityState.Modified;
				context.SaveChanges();
				return ManualMapper.MapUserToDto(updatedUser);
			}
		}

		protected override IUserDto Delete(int performingUserId, IUserDto userDto)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.Users.Find(userDto.Id);
				context.Users.Remove(toDelete);
				context.SaveChanges();
			}
			return null;
		}

		public IEnumerable<IRoleDto> AddRolesToUser(int performingUserId, int adjustedUserId, IEnumerable<IRoleDto> rolesToAdd)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.Users.Any(x => x.Id == adjustedUserId))
					throw new EntityNotFoundException("Could not add Roles to User.", typeof(User), adjustedUserId);

				var updatedUser = context.Users.Find(adjustedUserId);
				context.Users.Attach(updatedUser);

				foreach (var role in rolesToAdd)
				{
					updatedUser.Roles.Add(ManualMapper.MapDtoToRole(role));
				}

				context.Entry(updatedUser).State = EntityState.Modified;
				context.SaveChanges();

				foreach (var updatedUserRole in updatedUser.Roles)
				{
					yield return ManualMapper.MapRoleToDto(updatedUserRole);
				}
			}
		}

		protected override bool UserHasPermissionToModify(int performingUserId, EntityModification modification, out object permission)
		{
			permission = null;
			return true;
		}
	}
}
