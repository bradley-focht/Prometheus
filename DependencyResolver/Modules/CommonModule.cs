using Common.Dto;
using Common.Utilities;
using Ninject.Modules;

namespace DependencyResolver.Modules
{
	public class CommonModule : NinjectModule
	{
		public override void Load()
		{
			LoadDtos();
			LoadUtilities();
		}

		private void LoadDtos()
		{
			Bind<IApprovalDto>().To<ApprovalDto>();
			Bind<IDepartmentDto>().To<DepartmentDto>();
			Bind<IInputGroupDto>().To<InputGroupDto>();
			Bind<ILifecycleStatusDto>().To<LifecycleStatusDto>();
			Bind<IPriceDto>().To<PriceDto>();
			Bind<IRoleDto>().To<RoleDto>();
			Bind<IScriptDto>().To<ScriptDto>();
			Bind<IScriptedSelectionInputDto>().To<ScriptedSelectionInputDto>();
			Bind<ISelectionInputDto>().To<SelectionInputDto>();
			Bind<IServiceBundleDto>().To<ServiceBundleDto>();
			Bind<IServiceContractDto>().To<ServiceContractDto>();
			Bind<IServiceDocumentDto>().To<ServiceDocumentDto>();
			Bind<IServiceDto>().To<ServiceDto>();
			Bind<IServiceGoalDto>().To<ServiceGoalDto>();
			Bind<IServiceMeasureDto>().To<ServiceMeasureDto>();
			Bind<IServiceOptionCategoryDto>().To<ServiceOptionCategoryDto>();
			Bind<IServiceOptionCategoryTagDto>().To<ServiceOptionCategoryTagDto>();
			Bind<IServiceOptionDto>().To<ServiceOptionDto>();
			Bind<IServiceProcessDto>().To<ServiceProcessDto>();
			Bind<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>>().To<ServiceRequestDto>();
			Bind<IServiceRequestOptionDto>().To<ServiceRequestOptionDto>();
			Bind<IServiceProcessDto>().To<ServiceProcessDto>();
			Bind<IServiceRequestUserInputDto>().To<ServiceRequestUserInputDto>();
			Bind<IServiceSwotDto>().To<ServiceSwotDto>();
			Bind<IServiceTagDto>().To<ServiceTagDto>();
			Bind<IServiceWorkUnitDto>().To<ServiceWorkUnitDto>();
			Bind<ISwotActivityDto>().To<SwotActivityDto>();
			Bind<ITextInputDto>().To<TextInputDto>();
			Bind<IUserDto>().To<UserDto>();
		}

		private void LoadUtilities()
		{
			Bind<IScriptExecutor>().To<ScriptExecutor>();
		}
	}
}
