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
			throw new PermissionException("Cannot view Business Catalog.", requestingUserId, BusinessCatalog.CanViewCatalog);
		}

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
			throw new PermissionException("Cannot view Support Catalog.", requestingUserId, SupportCatalog.CanViewCatalog);
		}
	}
}
