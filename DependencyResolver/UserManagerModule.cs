using Common.Dto;
using Ninject.Modules;

namespace DependencyResolver
{
	public class DtoModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ILifecycleStatusDto>().To<LifecycleStatusDto>();
			Bind<IRoleDto>().To<RoleDto>();
			Bind<IServiceBundleDto>().To<ServiceBundleDto>();
			Bind<IServiceContractDto>().To<ServiceContractDto>();
			Bind<IServiceDocumentDto>().To<ServiceDocumentDto>();
			Bind<IServiceDto>().To<ServiceDto>();
			Bind<IServiceGoalDto>().To<ServiceGoalDto>();
			Bind<IServiceMeasureDto>().To<ServiceMeasureDto>();
			Bind<IServiceRequestOptionDto>().To<ServiceRequestOptionDto>();
			Bind<IServiceSwotDto>().To<ServiceSwotDto>();
			Bind<IServiceWorkUnitDto>().To<ServiceWorkUnitDto>();
			Bind<ISwotActivityDto>().To<SwotActivityDto>();
			Bind<IUserDto>().To<UserDto>();
		}
	}
}
