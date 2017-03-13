using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Basis;
using LittleBackstage.Core.Models;
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
        private readonly ISystemLogService _systemLogService;
        public UserController(IManagerService managerService,
            IHelperServices helperServices,
            IRoleService roleService,
            IUserService userService,
            ISystemLogService systemLogService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
            _userService = userService;
            _systemLogService = systemLogService;
        }
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        /*管理员*/
        public ActionResult AddManager()
        {
            TreeBindRole(0, 0);
            return View();
        }

        /// <summary>
        ///  绑定类别
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        private void TreeBindRole(int type, int id)
        {
            var list = new List<Role>();

            list = _roleService.List().Where(x => x.RoleType == type).ToList();

            var selectList = new List<SelectListItem>();

            foreach (var r in list)
            {
                if (r.RoleId == id)
                {
                    selectList.Add(new SelectListItem()
                    {
                        Value = r.RoleId.ToString(),
                        Text = r.RoleName,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem()
                    {
                        Value = r.RoleId.ToString(),
                        Text = r.RoleName,
                    });
                }
            }
            ViewBag.RoleSelect = selectList;
        }

        [HttpPost]
        public ActionResult AddManager(string userName, string passWord, int? IsExamineRadios = 0, int? role = 0)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                return Content("<script>alert('创建失败,登录帐号或密码不能为空!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
            }
            if (role == 0)
            {
                return Content("<script>alert('创建失败,未选择角色!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
            }
            if (_managerService.FindByUserName(userName))
            {
                return Content("<script>alert('创建失败,登录帐号已存在!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
            }

            var manager = new Manager();
            manager.IsExamine = (int)IsExamineRadios;
            manager.IsEnable = (int)IsExamineRadios;
            manager.PassWord = _helperServices.MD5CSP(passWord);
            manager.Register = DateTime.Now;
            manager.UserName = userName;
            manager.Role = _roleService.Get((int)role);

            _managerService.Add(manager);
            return Content("<script>alert('创建成功!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
        }

        public ActionResult EditManager(int id)
        {
            var model = _managerService.Get(id);
            if (model == null)
            {
                return Content("<script>alert('参数错误,返回列表!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
            }
            if (model.Role != null)
                TreeBindRole(0, model.Role.RoleId);
            else
                TreeBindRole(0, 0);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditManager(Manager m, string passWord, int? IsExamineRadios = 0)
        {
            if (string.IsNullOrEmpty(m.UserName))
            {
                return Content("<script>alert('编辑失败,登录帐号不能为空!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
            }

            var old = _managerService.Get(m.ManagerId);
            if (old == null)
            {
                return Content("<script>alert('编辑失败,数据错误!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
            }

            if (!string.IsNullOrEmpty(passWord))
            {
                old.PassWord = _helperServices.MD5CSP(passWord);
            }
            old.IsEnable = (int)IsExamineRadios;
            old.IsExamine = (int)IsExamineRadios;
            if (m.Role.RoleId != 0)
            {
                old.Role = _roleService.Get(m.Role.RoleId);
            }
            old.UserName = m.UserName;
            _managerService.Update(old);
            return Content("<script>alert('编辑成功!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
        }

        public ActionResult ManagerList()
        {
            var list = _managerService.List().Where(x => x.UserName != "admin").OrderByDescending(x => x.Register);
            return View(list.ToList());
        }

        public ActionResult DelManager(int id)
        {
            var m = _managerService.Get(id);
            if (m == null)
            {
                return Content("<script>alert('删除失败,参数错误!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
            }
            _managerService.Delete(id);
            return Content("<script>alert('删除成功!');window.location.href='" + Url.Action("ManagerList") + "';</script>");
        }

        /*角色*/
        public ActionResult AddRole()
        {
            return View();
        }

        public ActionResult EditRole(int id)
        {
            var model = _roleService.Get(id);
            if (model == null)
            {
                return Content("<script>alert('参数错误,返回列表!');window.location.href='" + Url.Action("RoleList") + "';</script>");
            }
            return View(model);
        }

        public ActionResult RoleList()
        {
            var list = _roleService.List().Where(x => !x.Permissions.Contains("all")).OrderByDescending(x => x.CreateTime);
            return View(list.ToList());
        }
        public ActionResult DelRole(int id)
        {
            var m = _roleService.Get(id);
            if (m == null)
            {
                return Content("<script>alert('删除失败,参数错误!');window.location.href='" + Url.Action("RoleList") + "';</script>");
            }
            _roleService.Delete(id);
            return Content("<script>alert('删除成功!');window.location.href='" + Url.Action("RoleList") + "';</script>");
        }

        [HttpPost]
        public ActionResult AddRole(string roleName, int? roleType = 0)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return Content("<script>alert('创建失败,角色名不能为空!');window.location.href='" + Url.Action("RoleList") + "';</script>");
            }

            var permissions = Request.Form["permissions"];
            if (string.IsNullOrEmpty(permissions))
            {
                return Content("<script>alert('创建失败,权限不能为空!');window.location.href='" + Url.Action("RoleList") + "';</script>");
            }
            var role = new Role();
            role.CreateTime = DateTime.Now;
            role.Permissions = permissions;
            role.RoleName = roleName;
            role.RoleType = (int)roleType;
            _roleService.Add(role);
            return Content("<script>alert('创建成功!');window.location.href='" + Url.Action("RoleList") + "';</script>");
        }

        [HttpPost]
        public ActionResult EditRole(Role r, int? roleType = 0)
        {
            if (string.IsNullOrEmpty(r.RoleName))
            {
                return Content("<script>alert('编辑失败,角色名不能为空!');window.location.href='" + Url.Action("RoleList") + "';</script>");
            }

            var permissions = Request.Form["permissions"];
            if (string.IsNullOrEmpty(permissions))
            {
                return Content("<script>alert('编辑失败,权限不能为空!');window.location.href='" + Url.Action("RoleList") + "';</script>");
            }

            var old = _roleService.Get(r.RoleId);
            if (r.RoleName != old.RoleName && _roleService.GetByName(r.RoleName))
            {
                return Content("<script>alert('编辑失败,角色名已存在!');window.location.href='" + Url.Action("RoleList") + "';</script>");
            }

            old.Permissions = permissions;
            old.RoleName = r.RoleName;
            old.RoleType = (int)roleType;
            _roleService.Update(old);
            return Content("<script>alert('编辑成功!');window.location.href='" + Url.Action("RoleList") + "';</script>");
        }

        /// <summary>
        /// 判断权限是否被选中
        /// </summary>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public string JudgeCheck(string permissionlist, string permission)
        {
            var i = _helperServices.GetStrCount(permissionlist, permission);
            return i > 0 ? "checked" : null;
        }

        /*用户*/
        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult EditUser(int id)
        {
            var model = _userService.Get(id);
            if (model == null)
            {
                return Content("<script>alert('参数错误,返回列表!');window.location.href='" + Url.Action("UserList") + "';</script>");
            }
            TreeBindRole(1, model.Role.RoleId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(User u, string passWord, int? IsExamineRadios = 0)
        {
            var m = UserLogin.GetUserInfo("SESSION_USER_INFO");
            var old = _userService.Get(u.UserId);
            if (old == null)
            {
                _systemLogService.UserLog(m.UserName, m.ManagerId, "编辑会员资料-失败", "编辑会员资料-失败", old.UserId);
                return Content("<script>alert('编辑失败,数据错误!');window.location.href='" + Url.Action("UserList") + "';</script>");
            }
            if (!string.IsNullOrEmpty(passWord))
            {
                old.PassWord = _helperServices.MD5CSP(passWord);
            }

            var operate = "";
            if (old.IsEnable != (int)IsExamineRadios)
            {
                operate = "审核会员";
            }
            old.IsEnable = (int)IsExamineRadios;
            old.IsExamine = (int)IsExamineRadios;
            if (u.Role.RoleId != 0 && u.Role.RoleId != old.Role.RoleId)
            {
                old.Role = _roleService.Get(u.Role.RoleId);
            }
            _userService.Update(old);
            _systemLogService.UserLog(m.UserName, m.ManagerId, operate, operate, old.UserId);
            return Content("<script>alert('编辑成功!');window.location.href='" + Url.Action("UserList") + "';</script>");
        }

        public ActionResult UserList()
        {
            var list = _userService.List().OrderByDescending(x => x.Register);
            return View(list.ToList());
        }
        public ActionResult DelUser(int id)
        {
            var m = _userService.Get(id);
            if (m == null)
            {
                return Content("<script>alert('删除失败,参数错误!');window.location.href='" + Url.Action("UserList") + "';</script>");
            }
            _userService.Delete(id);
            return Content("<script>alert('删除成功!');window.location.href='" + Url.Action("UserList") + "';</script>");
        }
    }
}