using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Management.Automation.Runspaces;
using System.Web.Mvc;
using DataService.Models;
using Prometheus.WebUI.Models.Shared;

namespace Prometheus.WebUI.Controllers
{
    public class ScriptController : Controller
    {
		/// <summary>
		/// Default page of Scripting
		/// </summary>
		/// <returns></returns>
	    public ActionResult Index()
	    {
		    return View();
	    }

        /// <summary>
        /// For adding scripts scripts
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveScript(int id)
        {
            return View();
        }

        /// <summary>
        /// To get a specific entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetScript(int id)
        {
            LinkListModel model = new LinkListModel();
            return View("PartialViews/_LinkList", model);
        }

        public ActionResult Add()
        {
            return View();
        }

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