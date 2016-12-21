using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Models;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Helpers;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    /* 分类字段管理 */
    public class CategoryFieldController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ILogService _logService;
        private readonly ICategoryFieldService _categoryFieldService;
        public CategoryFieldController(IManagerService managerService,
            IHelperServices helperServices,
            IRoleService roleService,
            IUserService userService,
            ICategoryService categoryService,
            ILogService logService, ICategoryFieldService categoryFieldService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
            _userService = userService;
            _categoryService = categoryService;
            _logService = logService;
            _categoryFieldService = categoryFieldService;
        }

        /// <summary>
        ///  绑定类别
        /// </summary>
        /// <param name="id"></param>
        private void TreeBindCategory(int id)
        {
            var list = new List<Category>();

            list = _categoryService.List().ToList();

            var selectList = new List<SelectListItem>();

            foreach (var r in list)
            {
                if (r.CategoryId == id)
                {
                    selectList.Add(new SelectListItem()
                    {
                        Value = r.CategoryId.ToString(),
                        Text = r.CategoryName,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem()
                    {
                        Value = r.CategoryId.ToString(),
                        Text = r.CategoryName,
                    });
                }
            }
            ViewBag.CategorySelect = selectList;
        }

        public ActionResult CategoryFieldsList()
        {
            TreeBindCategory(0);
            return View(new List<CategoryField>());
        }

        public ActionResult CategoryFieldsListByCategoryId(int categoryId)
        {
            TreeBindCategory(categoryId);
            var model = _categoryService.Get(categoryId);
            if (model == null)
            {
                return Content("<script>alert('参数错误,返回列表!');window.location.href='" + Url.Action("CategoryFieldsList") + "';</script>");
            }
            return View("CategoryFieldsList", model.CategoryFields);
        }

        public ActionResult AddCategoryField()
        {
            return View();
        }

        public ActionResult EditCategoryField()
        {
            return View();
        }

        public ActionResult DelCategoryField(int id)
        {
            return View();
        }
    }
}