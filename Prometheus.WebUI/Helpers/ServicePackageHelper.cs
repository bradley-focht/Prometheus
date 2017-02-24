using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using ServicePortfolioService;

namespace Prometheus.WebUI.Helpers
{
	/// <summary>
	/// Service Package functions
	/// </summary>
	public class ServicePackageHelper
	{
		/// <summary>
		/// Returns a Service Package for the option if it exists or creates a new one for it and it's id
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="portfolioService"></param>
		/// <param name="optionId"></param>
		/// <returns></returns>
		public static IServiceRequestPackageDto GetPackage(int performingUserId, IPortfolioService portfolioService, int optionId)
		{
			IServiceRequestPackageDto package = null;
			try
			{
				package = portfolioService.GetServiceRequestPackagesForServiceOption(performingUserId, optionId).FirstOrDefault();
			}
			catch (Exception) { /* situation is dealt with below */}

			if (package == null) //make new default package if necessary
			{
				package = new ServiceRequestPackageDto();
				package.ServiceOptionCategoryTags = new List<IServiceOptionCategoryTagDto>();
					//it consists of just the option category
				int categoryId = portfolioService.GetServiceOption(performingUserId, optionId).ServiceOptionCategoryId;

				package.ServiceOptionCategoryTags.Add(
					new ServiceOptionCategoryTagDto
					{
						ServiceOptionCategory = portfolioService.GetServiceOptionCategory(performingUserId, categoryId), 
						ServiceOptionCategoryId = categoryId
					});
				
			}

			package.Name = package.ServiceOptionCategoryTags.First().ServiceOptionCategory.Name;
			return package;
		}

		public static IServiceRequestPackageDto GetPackage(IPortfolioService portfolioService, int? serviceOptionId)
		{
			if (serviceOptionId.HasValue)               //invalid input
			{
				return GetPackage(portfolioService, serviceOptionId.Value);
			}
				throw new Exception("Cannot retrieve package, Invalid Service Option parameter");           //you have reached a dangerous place    
		}
	}
}