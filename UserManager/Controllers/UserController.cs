using System;
using Common.Dto;
using Common.Enums.Entities;

namespace UserManager.Controllers
{
	public class UserController : EntityController<IUserDto>
	{

		public IUserDto ModifyUser(IUserDto userDto, EntityModification modification)
		{
			return base.ModifyEntity(userDto, modification);
		}

		protected override IUserDto Create(IUserDto userDto)
		{
			throw new NotImplementedException();
		}

		protected override IUserDto Update(IUserDto userDto)
		{
			throw new NotImplementedException();
		}

		protected override IUserDto Delete(IUserDto userDto)
		{
			throw new NotImplementedException();
		}


	}
}
