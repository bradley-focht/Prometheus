using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums;
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
		/// <param name="action">Add or Remove</param>
		/// <returns></returns>
		public static IServiceRequestPackageDto GetPackage(int performingUserId, IPortfolioService portfolioService, int optionId, ServiceRequestAction action)
		{
			IServiceRequestPackageDto package = null;
			try
			{
				package = portfolioService.GetServiceRequestPackagesForServiceOption(performingUserId, optionId, action).FirstOrDefault();
			}
			catch (Exception) { /* situation is dealt with below */}

			return package;
		}

		/// <summary>
		/// Make a default package when none exists
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="portfolioService"></param>
		/// <param name="optionId"></param>
		/// <returns></returns>
		public static IServiceRequestPackageDto GetPackage(int performingUserId, IPortfolioService portfolioService,
			int optionId)
		{
			IServiceRequestPackageDto package = null;
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
				package.Name = package.ServiceOptionCategoryTags.First().ServiceOptionCategory.Name;
			return package;
		}

		/// <summary>
		/// deal with the nullable int id
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="portfolioService"></param>
		/// <param name="serviceOptionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static IServiceRequestPackageDto GetPackage(int userId, IPortfolioService portfolioService, int? serviceOptionId, ServiceRequestAction action)	//overload
		{
			if (serviceOptionId.HasValue)               //invalid input
			{

				return GetPackage(userId, portfolioService, serviceOptionId.Value, action);
			}
				throw new Exception("Cannot retrieve package, Invalid Service Option parameter");           //you have reached a dangerous place    
		}

		public static IServiceRequestPackageDto GetPackage(int userId, IPortfolioService portfolioService, int? serviceOptionId)   //overload
		{
			if (serviceOptionId.HasValue)               //invalid input
			{

				return GetPackage(userId, portfolioService, serviceOptionId.Value);
			}
			throw new Exception("Cannot retrieve package, Invalid Service Option parameter");           //you have reached a dangerous place    
		}
	}
}