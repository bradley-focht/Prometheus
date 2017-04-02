namespace Common.Enums.Entities
{
	/// <summary>
	/// Describes the timing for a payment
	/// </summary>
	public enum PriceType
	{
		/// <summary>
		/// Single payment
		/// </summary>
		OneTime,

		/// <summary>
		/// Monthly recurring payment
		/// </summary>
		Monthly,

		/// <summary>
		/// Annual recurring payment
		/// </summary>
		Yearly
	}
}
