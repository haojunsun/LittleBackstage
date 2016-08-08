using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Web.Helpers
{
    public class ControllerHelper : Controller
    {
        private readonly ISimpleAccountManager _simpleAccount;
        //private readonly IArticleService _articleService;

        public ControllerHelper(ISimpleAccountManager simpleAccount)
        {
            _simpleAccount = simpleAccount;
            //_articleService = articleService;
        }

        public ContentResult CResult(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            return Content(message);
        }


        public ActionResult JumpUrl(string page, string message)
        {
            return Content("<script>alert('" + message + "');window.location.href='" + Url.Action(page) + "';</script>");
        }

        /// <summary>
        /// 检查当前用户是否具有特定的权限代码
        /// </summary>
        /// <param name="permissionCode">权限代码字符串，如果有多个则用空格进行分割，符合其中一个即返回真值。</param>
        public bool Check(string permissionCode)
        {
            string[] permissions = _simpleAccount.GetCurrentPermissions().ToArray();

            //valid for all permissions
            return permissions[0] == "*" || permissionCode.Split(' ').Any(code => permissions.Contains(code.ToLower()));
            //check if permission code exists in the user's permission list
        }

        /// <summary>
        /// 检查当前用户是否不具有特定的权限代码
        /// </summary>
        /// <param name="permissionCode">权限代码字符串，如果有多个则用空格进行分割，符合其中一个即可。</param>
        public bool CheckNot(string permissionCode)
        {
            string[] permissions = _simpleAccount.GetCurrentPermissions().ToArray();
            foreach (var code in permissionCode.Split(' '))
            {
                if (permissions.Contains(code.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}