using System;
using System.Collections.Generic;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;

namespace RequestService.Controllers
{
	public class ScriptFileController : EntityController<IScriptDto>, IScriptFileController
	{
		public IScriptDto GetScript(int performingUserId, int id)
		{
			throw new NotImplementedException();
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
					foreach(var script in scripts)
					yield return ManualMapper.MapScriptToDto(script);
				}
			}
		}

		public IScriptDto ModifyScript(int performingUserId, IScriptDto script, EntityModification modification)
		{
			throw new NotImplementedException();
		}

		protected override IScriptDto Create(int performingUserId, IScriptDto entity)
		{
			throw new NotImplementedException();
		}

		protected override IScriptDto Update(int performingUserId, IScriptDto entity)
		{
			throw new NotImplementedException();
		}

		protected override IScriptDto Delete(int performingUserId, IScriptDto entity)
		{
			throw new NotImplementedException();
		}
	}
}
