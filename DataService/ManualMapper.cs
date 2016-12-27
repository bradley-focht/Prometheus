using Common.Dto;
using DataService.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataService
{
	/// <summary>
	/// Temporary replacement for Automapper. As you can see, it is of limited use. But hey, it works.
	/// </summary>
	public class ManualMapper
	{
		public static LifecycleStatusDto MapLifecycleStatusToDto(ILifecycleStatus src)
		{
			if (src == null) return null;

			return new LifecycleStatusDto
			{
				Id = src.Id,
				Name = src.Name,
				CatalogVisible = src.CatalogVisible,
				Position = src.Position
			};
		}

		public static LifecycleStatus MapDtoToLifecycleStatus(ILifecycleStatusDto src)
		{
			if (src == null) return null;

			return new LifecycleStatus
			{
				Id = src.Id,
				Name = src.Name,
				CatalogVisible = src.CatalogVisible,
				Position = src.Position
			};
		}

        public static ServiceOptionDto MapServiceOptionToDto(IServiceOption src)
        {
            if (src == null) return null;
            return new ServiceOptionDto
            {
                Id = src.Id,
                Popularity = src.Popularity,
                Description = src.Description,
                ServiceId = src.ServiceId,
                Name = src.Name,
                BusinessValue = src.BusinessValue,
                Prices = src.Prices,
                Cost = src.Cost,
                Picture = src.Picture            
            };
        }

        public static ServiceOption MapDtoToServiceOption(IServiceOptionDto src)
        {
            if (src == null) return null;
            return new ServiceOption
            {
                Id = src.Id,
                Popularity = src.Popularity,
                Description = src.Description,
                ServiceId = src.ServiceId,
                Name = src.Name,
                Prices = src.Prices,
                Cost = src.Cost,
                BusinessValue = src.BusinessValue,
                Picture = src.Picture
            };
        }
        public static Service MapDtoToService(IServiceDto src)
		{
			if (src == null) return null;

			return new Service
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				BusinessOwner = src.BusinessOwner,
				ServiceOwner = src.ServiceOwner,
				LifecycleStatusId = src.LifecycleStatusId,
				ServiceTypeProvision = src.ServiceTypeProvision,
				ServiceTypeRole = src.ServiceTypeRole,
				ServiceBundleId = src.ServiceBundleId
			};
		}

		/// <summary>
		/// Contains all information about the service, all sections
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static ServiceDto MapServiceToDto(IService src)
		{
			if (src == null) return null;

			var serviceDto = new ServiceDto
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				BusinessOwner = src.BusinessOwner,
				ServiceOwner = src.ServiceOwner,
				LifecycleStatusId = src.LifecycleStatusId,
				ServiceTypeProvision = src.ServiceTypeProvision,
				ServiceTypeRole = src.ServiceTypeRole,
				ServiceBundleId = src.ServiceBundleId,

				LifecycleStatusDto = MapLifecycleStatusToDto(src.LifecycleStatus)
			};

			//Documents
			if (src.ServiceDocuments != null)
			{
				serviceDto.ServiceDocuments = new List<IServiceDocumentDto>();
				foreach (var doc in src.ServiceDocuments)
				{
					serviceDto.ServiceDocuments.Add(MapServiceDocumentToDto(doc));
				}
			}

			//Swot
			if (src.ServiceSwots != null)
			{
				serviceDto.ServiceSwots = new List<IServiceSwotDto>();
				foreach (var doc in src.ServiceSwots)
				{
					serviceDto.ServiceSwots.Add(MapServiceSwotToDto(doc));
				}
			}

			//Goals
			if (src.ServiceGoals != null)
			{
				serviceDto.ServiceGoals = new List<IServiceGoalDto>();
				foreach (var goal in src.ServiceGoals)
				{
					serviceDto.ServiceGoals.Add(MapServiceGoalToDto(goal));
				}
			}

			//Work Units
			if (src.ServiceWorkUnits != null)
			{
				serviceDto.ServiceWorkUnits = new List<IServiceWorkUnitDto>();
				foreach (var unit in src.ServiceWorkUnits)
				{
					serviceDto.ServiceWorkUnits.Add(MapServiceWorkUnitToDto(unit));
				}
			}

            //Contracts
            if (src.ServiceContracts != null)
            {
                serviceDto.ServiceContracts = new List<IServiceContractDto>();
                foreach (var contra in src.ServiceContracts)
                {
                    serviceDto.ServiceContracts.Add(MapServiceContractToDto(contra));
                }
            }

            //Measures
		    if (src.ServiceMeasures != null)
		    {
                serviceDto.ServiceMeasures = new List<IServiceMeasureDto>();
                foreach (var measure in src.ServiceMeasures)
                {
                    serviceDto.ServiceMeasures.Add(MapServiceMeasureToDto(measure));
                }
            }

            //Options
            if (src.ServiceRequestOptions != null)
            {
                serviceDto.ServiceOptions = new List<IServiceOptionDto>();
                foreach (var option in src.ServiceRequestOptions)
                {
                    serviceDto.ServiceOptions.Add(MapServiceOptionToDto(option));
                }
            }

            //Status
            serviceDto.LifecycleStatusDto = MapLifecycleStatusToDto(src.LifecycleStatus);

			return serviceDto;
		}

		public static ServiceBundleDto MapServiceBundleToDto(IServiceBundle src)
		{
			if (src == null) return null;

			var serviceBundle = new ServiceBundleDto
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				BusinessValue = src.BusinessValue,
				Measures = src.Measures,
                Services =  new List<IServiceDto>()
			};

            //just copy the minimum needed at this time
		    if (src.Services != null && src.Services.Any())
		    {
		        foreach (var service in src.Services)
		        {
		            serviceBundle.Services.Add(new ServiceDto {Id = service.Id, Name = service.Name});
		        }
		    }
            
            return serviceBundle;
		}


		public static ServiceBundle MapDtoToServiceBundle(IServiceBundleDto src)
		{
			if (src == null) return null;

			return new ServiceBundle
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				BusinessValue = src.BusinessValue,
				Measures = src.Measures,

			};


		}


		public static ServiceDocument MapDtoToServiceDocument(IServiceDocumentDto src)
		{
			if (src == null) return null;

			return new ServiceDocument
			{
				Id = src.Id,
				ServiceId = src.ServiceId,
				StorageNameGuid = src.StorageNameGuid,
				Filename = src.Filename,
				FileExtension = src.FileExtension

			};
		}

		public static ServiceDocumentDto MapServiceDocumentToDto(IServiceDocument src)
		{
			if (src == null) return null;

			return new ServiceDocumentDto
			{
				Id = src.Id,
				ServiceId = src.ServiceId,
				StorageNameGuid = src.StorageNameGuid,
				Filename = src.Filename,
				FileExtension = src.FileExtension
			};
		}

		public static ServiceSwotDto MapServiceSwotToDto(IServiceSwot src)
		{
			if (src == null) return null;

			ServiceSwotDto serviceSwotDto = new ServiceSwotDto
			{
				Id = src.Id,
				Description = src.Description,
				Type = src.Type,
				Item = src.Item,
				ServiceId = src.ServiceId
			};

			if (src.SwotActivities != null && src.SwotActivities.Any())
			{
				serviceSwotDto.SwotActivities = new List<ISwotActivityDto>();
				foreach (var activity in src.SwotActivities)
				{
					serviceSwotDto.SwotActivities.Add(MapSwotActivityToDto(activity));
				}
			}
			return serviceSwotDto;
		}

		public static SwotActivityDto MapSwotActivityToDto(ISwotActivity src)
		{
			if (src == null) { return null; }

			return new SwotActivityDto
			{
				Id = src.Id,
				Description = src.Description,
				Date = src.Date,
				Name = src.Name,
				ServiceSwotId = src.ServiceSwotId
			};
		}

		public static ServiceSwot MapDtoToServiceSwot(IServiceSwotDto src)
		{
			if (src == null) return null;

			ServiceSwot serviceSwot = new ServiceSwot
			{
				Id = src.Id,
				Description = src.Description,
				Type = src.Type,
				Item = src.Item,
				ServiceId = src.ServiceId
			};

			if (src.SwotActivities != null && src.SwotActivities.Any())
			{
				serviceSwot.SwotActivities = new List<SwotActivity>();
				foreach (var activity in src.SwotActivities)
				{
					serviceSwot.SwotActivities.Add(MapDtoToSwotActivity(activity));
				}
			}
			return serviceSwot;
		}

		public static SwotActivity MapDtoToSwotActivity(ISwotActivityDto src)
		{
			if (src == null) { return null; }

			return new SwotActivity
			{
				Id = src.Id,
				Description = src.Description,
				Date = src.Date,
				Name = src.Name,
				ServiceSwotId = src.ServiceSwotId
			};
		}

		public static ServiceProcessDto MapServiceProcessToDto(IServiceProcess src)
		{
			if (src == null) { return null; }

			return new ServiceProcessDto
			{
				Id = src.Id,
				Description = src.Description,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Improvements = src.Improvements,
				Benefits = src.Benefits
			};
		}

		public static ServiceProcess MapDtoToServiceProcess(IServiceProcessDto src)
		{
			if (src == null) { return null; }

			return new ServiceProcess
			{
				Id = src.Id,
				Description = src.Description,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Improvements = src.Improvements,
				Benefits = src.Benefits
			};
		}

		public static IServiceGoalDto MapServiceGoalToDto(ServiceGoal src)
		{
			if (src == null) { return null; }

			return new ServiceGoalDto
			{
				Id = src.Id,
				Description = src.Description,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Type = src.Type,
				StartDate = src.StartDate,
				EndDate = src.EndDate
			};
		}

		public static ServiceGoal MapDtoToServiceGoal(IServiceGoalDto src)
		{
			if (src == null) { return null; }

			return new ServiceGoal
			{
				Id = src.Id,
				Description = src.Description,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Type = src.Type,
				StartDate = src.StartDate,
				EndDate = src.EndDate,
			};
		}

		public static ServiceContractDto MapServiceContractToDto(IServiceContract src)
		{
			if (src == null) { return null; }

			return new ServiceContractDto
			{
				Id = src.Id,
				Description = src.Description,
				ServiceId = src.ServiceId,
				StartDate = src.StartDate,
				ContractType = src.ContractType,
				ContractNumber = src.ContractNumber,
				ExpiryDate = src.ExpiryDate,
				ServiceProvider = src.ServiceProvider
			};
		}

		public static ServiceContract MapDtoToServiceContract(IServiceContractDto src)
		{
			if (src == null) { return null; }

			return new ServiceContract
			{
				Id = src.Id,
				Description = src.Description,
				ServiceId = src.ServiceId,
				StartDate = src.StartDate,
				ContractType = src.ContractType,
				ContractNumber = src.ContractNumber,
				ExpiryDate = src.ExpiryDate,
				ServiceProvider = src.ServiceProvider
			};
		}

		public static ServiceMeasureDto MapServiceMeasureToDto(IServiceMeasure src)
		{
			if (src == null) { return null; }

			return new ServiceMeasureDto
			{
				Id = src.Id,
				Method = src.Method,
				Outcome = src.Outcome,
				ServiceId = src.ServiceId
			};
		}

		public static ServiceMeasure MapDtoToServiceMeasure(IServiceMeasureDto src)
		{
			if (src == null) { return null; }

			return new ServiceMeasure
			{
				Id = src.Id,
				Method = src.Method,
				Outcome = src.Outcome,
				ServiceId = src.ServiceId
			};
		}

		public static ServiceWorkUnitDto MapServiceWorkUnitToDto(IServiceWorkUnit src)
		{
			if (src == null) { return null; }

			return new ServiceWorkUnitDto
			{
				Id = src.Id,
				ServiceId = src.ServiceId,
				Contact = src.Contact,
				Responsibilities = src.Responsibilities,
				Name = src.Name
			};
		}

		public static ServiceWorkUnit MapDtoToServiceWorkUnit(IServiceWorkUnitDto src)
		{
			if (src == null) { return null; }

			return new ServiceWorkUnit
			{
				Id = src.Id,
				ServiceId = src.ServiceId,
				Contact = src.Contact,
				Responsibilities = src.Responsibilities,
				Name = src.Name
			};
		}

	    public static OptionCategoryDto MapServiceCategoryToDto(IOptionCategoryDto src)
	    {
	        if (src == null) { return null;}

	        return new OptionCategoryDto
	        {
                Id = src.Id,
                ServiceId = src.Id,
                Name = src.Name,
                Description = src.Description
	        };
	    }
	}
}