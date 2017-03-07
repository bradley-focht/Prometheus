using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Common.Utilities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.Shared;
using Prometheus.WebUI.Models.SystemAccess;
using UserManager;
using UserManager.AdService;

namespace Prometheus.WebUI.Controllers
{
	[Authorize]
	public class SystemAccessController : PrometheusController
	{
		private readonly IUserManager _userManager;
		private readonly IScriptExecutor _scriptExecutor;
		private readonly int _userPageSize;

		public SystemAccessController()
		{
			_userManager = InterfaceFactory.CreateUserManagerService();
			_userPageSize = ConfigHelper.GetPaginationSize();
			_scriptExecutor = new ScriptExecutor();
		}

		/// <summary>
		/// Returns system access home page 
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Returns default permissions and roles page for initial crud operations
		/// </summary>
		/// <returns></returns>
		public ActionResult PermissionsAndRoles()
		{
			return View("PermissionsAndRoles", new RoleDto());
		}

		/// <summary>
		/// Default page to show and edit roles
		/// </summary>
		/// <param name="id">role id</param>
		/// <returns></returns>
		public ActionResult ShowRole(int id = 0)
		{
			IRoleDto role = null;
			try
			{
				role = _userManager.GetRole(UserId, id);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
			}
			return View("PermissionsAndRoles", role);
		}

		/// <summary>
		/// Show the link list of roles
		/// </summary>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowRoles(int id = 0)
		{
			var model = new LinkListModel();
			model.Title = "Roles";
			model.AddAction = "AddRole";
			model.SelectAction = "ShowRole";

			model.SelectedItemId = id;
			try
			{
				model.ListItems = from r in _userManager.GetRoles(UserId)  /*put users & Guids into tuples */
								  select new Tuple<int, string>(r.Id, r.Name);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
			}
			return View("PartialViews/_LinkList", model);
		}

		/// <summary>
		/// Returns screen to add a new role 
		/// </summary>
		/// <returns></returns>
		public ActionResult AddRole()
		{
			return View("UpdateRole", new RoleModel { Role = new RoleDto(), Action = "Add" });
		}

		/// <summary>
		/// Retrieve a role to update
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateRole(int id)
		{
			var model = new RoleModel { Action = "Update" };
			try
			{
				model.Role = _userManager.GetRole(UserId, id);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
			}

			return View(model);
		}

		/// <summary>
		/// Save new or update existing
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveRole(RoleDto role)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save Role due to invalid data";
				if (role.Id == 0)
				{
					RedirectToAction("AddRole");
				}
				return RedirectToAction("UpdateRole", new { id = role.Id }); //return to last page
			}
			try
			{
				_userManager.ModifyRole(UserId, role, role.Id > 0 ? EntityModification.Update : EntityModification.Create);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save Role, error: {exception.Message}";
				if (role.Id == 0)
				{
					RedirectToAction("AddRole");
				}
				return RedirectToAction("ManageUsers"); //return to last page
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved Role";
			return RedirectToAction("PermissionsAndRoles");
		}

		/// <summary>
		/// Confirmation message
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteRole(int id = 0)
		{
			IRoleDto role = null;
			try
			{
				role = _userManager.GetRole(UserId, id);
				if (role == null)
					throw new Exception("Role not found");
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
			}
			ConfirmDeleteModel model = new ConfirmDeleteModel
			{
				DeleteAction = "DeleteRole",
				ReturnAction = "ShowRole",
				Name = role.Name,
				Id = role.Id
			};
			return View("ConfirmDelete", model);
		}


		/// <summary>
		/// Complete the deletion process
		/// </summary>
		/// <param name="model">all info needed to complete a deletion</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteRole(DeleteModel model)
		{
			try
			{
				_userManager.ModifyRole(UserId, new RoleDto { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete Role {model.Name}, error: {exception.Message}";
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully deleted Role";

			return RedirectToAction("PermissionsAndRoles");
		}

		/// <summary>
		/// initial screen for managing users
		/// </summary>
		/// <returns></returns>
		public ActionResult ManageUsers()
		{
			UserControlsModel controls = new UserControlsModel();
			controls.Roles = from r in _userManager.GetRoles(UserId) select new Tuple<int, string>(r.Id, r.Name);

			return View("ManageUsers", new ManageUsersModel { Controls = controls, ReturningSearch = false });
		}

		/// <summary>
		/// Apply a filter on the results
		/// </summary>
		/// <param name="id">role Id</param>
		/// <param name="pageId">page of results</param>
		/// <returns></returns>
		public ActionResult FilterByRole(int id, int pageId = 0)
		{
			UserControlsModel controls = new UserControlsModel { CurrentPage = pageId, SelectedRole = id }; /*construct model for view */
			var model = new ManageUsersModel { Controls = controls, ReturningSearch = false };
			model.Roles = _userManager.GetRoles(UserId).ToList(); //stop forgetting these roles, ok?

			controls.Roles = from r in _userManager.GetRoles(UserId) select new Tuple<int, string>(r.Id, r.Name);

			IEnumerable<IUserDto> users = new List<UserDto>();
			if (id == 0)
				users = (from u in _userManager.GetUsers(UserId) select u);
			else if (id > 0)
				users = (from u in _userManager.GetUsers(UserId) where u.Roles.Any(role => role.Id == id) select u);

			List<UserDetailsModel> modelUsers = new List<UserDetailsModel>();

			IAdSearch searcher = new AdSearch();
			foreach (var user in users)
			{
				string displayName = null; //debugging with no AD
				if (user.Name == null)
				{
					try
					{
						displayName = searcher.GetUserDisplayName(user.AdGuid);
					}
					catch
					{
						displayName = user.AdGuid.ToString();
					}
				}
				else
				{
					displayName = user.Name;
				}
				

				modelUsers.Add(new UserDetailsModel { UserDto = user, DisplayName = displayName });
			}
			modelUsers = modelUsers.OrderBy(o => o.DisplayName).ToList();

			if (modelUsers.Count() > _userPageSize)                           //pagination
			{
				model.Controls.TotalPages = ((modelUsers.Count() + _userPageSize - 1) / _userPageSize);   //# pages
				modelUsers = modelUsers.Skip(_userPageSize * pageId).Take(_userPageSize).ToList();        //contents of the page
			}
			model.Users = modelUsers;

			return View("ManageUsers", model);
		}

		/// <summary>
		/// Setup the partial View for searching AD accounts   
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SearchAdAccount(string searchString)
		{
			var model = new ManageUsersModel { ReturningSearch = true, Users = new List<UserDetailsModel>() };
			var controls = new UserControlsModel();
			model.Controls = controls;
			controls.Roles = from r in _userManager.GetRoles(UserId) select new Tuple<int, string>(r.Id, r.Name);

			List<Tuple<Guid, string>> directoryUsers;
			List<Tuple<Guid, ICollection<IRoleDto>>> existingUsers;
			try
			{
				model.Roles = _userManager.GetRoles(UserId).ToList();
				directoryUsers = _userManager.SearchUsers(searchString).ToList();
				existingUsers = (from u in _userManager.GetUsers(UserId) select new Tuple<Guid, ICollection<IRoleDto>>(u.AdGuid, u.Roles)).ToList();
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
				return View("ManageUsers", model);
			}

			foreach (var user in directoryUsers)
			{
				//other users
				model.Users.Add(new UserDetailsModel
				{
					UserDto = new UserDto { AdGuid = user.Item1 },
					DisplayName = user.Item2
				});

				var roles = (from u in existingUsers where u.Item1 == user.Item1 select u.Item2).FirstOrDefault();
				if (roles != null)
					model.Users.Last().UserDto.Roles = roles;
			}

			return View("ManageUsers", model);
		}

		/// <summary>
		/// Save user accounts and roles
		/// </summary>
		/// <param name="roles"></param>
		/// <param name="users"></param>
		/// <param name="submitButton">the calling submit button</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveUsers(ICollection<int> roles, ICollection<Guid> users, string submitButton)
		{
			if (submitButton == "Remove")
			{
				if (users != null)
				{
					List<UserDetailsModel> model = new List<UserDetailsModel>();
					IAdSearch searcher = new AdSearch();

					foreach (var user in users)
					{
						string displayName = "honey bunny"; //TODO: AD

						model.Add(new UserDetailsModel
						{
							UserDto = new UserDto { AdGuid = user },
							DisplayName = displayName
						});
					}

					return View("ConfirmDeleteUsers", model);
				}
			}

			if (users != null && roles != null && users.Any() && roles.Any())
			{
				foreach (var user in users)
				{
					IUserDto userDto = null;        //the dto, user is just the guid
					try
					{
						userDto = _userManager.GetUser(user);
					}
					catch (Exception) {/* user does not exist */}

					if (userDto == null) /* first add anyone new if not found above*/
					{
						try
						{
							userDto = _userManager.ModifyUser(UserId, new UserDto { AdGuid = user, DepartmentId = _scriptExecutor.GetUserDepartment(user) }, EntityModification.Create);
						}
						catch (Exception exception)
						{
							TempData["MessageType"] = WebMessageType.Failure;
							TempData["Message"] = $"Failed to save adding a user, error: {exception.Message}";
							return RedirectToAction("ManageUsers");
						}
					}


					//get the userId of any user
					foreach (var role in userDto.Roles) //remove all roles        
					{
						try
						{
							_userManager.RemoveRoleFromUsers(UserId, role, new List<IUserDto> { userDto });
						}
						catch (Exception) { /* ignore if user did not have role somehow */ }
					}
					//add roles
					foreach (var role in roles)
						try
						{
							var updatedUser = _userManager.AddRolesToUser(UserId, userDto.Id, new List<IRoleDto> { new RoleDto { Id = role } });
						}
						catch (Exception exception)
						{
							TempData["MessageType"] = WebMessageType.Failure;
							TempData["Message"] = $"Failed to save user changes, error: {exception.Message}";
							return RedirectToAction("ManageUsers");
						}

				}

				TempData["MessageType"] = WebMessageType.Success; //successful assumed now
				TempData["Message"] = "Successfully saved users";
			}
			return RedirectToAction("ManageUsers");
		}

		/// <summary>
		/// Confirm deletion
		/// </summary>
		/// <param name="users">users to delete</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteUsers(ICollection<Guid> users)
		{
			try
			{
				foreach (var user in users)
				{
					int id = (from u in _userManager.GetUsers(UserId) where u.AdGuid == user select u.Id).FirstOrDefault(); //need the user Id
					_userManager.ModifyUser(UserId, new UserDto { Id = id }, EntityModification.Delete);     //perform the deletion
				}
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to remove user, error: {exception.Message}";
				return RedirectToAction("ManageUsers");
			}

			TempData["MessageType"] = WebMessageType.Success; //successful assumed now
			TempData["Message"] = "Successfully removed users";
			return RedirectToAction("ManageUsers");
		}

		public ActionResult ShowQueues()
		{
			ServiceQueueModel model = new ServiceQueueModel();

			//get all queues

			return View(model);
		}

		public ActionResult AddServiceQueue()
		{
			ServiceQueueModel model = new ServiceQueueModel { EnableAdd = true };

			// get all queues again

			return View("ShowQueues", model);
		}

		/// <summary>
		/// Save changes to a queue
		/// </summary>
		/// <param name="queue">queue to add or update</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveServiceQueue(DepartmentDto queue)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save queue due to invalid data";
				return RedirectToAction("ShowQueues");
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved queue";

			return RedirectToAction("ShowQueues");
		}

		public ActionResult UpdateServiceQueue(int id)
		{
			ServiceQueueModel model = new ServiceQueueModel { SelectedQueue = new DepartmentDto { Id = id } };

			// get all queues again
			return View("ShowQueues", model);
		}
		public ActionResult ConfirmDeleteServiceQueue(int id)
		{
			DepartmentDto model = new DepartmentDto();

			// get all queues again

			return View(model);
		}
		/// <summary>
		/// Confirm delete of a queue
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteQueue(int id)
		{

			TempData["MessageType"] = WebMessageType.Success; //successful assumed now
			TempData["Message"] = "Successfully deleted queue";
			return RedirectToAction("ShowQueues");
		}

		public ActionResult UserDepartments()
		{
			return View();
		}

	}
}