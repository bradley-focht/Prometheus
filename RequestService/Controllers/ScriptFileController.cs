using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace RequestService.Controllers
{
	public class ScriptFileController : EntityController<IScriptDto>, IScriptFileController
	{
		/// <summary>
		/// Retrieve a single script
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptId">script id</param>
		/// <returns></returns>
		public IScriptDto GetScript(int performingUserId, int scriptId)
		{
			using (var context = new PrometheusContext())
			{
				var document = context.Scripts.ToList().FirstOrDefault(x => x.Id == scriptId);
				return ManualMapper.MapScriptToDto(document);
			}
		}

		/// <summary>
		/// Retrieve all scripts
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IScriptDto> GetScripts(int performingUserId)
		{
			using (var context = new PrometheusContext())
			{
				var scripts = context.Scripts;
				if (scripts != null)
				{
					foreach (var script in scripts)
						yield return ManualMapper.MapScriptToDto(script);
				}
			}
		}

		/// <summary>
		/// Modify a script
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="script">script to modify</param>
		/// <param name="modification">change to make</param>
		/// <returns></returns>
		public IScriptDto ModifyScript(int performingUserId, IScriptDto script, EntityModification modification)
		{
			return base.ModifyEntity(performingUserId, script, modification);

		}

		protected override IScriptDto Create(int performingUserId, IScriptDto scriptDto)
		{
			using (var context = new PrometheusContext())
			{
				var script = context.Scripts.FirstOrDefault(x => x.ScriptFile == scriptDto.ScriptFile);
				if (script != null)
				{
					throw new InvalidOperationException(string.Format("Service Document with ID {0} already exists.", script.ScriptFile));
				}
				var savedDocument = context.Scripts.Add(ManualMapper.MapDtoToScript(scriptDto));
				context.SaveChanges(performingUserId);
				return ManualMapper.MapScriptToDto(savedDocument);
			}
		}

		protected override IScriptDto Update(int performingUserId, IScriptDto script)
		{
			using (var context = new PrometheusContext())
			{
				if (!context.Scripts.Any(x => x.Id == script.Id))
				{
					throw new InvalidOperationException(string.Format("Script with ID {0} cannot be updated since it does not exist.", script.Id));
				}
				var updated = ManualMapper.MapDtoToScript(script);
				context.Entry(updated).State = EntityState.Modified;
				context.SaveChanges(performingUserId);
				return ManualMapper.MapScriptToDto(updated);
			}

		}

		protected override IScriptDto Delete(int performingUserId, IScriptDto script)
		{
			using (var context = new PrometheusContext())
			{
				var toDelete = context.Scripts.Find(script.Id);
				context.Scripts.Remove(toDelete);
				context.SaveChanges(performingUserId);
			}
			return null;
		}
	}
}
