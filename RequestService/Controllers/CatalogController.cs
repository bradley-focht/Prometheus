using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums.Entities;
using Common.Enums.Permissions;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using UserManager;

namespace RequestService.Controllers
{
	public class CatalogController : ICatalogController
	{
		private IUserManager _userManager;

		public CatalogController(IUserManager userManager)
		{
			_userManager = userManager;
		}

		/// <summary>
		/// Retrieves the list of Services that are valid for viewing in the Business Catalog
		/// provided the user has permission to view the catalog.
		/// 
		/// Throws permission exception in event that user does not have access to this catalog
		/// </summary>
		/// <param name="requestingUserId">User requesting the Catalog</param>
		/// <returns></returns>
		public IEnumerable<IServiceDto> RequestBusinessCatalog(int requestingUserId)
		{
			if (_userManager.UserHasPermission(requestingUserId, BusinessCatalog.CanViewCatalog))
			{
				using (var context = new PrometheusContext())
				{
					var services = context.Services.ToList()
						.Where(x => x.ServiceTypeRole == ServiceTypeRole.Business && x.LifecycleStatus.CatalogVisible == true);

					foreach (var service in services)
					{
						yield return ManualMapper.MapServiceToDto(service);
					}
				}
			}
			else
			{
				throw new PermissionException("Cannot view Business Catalog.", requestingUserId, BusinessCatalog.CanViewCatalog);
			}

		}

		/// <summary>
		/// Retrieves the list of Services that are valid for viewing in the Support Catalog
		/// provided the user has permission to view the catalog.
		/// 
		/// Throws permission exception in event that user does not have access to this catalog
		/// </summary>
		/// <param name="requestingUserId">User requesting the Catalog</param>
		/// <returns></returns>
		public IEnumerable<IServiceDto> RequestSupportCatalog(int requestingUserId)
		{
			if (_userManager.UserHasPermission(requestingUserId, SupportCatalog.CanViewCatalog))
			{
				using (var context = new PrometheusContext())
				{
					var services = context.Services.ToList()
						.Where(x => x.ServiceTypeRole == ServiceTypeRole.Supporting && x.LifecycleStatus.CatalogVisible == true);

					foreach (var service in services)
					{
						yield return ManualMapper.MapServiceToDto(service);
					}
				}
			}
			else
			{
				throw new PermissionException("Cannot view Support Catalog.", requestingUserId, SupportCatalog.CanViewCatalog);
			}
		}
	}
}
