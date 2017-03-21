using System;

namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models
{
	internal interface ICreatedEntity
	{
		DateTime DateCreated { get; set; }
		DateTime DateUpdated { get; set; }
		int CreatedByUserId { get; set; }
		int UpdatedByUserId { get; set; }
	}
}
