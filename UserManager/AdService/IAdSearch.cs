using System;
using System.Collections.Generic;

namespace UserManager.AdService
{
	public interface IAdSearch
	{
		/// <summary>
		/// This is similar to search users except that it does not work.
		/// </summary>
		/// <param name="queryString"></param>
		/// <returns></returns>
		ICollection<Tuple<Guid, string>> SearchDirectoryGroups(string queryString);

		/// <summary>
		/// Search AD for a list of users
		/// </summary>
		/// <param name="queryString">Display Name Query, do not append '*'</param>
		/// <returns></returns>
		ICollection<Tuple<Guid, string>> SearchDirectoryUsers(string queryString);

		/// <summary>
		/// Resolve a user Guid to a displayname
		/// </summary>
		/// <param name="userGuid"></param>
		/// <returns></returns>
		string GetUserDisplayName(Guid userGuid);

		/// <summary>
		/// Resolve a group displayname
		/// </summary>
		/// <param name="groupGuid"></param>
		/// <returns></returns>
		string GetGroupDisplayName(Guid groupGuid);
	}
}