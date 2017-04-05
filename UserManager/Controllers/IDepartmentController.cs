using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace UserManager.Controllers
{
	public interface IDepartmentController
	{
		/// <summary>
		/// Retrieve a single department
		/// </summary>
		/// <param name="performingUserId">user making the request</param>
		/// <param name="departmentId">department to retrieve</param>
		/// <returns></returns>
		IDepartmentDto GetDepartment(int performingUserId, int departmentId);

		/// <summary>
		/// Retrieves all departments
		/// </summary>
		/// <param name="performingUserId">user making the request</param>
		/// <returns></returns>
		IEnumerable<IDepartmentDto> GetDepartments(int performingUserId);

		/// <summary>
		/// Retrieves the unique identifier of the Department Script from ID provided
		/// </summary>
		/// <param name="scriptId"></param>
		/// <returns></returns>
		Guid GetDepartmentScriptFromId(int scriptId);

		/// <summary>
		/// Modifies the Department in the database
		/// </summary>
		/// <param name="performingUserId">User ID for the user performing the modification</param>
		/// <param name="departmentDto"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns></returns>
		IDepartmentDto ModifyDepartment(int performingUserId, IDepartmentDto departmentDto, EntityModification modification);
	}
}