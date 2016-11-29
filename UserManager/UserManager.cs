using System;
using System.Collections.Generic;
using Common.Dto;
using UserManager.AdService;

namespace UserManager
{
	public class UserManager
	{
		//TODO: Sean implement login
		public IUserDto Login(string username, string password)
		{
			// <hack>I'm in</hack>
			//return null;      //perhaps not

            AdUser user = new AdUser();
		    if (user.AuthenticateUser(username, password))
		    {
		        return new UserDto
		        {
		            Name = user.DisplayName,
		            //Id = user.UserGuid.ToInt(), //this doesn't seem to work... hmmm
		            Id = 0,
		            Password = "bubba lou", //maybe not a field that is needed...
		            Role = new RoleDto {Name = "God Mode"}
		        };
		    }
		    return null;
		}

	    public ICollection<Tuple<Guid, string>> SearchUsers(string searchString)
	    {
            IAdSearch userSearch = new AdSearch();   
	        return userSearch.SearchDirectoryUsers(searchString);
        }
	} 
}
