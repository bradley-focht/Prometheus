using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface ILifecycleStatusController
	{
		/// <summary>
		/// Finds lifecycle status with identifier provided and returns its DTO
		/// </summary>
		/// <param name="lifecycleStatusId"></param>
		/// <returns></returns>
		ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId);

		/// <summary>
		/// KVP of all lifecycle IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		IEnumerable<Tuple<int, string>> GetLifecycleStatusNames();

		/// <summary>
		/// returns a count of the number of Lifecycle statuses found
		/// </summary>
		/// <returns></returns>
		int CountLifecycleStatuses();

		/// <summary>
		/// Modifies the status in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing the modification</param>
		/// <param name="status"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Lifecycle Status</returns>
		ILifecycleStatusDto ModifyLifecycleStatus(int performingUserId, ILifecycleStatusDto status, EntityModification modification);
	}
}