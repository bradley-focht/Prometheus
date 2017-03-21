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

		public ServiceRequestAction Action { get; set; }

		/// <summary>
		///  after Approval, this field is removed and is no longer valid
		/// </summary>
		public int? ServiceOptionId { get; set; }

		public int DepartmentId { get; set; }

		public string Name { get; set; }
		public int RequestedByUserId { get; set; }
		public string RequestedForGuids { get; set; }
		public Guid RequestedByGuid { get; set; }
		public string Comments { get; set; }
		public string Officeuse { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? ApprovedDate { get; set; }
		public DateTime? DeniedDate { get; set; }
		public DateTime? CancelledDate { get; set; }
		public DateTime? FulfilledDate { get; set; }
		public DateTime RequestedForDate { get; set; }
		/* end here */
		public int ApproverUserId { get; set; }
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
				return (decimal)this.ServiceRequestOptions.Sum(x => x.ServiceOption.PriceMonthly);
			}
		}

		/// <summary>
		/// Total upfront price of service request
		/// </summary>
		public decimal UpfrontPrice
		{
			get
			{
				if (this.ServiceRequestOptions == null)
					return 0;
				return (decimal)this.ServiceRequestOptions.Sum(x => x.ServiceOption.PriceUpFront);
			}
		}

		public decimal FinalUpfrontPrice { get; set; }
		public decimal FinalMonthlyPrice { get; set; }
		#endregion

		public virtual ServiceOption ServiceOption { get; set; }
		public virtual Department Department { get; set; }
		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		public virtual ICollection<ServiceRequestUserInput> ServiceRequestUserInputs { get; set; }
	}
}
