using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;
using FakeItEasy;
using ServicePortfolioService.Controllers;
using Xunit;

namespace ServicePortfolioServiceTests.Controllers
{
	public class ServiceControllerTests
	{
		private const int UserId = 1;

		private const string ServiceName = "FakeServiceName";
		private const string BundleName = "FakeBundleName";
		private const string LifecycleStatusName = "FakeStatusName";

		[Fact]
		public void ServiceController_GetService()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int serviceId = CreateFakeService(ServiceName, statusId);

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.GetService(serviceId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(serviceId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.Equal(ServiceName, result.Name);
		}

		[Fact]
		public void ServiceController_GetServiceNamesForServiceBundle()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int bundleId = CreateFakeServiceBundle(BundleName);
			int serviceId = CreateFakeService(ServiceName, statusId, bundleId);

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.GetServiceNamesForServiceBundle(bundleId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(serviceId);
			RemoveFakeServiceBundle(bundleId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.Equal(serviceId, result.First().Item1);
			Assert.Equal(ServiceName, result.First().Item2);
		}

		[Fact]
		public void ServiceController_GetServicesForServiceBundle()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int bundleId = CreateFakeServiceBundle(BundleName);
			int serviceId = CreateFakeService(ServiceName, statusId, bundleId);

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.GetServicesForServiceBundle(bundleId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(serviceId);
			RemoveFakeServiceBundle(bundleId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.Equal(serviceId, result.First().Id);
		}

		[Fact]
		public void ServiceController_SetServiceBundleForServices()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int bundleId = CreateFakeServiceBundle(BundleName);
			int serviceId = CreateFakeService(ServiceName, statusId, bundleId);

			IEnumerable<IServiceDto> services;

			using (var context = new PrometheusContext())
			{
				var service = context.Services.Find(serviceId);
				services = new List<IServiceDto>()
				{
					ManualMapper.MapServiceToDto(service)
				};
			}

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.SetServiceBundleForServices(UserId, bundleId, services);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(serviceId);
			RemoveFakeServiceBundle(bundleId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.Equal(bundleId, result.First().ServiceBundleId);
		}

		[Fact]
		public void ServiceController_ModifyService_Create()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			IServiceDto fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.LifecycleStatusId).Returns(statusId);

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.ModifyService(UserId, fakeService, EntityModification.Create);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(result.Id);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.NotEqual(fakeService.Id, result.Id);
		}

		[Fact]
		public void ServiceController_ModifyService_Update()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int serviceId = CreateFakeService(ServiceName, statusId);
			IServiceDto fakeService;
			using (var context = new PrometheusContext())
			{
				var service = context.Services.Find(serviceId);
				fakeService = ManualMapper.MapServiceToDto(service);
			}

			//Do Test Action
			string originalDescription = fakeService.Description;
			string fakeDescription = "Updated fake description";
			fakeService.Description = fakeDescription;

			ServiceController controller = new ServiceController();
			var result = controller.ModifyService(UserId, fakeService, EntityModification.Update);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(serviceId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.NotEqual(originalDescription, result.Description);
			Assert.Equal(fakeDescription, result.Description);
		}

		[Fact]
		public void ServiceController_ModifyService_Delete()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int serviceId = CreateFakeService(ServiceName, statusId);
			IServiceDto fakeService;
			using (var context = new PrometheusContext())
			{
				var service = context.Services.Find(serviceId);
				fakeService = ManualMapper.MapServiceToDto(service);
			}

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.ModifyService(UserId, fakeService, EntityModification.Delete);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			//This test cleans service up if it works
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			bool serviceExists;
			using (var context = new PrometheusContext())
			{
				serviceExists = context.Services.Any(x => x.Id == serviceId);
			}
			Assert.Equal(false, serviceExists);
		}

		[Fact]
		public void ServiceController_GetServiceNames()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int serviceId = CreateFakeService(ServiceName, statusId);

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.GetServiceNames();

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(serviceId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.Equal(true, result.Any(x => x.Item1 == serviceId && x.Item2 == ServiceName));
		}

		[Fact]
		public void ServiceController_GetServices()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int serviceId = CreateFakeService(ServiceName, statusId);

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.GetServices();

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeService(serviceId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.Equal(true, result.Any(x => x.Id == serviceId));
		}

		[Fact]
		public void ServiceController_GetDocuments()
		{
			//Set up test
			int statusId = CreateFakeLifecycleStatus(LifecycleStatusName);
			int serviceId = CreateFakeService(ServiceName, statusId);
			int documentId = CreateFakeDocument(serviceId);

			//Do Test Action
			ServiceController controller = new ServiceController();
			var result = controller.GetServiceDocuments(serviceId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeDocument(documentId);
			RemoveFakeService(serviceId);
			RemoveFakeLifecycleStatus(statusId);

			//Assert
			Assert.Equal(true, result.Any(x => x.Id == documentId));
		}

		/// <summary>
		/// Makes a fake Service in the DB and returns its ID.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="lifecycleStatusId">Lifecycle status to link to</param>
		/// <param name="bundleId">Service Bundle to link to</param>
		/// <returns></returns>
		private int CreateFakeService(string name, int lifecycleStatusId, int? bundleId = null)
		{
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(name);
			A.CallTo(() => fakeService.LifecycleStatusId).Returns(lifecycleStatusId);
			A.CallTo(() => fakeService.ServiceBundleId).Returns(bundleId);

			using (var context = new PrometheusContext())
			{
				var service = ManualMapper.MapDtoToService(fakeService);
				context.Services.Add(service);
				context.SaveChanges();

				return service.Id;
			}
		}

		private int CreateFakeServiceBundle(string name)
		{
			var fakeBundle = A.Fake<IServiceBundleDto>();
			A.CallTo(() => fakeBundle.Name).Returns(name);

			using (var context = new PrometheusContext())
			{
				var bundle = ManualMapper.MapDtoToServiceBundle(fakeBundle);
				context.ServiceBundles.Add(bundle);
				context.SaveChanges();

				return bundle.Id;
			}
		}

		private int CreateFakeLifecycleStatus(string name)
		{
			var fakeStatus = A.Fake<ILifecycleStatusDto>();
			A.CallTo(() => fakeStatus.Name).Returns(name);

			using (var context = new PrometheusContext())
			{
				var status = ManualMapper.MapDtoToLifecycleStatus(fakeStatus);
				context.LifecycleStatuses.Add(status);
				context.SaveChanges();

				return status.Id;
			}
		}

		/// <summary>
		/// Makes a fake Service Document in the DB and returns its ID.
		/// </summary>
		/// <param name="serviceId">Service to link to</param>
		/// <returns></returns>
		private int CreateFakeDocument(int serviceId)
		{
			var fakeDocument = A.Fake<IServiceDocumentDto>();
			A.CallTo(() => fakeDocument.ServiceId).Returns(serviceId);
			A.CallTo(() => fakeDocument.UploadDate).Returns(DateTime.UtcNow);

			using (var context = new PrometheusContext())
			{
				var document = ManualMapper.MapDtoToServiceDocument(fakeDocument);
				context.ServiceDocuments.Add(document);
				context.SaveChanges();

				return document.Id;
			}
		}

		private void RemoveFakeService(int serviceId)
		{
			using (var context = new PrometheusContext())
			{
				var service = context.Services.Find(serviceId);
				context.Services.Remove(service);
				context.SaveChanges();
			}
		}

		private void RemoveFakeServiceBundle(int bundleId)
		{
			using (var context = new PrometheusContext())
			{
				var bundle = context.ServiceBundles.Find(bundleId);
				context.ServiceBundles.Remove(bundle);
				context.SaveChanges();
			}
		}

		private void RemoveFakeLifecycleStatus(int lifecycleStatusId)
		{
			using (var context = new PrometheusContext())
			{
				var status = context.LifecycleStatuses.Find(lifecycleStatusId);
				context.LifecycleStatuses.Remove(status);
				context.SaveChanges();
			}
		}

		private void RemoveFakeDocument(int documentId)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.ServiceDocuments.Find(documentId);
				context.ServiceDocuments.Remove(document);
				context.SaveChanges();
			}
		}
	}
}
