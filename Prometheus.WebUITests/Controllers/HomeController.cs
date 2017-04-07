using System.Security.Cryptography.X509Certificates;
using Prometheus.WebUI.Controllers;
using Xunit;
using Xunit.Sdk;

namespace Prometheus.WebUITests.Controllers
{
	public class HomeControllerTests
	{

		[Fact]
		public void CanLogin()
		{
			//arrange?
			HomeController homeController = new HomeController();
			//act?
			Assert.Equal<int>(1 + 1, 2);
			//assert

		}
	}
}