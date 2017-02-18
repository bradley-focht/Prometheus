using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Dto;
using Common.Enums.Entities;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;

namespace UserManager.Controllers
{
	public class UserController : UMEntityController<IUserDto>, IUserController
	{

		public IUserDto ModifyUser(int performingUserId, IUserDto userDto, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, userDto, modification);
		}

		public IEnumerable<UserDto> GetUsers(int performingUserId)
		{
			//TODO: Sean -  need to check permissions...
			{

				using (var context = new PrometheusContext())
				{
					var users = context.Users;
					foreach (var user in users)
					{
						yield return (UserDto)ManualMapper.MapUserToDto(user);
					}
				}
			}
		}



		public UserDto GetUser(int performingUserId, int userId)
		{
			//TODO: Sean - need to do permissions stuff here

			using (var context = new PrometheusContext())
			{
				var user = (from u in context.Users
							where u.Id == userId
							select u).FirstOrDefault();

				return (UserDto)ManualMapper.MapUserToDto(user);				//will return null if user not found
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
