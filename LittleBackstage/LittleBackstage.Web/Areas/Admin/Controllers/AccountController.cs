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
        public AccountController(IManagerService managerService)
        {
            _managerService = managerService;
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

            var result = _managerService.LoginByPassword(username, password);
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
    }
}