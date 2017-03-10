using System.Collections.Generic;
using System.Web.Http;
using Common.Dto;
using Common.Enums.Entities;
using Common.Utilities;
using RequestService;
using RequestService.Controllers;
using RESTAPI.Models;
using UserManager;
using UserManager.Controllers;

namespace RESTAPI.Controllers
{
	public class RequestController : ApiController
	{
		// GET: api/Request
		public IEnumerable<string> Get()
		{
			//Need to determine what we want
			return new string[] { "value1", "value2" };
		}

		// GET: api/Request/5
		public IServiceRequestDto Get(int id)
		{
			var um = new UserManagerService(
				new PermissionController(),
				new UserController(),
				new RoleController(new PermissionController()),
                new ScriptExecutor(),
                new DepartmentController()
			);
			var srController = new ServiceRequestController(um);

			//Need to change userId
			return srController.GetServiceRequest(um.GuestId, id);
		}

		// POST: api/Request
		public void Post([FromBody]Request value)
		{
			var um = new UserManagerService(
				new PermissionController(),
				new UserController(),
				new RoleController(new PermissionController()),
                new ScriptExecutor(),
                new DepartmentController()
			);
			var srController = new ServiceRequestController(um);

			//Need to change userId
			srController.ModifyServiceRequest(um.GuestId, value, EntityModification.Create);
		}

		// PUT: api/Request/5
		public void Put(int id, [FromBody]string value)
		{
			var rm = new RequestManager(new PermissionController());
			var um = new UserManagerService(
				new PermissionController(),
				new UserController(),
				new RoleController(new PermissionController()),
                new ScriptExecutor(), 
                new DepartmentController()
			);

			//Convert the string to a Request
			var request = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(value);

			//Need to change userId
			rm.ChangeRequestState(um.GuestId, id, request.State);
		}

		// DELETE: api/Request/5
		public void Delete(int id)
		{
			var um = new UserManagerService(
				new PermissionController(),
				new UserController(),
				new RoleController(new PermissionController()),
                new ScriptExecutor(), 
                new DepartmentController()
			);
			var srController = new ServiceRequestController(um);

			//Convert the string to a Request
			var request = srController.GetServiceRequest(um.GuestId, id);

			//Need to change userId
			srController.ModifyServiceRequest(um.GuestId, request, EntityModification.Delete);
		}
	}
}
