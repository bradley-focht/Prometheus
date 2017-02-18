using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace ServicePortfolioService.Controllers
{
	public class ServiceRequestPackageController : EntityController<IServiceRequestPackageDto>, IServiceRequestPackageController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceRequestPackageController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceRequestPackageController(int userId)
		{
			_userId = userId;
		}

		public IServiceRequestPackageDto GetServiceRequestPackage(int servicePackageId)
		{
			using (var context = new PrometheusContext())
			{
				var servicePackage = context.ServiceRequestPackages.Find(servicePackageId);
				if (servicePackage != null)
					return ManualMapper.MapServiceRequestPackageToDto(servicePackage);
				return null;
			}
		}

		public IServiceRequestPackageDto ModifyServiceRequestPackage(IServiceRequestPackageDto servicePackage, EntityModification modification)
		{
			return base.ModifyEntity(servicePackage, modification);
		}

		protected override IServiceRequestPackageDto Create(IServiceRequestPackageDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var servicePackage = context.ServiceRequestPackages.Find(entity.Id);
				if (servicePackage != null)
				{
					throw new InvalidOperationException(string.Format("Service Request Package with ID {0} already exists.", entity.Id));
				}
				var savedPackage = context.ServiceRequestPackages.Add(ManualMapper.MapDtoToServiceRequestPackage(entity));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceRequestPackageToDto(savedPackage);
			}
		}

		protected override IServiceRequestPackageDto Update(IServiceRequestPackageDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceRequestPackages.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(string.Format("Service Request Package with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedPackage = ManualMapper.MapDtoToServiceRequestPackage(entity);
				context.ServiceRequestPackages.Attach(updatedPackage);
				context.Entry(updatedPackage).State = EntityState.Modified;
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceRequestPackageToDto(updatedPackage);
			}
		}

		protected override IServiceRequestPackageDto Delete(IServiceRequestPackageDto entity)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceRequestPackages.Find(entity.Id);
				context.ServiceRequestPackages.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}

		public IEnumerable<IServiceRequestPackageDto> AllServiceRequestPackages
		{
			get
			{
				using (var context = new PrometheusContext())
				{
					foreach (var contextServiceRequestPackage in context.ServiceRequestPackages)
					{
						yield return ManualMapper.MapServiceRequestPackageToDto(contextServiceRequestPackage);
					}
				}
			}
		}

		public IEnumerable<IServiceRequestPackageDto> GetServiceRequestPackagesForServiceOption(int serviceOptionId)
		{
			using (var context = new PrometheusContext())
			{
				var option = context.ServiceOptions.Find(serviceOptionId);
				if (option == null)
					throw new InvalidOperationException(string.Format("Service Option with ID {0} does not exist. Cannot retrieve service package with option identifier {0}.", serviceOptionId));

				//All packages where the service option exists in the first category of the package
				var packages = context.ServiceRequestPackages.Where(
					x => x.ServiceOptionCategoryTags.Any(
						y => y.Order == 1 && y.ServiceOptionCategory.ServiceOptions.Any(
							z => z.Id == serviceOptionId)));

				if (!packages.Any())
					throw new InvalidOperationException(string.Format("Service Request Package with Service Option ID {0} does not exist.", serviceOptionId));

				foreach (var package in packages)
				{
					yield return ManualMapper.MapServiceRequestPackageToDto(package);
				}
			}
		}
	}
}