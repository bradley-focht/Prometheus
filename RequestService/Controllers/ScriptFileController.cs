using System;
using System.Collections.Generic;
using System.Linq;
using Common.Controllers;
using Common.Dto;
using Common.Enums.Entities;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;

namespace RequestService.Controllers
{
	public class ScriptFileController : EntityController<IScriptDto>, IScriptFileController
	{
		public IScriptDto GetScript(int performingUserId, int scriptId)
		{
            // throw new NotImplementedException();
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
					foreach(var script in scripts)
					yield return ManualMapper.MapScriptToDto(script);
				}
			}
		}

		public IScriptDto ModifyScript(int performingUserId, IScriptDto script, EntityModification modification)
		{
            // throw new NotImplementedException();
            return base.ModifyEntity(performingUserId, script, modification);

        }

        protected override IScriptDto Create(int performingUserId, IScriptDto script)
		{
            // throw new NotImplementedException();
            using (var context = new PrometheusContext())
            {
                var document = context.Scripts.ToList().FirstOrDefault(x => x.ScriptFile == script.ScriptFile);
                if (document != null)
                {
                    throw new InvalidOperationException(string.Format("Service Document with ID {0} already exists.", script.ScriptFile));
                }
                var savedDocument = context.Scripts.Add(ManualMapper.MapDtoToScript(script));
                context.SaveChanges(performingUserId);
                return ManualMapper.MapScriptToDto(savedDocument);
            }
        }

		protected override IScriptDto Update(int performingUserId, IScriptDto script)
		{
            // throw new NotImplementedException();
            throw new ModificationException(string.Format("Modification {0} cannot be performed on Service Documents.", EntityModification.Update));

        }

        protected override IScriptDto Delete(int performingUserId, IScriptDto script)
		{
            // throw new NotImplementedException();
            using (var context = new PrometheusContext())
            {
                var toDelete = context.Scripts.ToList().FirstOrDefault(x => x.Id == script.Id);
                context.Scripts.Remove(toDelete);
                context.SaveChanges(performingUserId);
            }
            return null;
        }
	}
}
