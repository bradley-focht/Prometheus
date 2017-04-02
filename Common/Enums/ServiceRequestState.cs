namespace Common.Enums
{
	/// <summary>
	/// Applicable states for a Service Request to be in
	/// </summary>
	public enum ServiceRequestState
	{
		/// <summary>
		/// Service Request has not yet been submitted, but a Service Package has been selected
		/// </summary>
		Incomplete,

		/// <summary>
		/// Service Request is complete and has been submitted for approval
		/// </summary>
		Submitted,

		/// <summary>
		/// The Requestor of the Service Request has cancelled the Service Request before it could be approved or denied
		/// </summary>
		Cancelled,

		/// <summary>
		/// Approver has approved the Service Request
		/// </summary>
		Approved,

		/// <summary>
		/// Approver has denied the Service Request
		/// </summary>
		Denied,

		/// <summary>
		/// Service Request is Fulfilled
		/// </summary>
		Fulfilled
	}
}