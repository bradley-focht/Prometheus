namespace Common.Enums.Permissions
{
	/// <summary>
	/// Affects the Service Controller
	/// </summary>
	public enum ServiceDetails
	{
		/// <summary>
		/// Cannot view services 
		/// </summary>
		NoAccess,

		/// <summary>
		/// Can Get* services
		/// </summary>
		CanViewServiceDetails,

		/// <summary>
		/// Can modify services
		/// </summary>
		CanEditServiceDetails
	}
}