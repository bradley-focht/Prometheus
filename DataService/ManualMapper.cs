using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using DataService.Models;

namespace DataService
{
	/// <summary>
	/// Lazy loading from entities in the dataservice layer to DTOs
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
		public static IServiceOptionDto MapServiceOptionToDto(IServiceOption src)
		{
			if (src == null) return null;

			Lazy<IServiceOptionDto> option = new Lazy<IServiceOptionDto>(() => new ServiceOptionDto
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
				BasicRequest = src.BasicRequest,

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
				Utilization = src.Utilization,
				BasicRequest = src.BasicRequest
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
			Lazy<ServiceBundleDto> bundle = new Lazy<ServiceBundleDto>(() => new ServiceBundleDto
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				BusinessValue = src.BusinessValue,
				Measures = src.Measures
			});
			return bundle.Value;
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
				Code = src.Code,
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
				Code = src.Code,
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

		public static IRoleDto MapRoleToDto(IRole src)
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
				UserRoleAssignmentAccess = src.UserRoleAssignmentAccess,
				ServiceCatalogMaintenanceAccess = src.ServiceCatalogMaintenanceAccess,
				ServicePortfolioAccess = src.ServicePortfolioAccess,
				ApiAccess = src.ApiAccess,
				ScriptAccess = src.ScriptAccess,
				FulfillmentAccess = src.FulfillmentAccess,
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
				UserRoleAssignmentAccess = src.UserRoleAssignmentAccess,
				ServiceCatalogMaintenanceAccess = src.ServiceCatalogMaintenanceAccess,
				ServicePortfolioAccess = src.ServicePortfolioAccess,
				ApiAccess = src.ApiAccess,
				ScriptAccess = src.ScriptAccess,
				FulfillmentAccess = src.FulfillmentAccess,
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
				HelpToolTip = src.HelpToolTip,
				AvailableOnRemove = src.AvailableOnRemove,
				AvailableOnChange = src.AvailableOnChange,
				AvailableOnAdd = src.AvailableOnAdd
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
				HelpToolTip = src.HelpToolTip,
				AvailableOnRemove = src.AvailableOnRemove,
				AvailableOnChange = src.AvailableOnChange,
				AvailableOnAdd = src.AvailableOnAdd
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
				AvailableOnRemove = src.AvailableOnRemove,
				AvailableOnChange = src.AvailableOnChange,
				AvailableOnAdd = src.AvailableOnAdd
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
				AvailableOnRemove = src.AvailableOnRemove,
				AvailableOnAdd = src.AvailableOnAdd
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
				ScriptId = src.ScriptId,
				AvailableOnRemove = src.AvailableOnRemove,
				AvailableOnAdd = src.AvailableOnAdd
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
				ScriptId = src.ScriptId,
				AvailableOnRemove = src.AvailableOnRemove,
				AvailableOnAdd = src.AvailableOnAdd
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
				AdGuid = src.AdGuid,
				DepartmentId = src.DepartmentId
			};
		}

		public static IUserDto MapUserToDto(IUser src)
		{
			if (src == null) return null;

			List<IRoleDto> roles = new List<IRoleDto>();
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
				Name = src.Name,
				AdGuid = src.AdGuid,
				DepartmentId = src.DepartmentId,
				Department = MapDepartmentToDto(src.Department)
			};
		}

		public static IServiceRequestPackageDto MapServiceRequestPackageToDto(ServiceRequestPackage src)
		{
			if (src == null) return null;

			List<IServiceOptionCategoryTagDto> serviceOptionCategoryTags = new List<IServiceOptionCategoryTagDto>();
			if (src.ServiceOptionCategoryTags != null)
			{
				foreach (var tag in src.ServiceOptionCategoryTags)
				{
					serviceOptionCategoryTags.Add(new ServiceOptionCategoryTagDto
					{
						Id = tag.Id,
						Order = tag.Order,
						ServiceOptionCategoryId = tag.ServiceOptionCategoryId,
						ServiceRequestPackageId = tag.ServiceRequestPackageId,
						ServiceOptionCategory = MapOptionCategoryToDto(tag.ServiceOptionCategory),
						//ServiceRequestPackage = MapServiceRequestPackageToDto(tag.ServiceRequestPackage)
					});
				}
			}

			List<IServiceTagDto> serviceTags = new List<IServiceTagDto>();
			if (src.ServiceTags != null)
			{
				foreach (var tag in src.ServiceTags)
				{
					serviceTags.Add(new ServiceTagDto()
					{
						Id = tag.Id,
						Order = tag.Order,
						ServiceId = tag.ServiceId,
						ServiceRequestPackageId = tag.ServiceRequestPackageId,
						Service = MapServiceToDto(tag.Service),
						//ServiceRequestPackage = MapServiceRequestPackageToDto(tag.ServiceRequestPackage)
					});
				}
			}

			return new ServiceRequestPackageDto()
			{
				Id = src.Id,
				Name = src.Name,
				Action = src.Action,
				ServiceOptionCategoryTags = serviceOptionCategoryTags,
				ServiceTags = serviceTags
			};
		}

		public static ServiceOptionCategoryTag MapDtoToServiceOptionCategoryTag(IServiceOptionCategoryTagDto src)
		{
			if (src == null) return null;

			return new ServiceOptionCategoryTag
			{
				Id = src.Id,
				Order = src.Order,
				ServiceOptionCategoryId = src.ServiceOptionCategoryId,
				ServiceRequestPackageId = src.ServiceRequestPackageId
			};
		}

		public static ServiceTag MapDtoToServiceTag(IServiceTagDto src)
		{

			return new ServiceTag
			{
				Id = src.Id,
				Order = src.Order,
				ServiceId = src.ServiceId,
				ServiceRequestPackageId = src.ServiceRequestPackageId
			};
		}


		public static ServiceRequestPackage MapDtoToServiceRequestPackage(IServiceRequestPackageDto src)
		{
			if (src == null) return null;
			return new ServiceRequestPackage
			{
				Id = src.Id,
				Action = src.Action,
				Name = src.Name
			};
		}

		public static ServiceRequest MapDtoToServiceRequest(IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> src)
		{
			if (src == null) return null;
			return new ServiceRequest
			{
				Id = src.Id,
				Name = src.Name,
				Action = src.Action,
				State = src.State,
				ApprovedDate = src.ApprovedDate,
				ApproverUserId = src.ApproverUserId,
				Comments = src.Comments,
				CreationDate = src.CreationDate,
				Officeuse = src.Officeuse,
				RequestedByUserId = src.RequestedByUserId,
				RequestedForGuids = src.RequestedForGuids,
				RequestedByGuid = src.RequestedByGuid,
				SubmissionDate = src.SubmissionDate,
				RequestedForDate = src.RequestedForDate,
				ServiceOptionId = src.ServiceOptionId,
				DepartmentId = src.DepartmentId,
				CancelledDate = src.CancelledDate,
				DeniedDate = src.DeniedDate,
				FulfilledDate = src.FulfilledDate,
				FinalMonthlyPrice = src.FinalMonthlyPrice,
				FinalUpfrontPrice = src.FinalUpfrontPrice
			};
		}

		public static IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> MapServiceRequestToDto(ServiceRequest src)
		{
			if (src == null) return null;

			Lazy<ServiceRequestDto> serviceRequest = new Lazy<ServiceRequestDto>(() => new ServiceRequestDto
			{
				Id = src.Id,
				Name = src.Name,
				Action = src.Action,
				State = src.State,
				ApprovedDate = src.ApprovedDate,
				ApproverUserId = src.ApproverUserId,
				Comments = src.Comments,
				CreationDate = src.CreationDate,
				Officeuse = src.Officeuse,
				RequestedByUserId = src.RequestedByUserId,
				RequestedForGuids = src.RequestedForGuids,
				RequestedByGuid = src.RequestedByGuid,
				SubmissionDate = src.SubmissionDate,
				RequestedForDate = src.RequestedForDate,
				ServiceOptionId = src.ServiceOptionId,
				DepartmentId = src.DepartmentId,
				UpfrontPrice = src.UpfrontPrice,
				MonthlyPrice = src.MonthlyPrice,
				CancelledDate = src.CancelledDate,
				DeniedDate = src.DeniedDate,
				FulfilledDate = src.FulfilledDate,
				FinalMonthlyPrice = src.FinalMonthlyPrice,
				FinalUpfrontPrice = src.FinalUpfrontPrice
			});

			//options
			List<IServiceRequestOptionDto> serviceRequestOptions = new List<IServiceRequestOptionDto>();
			if (src.ServiceRequestOptions != null)
			{
				foreach (var serviceRequestOption in src.ServiceRequestOptions)
				{
					serviceRequestOptions.Add(MapServiceRequestOptionToDto(serviceRequestOption));
				}
			}
			//user input data
			List<IServiceRequestUserInputDto> serviceRequestInputs = new List<IServiceRequestUserInputDto>();
			if (src.ServiceRequestUserInputs != null)
			{
				foreach (var userInput in src.ServiceRequestUserInputs)
				{
					serviceRequestInputs.Add(MapServiceRequestUserInputToDto(userInput));
				}
			}

			serviceRequest.Value.ServiceRequestOptions = serviceRequestOptions;
			serviceRequest.Value.ServiceRequestUserInputs = serviceRequestInputs;

			//approval comments


			return serviceRequest.Value;
		}

		public static IServiceRequestOptionDto MapServiceRequestOptionToDto(ServiceRequestOption src)
		{
			if (src == null) return null;

			return new ServiceRequestOptionDto()
			{
				Id = src.Id,
				ApproverUserId = src.ApproverUserId,
				RequestedByUserId = src.RequestedByUserId,
				ServiceOptionId = src.ServiceOptionId,
				Quantity = src.Quantity,
				Code = src.Code,
				ServiceOptionName = src.ServiceOptionName,
				ServiceRequestId = src.ServiceRequestId,
				BasicRequest = src.BasicRequest,
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
				Quantity = src.Quantity,
				Code = src.Code,
				ServiceOptionName = src.ServiceOptionName,
				ServiceOptionId = src.ServiceOptionId,
				ServiceRequestId = src.ServiceRequestId,
				BasicRequest = src.BasicRequest
			};
		}

		public static Script MapDtoToScript(IScriptDto src)
		{
			if (src == null)
				return null;

			return new Script
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				Version = src.Version,
				ScriptFile = src.ScriptFile,
				MimeType = src.MimeType,
				UploadDate = src.UploadDate
			};
		}

		public static ScriptDto MapScriptToDto(IScript src)
		{
			if (src == null)
				return null;

			return new ScriptDto
			{
				Id = src.Id,
				Name = src.Name,
				Description = src.Description,
				Version = src.Version,
				ScriptFile = src.ScriptFile,
				MimeType = src.MimeType,
				UploadDate = src.UploadDate
			};
		}

		public static IServiceRequestUserInputDto MapServiceRequestUserInputToDto(ServiceRequestUserInput src)
		{
			if (src == null)
				return null;

			return new ServiceRequestUserInputDto()
			{
				Id = src.Id,
				Name = src.Name,
				ServiceRequestId = src.ServiceRequestId,
				InputId = src.InputId,
				UserInputType = src.UserInputType,
				Value = src.Value
			};
		}

		public static ServiceRequestUserInput MapDtoToServiceRequestUserInput(IServiceRequestUserInputDto src)
		{
			if (src == null)
				return null;

			return new ServiceRequestUserInput
			{
				Id = src.Id,
				Name = src.Name,
				ServiceRequestId = src.ServiceRequestId,
				InputId = src.InputId,
				UserInputType = src.UserInputType,
				Value = src.Value
			};
		}

		public static Department MapDtoToDepartment(IDepartmentDto src)
		{
			if (src == null)
				return null;

			return new Department
			{
				Id = src.Id,
				Name = src.Name
			};
		}

		public static IDepartmentDto MapDepartmentToDto(Department src)
		{
			if (src == null)
				return null;

			return new DepartmentDto
			{
				Id = src.Id,
				Name = src.Name,
			};
		}

		public static IApprovalDto MapApprovalToDto(Approval src)
		{
			if (src == null)
				return null;

			return new ApprovalDto
			{
				Id = src.Id,
				ServiceRequestId = src.ServiceRequestId,
				Comments = src.Comments,
				ApproverId = src.ApproverId,
				RequestorId = src.RequestorId,
				Result = src.Result,
			};
		}

		public static Approval MapDtoToApproval(IApprovalDto src)
		{
			if (src == null)
				return null;

			return new Approval()
			{
				Id = src.Id,
				ServiceRequestId = src.ServiceRequestId,
				Comments = src.Comments,
				ApproverId = src.ApproverId,
				RequestorId = src.RequestorId,
				Result = src.Result,
			};
		}
	}
}
