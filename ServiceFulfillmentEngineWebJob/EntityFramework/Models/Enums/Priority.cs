namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models.Enums
{
	/// <summary>
	/// Relative execution priority for scripts 
	/// </summary>
	public enum Priority
	{
		/// <summary>
		/// This script will run before all Low Priority scripts on a Service Request
		/// </summary>
		High,

		/// <summary>
		/// This script will run after all High Priority scripts on a Service Request
		/// </summary>
		Low
	}
}
