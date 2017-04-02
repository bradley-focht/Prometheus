namespace DataService.Models
{
	public interface IServiceWorkUnit : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// ID of Service this Work Unit is assigned to
		/// </summary>
		int ServiceId { get; set; }

		/// <summary>
		/// A manager or someone's name to contact
		/// </summary>
		string Contact { get; set; }

		/// <summary>
		/// A list of things that this team does to support this service
		/// </summary>
		string Responsibilities { get; set; }

		/// <summary>
		/// The title of a team in the company
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// department the team belongs to
		/// </summary>
		string Department { get; set; }

		Service Service { get; set; }
	}
}