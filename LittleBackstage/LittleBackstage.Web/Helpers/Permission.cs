using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleBackstage.Core.Basis;

namespace LittleBackstage.Web.Helpers
{
    public static class Permission
    {
        public static bool Check(string action, string controller)
        {
            var permission = UserLogin.GetPermission("SESSION_ADMIN_PERMISSIONS"); ;
            if (permission == null)
                return false;

            if (permission.Contains("all"))
                return true;

            if (permission.ToLower().IndexOf(controller.ToLower() + "_" + action.ToLower()) > -1)
            {
                return true;
            }
            return false;
        }

        public static bool Check(string action)
        {
            var controller = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");

            return Check(action, controller);
        }

      
    }//Helpers.Permission.CheckTwo("Edit", "Admin")
}