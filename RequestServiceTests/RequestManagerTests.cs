using System;
using Common.Dto;
using Common.Enums;
using Common.Enums.Permissions;
using DataService;
using DataService.DataAccessLayer;
using FakeItEasy;
using RequestService;
using UserManager.Controllers;
using Xunit;

namespace RequestServiceTests
{
	public class RequestManagerTests
	{
		private const int UserId = 1;
		private const int DepartmentId = 1;

		[Fact]
		public void RequestManager_SubmittingRequest()
		{
			//Set up test
			var fakePermissionController = A.Fake<IPermissionController>();
			A.CallTo(() => fakePermissionController.UserHasPermission(UserId, ServiceRequestSubmission.CanSubmitRequests)).Returns(true);
			int requestId = CreateFakeRequest(ServiceRequestState.Incomplete);

			//Do Test Action
			RequestManager rm = new RequestManager(fakePermissionController);
			var result = rm.SubmitRequest(UserId, requestId);

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeRequest(requestId);

			//Assert
			Assert.Equal(ServiceRequestState.Submitted, result.State);
		}

		[Fact]
		public void RequestManager_ApproveRequest()
		{
			//Set up test
			var fakePermissionController = A.Fake<IPermissionController>();
			A.CallTo(() => fakePermissionController.UserHasPermission(UserId, ApproveServiceRequest.ApproveAnyRequests)).Returns(true);
			int requestId = CreateFakeRequest(ServiceRequestState.Submitted);

			//Do Test Action
			RequestManager rm = new RequestManager(fakePermissionController);
			var result = rm.ApproveRequest(UserId, requestId, ApprovalResult.Approved, "Approved");

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeRequest(requestId);

			//Assert
			Assert.Equal(ServiceRequestState.Approved, result.State);
		}

		[Fact]
		public void RequestManager_DenyRequest()
		{
			//Set up test
			var fakePermissionController = A.Fake<IPermissionController>();
			A.CallTo(() => fakePermissionController.UserHasPermission(UserId, ApproveServiceRequest.ApproveAnyRequests)).Returns(true);
			int requestId = CreateFakeRequest(ServiceRequestState.Submitted);

			//Do Test Action
			RequestManager rm = new RequestManager(fakePermissionController);
			var result = rm.ApproveRequest(UserId, requestId, ApprovalResult.Denied, "Denied");

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeRequest(requestId);

			//Assert
			Assert.Equal(ServiceRequestState.Denied, result.State);
		}

		[Fact]
		public void RequestManager_CancelRequest()
		{
			//Set up test
			var fakePermissionController = A.Fake<IPermissionController>();
			A.CallTo(() => fakePermissionController.UserHasPermission(UserId, ServiceRequestSubmission.CanSubmitRequests)).Returns(true);
			int requestId = CreateFakeRequest(ServiceRequestState.Submitted);

			//Do Test Action
			RequestManager rm = new RequestManager(fakePermissionController);
			var result = rm.CancelRequest(UserId, requestId, "Cancelled");

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeRequest(requestId);

			//Assert
			Assert.Equal(ServiceRequestState.Cancelled, result.State);
		}

		[Fact]
		public void RequestManager_FulfillRequest()
		{
			//Set up test
			var fakePermissionController = A.Fake<IPermissionController>();
			A.CallTo(() => fakePermissionController.UserHasPermission(UserId, FulfillmentAccess.CanFulfill)).Returns(true);
			int requestId = CreateFakeRequest(ServiceRequestState.Approved);

			//Do Test Action
			RequestManager rm = new RequestManager(fakePermissionController);
			var result = rm.FulfillRequest(UserId, requestId, "Fulfilled");

			//Clean up before Assert in case the Assert Fails and you dont reach code beyond it... If you want
			RemoveFakeRequest(requestId);

			//Assert
			Assert.Equal(ServiceRequestState.Fulfilled, result.State);
		}


		private int CreateFakeRequest(ServiceRequestState state)
		{
			var fakeRequest = A.Fake<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>>();
			A.CallTo(() => fakeRequest.State).Returns(state);
			A.CallTo(() => fakeRequest.CreationDate).Returns(DateTime.UtcNow);
			A.CallTo(() => fakeRequest.RequestedForDate).Returns(DateTime.UtcNow);
			A.CallTo(() => fakeRequest.DepartmentId).Returns(DepartmentId);
			A.CallTo(() => fakeRequest.RequestedByUserId).Returns(UserId);

			using (var context = new PrometheusContext())
			{
				var request = ManualMapper.MapDtoToServiceRequest(fakeRequest);
				context.ServiceRequests.Add(request);
				context.SaveChanges();

				return request.Id;
			}
		}

		private void RemoveFakeRequest(int requestId)
		{
			using (var context = new PrometheusContext())
			{
				var request = context.ServiceRequests.Find(requestId);
				context.ServiceRequests.Remove(request);
				context.SaveChanges();
			}
		}
	}
}
