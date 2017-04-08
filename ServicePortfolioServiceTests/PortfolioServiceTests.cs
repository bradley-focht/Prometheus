using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums.Entities;
using FakeItEasy;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;
using Xunit;

namespace ServicePortfolioServiceTests
{
	public class PortfolioServiceTests
	{
		private const int UserId = 1;
		private const int ServiceId = 1;
		private readonly int? _serviceBundleId = 1;
		private const int ServiceDocumentId = 1;

		private const string ServiceName = "FakeServiceName";

		[Fact]
		public void PortfolioService_GetService()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.GetService(ServiceId)).Returns(fakeService);

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.GetService(ServiceId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.Equal(ServiceName, result.Name);
		}

		[Fact]
		public void PortfolioService_GetServiceNamesForServiceBundle()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.GetServiceNamesForServiceBundle(_serviceBundleId.Value)).Returns(new List<Tuple<int, string>>()
			{
				new Tuple<int, string>(fakeService.Id, fakeService.Name)
			});

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.GetServiceNamesForServiceBundle(ServiceId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.Equal(ServiceId, result.First().Item1);
			Assert.Equal(ServiceName, result.First().Item2);
		}

		[Fact]
		public void PortfolioService_GetServicesForServiceBundle()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.GetServicesForServiceBundle(_serviceBundleId.Value)).Returns(new List<IServiceDto>()
			{
				fakeService
			});

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.GetServicesForServiceBundle(ServiceId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you wPortfolioService controllerant

			//Assert
			Assert.Equal(ServiceId, result.First().Id);
		}

		[Fact]
		public void PortfolioService_SetServiceBundleForServices()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.ServiceBundleId).Returns(_serviceBundleId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.SetServiceBundleForServices(UserId, _serviceBundleId, null)).Returns(new List<IServiceDto>()
			{
				fakeService
			});

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.SetServiceBundleForServices(UserId, _serviceBundleId, null);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.True(result.Any(x => x.ServiceBundleId == _serviceBundleId));
		}

		[Fact]
		public void PortfolioService_ModifyService_Create()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.ModifyService(UserId, fakeService, EntityModification.Create)).Returns(fakeService);

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.ModifyService(UserId, fakeService, EntityModification.Create);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.Equal(ServiceId, result.Id);
		}

		[Fact]
		public void PortfolioService_ModifyService_Update()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.ModifyService(UserId, fakeService, EntityModification.Update)).Returns(fakeService);

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.ModifyService(UserId, fakeService, EntityModification.Update);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.Equal(ServiceId, result.Id);
		}

		[Fact]
		public void PortfolioService_ModifyService_Delete()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.ModifyService(UserId, fakeService, EntityModification.Delete)).Returns(null);

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.ModifyService(UserId, fakeService, EntityModification.Delete);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.Null(result);
		}

		[Fact]
		public void PortfolioService_GetServiceNames()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.GetServiceNames()).Returns(new List<Tuple<int, string>>()
			{
				new Tuple<int, string>(fakeService.Id, fakeService.Name)
			});

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.GetServiceNames();

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.True(result.Any(x => x.Item1 == ServiceId && x.Item2 == ServiceName));
		}

		[Fact]
		public void PortfolioService_GetServices()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.GetServices()).Returns(new List<IServiceDto>()
			{
				fakeService
			});

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.GetServices();

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.True(result.Any(x => x.Id == ServiceId));
		}

		[Fact]
		public void PortfolioService_GetServiceDocuments()
		{
			//Set up test
			var fakeService = A.Fake<IServiceDto>();
			A.CallTo(() => fakeService.Name).Returns(ServiceName);
			A.CallTo(() => fakeService.Id).Returns(ServiceId);

			var fakeDocument = A.Fake<IServiceDocumentDto>();
			A.CallTo(() => fakeDocument.Id).Returns(ServiceDocumentId);
			A.CallTo(() => fakeDocument.ServiceId).Returns(ServiceId);
			A.CallTo(() => fakeDocument.UploadDate).Returns(DateTime.UtcNow);

			var fakeServiceController = A.Fake<IServiceController>();
			A.CallTo(() => fakeServiceController.GetServiceDocuments(ServiceId)).Returns(new List<IServiceDocumentDto>()
			{
				fakeDocument
			});

			//Do Test Action
			PortfolioService portfolioService = new PortfolioService(A.Fake<IServiceBundleController>(), fakeServiceController, A.Fake<ILifecycleStatusController>()
				, A.Fake<IServiceSwotController>(), A.Fake<ISwotActivityController>(), A.Fake<IServiceDocumentController>(), A.Fake<IServiceGoalController>()
				, A.Fake<IServiceContractController>(), A.Fake<IServiceWorkUnitController>(), A.Fake<IServiceMeasureController>(), A.Fake<IServiceOptionController>()
				, A.Fake<IServiceOptionCategoryController>(), A.Fake<IServiceProcessController>(), A.Fake<ITextInputController>(), A.Fake<ISelectionInputController>()
				, A.Fake<IScriptedSelectionInputController>(), A.Fake<IServiceRequestPackageController>());
			var result = portfolioService.GetServiceDocuments(ServiceId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want

			//Assert
			Assert.True(result.Any(x => x.Id == ServiceDocumentId));
		}
	}
}
