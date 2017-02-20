namespace Prometheus.WebUI.Models.Shared
{
	/// <summary>
	/// Send data to display user inputs
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public class InputModel<T>
    {
		/// <summary>
		/// Id of the record in database if it exists
		/// </summary>
	    public int Id { get; set; }
		/// <summary>
		/// User input control
		/// </summary>
        public T Control { get; set; }
		/// <summary>
		/// Name to be prepended to the user control name
		/// </summary>
        public string ControlName { get; set; }
    }
}