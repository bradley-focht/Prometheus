using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Common.Utilities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.Shared;
using Prometheus.WebUI.Models.SystemAccess;
using RequestService.Controllers;
using UserManager;
using UserManager.AdService;
using UserManager.Controllers;

namespace Prometheus.WebUI.Controllers
{
	[Authorize]
	public class SystemAccessController : PrometheusController
	{
		private readonly IScriptExecutor _scriptExecutor;
		private readonly int _userPageSize;
		private readonly IDepartmentController _departmentController;

		public SystemAccessController(IUserManager userManager, IScriptExecutor scriptExecutor, IDepartmentController departmentController)
		{
			_userManager = userManager;
			_userPageSize = ConfigHelper.GetPaginationSize();
			_scriptExecutor = scriptExecutor;
			_departmentController = departmentController;
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
			RoleDto role = null;
			try
			{
				role = (RoleDto)_userManager.GetRole(UserId, id);
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
						displayName = "Name not found";
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
		/// <param name="roleIds">IDs for all roles the user is being set to</param>
		/// <param name="users"></param>
		/// <param name="submitButton">the calling submit button</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveUsers(ICollection<int> roleIds, ICollection<Guid> users, string submitButton)
		{
			if (submitButton == "Remove")
			{
				if (users != null)
				{
					List<UserDetailsModel> model = new List<UserDetailsModel>();
					IAdSearch searcher = new AdSearch();

					foreach (var user in users)
					{
						string displayName;
						try
						{
							displayName = searcher.GetUserDisplayName(user);
						}
						catch (Exception)
						{
							displayName = "Name not found";
						}

						model.Add(new UserDetailsModel
						{
							UserDto = new UserDto { AdGuid = user },
							DisplayName = displayName
						});
					}

					return View("ConfirmDeleteUsers", model);
				}
			}

			if (users != null && roleIds != null && users.Any() && roleIds.Any())
			{
				foreach (var user in users)
				{
					IUserDto userDto = null; //the dto, user is just the guid
					try
					{
						userDto = _userManager.GetUser(user);
					}
					catch (Exception)
					{
						/* user does not exist */
					}

					if (userDto == null) /* first add anyone new if not found above*/
					{
						UserDto newUser = new UserDto { AdGuid = user };
						ScriptExecutor scriptExecutor = new ScriptExecutor();
						ScriptFileController scriptController = new ScriptFileController();
						try
						{
							var scriptGuid = scriptController.GetScript(UserId, ConfigHelper.GetDepartmentScriptId()).ScriptFile;
							newUser.DepartmentId = (from d in _departmentController.GetDepartments(UserId)
													where d.Name == scriptExecutor.GetUserDepartment(user, scriptGuid)
													select d.Id).FirstOrDefault();
							userDto = _userManager.ModifyUser(UserId, newUser, EntityModification.Create);
						}
						catch (Exception exception)
						{
							TempData["MessageType"] = WebMessageType.Failure;
							TempData["Message"] = $"Failed to save adding a user, error: {exception.Message}";
							return RedirectToAction("ManageUsers");
						}
					}

					//Remove roles from the user where all the roleIds do not match the ID
					foreach (var role in userDto.Roles.Where(x => roleIds.All(y => y != x.Id)))
					{
						try
						{
							/* useless is a lazy loading work around */
							var useless = _userManager.RemoveRoleFromUsers(UserId, role, new List<IUserDto> { userDto });
							foreach (var unused in useless)
							{
								/* do nothing */
							}
						}
						catch (Exception)
						{
							/* ignore if user did not have role somehow */
						}
					}
					//add roles
					foreach (var roleId in roleIds)
						try
						{
							var useless = _userManager.AddRolesToUser(UserId, userDto.Id, new List<IRoleDto> { new RoleDto { Id = roleId } });
							foreach (var unused in useless)
							{
								/*do nothing */
							} //lazy loading work around
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
			else
			{
				TempData["MessageType"] = WebMessageType.Info;
				TempData["Message"] = "No changes made";
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
			if (users != null)
			{
				foreach (var user in users)
				{
					try
					{//need the user Id
						int id = (from u in _userManager.GetUsers(UserId) where u.AdGuid == user select u.Id).FirstOrDefault();
						_userManager.ModifyUser(UserId, new UserDto { Id = id }, EntityModification.Delete); //perform the deletion
					}
					catch (Exception exception)
					{
						TempData["MessageType"] = WebMessageType.Failure;
						TempData["Message"] = $"Failed to remove user, error: {exception.Message}";
						return RedirectToAction("ManageUsers");
					}
				}


				TempData["MessageType"] = WebMessageType.Success; //successful assumed now
				TempData["Message"] = "Successfully removed users";
			}
			return RedirectToAction("ManageUsers");
		}

		/// <summary>
		/// Show all departments
		/// </summary>
		/// <returns></returns>
		public ActionResult ShowDepartments()
		{
			DepartmentModel model = new DepartmentModel();

			model.Departments = _departmentController.GetDepartments(UserId);

			return View(model);
		}

		/// <summary>
		/// Show all departments and enable add
		/// </summary>
		/// <returns></returns>
		public ActionResult AddDepartment()
		{
			DepartmentModel model = new DepartmentModel { EnableAdd = true };

			model.Departments = _departmentController.GetDepartments(UserId);

			return View("ShowDepartments", model);
		}

		/// <summary>
		/// Save changes to a department
		/// </summary>
		/// <param name="department">department to add or update</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveDepartment(DepartmentDto department)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save queue due to invalid data";
				return RedirectToAction("ShowDepartments");
			}

			try
			{
				_departmentController.ModifyDepartment(UserId, department,
					department.Id > 0 ? EntityModification.Update : EntityModification.Create);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save department, error: {exception.Message}";
				return RedirectToAction("ShowDepartments");
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved queue";

			return RedirectToAction("ShowDepartments");
		}


		public ActionResult UpdateDepartment(int id)
		{
			DepartmentModel model = new DepartmentModel { SelectedDepartment = new DepartmentDto { Id = id } };

			model.Departments = _departmentController.GetDepartments(UserId);

			return View("ShowDepartments", model);
		}

		/// <summary>
		/// Confirm deletion of a department
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteDepartment(int id)
		{
			DepartmentDto model = (DepartmentDto)_departmentController.GetDepartment(UserId, id);

			return View(model);
		}
		/// <summary>
		/// Confirm delete of a queue
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteDepartment(int id)
		{
			try
			{
				_departmentController.ModifyDepartment(UserId, new DepartmentDto { Id = id }, EntityModification.Delete);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete department, error: {exception.Message}";
				return RedirectToAction("ShowDepartments");
			}

			TempData["MessageType"] = WebMessageType.Success; //successful assumed now
			TempData["Message"] = "Successfully deleted queue";
			return RedirectToAction("ShowDepartments");
		}

	}
}