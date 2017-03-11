using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace RESTAPI.NinjectUtilities
{
	//http://www.peterprovost.org/blog/2012/06/19/adding-ninject-to-web-api
	public class NinjectDependencyScope : IDependencyScope
	{
		private IResolutionRoot _resolver;

		internal NinjectDependencyScope(IResolutionRoot resolver)
		{
			Contract.Assert(resolver != null);

			this._resolver = resolver;
		}

		public void Dispose()
		{
			IDisposable disposable = _resolver as IDisposable;
			if (disposable != null)
				disposable.Dispose();

			_resolver = null;
		}

		public object GetService(Type serviceType)
		{
			if (_resolver == null)
				throw new ObjectDisposedException("this", "This scope has already been disposed");

			return _resolver.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			if (_resolver == null)
				throw new ObjectDisposedException("this", "This scope has already been disposed");

			return _resolver.GetAll(serviceType);
		}
	}
}