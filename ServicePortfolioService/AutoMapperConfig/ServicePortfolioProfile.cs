using AutoMapper;
using Common.Dto;
using DataService.Models;
using System;

namespace ServicePortfolioService.AutoMapperConfig
{
	internal class ServicePortfolioProfile : Profile
	{
		public ServicePortfolioProfile()
		{
			CreateMap<IService, IServiceDto>();
			CreateMap<IServiceDto, IService>();

			CreateMap<ILifecycleStatus, ILifecycleStatusDto>();
			CreateMap<ILifecycleStatusDto, ILifecycleStatus>();

			CreateMap<IServiceBundle, IServiceBundleDto>();
			CreateMap<IServiceBundleDto, IServiceBundle>();
		}

		[Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
		protected override void Configure()
		{
			CreateMap<IService, IServiceDto>();
			CreateMap<IServiceDto, IService>();

			CreateMap<ILifecycleStatus, ILifecycleStatusDto>();
			CreateMap<ILifecycleStatusDto, ILifecycleStatus>();

			CreateMap<IServiceBundle, IServiceBundleDto>();
			CreateMap<IServiceBundleDto, IServiceBundle>();
		}
	}
}
