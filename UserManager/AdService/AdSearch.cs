using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace UserManager.AdService
{
    public class AdSearch : IAdSearch
    {
        /// <summary>
        /// Search AD for a list of users
        /// </summary>
        /// <param name="queryString">Display Name Query, do not append '*'</param>
        /// <returns></returns>
        public ICollection<Tuple<Guid, string>> SearchDirectoryUsers(string queryString)
        {
            var searchPrincipal = new UserPrincipal(new PrincipalContext(ContextType.Domain));
            searchPrincipal.DisplayName = queryString + "*";    // add this for easier user-searching

            var searcher = new PrincipalSearcher();
            searcher.QueryFilter = searchPrincipal;

            var results = searcher.FindAll();

            return (from result in results where result.Guid != null select new Tuple<Guid, string>(result.Guid.Value, result.DisplayName)).ToList();
        }

        /// <summary>
        /// This is similar to search users except that it does not work.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public ICollection<Tuple<Guid, string>> SearchDirectoryGroups(string queryString)
        {
            var searchPrincipal = new GroupPrincipal(new PrincipalContext(ContextType.Domain));
            var searcher = new PrincipalSearcher();

            searchPrincipal.DisplayName = queryString + "*";
            searcher.QueryFilter = searchPrincipal;

            var results = searcher.FindAll();

            return (from result in results where result.Guid != null select new Tuple<Guid, string>(result.Guid.Value, result.DisplayName)).ToList();
        }

        /// <summary>
        /// Resolve a user Guid to a displayname
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
	    public string GetUserDisplayName(Guid userGuid)
	    {
		    UserPrincipal user = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain), IdentityType.Guid, userGuid.ToString());

		    return user?.DisplayName;
	    }
    }
}
