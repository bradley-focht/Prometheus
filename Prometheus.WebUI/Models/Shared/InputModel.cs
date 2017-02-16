using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Models;

namespace Prometheus.WebUI.Models.Shared
{
    public class InputModel<T>
    {
        public T Control { get; set; }
        public string ControlName { get; set; }
    }
}