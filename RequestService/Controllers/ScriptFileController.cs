using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;
using Common.Controllers;
using DataService;
using DataService.DataAccessLayer;

namespace RequestService.Controllers
{
	class ScriptFileController : EntityController<ScriptDto>, IScriptFileController
	{
		public ScriptDto GetScript(int id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Retrieve all scripts
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ScriptDto> GetScripts()
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

		public ScriptDto ModifyScript(IScriptDto script, EntityModification modification)
		{
			throw new NotImplementedException();
		}

		protected override ScriptDto Create(ScriptDto entity)
		{
			throw new NotImplementedException();
		}

		protected override ScriptDto Update(ScriptDto entity)
		{
			throw new NotImplementedException();
		}

		protected override ScriptDto Delete(ScriptDto entity)
		{
			throw new NotImplementedException();
		}
	}
}
