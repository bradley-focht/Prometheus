using Common.Dto;
using Common.Enums;
using DataService;
using DataService.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using Common.Enums.Entities;

namespace RequestService
{
	public class CatalogController : ICatalogController
	{
		private int _userId;

		public CatalogController(int userId)
		{
			_userId = userId;
		}

		public IEnumerable<IServiceDto> BusinessCatalog
		{
			get
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
		}

		//TODO: Add in role check once permissions are in place
		public IEnumerable<IServiceDto> SupportCatalog
		{
			get
			{
				using (var context = new PrometheusContext())
				{
					var user = context.Users.Find(_userId);
					if (user == null)
						yield break;

					var services = context.Services.ToList()
						.Where(x => x.ServiceTypeRole == ServiceTypeRole.Supporting && x.LifecycleStatus.CatalogVisible == true);

					foreach (var service in services)
					{
						yield return ManualMapper.MapServiceToDto(service);
					}
				}
			}
		}
	}
}
