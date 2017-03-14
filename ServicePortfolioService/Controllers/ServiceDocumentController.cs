using System;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace ServicePortfolioService.Controllers
{
	public class ServiceDocumentController : EntityController<IServiceDocumentDto>, IServiceDocumentController
	{
		public IServiceDocumentDto GetServiceDocument(int performingUserId, int serviceDocumentId)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.ServiceDocuments.ToList().FirstOrDefault(x => x.Id == serviceDocumentId);
				return ManualMapper.MapServiceDocumentToDto(document);
			}
		}

		public IServiceDocumentDto ModifyServiceDocument(int performingUserId, IServiceDocumentDto serviceDocument, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, serviceDocument, modification);
		}

		protected override IServiceDocumentDto Create(int performingUserId, IServiceDocumentDto serviceDocument)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.ServiceDocuments.ToList().FirstOrDefault(x => x.StorageNameGuid == serviceDocument.StorageNameGuid);
				if (document != null)
				{
					throw new InvalidOperationException(string.Format("Service Document with ID {0} already exists.", serviceDocument.StorageNameGuid));
				}
				var savedDocument = context.ServiceDocuments.Add(ManualMapper.MapDtoToServiceDocument(serviceDocument));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceDocumentToDto(savedDocument);
			}
		}

		protected override IServiceDocumentDto Update(int performingUserId, IServiceDocumentDto entity)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.ServiceDocuments.Any(x => x.Id == entity.Id))
				{
					throw new InvalidOperationException(
						string.Format("Service Document with ID {0} cannot be updated since it does not exist.", entity.Id));
				}
				var updatedServiceDocument = ManualMapper.MapDtoToServiceDocument(entity);
				context.ServiceDocuments.Attach(updatedServiceDocument);
				context.Entry(updatedServiceDocument).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapServiceDocumentToDto(updatedServiceDocument);
			}
		}

		protected override IServiceDocumentDto Delete(int performingUserId, IServiceDocumentDto document)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceDocuments.ToList().FirstOrDefault(x => x.Id == document.Id);
				context.ServiceDocuments.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}