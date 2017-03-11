using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DependencyResolver.Modules;
using Ninject;
using Ninject.Modules;

namespace Prometheus.WebUI.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver(IKernel kernel)
		{
			this.kernel = kernel;
			LoadModules();
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
		private void LoadModules()
		{
			var modules = new List<INinjectModule>
			{
				new CommonModule(),
				new RequestServiceModule(),
				new ServicePortfolioServiceModule(),
				new UserManagerModule()
			};

			kernel.Load(modules);
		}
	}
}