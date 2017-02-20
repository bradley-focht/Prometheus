namespace Common.Enums
{
	/// <summary>
	/// Applicable states for a Service Request to be in
	/// </summary>
	public enum ServiceRequestState
	{
		Incomplete,
		Submitted,
		Approved,
		Cancelled,
		Denied,
		Fulfilled
	}
}