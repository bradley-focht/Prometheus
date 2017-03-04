﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		///  after Approval, this field is removed and is no longer valid
		/// </summary>
		public int? ServiceOptionId { get; set; }

		public int DepartmentId { get; set; }

		public string Name { get; set; }
		public int RequestedByUserId { get; set; }
		public string Comments { get; set; }
		public string Officeuse { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? ApprovalDate { get; set; }
		public DateTime RequestedForDate { get; set; }
		/* end here */
		public int ApproverUserId { get; set; }
		public ServiceRequestState State { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ServiceOption ServiceOption { get; set; }
		public virtual Department Department { get; set; }
		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		public virtual ICollection<ServiceRequestUserInput> ServiceRequestUserInputs { get; set; }
	}
}
