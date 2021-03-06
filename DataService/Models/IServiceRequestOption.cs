﻿namespace DataService.Models
{
	/// <summary>
	/// Item stored in a ServiceRequest
	/// </summary>
	public interface IServiceRequestOption : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// ID of Service Option that SRO was created from
		/// </summary>
		int ServiceOptionId { get; set; }

		/// <summary>
		/// ID of Service Request that SRO is a part of
		/// </summary>
		int ServiceRequestId { get; set; }

		/// <summary>
		/// Number the the Service Option Requested
		/// </summary>
		int Quantity { get; set; }

		/// <summary>
		/// If the request is Basic for the purposes of ApproveServiceRequest.ApproveBasicRequests
		/// </summary>
		bool BasicRequest { get; set; }
		string Code { get; set; }
		string ServiceOptionName { get; set; }

		/// <summary>
		/// Approver
		/// </summary>
		int ApproverUserId { get; set; }

		/// <summary>
		/// Requestor
		/// </summary>
		int RequestedByUserId { get; set; }

		ServiceOption ServiceOption { get; set; }
		ServiceRequest ServiceRequest { get; set; }
	}
}