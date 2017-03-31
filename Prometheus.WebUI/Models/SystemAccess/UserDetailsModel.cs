using System;
using Common.Dto;

namespace Prometheus.WebUI.Models.SystemAccess
{
	/// <summary>
	/// User DTO with the AD display name attached
	/// </summary>
    public class UserDetailsModel
    {
        public IUserDto UserDto { get; set; }
        public Guid UserGuid => UserDto.AdGuid;
        public int UserId => UserDto.Id;
        public string DisplayName { get; set; }
    }
}