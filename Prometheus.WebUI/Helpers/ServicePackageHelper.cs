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
        /// <param name="portfolioService"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
        public static IServiceRequestPackageDto GetPackage(IPortfolioService portfolioService, int optionId)
        {
            IServiceRequestPackageDto package = null;

            try
            {
                package = portfolioService.GetServiceRequestPackagesForServiceOption(optionId).FirstOrDefault();
            }
            catch (Exception) { /* dealt with below */}

            if (package == null)    //make new default package if necessary
            {
                package = new ServiceRequestPackageDto();
                package.ServiceOptionCategories = new List<IServiceOptionCategoryDto>();    //it consists of just the option category
                package.ServiceOptionCategories.Add(portfolioService.GetServiceOptionCategory(portfolioService.GetServiceOption(optionId).ServiceOptionCategoryId));
                package.Name = package.ServiceOptionCategories.First().Name;
            }

            return package;
        }
    }
}