using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using LittleBackstage.Core.Basis;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Web.Filters
{
    public class LittleBackstageAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly LogService Log = new LogService();

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = UserLogin.GetUserInfo("SESSION_USER_INFO");
            //先判断是否登录
            if (currentUser != null)
            {
                //从session 中读取管理员信息
                var rd = httpContext.Request.RequestContext.RouteData;
                var controllerName = rd.GetRequiredString("controller").ToLower(); //当前访问的controller名称
                var actionName = rd.GetRequiredString("action").ToLower(); //当前访问的action名称
                var permission = UserLogin.GetPermission("SESSION_ADMIN_PERMISSIONS");
                if (permission == null)
                    return false;

                if (permission.Contains("all")) //超级管理员
                    return true;

                //if (controllerName == "admin" && actionName == "password")
                //    return true;
                if (permission.IndexOf(controllerName + "_" + actionName) > -1)
                {
                    return true;
                }
                //返回true表示验证通过，返回false表示验证失败，服务器发起401错误提示
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证未通过时默认是返回401错误，这里可对该行为进行自定义，跳转用户到指定的地址
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //1.获取 来访的 参数
            //var returnUri = filterContext.RequestContext.HttpContext.Request.Url;
            //var reqParams = filterContext.RequestContext.HttpContext.Request.Params;
            //Log.Info("HandleUnauthorizedRequest: {returnUri} - {reqParams}");
            var currentUser = UserLogin.GetUserInfo("SESSION_USER_INFO");
            if (currentUser == null)
            {
                Log.Info("授权跳转：URL={returnUri}失败");
                filterContext.Result = new RedirectToRouteResult
                    (
                    new RouteValueDictionary(new { controller = "Home", action = "Login" })
                    );
            }
        }
    }
}