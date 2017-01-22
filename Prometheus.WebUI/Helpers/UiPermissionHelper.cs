using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManager.Controllers;

namespace Prometheus.WebUI.Helpers
{
    public static class UiPermissionHelper
    {
        public static bool HasPermission<T>(int userId, T permission)
        {
            IPermissionController perm = new PermissionController();
            return perm.UserHasPermission(userId, permission);
        }
    }
}