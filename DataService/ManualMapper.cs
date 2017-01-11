using System;
using Common.Dto;
using DataService.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataService
{
	/// <summary>
	/// Lazy loading from entities in the dataservice layer to DTOs
	/// 
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

		/// <summary>
		/// Lazy load a service option entity to a DTO
		/// </summary>
		/// <param name="src">source entity</param>
		/// <returns></returns>
		public static ServiceOptionDto MapServiceOptionToDto(IServiceOption src)
		{
			if (src == null) return null;

			Lazy<ServiceOptionDto> option = new Lazy<ServiceOptionDto>(()=>new ServiceOptionDto {
				Id = src.Id,
				CategoryId = src.OptionCategoryId,
				Popularity = src.Popularity,
				Description = src.Description,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Features = src.Features,
				Benefits = src.Benefits,
				Support = src.Support,
				BusinessValue = src.BusinessValue,
				PriceMonthly = src.PriceMonthly,
				PriceUpFront = src.PriceUpFront,
				Cost = src.Cost,
				Picture = src.Picture,
				PictureMimeType = src.PictureMimeType,
				Usage = src.Usage,
				TextInputs = new List<ITextInputDto>() /* for lazy loading items later */
			});

			if (src.TextInputs != null)
			{
				foreach (var t in src.TextInputs)
				{
					option.Value.TextInputs.Add(MapTextInputToDto(t));	
				}
			}

			return option.Value;
		}

		/// <summary>
		/// Convert a dto to the service option
		/// </summary>
		/// <param name="src">source dto</param>
		/// <returns></returns>
		public static ServiceOption MapDtoToServiceOption(IServiceOptionDto src)
		{
			if (src == null) return null;

			ServiceOption serviceOption = new ServiceOption
			{
				Id = src.Id,
				OptionCategoryId = src.CategoryId,
				Popularity = src.Popularity,
				Description = src.Description,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Features = src.Features,
				Benefits = src.Benefits,
				Support = src.Support,
				PriceMonthly = src.PriceMonthly,
				PriceUpFront = src.PriceUpFront,
				Cost = src.Cost,
				BusinessValue = src.BusinessValue,
				Picture = src.Picture,
				PictureMimeType = src.PictureMimeType,
				Usage = src.Usage
			};

			if (src.TextInputs != null)									//deal with text inputs
			{
				serviceOption.TextInputs = new List<ITextInput>();
				foreach(var t in src.TextInputs)
					serviceOption.TextInputs.Add(MapDtoToTextInput(t));
			}
			return serviceOption;
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
				ServiceBundleId = src.ServiceBundleId,
				Popularity = src.Popularity,
				ServiceIds = src.ServiceIds,
				
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
				Popularity = src.Popularity,
				ServiceIds = src.ServiceIds,
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
			if (src.ServiceOptions != null)
			{
				serviceDto.ServiceOptions = new List<IServiceOptionDto>();
				foreach (var option in src.ServiceOptions)
				{
					serviceDto.ServiceOptions.Add(MapServiceOptionToDto(option));
				}
			}
			//Categories
			if (src.OptionCategories != null)
			{
				serviceDto.OptionCategories = new List<IOptionCategoryDto>();

				foreach (var category in src.OptionCategories)
				{
					serviceDto.OptionCategories.Add(MapOptionCategoryToDto(category));
				}
			}

			//Processes
			if (src.ServiceProcesses != null)
			{
				serviceDto.ServiceProcesses = new List<IServiceProcessDto>();
				foreach (var process in src.ServiceProcesses)
				{
					serviceDto.ServiceProcesses.Add(MapServiceProcessToDto(process));
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
				Services = new List<IServiceDto>()
			};

			//just copy the minimum needed at this time
			if (src.Services != null && src.Services.Any())
			{
				foreach (var service in src.Services)
				{
					serviceBundle.Services.Add(new ServiceDto { Id = service.Id, Name = service.Name });
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
				MimeType = src.MimeType,
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
				MimeType = src.MimeType,
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
			Lazy<ServiceWorkUnitDto> unit = new Lazy<ServiceWorkUnitDto>(()=>
			 new ServiceWorkUnitDto
			{
				Id = src.Id,
				ServiceId = src.ServiceId,
				Contact = src.Contact,
				Responsibilities = src.Responsibilities,
				Name = src.Name
			});
			return unit.Value;
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

		public static OptionCategoryDto MapOptionCategoryToDto(IOptionCategory src)
		{
			if (src == null) { return null; }

			var categoryDto = new OptionCategoryDto
			{
				Id = src.Id,
				ServiceId = src.ServiceId,
				Popularity = src.Popularity,
				Features = src.Features,
				Benefits = src.Benefits,
				Support = src.Support,
				Name = src.Name,
				BusinessValue = src.BusinessValue
			};
			if (src.ServiceOptions != null)
			{
				categoryDto.ServiceOptions = new List<ServiceOptionDto>();
				foreach (var option in src.ServiceOptions)
				{
					categoryDto.ServiceOptions.Add(MapServiceOptionToDto(option));
				}
			}
			return categoryDto;

		}

		public static OptionCategory MapDtoToOptionCategory(IOptionCategoryDto src)
		{
			if (src == null) { return null; }

			OptionCategory category = new OptionCategory
			{
				Id = src.Id,
				Popularity = src.Popularity,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Features = src.Features,
				Benefits = src.Benefits,
				Support = src.Support,
				BusinessValue = src.BusinessValue
			};
			return category;
		}

		#region User Inputs

		/// <summary>
		/// Lazy loads a text input mapped to a dto
		/// </summary>
		/// <param name="src">source entity</param>
		/// <returns></returns>
		public static TextInputDto MapTextInputToDto(ITextInput src)
		{
			if (src == null) return null;
			Lazy<TextInputDto> textInput = new Lazy<TextInputDto>(()=>new TextInputDto
			{
				DisplayName = src.DisplayName,
				Id = src.Id,
				ServiceOptionId = src.ServiceOptionId,
				MultiLine = src.MultiLine,
			    HelpToolTip = src.HelpToolTip
			});

			return textInput.Value;
		}

		/// <summary>
		/// Map dto to its entity
		/// </summary>
		/// <param name="src">source dto</param>
		/// <returns></returns>
		public static TextInput MapDtoToTextInput(ITextInputDto src)
		{
			if (src == null) return null;
			return new TextInput
			{
				DisplayName = src.DisplayName,
				Id = src.Id,
				MultiLine = src.MultiLine,
				HelpToolTip = src.HelpToolTip
			};
		}


		#endregion

	}
}