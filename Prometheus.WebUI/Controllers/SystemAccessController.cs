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


namespace Prometheus.WebUI.Controllers
{
	//[Authorize]
	public class SystemAccessController : Controller
	{
		private readonly IUserManager _userManager;
		private int uid = 1;
		public SystemAccessController()
		{
			_userManager = InterfaceFactory.CreateUserManagerService();
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
				model.ListItems = from r in _userManager.GetRoles(uid)
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
				return RedirectToAction("UpdateRole", new { id = role.Id });    //return to last page
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
				return RedirectToAction("UpdateRole", new { id = role.Id });    //return to last page
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
			RoleDto role = null;
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
		public ActionResult AddUsers()
		{
			return View(new UserModel {ReturningSearch = false});
		}

		/// <summary>
		/// Setup the partial View for searching AD accounts   
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SearchAdAccount(string searchString)
		{
			var model = new UserModel {ReturningSearch = true};

			try
			{
				model.Users = _userManager.SearchUsers(searchString);
				model.Roles = _userManager.GetRoles(uid).ToList();
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
				return View("AddUsers", model);
			}
			return View("AddUsers", model);
		}

		/// <summary>
		/// Save user accounts and roles
		/// </summary>
		/// <param name="roles"></param>
		/// <param name="users"></param>
		/// <returns></returns>
		[HttpPost]
		ActionResult SaveUsers(ICollection<int> roles, ICollection<Guid> users)
		{
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved new users";

			return RedirectToAction("AddUsers");
		}

	}
}