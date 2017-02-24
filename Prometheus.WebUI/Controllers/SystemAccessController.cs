using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.Shared;
using Prometheus.WebUI.Models.SystemAccess;
using UserManager;
using UserManager.AdService;

namespace Prometheus.WebUI.Controllers
{
	//[Authorize]
	public class SystemAccessController : Controller
	{
		private readonly IUserManager _userManager;
		private int uid = 1;
		private readonly int _userPageSize;

		public SystemAccessController()
		{
			_userManager = InterfaceFactory.CreateUserManagerService();
			_userPageSize = ConfigHelper.GetPaginationSize();
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
				role = _userManager.GetRole(uid, id);
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
				model.ListItems = from r in _userManager.GetRoles(uid)  /*put users & Guids into tuples */
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
			return View("UpdateRole", new RoleModel {Role = new RoleDto(), Action = "Add"});
		}

		/// <summary>
		/// Retrieve a role to update
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateRole(int id)
		{
			var model = new RoleModel {Action = "Update"};
			try
			{
				model.Role = _userManager.GetRole(uid, id);
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
				return RedirectToAction("UpdateRole", new {id = role.Id}); //return to last page
			}
			try
			{
				_userManager.ModifyRole(uid, role, role.Id > 0 ? EntityModification.Update : EntityModification.Create);
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
				role = _userManager.GetRole(uid, id);
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
				_userManager.ModifyRole(uid, new RoleDto {Id = model.Id}, EntityModification.Delete);
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
			controls.Roles = from r in _userManager.GetRoles(uid) select new Tuple<int, string>(r.Id, r.Name);

			return View("ManageUsers", new ManageUsersModel {Controls = controls, ReturningSearch = false});
		}

		/// <summary>
		/// Apply a filter on the results
		/// </summary>
		/// <param name="id">role Id</param>
		/// <param name="pageId">page of results</param>
		/// <returns></returns>
		public ActionResult FilterByRole(int id, int pageId = 0)
		{
			UserControlsModel controls = new UserControlsModel {CurrentPage = pageId, SelectedRole = id}; /*construct model for view */
			var model = new ManageUsersModel {Controls = controls, ReturningSearch = false};
			model.Roles = _userManager.GetRoles(uid).ToList(); //stop forgetting these roles, ok?

			controls.Roles = from r in _userManager.GetRoles(uid) select new Tuple<int, string>(r.Id, r.Name);

			IEnumerable<IUserDto> users = new List<UserDto>();
			if ( id == 0)
				users = (from u in _userManager.GetUsers(uid) select u);
			else if (id > 0)
				users = (from u in _userManager.GetUsers(uid) where u.Roles.Any(role => role.Id == id) select u);

			List<UserDetailsModel> modelUsers = new List<UserDetailsModel>();

			IAdSearch searcher = new AdSearch();
			foreach (var user in users)
			{
				//TODO: AD string displayName = searcher.GetUserDisplayName(user.AdGuid);  //name resolution
				string displayName = "honey bunny"; //debugging with no AD

				modelUsers.Add(new UserDetailsModel {UserDto = user, DisplayName = displayName});
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
			var model = new ManageUsersModel {ReturningSearch = true, Users = new List<UserDetailsModel>()};
			var controls = new UserControlsModel();
			model.Controls = controls;
			controls.Roles = from r in _userManager.GetRoles(uid) select new Tuple<int, string>(r.Id, r.Name);

			List<Tuple<Guid, string>> directoryUsers;
			List<Tuple<Guid, ICollection<RoleDto>>> existingUsers;
			try
			{
				model.Roles = _userManager.GetRoles(uid).ToList();
				directoryUsers = _userManager.SearchUsers(searchString).ToList();
				existingUsers = (from u in _userManager.GetUsers(uid) select new Tuple<Guid, ICollection<RoleDto>>(u.AdGuid, u.Roles)).ToList();
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

				var roles = (from u in existingUsers where u.Item1 == user.Item1 select u.Item2).FirstOrDefault() ;
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
						try
						{
							string displayName = "honey bunny"; //TODO: AD

							model.Add(new UserDetailsModel
							{
								UserDto = new UserDto {AdGuid = user},
								DisplayName = displayName
							});
						}
						catch (Exception exception)
						{
							TempData["MessageType"] = WebMessageType.Failure;
							TempData["Message"] = $"Failed to save a user, error: {exception.Message}";
						}

					return View("ConfirmDeleteUsers", model);
				}
			}

			if (users != null && roles != null && users.Any() && roles.Any())
			{
				var userList = _userManager.GetUsers(uid).ToList(); //get the user list to check new users in
				var roleList = _userManager.GetRoles(uid).ToList();

				foreach (var user in users)
				{
					if (userList.All(u => user != u.AdGuid)) /* first add anyone new */
					{
						try
						{
							_userManager.ModifyUser(uid, new UserDto {AdGuid = user}, EntityModification.Create);
						}
						catch (Exception exception)
						{
							TempData["MessageType"] = WebMessageType.Failure;
							TempData["Message"] = $"Failed to save a user, error: {exception.Message}";
						}
					}
					IUserDto userDto = (from u in userList where u.AdGuid == user select u).FirstOrDefault();
					//get the userId of any user

					foreach (var role in roleList) //remove all roles        
					{
						try
						{
							_userManager.RemoveRoleFromUsers(uid, role, new List<IUserDto> {userDto});
						}
						catch (Exception) { } //ignore if user did not have role                      
					}

					foreach (var role in roles) //add roles
						try
						{
							if (userDto != null) //userDto may have been deleted already 
								_userManager.AddRolesToUser(uid, userDto.Id,
									new List<IRoleDto> {new RoleDto {Id = role}});
						}
						catch (Exception exception)
						{
							TempData["MessageType"] = WebMessageType.Failure;
							TempData["Message"] = $"Failed to save user changes, error: {exception.Message}";
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
					int id = (from u in _userManager.GetUsers(uid) where u.AdGuid == user select u.Id).FirstOrDefault(); //need the user Id
					_userManager.ModifyUser(uid, new UserDto {Id = id}, EntityModification.Delete);     //perform the deletion
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
	}
}