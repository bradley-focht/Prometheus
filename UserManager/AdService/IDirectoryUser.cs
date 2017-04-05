using System;
using System.Collections.Generic;

namespace UserManager.AdService
{
	public interface IDirectoryUser
	{
		/// <summary>
		/// Displayname is readonly
		/// </summary>
		string DisplayName { get; }

		/// <summary>
		/// AD identifiers of all AD Groups the User is a part of
		/// </summary>
		ICollection<Guid> GroupGuids { get; }

		/// <summary>
		/// AD identifier for the User
		/// </summary>
		Guid UserGuid { get; }

		/// <summary>
		/// Authenticate and fill the user with contents
		/// </summary>
		/// <param name="samAccountName">AD username</param>
		/// <param name="password"></param>
		/// <returns></returns>
		bool AuthenticateUser(string samAccountName, string password);
	}
}