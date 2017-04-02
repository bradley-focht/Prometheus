using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Common.Enums;

namespace DataService.Models
{
	public class ServiceRequest : IServiceRequest
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		// see http://stackoverflow.com/questions/14062216/introducing-foreign-key-constraint-fk-dbo-models-dbo-makes-makeid-on-table-mo
		// prevent a cycle in a cascading delete
		// the fk is for temporary use until request is approved

		/// <summary>
		/// Action being performed
		/// </summary>
		public ServiceRequestAction Action { get; set; }

		/// <summary>
		///  after Approval, this field is removed and is no longer valid
		/// </summary>
		public int? ServiceOptionId { get; set; }

		/// <summary>
		/// Department used for Approval
		/// </summary>
		public int DepartmentId { get; set; }

		/// <summary>
		/// Service Request Name (e.g. an invoice name)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// User Making the request, the requestor
		/// </summary>
		public int RequestedByUserId { get; set; }

		/// <summary>
		/// Requestees 
		/// </summary>
		public string RequestedForGuids { get; set; }

		/// <summary>
		/// Requestor
		/// </summary>
		public Guid RequestedByGuid { get; set; }

		/// <summary>
		/// Requestor Comments
		/// </summary>
		public string Comments { get; set; }

		/// <summary>
		/// In office use such as billing code
		/// </summary>
		public string Officeuse { get; set; }

		/// <summary>
		/// Date that the Service Request was created
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Submitted State
		/// </summary>
		public DateTime? SubmissionDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Approved State
		/// </summary>
		public DateTime? ApprovedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Denied State
		/// </summary>
		public DateTime? DeniedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Cancelled State
		/// </summary>
		public DateTime? CancelledDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Fulfilled State
		/// </summary>
		public DateTime? FulfilledDate { get; set; }

		/// <summary>
		/// Date that the Service Request is requested for
		/// </summary>
		public DateTime RequestedForDate { get; set; }

		/// <summary>
		/// Approver
		/// </summary>
		public int ApproverUserId { get; set; }

		/// <summary>
		/// State that the SR is currently in
		/// </summary>
		public ServiceRequestState State { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		#region Calculated Fields
		//These properties are ignored in PrometheusContext.OnModelCreating()
		/// <summary>
		/// If all SROs on the SR are basic
		/// </summary>
		public bool BasicRequest
		{
			get
			{
				if (ServiceRequestOptions == null)
					return false;
				return this.ServiceRequestOptions.All(x => x.BasicRequest == true);
			}
		}

		/// <summary>
		/// Total monthly price of service request
		/// </summary>
		public decimal MonthlyPrice
		{
			get
			{
				if (this.ServiceRequestOptions == null)
					return 0;

				int numRequestees = RequestedForGuids.Split(',').Length;

				return (decimal)this.ServiceRequestOptions.Sum(x => x.ServiceOption.PriceMonthly * x.Quantity) * numRequestees;
			}
		}

		/// <summary>
		/// Total upfront price of service request
		/// </summary>
		public decimal UpfrontPrice
		{
			get
			{
				int numRequestees = RequestedForGuids.Split(',').Length;

				if (this.ServiceRequestOptions == null)
					return 0;
				return (decimal)this.ServiceRequestOptions.Sum(x => x.ServiceOption.PriceUpFront * x.Quantity) * numRequestees;
			}
		}

		/// <summary>
		/// Total upfront price of service request at the time of Approval
		/// </summary>
		public decimal FinalUpfrontPrice { get; set; }

		/// <summary>
		/// Total monthly price of service request at the time of Approval
		/// </summary>
		public decimal FinalMonthlyPrice { get; set; }
		#endregion

		public virtual ServiceOption ServiceOption { get; set; }
		public virtual Department Department { get; set; }
		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		public virtual ICollection<ServiceRequestUserInput> ServiceRequestUserInputs { get; set; }
	}
}
