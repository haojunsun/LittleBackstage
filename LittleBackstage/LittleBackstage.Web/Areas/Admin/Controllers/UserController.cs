using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Areas.Admin.Models;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public UserController(IManagerService managerService, IHelperServices helperServices, IRoleService roleService, IUserService userService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
            _userService = userService;
        }
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        /*管理员*/
        public ActionResult AddManager()
        {
            return View();
        }
        public ActionResult EditManager(int id)
        {
            return View();
        }
        public ActionResult ManagerList()
        {
            var list = _managerService.List().Where(x => x.UserName != "admin").OrderByDescending(x => x.Register);
            return View(list.ToList());
        }

        /*角色*/
        public ActionResult AddRole()
        {
            return View();
        }
        public ActionResult EditRole(int id)
        {
            return View();
        }
        public ActionResult RoleList()
        {
            var list = _roleService.List().OrderByDescending(x => x.CreateTime);
            return View(list.ToList());
        }

        /*用户*/
        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult EditUser(int id)
        {
            return View();
        }
        public ActionResult UserList()
        {
            var list = _userService.List().OrderByDescending(x => x.Register);
            return View(list.ToList());
        }
    }
}