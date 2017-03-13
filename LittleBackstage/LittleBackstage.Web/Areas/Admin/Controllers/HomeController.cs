using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Basis;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Filters;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    //[LittleBackstageAuthorize]
    public class HomeController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly ISystemLogService _systemLogService;

        public HomeController(IManagerService managerService,
            IHelperServices helperServices,
            IRoleService roleService,
            ISystemLogService systemLogService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
            _systemLogService = systemLogService;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public string GetName()
        {
            var m = UserLogin.GetUserInfo("SESSION_USER_INFO");
            return m.UserName;
        }
    }
}