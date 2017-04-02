using System;

namespace Common.Dto
{
	/// <summary>
	/// A requestable item that a Service provides
	/// </summary>
	public interface IServiceOptionDto : IRequestableDto, IUserCreatedEntityDto
	{
		int Id { get; set; }

		/// <summary>
		/// Unique name
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Service Request Category
		/// </summary>
		int ServiceOptionCategoryId { get; set; }

		/// <summary>
		/// Uploaded picture, only one per option allowed
		/// </summary>
		Guid? Picture { get; set; }

		/// <summary>
		/// Media type of the Picture
		/// </summary>
		string PictureMimeType { get; set; }

		/// <summary>
		/// profit made
		/// </summary>
		double PriceUpFront { get; set; }

		/// <summary>
		/// profit made
		/// </summary>
		double PriceMonthly { get; set; }

		/// <summary>
		/// Cost paid by the company to provide the option
		/// </summary>
		double Cost { get; set; }

		/// <summary>
		/// Utilization of the option by clients
		/// </summary>
		string Utilization { get; set; }

		/// <summary>
		/// Included in the price
		/// </summary>
		string Included { get; set; }

		/// <summary>
		/// method of procuring
		/// </summary>
		string Procurement { get; set; }

		/// <summary>
		/// service design package description
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// technical details
		/// </summary>
		string Details { get; set; }

		/// <summary>
		/// Service Catalog display information
		/// </summary>
		string BusinessValue { get; set; }

		/// <summary>
		/// ordering for catalog publishing, not part of service design
		/// </summary>
		int Popularity { get; set; }

		/// <summary>
		/// Option "risk" level. Basic Requests can be Approved by users with 
		/// the ApproveServiceRequest.ApproveBasicRequests permission
		/// </summary>
		bool BasicRequest { get; set; }
	}
}