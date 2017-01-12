namespace UserManager.Controllers
{
	public interface IPermissionController
	{
		/// <summary>
		/// Determines whether or not the supplied userID corresponds with a user that has access to the permission provided.
		/// 
		/// example use:
		//*		bool canViewServiceDetails = UserHasPermission<ServiceDetails>(userId, ServiceDetails.CanViewServiceDetails);
		/// </summary>
		/// <typeparam name="T">Permission type</typeparam>
		/// <param name="userId">ID for user to check</param>
		/// <param name="permission">Permission level to ensure user has</param>
		/// <returns></returns>
		bool UserHasPermission<T>(int userId, T permission);
	}
}