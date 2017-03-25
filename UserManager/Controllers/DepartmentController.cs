using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace UserManager.Controllers
{
	public class DepartmentController : EntityController<IDepartmentDto>, IDepartmentController
	{
		public IDepartmentDto GetDepartment(int performingUserId, int departmentId)
		{
			using (var context = new PrometheusContext())
			{
				return ManualMapper.MapDepartmentToDto(context.Departments.FirstOrDefault(x => x.Id == departmentId));
			}
		}

		public IEnumerable<IDepartmentDto> GetDepartments(int performingUserId)
		{
			using (var context = new PrometheusContext())
			{
				foreach (var department in context.Departments)
				{
					yield return ManualMapper.MapDepartmentToDto(department);
				}
			}
		}

		public string GetDepartmentFromScript(int scriptId)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.Scripts.ToList().FirstOrDefault(x => x.Id == scriptId);
				string department = document.ScriptFile.ToString();
				return department;
			}
		}

		public IDepartmentDto ModifyDepartment(int performingUserId, IDepartmentDto departmentDto, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, departmentDto, modification);
		}

		protected override IDepartmentDto Create(int performingUserId, IDepartmentDto departmentDto)
		{
			using (var context = new PrometheusContext())
			{
				var department = context.Departments.Find(departmentDto.Id);
				if (department != null)
				{
					throw new InvalidOperationException(string.Format("Department with ID {0} already exists.", departmentDto.Id));
				}
				var savedDepartment = context.Departments.Add(ManualMapper.MapDtoToDepartment(departmentDto));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapDepartmentToDto(savedDepartment);
			}
		}

		protected override IDepartmentDto Update(int performingUserId, IDepartmentDto departmentDto)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.Departments.Any(x => x.Id == departmentDto.Id))
				{
					throw new InvalidOperationException(string.Format("Department with ID {0} cannot be updated since it does not exist.",
						departmentDto.Id));
				}
				var updatedDepartment = ManualMapper.MapDtoToDepartment(departmentDto);
				context.Departments.Attach(updatedDepartment);
				context.Entry(updatedDepartment).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapDepartmentToDto(updatedDepartment);
			}
		}

		protected override IDepartmentDto Delete(int performingUserId, IDepartmentDto departmentDto)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.Departments.Find(departmentDto.Id);
				context.Departments.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}
