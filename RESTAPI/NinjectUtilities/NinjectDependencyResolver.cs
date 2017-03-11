using System.Web.Http.Dependencies;
using Ninject;

namespace RESTAPI.NinjectUtilities
{
	//http://www.peterprovost.org/blog/2012/06/19/adding-ninject-to-web-api
	public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
	{
		private IKernel _kernel;

		public NinjectDependencyResolver(IKernel kernel)
			: base(kernel)
		{
			this._kernel = kernel;
		}

		public IDependencyScope BeginScope()
		{
			return new NinjectDependencyScope(_kernel.BeginBlock());
		}
	}
}