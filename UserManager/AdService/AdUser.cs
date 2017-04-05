using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace UserManager.AdService
{
	public class AdUser : IDirectoryUser
	{
		private string _displayName;
		private Guid _guid;
		private ICollection<Guid> _groupGuids;

		public AdUser()
		{
			_groupGuids = new List<Guid>();
		}

		/// <summary>
		/// Authenticate and fill the user with contents
		/// </summary>
		/// <param name="samAccountName">AD username</param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool AuthenticateUser(string samAccountName, string password)
		{

			//TODO: Brad using "PROM" here is a workaround to the 'unloaded' exception
			using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "PROM"))
			{
				// validate the credentials
				if (!pc.ValidateCredentials(samAccountName, password))
					return false;


				//take what you need now, principal will be disposed soon
				var user = UserPrincipal.FindByIdentity(pc, samAccountName);
				if (user != null)
				{
					_displayName = user.DisplayName;
					if (user.Guid != null)
					{
						_guid = user.Guid.Value;

						foreach (var authorizationGroup in user.GetAuthorizationGroups())
						{
							if (authorizationGroup.Guid != null)
								_groupGuids.Add(authorizationGroup.Guid.Value);
						}
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Displayname is readonly
		/// </summary>
		public string DisplayName { get { return _displayName; } }

		/// <summary>
		/// AD identifier for the User
		/// </summary>
		public Guid UserGuid { get { return _guid; } }

		/// <summary>
		/// AD identifiers of all AD Groups the User is a part of
		/// </summary>
		public ICollection<Guid> GroupGuids { get { return _groupGuids; } }

	}
}
