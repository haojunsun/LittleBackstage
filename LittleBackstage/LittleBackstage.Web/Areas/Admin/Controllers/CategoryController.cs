using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        public CategoryController(IManagerService managerService, IHelperServices helperServices, IRoleService roleService, IUserService userService, ICategoryService categoryService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
            _userService = userService;
            _categoryService = categoryService;
        }

        /* 分类管理 */
        public ActionResult CategoryList()
        {
            var list = _categoryService.List().OrderByDescending(x => x.CreateTime);
            return View(list.ToList());
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        public ActionResult EditCategory(int id)
        {
            return View();
        }

        /* 分类字段管理 */
        public ActionResult CategoryFieldList()
        {
            return View();
        }    
    }
}