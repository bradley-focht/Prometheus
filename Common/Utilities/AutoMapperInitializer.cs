using AutoMapper;
using System.Reflection;

namespace Common.Utilities
{
	public static class AutoMapperInitializer
	{
		/// <summary>
		/// Initializes AutoMapper with all "AutoMapper.Profile" classes in the assembly you are calling from
		/// If this doesnt work, move this class into the ServicePortfolioService.AutoMapperProfiles namespace
		/// </summary>
		public static void Initialize()
		{
			//Dear Lord I hope this works
			Mapper.Initialize(cfg => cfg.AddProfiles(Assembly.GetCallingAssembly()));
		}
	}
}
