using System.Collections.Generic;
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
            var people = new List<string> {"Brad", "Sarah", "Sean", "Chris", "Jamie"};

            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it

            runspace.Open();

            runspace.Close();

            return Json(people, JsonRequestBehavior.AllowGet);

        }
    }
}