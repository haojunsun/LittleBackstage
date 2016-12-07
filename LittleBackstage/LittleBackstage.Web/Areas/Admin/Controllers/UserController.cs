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
        public UserController(IManagerService managerService, IHelperServices helperServices,IRoleService roleService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
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
        public ActionResult ManagerList(string key, int? page, int? size)
        {
            return View();
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
        public ActionResult RoleList(string key, int? page, int? size)
        {
            return View();
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

        public ActionResult UserList(string key, int? page, int? size)
        {
            var list = new List<TestModel>();
            for (int i = 0; i < 30; i++)
            {
                var item = new TestModel();
                item.id = i;
                item.test1 = i + "渲染引擎";
                item.test2 = i + "浏览器";
                item.test3 = i + "平台";
                item.test4 = i + "引擎版本";
                item.test5 = i + "CSS等级";
                list.Add(item);
            }
            return View(list.OrderBy(x=>x.id).ToList());
        }
    }
}