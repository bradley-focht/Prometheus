using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
    public class ScriptController : Controller
    {
        // GET: Script
        public JsonResult People()
        {
            var people = new List<string>();

            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it

            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("[string[]] $run");
            string[] peeps = {"Brad", "Jamie"};
            runspace.SessionStateProxy.SetVariable("run", peeps );
            Collection<PSObject> results = pipeline.Invoke();
            runspace.Close();

            foreach (PSObject obj in results)
            {
                people.Add(obj.ToString());
            }

            return Json(people, JsonRequestBehavior.AllowGet);

        }
    }
}