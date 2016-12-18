using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;

namespace Prometheus.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        /// <summary>
        /// This is Sean's stuff that goes in here. He made the bindings 
        /// </summary>
        private void AddBindings()
        {
            kernel.Bind<IPortfolioService>().To<PortfolioService>();
            kernel.Bind<IServiceController>().To<ServiceController>();
            kernel.Bind<IServiceBundleController>().To<ServiceBundleController>();
            kernel.Bind<ILifecycleStatusController>().To<LifecycleStatusController>();
            kernel.Bind<IServiceContractController>().To<ServiceContractController>();
            kernel.Bind<IServiceDocumentController>().To<ServiceDocumentController>();
            kernel.Bind<IServiceGoalController>().To<ServiceGoalController>();
            kernel.Bind<IServiceMeasureController>().To<ServiceMeasureController>();
            kernel.Bind<IServiceSwotController>().To<ServiceSwotController>();
            kernel.Bind<IServiceWorkUnitController>().To<ServiceWorkUnitController>();
            kernel.Bind<ISwotActivityController>().To<SwotActivityController>();

        }
    }
}