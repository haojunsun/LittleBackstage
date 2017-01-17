using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Models;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Filters;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    // [LittleBackstageAuthorize]
    public class AccountController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly ISystemLogService _systemLogService;
        public AccountController(IManagerService managerService,
            IHelperServices helperServices,
            IRoleService roleService,
            ISystemLogService systemLogService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
            _systemLogService = systemLogService;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var ajaxBack = new AjaxResponse();
            ajaxBack.code = 200;

            var result = _managerService.LoginByPassword(username, _helperServices.MD5CSP(password));
            if (result == null)
            {
                ajaxBack.code = 400;
                ajaxBack.message = "账号密码错误！";
                _systemLogService.LoginLog(username, 0, "管理员登录失败-账号密码错误!", "管理员登录失败", 0);
                return Json(ajaxBack, JsonRequestBehavior.DenyGet);
            }
            if (result.IsExamine == 1 && result.IsEnable == 1)
            {
                ajaxBack.returnUrl = Url.Action("Index", "Home");
                //写session
                _helperServices.SetSession("SESSION_USER_INFO", result);
                if (result.Role != null && !string.IsNullOrEmpty(result.Role.Permissions))
                    _helperServices.SetSession("SESSION_ADMIN_PERMISSIONS", result.Role.Permissions);

                Session.Timeout = 60;
                _helperServices.WriteCookie("SESSION_USER_NAME", result.UserName, 1440);
                _systemLogService.LoginLog(result.UserName, result.ManagerId, "管理员登录", "管理员登录", 0);
            }
            else
            {
                ajaxBack.code = 500;
                ajaxBack.message = "账号已冻结无法登陆！";
                _systemLogService.LoginLog(result.UserName, result.ManagerId, "管理员登录失败-账号已冻结", "管理员登录失败", 0);
            }
            return Json(ajaxBack, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            //_systemLogService.LoginLog(result.UserName, result.ManagerId, "管理员登录失败-账号已冻结", "管理员登录失败", 0);
            Session["SESSION_USER_INFO"] = null;
            Session["SESSION_ADMIN_PERMISSIONS"] = null;
            _helperServices.WriteCookie("SESSION_USER_NAME", "");
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            var ajaxBack = new AjaxResponse();
            ajaxBack.code = 200;
            var m = new Manager();
            m.IsEnable = 0;
            m.IsExamine = 0;
            m.PassWord = _helperServices.MD5CSP(password);
            m.Register = DateTime.Now;
            m.UserName = username;
            //m.Role 需要默认一个 注册角色
            if (_managerService.FindByUserName(username))
            {
                ajaxBack.message = "登录名重复！";
                ajaxBack.code = 400;
                return Json(ajaxBack, JsonRequestBehavior.DenyGet);
            }
            _managerService.Add(m);
            ajaxBack.returnUrl = Url.Action("Login");
            return Json(ajaxBack, JsonRequestBehavior.DenyGet);
        }
    }
}