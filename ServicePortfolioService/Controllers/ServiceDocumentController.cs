using Common.Dto;
using Common.Enums;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using System;
using System.Linq;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public class ServiceDocumentController : EntityController<IServiceDocumentDto>, IServiceDocumentController
	{
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public ServiceDocumentController()
		{
			_userId = PortfolioService.GuestUserId;
		}

		public ServiceDocumentController(int userId)
		{
			_userId = userId;
		}

		public IServiceDocumentDto GetServiceDocument(Guid serviceDocumentId)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.ServiceDocuments.ToList().FirstOrDefault(x => x.StorageNameGuid == serviceDocumentId);
				return ManualMapper.MapServiceDocumentToDto(document);
			}
		}

		public IServiceDocumentDto ModifyServiceDocument(IServiceDocumentDto serviceDocument, EntityModification modification)
		{
			return base.ModifyEntity(serviceDocument, modification);
		}

		protected override IServiceDocumentDto Create(IServiceDocumentDto serviceDocument)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.ServiceDocuments.ToList().FirstOrDefault(x => x.StorageNameGuid == serviceDocument.StorageNameGuid);
				if (document != null)
				{
					throw new InvalidOperationException(string.Format("Service Document with ID {0} already exists.", serviceDocument.StorageNameGuid));
				}
				var savedDocument = context.ServiceDocuments.Add(ManualMapper.MapDtoToServiceDocument(serviceDocument));
				context.SaveChanges(_userId);
				return ManualMapper.MapServiceDocumentToDto(savedDocument);
			}
		}

		protected override IServiceDocumentDto Update(IServiceDocumentDto entity)
		{
			throw new ModificationException(string.Format("Modification {0} cannot be performed on Service Documents.", EntityModification.Update));
		}

		protected override IServiceDocumentDto Delete(IServiceDocumentDto document)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.ServiceDocuments.ToList().FirstOrDefault(x => x.StorageNameGuid == document.StorageNameGuid);
				context.ServiceDocuments.Remove(toDelete);
				context.SaveChanges(_userId);
			}
			return null;
		}
	}
}