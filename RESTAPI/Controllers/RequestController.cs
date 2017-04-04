using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Http;
using Common.Dto;
using Common.Enums.Entities;
using Common.Enums.Permissions;
using Common.Exceptions;
using RequestService;
using RequestService.Controllers;
using RESTAPI.Models;
using UserManager;


namespace RESTAPI.Controllers
{
	public class RequestController : ApiController
	{
		private readonly IUserManager _userManager;
		private readonly IRequestManager _requestManager;
		private readonly IServiceRequestController _serviceRequestController;

		public RequestController(IUserManager userManager, IRequestManager requestManager, IServiceRequestController serviceRequestController)
		{
			_userManager = userManager;
			_requestManager = requestManager;
			_serviceRequestController = serviceRequestController;
		}

		/// <summary>
		/// GET: /Request
		/// 
		/// Retrieves a list of all SRs that the User making the call can see and returns them.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Request> Get()
		{
			var userId = Authenticate().Id;

			IEnumerable<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>> requests =
				new List<IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>>();

			//Need to determine what we want
			if (_userManager.UserHasPermission(userId, ApiAccess.FullApiAccess))
			{
				requests = _serviceRequestController.GetServiceRequests(userId);
			}
			else if (_userManager.UserHasPermission(userId, ApiAccess.OnlyUsersRequests))
			{
				requests = _serviceRequestController.GetServiceRequests(userId).Where(x => x.ApproverUserId == userId || x.RequestedByUserId == userId);
			}

			return requests.Select(x => new Request(x));
		}

		/// <summary>
		/// GET: /Request/{id}
		/// 
		/// Retrieves an SR that matches the ID provided in the URI 
		/// </summary>
		/// <returns></returns>
		public Request Get(int id)
		{
			var userId = Authenticate().Id;

			var request = _serviceRequestController.GetServiceRequest(userId, id);

			//Need to determine what we want
			if (_userManager.UserHasPermission(userId, ApiAccess.FullApiAccess))
			{
				return new Request(request);
			}
			else if (_userManager.UserHasPermission(userId, ApiAccess.OnlyUsersRequests))
			{
				if (request.RequestedByUserId == userId || request.ApproverUserId == userId)
					return new Request(request);
			}
			throw new PermissionException($"User is not able to access Service Request with ID {id}", userId, ApiAccess.OnlyUsersRequests);
		}

		/// <summary>
		/// POST: /Request
		/// 
		/// This call is not supported
		/// </summary>
		/// <returns></returns>
		public void Post([FromBody]Request value)
		{
			throw new InvalidOperationException("The /Request API does not support creation of Service Requests");
		}

		/// <summary>
		/// PUT: /Request/{id}
		/// 
		/// Updates the SR with the ID provided with the new values provided in the Body of the call
		/// </summary>
		/// <returns>Updated version of the Request</returns>
		public Request Put(int id, [FromBody]string value)
		{
			var userId = Authenticate().Id;

			//Convert the string to a Request
			var request = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(value);

			if (request.Id != id)
				throw new InvalidOperationException("ID in the body of the PUT request must match the ID provided in the URI");

			//Need to determine what we want
			if (_userManager.UserHasPermission(userId, ApiAccess.FullApiAccess))
			{
				return new Request(_requestManager.ChangeRequestState(userId, id, request.State));
			}
			else if (_userManager.UserHasPermission(userId, ApiAccess.OnlyUsersRequests))
			{
				if (request.RequestedByUserId == userId || request.ApproverUserId == userId)
					return new Request(_requestManager.ChangeRequestState(userId, id, request.State));
			}
			throw new PermissionException($"User is not able to access Service Request with ID {id}", userId, ApiAccess.OnlyUsersRequests);
		}

		/// <summary>
		/// DELETE: /Request/{id}
		/// 
		/// Deletes the SR with the ID provided
		/// </summary>
		/// <returns></returns>
		public void Delete(int id)
		{
			var userId = Authenticate().Id;

			var request = _serviceRequestController.GetServiceRequest(userId, id);

			//Need to determine action based on permission we want
			if (_userManager.UserHasPermission(userId, ApiAccess.FullApiAccess))
			{
				_serviceRequestController.ModifyServiceRequest(userId, request, EntityModification.Delete);
			}
			else if (_userManager.UserHasPermission(userId, ApiAccess.OnlyUsersRequests))
			{
				if (request.RequestedByUserId == userId || request.ApproverUserId == userId)
					_serviceRequestController.ModifyServiceRequest(userId, request, EntityModification.Delete);
			}
			throw new PermissionException($"User is not able to access Service Request with ID {id}", userId, ApiAccess.OnlyUsersRequests);
		}

		/// <summary>
		/// Authenticates the call being made by attempting to log into AD with the credentials provided in the Headers of the call.
		/// Headers:
		/// 	Username - AD Username
		/// 	Password - AD Password
		/// </summary>
		/// <returns></returns>
		private IUserDto Authenticate()
		{
			var headers = HttpContext.Current.Request.Headers;
			var username = headers["Username"];
			var password = headers["Password"];
			var user = _userManager.Login(username, password);

			if (user.Id == 0)
				throw new AuthenticationException("Failed to login, please check username and password.");

			if (!_userManager.UserHasPermission(user.Id, ApiAccess.OnlyUsersRequests))
				throw new PermissionException("Cannot Access REST API", user.Id, ApiAccess.OnlyUsersRequests);

			return user;
		}
	}
}
