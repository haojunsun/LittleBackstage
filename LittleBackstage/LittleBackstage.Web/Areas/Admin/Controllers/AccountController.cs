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
        public AccountController(IManagerService managerService, IHelperServices helperServices)
        {
            _managerService = managerService;
            _helperServices = helperServices;
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
 
            var result = _managerService.LoginByPassword(username,  _helperServices.MD5CSP(password));
            if (result == null)
            {
                ajaxBack.code = 400;
                ajaxBack.message = "账号密码错误！";
                return Json(ajaxBack, JsonRequestBehavior.DenyGet);
            }
            ajaxBack.returnUrl = Url.Action("Index", "Home");
            return Json(ajaxBack, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
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