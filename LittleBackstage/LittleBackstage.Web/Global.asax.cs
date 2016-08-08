using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Web
{
    public class MvcApplication : HttpApplication
    {
        private readonly LogService _log = new LogService();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            var objExp = HttpContext.Current.Server.GetLastError();
            var strErr = new StringBuilder();
            strErr.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            strErr.Append("\r\n.客户信息：");
            var ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null ? Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").Trim() : Request.ServerVariables.Get("Remote_Addr").Trim();
            strErr.Append("\r\n\tIp:" + ip);
            strErr.Append("\r\n\t浏览器:" + Request.Browser.Browser);
            strErr.Append("\r\n\t浏览器版本:" + Request.Browser.MajorVersion);
            strErr.Append("\r\n\t操作系统:" + Request.Browser.Platform);
            strErr.Append("\r\n.错误信息：");
            strErr.Append("\r\n\t页面：" + Request.Url);
            strErr.Append("\r\n\t错误信息：" + objExp.Message);
            strErr.Append("\r\n\t错误源：" + objExp.Source);
            strErr.Append("\r\n\t异常方法：" + objExp.TargetSite);
            strErr.Append("\r\n\t堆栈信息：" + objExp.StackTrace);
            strErr.Append("\r\n--------------------------------------------------------------------------------------------------");
            _log.Error(strErr.ToString());
            //处理完及时清理异常 
            Server.ClearError();
            strErr.Clear();
            if (objExp.Message.Contains("ApiException"))
            {
                Response.Redirect("/Error/index?exLevel=1");
            }
            else
            {
                //Response.Redirect("/Home");
                Response.Redirect("/Error");
            }
            //LogHelper.ErrorLog("<br/><strong>客户机IP</strong>：" + Request.UserHostAddress + "<br /><strong>错误地址</strong>：" + Request.Url, objExp);
            //Response.Redirect("/home");
        }
    }
}
