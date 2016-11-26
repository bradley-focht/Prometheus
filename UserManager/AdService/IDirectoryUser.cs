using System;
using System.Collections.Generic;

namespace UserManager.AdService
{
    public interface IDirectoryUser
    {
        string DisplayName { get; }
        ICollection<Guid> GroupGuids { get; }
        Guid UserGuid { get; }

        bool AuthenticateUser(string samAccountName, string password);
    }
}