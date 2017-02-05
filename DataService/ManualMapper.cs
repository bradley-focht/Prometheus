using Common.Dto;
using DataService.Models;
using System;
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

			Lazy<ServiceOptionDto> option = new Lazy<ServiceOptionDto>(() => new ServiceOptionDto
			{
				BusinessValue = src.BusinessValue,
				Cost = src.Cost,
				Description = src.Description,
				Details = src.Details,
				Id = src.Id,
				Included = src.Included,
				Name = src.Name,
				Procurement = src.Procurement,
				Picture = src.Picture,
				PriceMonthly = src.PriceMonthly,
				PriceUpFront = src.PriceUpFront,
				PictureMimeType = src.PictureMimeType,
				Popularity = src.Popularity,
				ServiceOptionCategoryId = src.ServiceOptionCategoryId,
				Utilization = src.Utilization,

				TextInputs = new List<ITextInputDto>(), /* lazy loading items later */
				SelectionInputs = new List<ISelectionInputDto>(),
				ScriptedSelectionInputs = new List<IScriptedSelectionInputDto>()
			});

			// text inputs
			if (src.TextInputs != null)
			{
				foreach (var t in src.TextInputs)
				{
					option.Value.TextInputs.Add(MapTextInputToDto(t));
				}
			}

			// selection inputs
			if (src.SelectionInputs != null)
			{
				foreach (var t in src.SelectionInputs)
				{
					option.Value.SelectionInputs.Add(MapSelectionInputToDto(t));
				}
			}

			// scripted selection inputs
			if (src.ScriptedSelectionInputs != null)
			{
				foreach (var t in src.ScriptedSelectionInputs)
				{
					option.Value.ScriptedSelectionInputs.Add(MapScriptedSelectionInputToDto(t));
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
				BusinessValue = src.BusinessValue,
				Cost = src.Cost,
				Description = src.Description,
				Details = src.Details,
				Id = src.Id,
				Included = src.Included,
				Name = src.Name,
				Procurement = src.Procurement,
				Picture = src.Picture,
				PriceMonthly = src.PriceMonthly,
				PriceUpFront = src.PriceUpFront,
				PictureMimeType = src.PictureMimeType,
				Popularity = src.Popularity,
				ServiceOptionCategoryId = src.ServiceOptionCategoryId,
				Utilization = src.Utilization
			};

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
				BusinessValue = src.BusinessValue,
				BusinessOwner = src.BusinessOwner,
				ServiceOwner = src.ServiceOwner,
				LifecycleStatusId = src.LifecycleStatusId,
				ServiceTypeProvision = src.ServiceTypeProvision,
				ServiceTypeRole = src.ServiceTypeRole,
				ServiceBundleId = src.ServiceBundleId,
				Popularity = src.Popularity
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

			var serviceDto = new Lazy<ServiceDto>(() => new ServiceDto
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				BusinessOwner = src.BusinessOwner,
				BusinessValue = src.BusinessValue,
				ServiceOwner = src.ServiceOwner,
				LifecycleStatusId = src.LifecycleStatusId,
				ServiceTypeProvision = src.ServiceTypeProvision,
				ServiceTypeRole = src.ServiceTypeRole,
				ServiceBundleId = src.ServiceBundleId,
				Popularity = src.Popularity,
				LifecycleStatusDto = MapLifecycleStatusToDto(src.LifecycleStatus)
			});

			//Documents
			if (src.ServiceDocuments != null)
			{
				serviceDto.Value.ServiceDocuments = new List<IServiceDocumentDto>();
				foreach (var doc in src.ServiceDocuments)
				{
					serviceDto.Value.ServiceDocuments.Add(MapServiceDocumentToDto(doc));
				}
			}

			//Swot
			if (src.ServiceSwots != null)
			{
				serviceDto.Value.ServiceSwots = new List<IServiceSwotDto>();
				foreach (var doc in src.ServiceSwots)
				{
					serviceDto.Value.ServiceSwots.Add(MapServiceSwotToDto(doc));
				}
			}

			//Goals
			if (src.ServiceGoals != null)
			{
				serviceDto.Value.ServiceGoals = new List<IServiceGoalDto>();
				foreach (var goal in src.ServiceGoals)
				{
					serviceDto.Value.ServiceGoals.Add(MapServiceGoalToDto(goal));
				}
			}

			//Work Units
			if (src.ServiceWorkUnits != null)
			{
				serviceDto.Value.ServiceWorkUnits = new List<IServiceWorkUnitDto>();
				foreach (var unit in src.ServiceWorkUnits)
				{
					serviceDto.Value.ServiceWorkUnits.Add(MapServiceWorkUnitToDto(unit));
				}
			}

			//Contracts
			if (src.ServiceContracts != null)
			{
				serviceDto.Value.ServiceContracts = new List<IServiceContractDto>();
				foreach (var contra in src.ServiceContracts)
				{
					serviceDto.Value.ServiceContracts.Add(MapServiceContractToDto(contra));
				}
			}

			//Measures
			if (src.ServiceMeasures != null)
			{
				serviceDto.Value.ServiceMeasures = new List<IServiceMeasureDto>();
				foreach (var measure in src.ServiceMeasures)
				{
					serviceDto.Value.ServiceMeasures.Add(MapServiceMeasureToDto(measure));
				}
			}

			//Categories
			if (src.ServiceOptionCategories != null)
			{
				serviceDto.Value.ServiceOptionCategories = new List<IServiceOptionCategoryDto>();

				foreach (var category in src.ServiceOptionCategories)
				{
					serviceDto.Value.ServiceOptionCategories.Add(MapOptionCategoryToDto(category));
				}
			}

			//Processes
			if (src.ServiceProcesses != null)
			{
				serviceDto.Value.ServiceProcesses = new List<IServiceProcessDto>();
				foreach (var process in src.ServiceProcesses)
				{
					serviceDto.Value.ServiceProcesses.Add(MapServiceProcessToDto(process));
				}
			}

			//Status
			serviceDto.Value.LifecycleStatusDto = MapLifecycleStatusToDto(src.LifecycleStatus);

			return serviceDto.Value;
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
				FileExtension = src.FileExtension,
				UploadDate = src.UploadDate

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
				FileExtension = src.FileExtension,
				UploadDate = src.UploadDate
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
				Owner = src.Owner,
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
				Owner = src.Owner,
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
			Lazy<ServiceWorkUnitDto> unit = new Lazy<ServiceWorkUnitDto>(() =>
			 new ServiceWorkUnitDto
			 {
				 Id = src.Id,
				 ServiceId = src.ServiceId,
				 Contact = src.Contact,
				 Responsibilities = src.Responsibilities,
				 Name = src.Name,
				 Department = src.Department

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
				Name = src.Name,
				Department = src.Department
			};
		}

		public static ServiceOptionCategoryDto MapOptionCategoryToDto(IServiceOptionCategory src)
		{
			if (src == null) { return null; }

			var categoryDto = new ServiceOptionCategoryDto
			{
				Id = src.Id,
				ServiceId = src.ServiceId,
				Popularity = src.Popularity,
				Features = src.Features,
				Benefits = src.Benefits,
				Support = src.Support,
				Name = src.Name,
                Quantifiable = src.Quantifiable,
				BusinessValue = src.BusinessValue
			};
			if (src.ServiceOptions != null)
			{
				categoryDto.ServiceOptions = new List<IServiceOptionDto>();
				foreach (var option in src.ServiceOptions)
				{
					categoryDto.ServiceOptions.Add(MapServiceOptionToDto(option));
				}
			}
			return categoryDto;

		}

		public static ServiceOptionCategory MapDtoToOptionCategory(IServiceOptionCategoryDto src)
		{
			if (src == null) { return null; }

			ServiceOptionCategory category = new ServiceOptionCategory()
			{
				Id = src.Id,
				Popularity = src.Popularity,
				ServiceId = src.ServiceId,
				Name = src.Name,
				Features = src.Features,
				Benefits = src.Benefits,
				Support = src.Support,
                Quantifiable = src.Quantifiable,
                BusinessValue = src.BusinessValue
			};
			return category;
		}

		public static RoleDto MapRoleToDto(IRole src)
		{
			if (src == null) { return null; }

			return new RoleDto()
			{
				Id = src.Id,
				Name = src.Name,
				ApproveServiceRequestAccess = src.ApproveServiceRequestAccess,
				BusinessCatalogAccess = src.BusinessCatalogAccess,
				RolePermissionAdjustmentAccess = src.RolePermissionAdjustmentAccess,
				ServiceDetailsAccess = src.ServiceDetailsAccess,
				ServiceRequestSubmissionAccess = src.ServiceRequestSubmissionAccess,
				SupportCatalogAccess = src.SupportCatalogAccess,
				UserRoleAssignmentAccess = src.UserRoleAssignmentAccess

			};
		}

		public static Role MapDtoToRole(IRoleDto src)
		{
			if (src == null) { return null; }

			return new Role()
			{
				Id = src.Id,
				Name = src.Name,
				ApproveServiceRequestAccess = src.ApproveServiceRequestAccess,
				BusinessCatalogAccess = src.BusinessCatalogAccess,
				RolePermissionAdjustmentAccess = src.RolePermissionAdjustmentAccess,
				ServiceDetailsAccess = src.ServiceDetailsAccess,
				ServiceRequestSubmissionAccess = src.ServiceRequestSubmissionAccess,
				SupportCatalogAccess = src.SupportCatalogAccess,
				UserRoleAssignmentAccess = src.UserRoleAssignmentAccess
			};
		}

		#region User Inputs

		/// <summary>
		/// Lazy loads a text input mapped to a dto
		/// </summary>
		/// <param name="src">source entity</param>
		/// <returns></returns>
		public static ITextInputDto MapTextInputToDto(ITextInput src)
		{
			if (src == null) return null;
			Lazy<TextInputDto> textInput = new Lazy<TextInputDto>(() => new TextInputDto
			{
				DisplayName = src.DisplayName,
				Id = src.Id,
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

		public static ISelectionInputDto MapSelectionInputToDto(ISelectionInput src)
		{
			if (src == null) return null;
			Lazy<SelectionInputDto> input = new Lazy<SelectionInputDto>(() => new SelectionInputDto
			{
				DisplayName = src.DisplayName,
				Id = src.Id,
				Delimiter = src.Delimiter,
				HelpToolTip = src.HelpToolTip,
				NumberToSelect = src.NumberToSelect,
				SelectItems = src.SelectItems,
			});

			return input.Value;
		}

		/// <summary>
		/// Map dto to its entity
		/// </summary>
		/// <param name="src">source dto</param>
		/// <returns></returns>
		public static SelectionInput MapDtoToSelectionInput(ISelectionInputDto src)
		{
			if (src == null) return null;
			return new SelectionInput
			{
				DisplayName = src.DisplayName,
				Id = src.Id,
				Delimiter = src.Delimiter,
				HelpToolTip = src.HelpToolTip,
				NumberToSelect = src.NumberToSelect,
				SelectItems = src.SelectItems,
			};
		}

		public static IScriptedSelectionInputDto MapScriptedSelectionInputToDto(ScriptedSelectionInput src)
		{
			if (src == null) return null;
			Lazy<ScriptedSelectionInputDto> input = new Lazy<ScriptedSelectionInputDto>(() => new ScriptedSelectionInputDto
			{
				DisplayName = src.DisplayName,
				Id = src.Id,
				HelpToolTip = src.HelpToolTip,
				NumberToSelect = src.NumberToSelect,
				ExecutionEnabled = src.ExecutionEnabled,
				Script = src.Script,
			});

			return input.Value;
		}

		/// <summary>
		/// Map dto to its entity
		/// </summary>
		/// <param name="src">source dto</param>
		/// <returns></returns>
		public static ScriptedSelectionInput MapDtoToScriptedSelectionInput(IScriptedSelectionInputDto src)
		{
			if (src == null) return null;
			return new ScriptedSelectionInput
			{
				DisplayName = src.DisplayName,
				Id = src.Id,
				HelpToolTip = src.HelpToolTip,
				NumberToSelect = src.NumberToSelect,
				ExecutionEnabled = src.ExecutionEnabled,
				Script = src.Script,
			};
		}
		#endregion

		public static User MapDtoToUser(IUserDto src)
		{
			if (src == null) return null;
			return new User
			{
				Id = src.Id,
				Name = src.Name,
				AdGuid = src.AdGuid
			};
		}

		public static IUserDto MapUserToDto(IUser src)
		{
			if (src == null) return null;

			List<RoleDto> roles = new List<RoleDto>();
			if (src.Roles != null)
			{
				foreach (var role in src.Roles)
				{
					roles.Add(MapRoleToDto(role));
				}
			}

			return new UserDto()
			{
				Id = src.Id,
				Roles = roles,
				AdGuid = src.AdGuid
			};
		}

		public static IServiceRequestPackageDto MapServiceRequestPackageToDto(ServiceRequestPackage src)
		{
			if (src == null) return null;

			List<IServiceOptionCategoryDto> serviceOptionCategories = new List<IServiceOptionCategoryDto>();
			if (src.ServiceOptionCategories != null)
			{
				foreach (var category in src.ServiceOptionCategories)
				{
					serviceOptionCategories.Add(MapOptionCategoryToDto(category));
				}
			}


			return new ServiceRequestPackageDto()
			{
				Id = src.Id,
				Name = src.Name,
				ServiceOptionCategories = serviceOptionCategories
			};
		}

		public static ServiceRequestPackage MapDtoToServiceRequestPackage(IServiceRequestPackageDto src)
		{
			if (src == null) return null;
			return new ServiceRequestPackage
			{
				Id = src.Id,
				Name = src.Name
			};
		}

		public static ServiceRequest MapDtoToServiceRequest(IServiceRequestDto src)
		{
			if (src == null) return null;
			return new ServiceRequest
			{
				Id = src.Id,
				State = src.State,
				ApprovalDate = src.ApprovalDate,
				ApproverUserId = src.ApproverUserId,
				Comments = src.Comments,
				CreationDate = src.CreationDate,
				Officeuse = src.Officeuse,
				RequestedByUserId = src.RequestedByUserId,
				SubmissionDate = src.SubmissionDate,
                RequestedForDate = src.RequestedForDate
            };
		}

		public static IServiceRequestDto MapServiceRequestToDto(ServiceRequest src)
		{
			if (src == null) return null;

			List<IServiceRequestOptionDto> serviceRequestOptions = new List<IServiceRequestOptionDto>();
			if (src.ServiceRequestOptions != null)
			{
				foreach (var serviceRequestOption in src.ServiceRequestOptions)
				{
					serviceRequestOptions.Add(MapServiceRequestOptionToDto(serviceRequestOption));
				}
			}

			return new ServiceRequestDto()
			{
				Id = src.Id,
				State = src.State,
				ApprovalDate = src.ApprovalDate,
				ApproverUserId = src.ApproverUserId,
				Comments = src.Comments,
				CreationDate = src.CreationDate,
				Officeuse = src.Officeuse,
				RequestedByUserId = src.RequestedByUserId,
				SubmissionDate = src.SubmissionDate,
				ServiceRequestOptions = serviceRequestOptions,
                RequestedForDate = src.RequestedForDate
            };
		}

		private static IServiceRequestOptionDto MapServiceRequestOptionToDto(ServiceRequestOption src)
		{
			if (src == null) return null;

			var serviceRequestOptionScriptedSelectionInputs =
				src.ServiceRequestOptionScriptedSelectionInputs.Select(x => MapServiceRequestOptionScriptedSelectionInputToDto(x)).ToList();

			var serviceRequestOptionSelectionInputs =
				src.ServiceRequestOptionSelectionInputs.Select(x => MapServiceRequestOptionSelectionInputToDto(x)).ToList();

			var serviceRequestOptionTextInputs =
				src.ServiceRequestOptionTextInputs.Select(x => MapServiceRequestOptionTextInputToDto(x)).ToList();

			return new ServiceRequestOptionDto()
			{
				Id = src.Id,
				ApproverUserId = src.ApproverUserId,
				RequestedByUserId = src.RequestedByUserId,
				ServiceOptionId = src.ServiceOptionId,
				ServiceRequestId = src.ServiceRequestId,
				ServiceOption = MapServiceOptionToDto(src.ServiceOption),
				ServiceRequest = MapServiceRequestToDto(src.ServiceRequest),
				ServiceRequestOptionScriptedSelectionInputs = serviceRequestOptionScriptedSelectionInputs,
				ServiceRequestOptionSelectionInputs = serviceRequestOptionSelectionInputs,
				ServiceRequestOptionTextInputs = serviceRequestOptionTextInputs
			};
		}

		public static ServiceRequestOption MapDtoToServiceRequestOption(IServiceRequestOptionDto src)
		{
			if (src == null) return null;
			return new ServiceRequestOption
			{
				Id = src.Id,
				ApproverUserId = src.ApproverUserId,
				RequestedByUserId = src.RequestedByUserId,
				ServiceOptionId = src.ServiceOptionId,
				ServiceRequestId = src.ServiceRequestId
			};
		}

		private static IServiceRequestOptionScriptedSelectionInputDto MapServiceRequestOptionScriptedSelectionInputToDto(ServiceRequestOptionScriptedSelectionInput src)
		{
			if (src == null) return null;

			return new ServiceRequestOptionScriptedSelectionInputDto()
			{
				Id = src.Id,
				ScriptedSelectionInputId = src.ScriptedSelectionInputId,
				ServiceRequestOptionId = src.ServiceRequestOptionId,
				Value = src.Value,
				ScriptedSelectionInput = MapScriptedSelectionInputToDto(src.ScriptedSelectionInput),
				ServiceRequestOption = MapServiceRequestOptionToDto(src.ServiceRequestOption)
			};
		}

		public static ServiceRequestOptionScriptedSelectionInput MapDtoToServiceRequestOptionScriptedSelectionInput(IServiceRequestOptionScriptedSelectionInputDto src)
		{
			if (src == null) return null;

			return new ServiceRequestOptionScriptedSelectionInput
			{
				Id = src.Id,
				ScriptedSelectionInputId = src.ScriptedSelectionInputId,
				ServiceRequestOptionId = src.ServiceRequestOptionId,
				Value = src.Value
			};
		}

		private static IServiceRequestOptionSelectionInputDto MapServiceRequestOptionSelectionInputToDto(ServiceRequestOptionSelectionInput src)
		{
			if (src == null) return null;

			return new ServiceRequestOptionSelectionInputDto()
			{
				Id = src.Id,
				SelectionInputId = src.SelectionInputId,
				ServiceRequestOptionId = src.ServiceRequestOptionId,
				Value = src.Value,
				SelectionInput = MapSelectionInputToDto(src.SelectionInput),
				ServiceRequestOption = MapServiceRequestOptionToDto(src.ServiceRequestOption)
			};
		}

		public static ServiceRequestOptionSelectionInput MapDtoToServiceRequestOptionSelectionInput(IServiceRequestOptionSelectionInputDto src)
		{
			if (src == null) return null;

			return new ServiceRequestOptionSelectionInput
			{
				Id = src.Id,
				SelectionInputId = src.SelectionInputId,
				ServiceRequestOptionId = src.ServiceRequestOptionId,
				Value = src.Value
			};
		}

		private static IServiceRequestOptionTextInputDto MapServiceRequestOptionTextInputToDto(ServiceRequestOptionTextInput src)
		{
			if (src == null) return null;

			return new ServiceRequestOptionTextInputDto()
			{
				Id = src.Id,
				TextInputId = src.TextInputId,
				ServiceRequestOptionId = src.ServiceRequestOptionId,
				Value = src.Value,
				TextInput = MapTextInputToDto(src.TextInput),
				ServiceRequestOption = MapServiceRequestOptionToDto(src.ServiceRequestOption)
			};
		}

		public static ServiceRequestOptionTextInput MapDtoToServiceRequestOptionTextInput(IServiceRequestOptionTextInputDto src)
		{
			if (src == null) return null;

			return new ServiceRequestOptionTextInput
			{
				Id = src.Id,
				TextInputId = src.TextInputId,
				ServiceRequestOptionId = src.ServiceRequestOptionId,
				Value = src.Value
			};
		}
	}
}