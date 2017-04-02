using System;
using Common.Dto;


namespace DataService.Models
{
	/// <summary>
	/// A requestable item that a Service provides
	/// </summary>
	public interface IServiceOption : IRequestable, ICatalogPublishable, IUserCreatedEntity
	{
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
		/// Option "risk" level. Basic Requests can be Approved by users with 
		/// the ApproveServiceRequest.ApproveBasicRequests permission
		/// </summary>
		bool BasicRequest { get; set; }

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
		ServiceOptionCategory ServiceOptionCategory { get; set; }

	}
}