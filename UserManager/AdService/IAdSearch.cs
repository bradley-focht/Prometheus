using System;
using System.Collections.Generic;

namespace UserManager.AdService
{
    public interface IAdSearch
    {
        ICollection<Tuple<Guid, string>> SearchDirectoryGroups(string queryString);
        ICollection<Tuple<Guid, string>> SearchDirectoryUsers(string queryString);
    }
}