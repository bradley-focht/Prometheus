using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Http;
using Common.Dto;
using Common.Enums.Entities;
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

		// GET: api/Request
		public IEnumerable<Request> Get()
		{
			var userId = Authenticate().Id;
			//Need to determine what we want
			var requests = _serviceRequestController.GetServiceRequests(userId);
			return requests.Select(x => new Request(x));
		}

		// GET: api/Request/5
		public Request Get(int id)
		{
			var userId = Authenticate().Id;
			//Need to change userId
			return new Request(_serviceRequestController.GetServiceRequest(userId, id));
		}

		// POST: api/Request
		public void Post([FromBody]Request value)
		{
			//var um = new UserManagerService(
			//	new PermissionController(),
			//	new UserController(),
			//	new RoleController(new PermissionController())
			//);
			//var srController = new ServiceRequestController(um);

			////Need to change userId
			//srController.ModifyServiceRequest(um.GuestId, value, EntityModification.Create);
		}

		// PUT: api/Request/5
		public Request Put(int id, [FromBody]string value)
		{
			var userId = Authenticate().Id;
			//Convert the string to a Request
			var request = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(value);

			//Need to change userId
			return new Request(_requestManager.ChangeRequestState(userId, id, request.State));
		}

		// DELETE: api/Request/5
		public void Delete(int id)
		{
			var userId = Authenticate().Id;
			//Convert the string to a Request
			var request = _serviceRequestController.GetServiceRequest(userId, id);

			//Need to change userId
			_serviceRequestController.ModifyServiceRequest(userId, request, EntityModification.Delete);
		}

		private IUserDto Authenticate()
		{
			var headers = HttpContext.Current.Request.Headers;
			var username = headers["Username"];
			var password = headers["Password"];
			var user = _userManager.Login(username, password);

			if (user.Id == 0)
				throw new AuthenticationException("Failed to login, please check username and password.");

			return user;
		}
	}
}
