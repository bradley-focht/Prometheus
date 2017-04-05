namespace Common.Utilities
{
	/// <summary>
	/// Used for unpackaging Script outputs
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	/// <typeparam name="TLabel"></typeparam>
	public class ScriptResult<TValue, TLabel>
	{
		/// <summary>
		/// Value of the script output
		/// </summary>
		public TValue Value { get; set; }

		/// <summary>
		/// Descriptor of the script output
		/// </summary>
		public TLabel Label { get; set; }
	}
}
