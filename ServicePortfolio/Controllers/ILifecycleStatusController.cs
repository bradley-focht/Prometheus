using ServicePortfolio.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio.Controllers
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
		/// Saves the lifecycle status to the database
		/// </summary>
		/// <param name="lifecycleStatus"></param>
		/// <returns>True if successful</returns>
		bool SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus);

		/// <summary>
		/// Deletes the lifecycle status from the database
		/// </summary>
		/// <param name="lifecycleStatusId"></param>
		/// <returns>True if successful</returns>
		bool DeleteLifecycleStatus(int lifecycleStatusId);
	}
}