using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Models;
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

        public ActionResult DelCategory(int id)
        {
            var m = _categoryService.Get(id);
            if (m == null)
            {
                return Content("<script>alert('删除失败,参数错误!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }
            _categoryService.Delete(id);
            return Content("<script>alert('删除成功!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(string categoryName, string explain, int? IsEnableRadios = 0)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return Content("<script>alert('创建失败,分类名不能为空!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }

            if (_categoryService.FindByName(categoryName) != null)
            {
                return Content("<script>alert('创建失败,分类名已存在!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }
            var category = new Category();
            category.CreateTime = DateTime.Now;
            category.CategoryName = categoryName;
            category.Explain = explain;
            category.IsEnable = (int)IsEnableRadios;
            category.IsCreateTable = 0;//默认创建分类 不创建模板（数据表）
            //category.DataTableName = _helperServices.GetAppSettings("DataTableName") + GenerateCode();//前缀+ 随机数
            //category.DataTableFieldSet = "";
            _categoryService.Add(category);
            return Content("<script>alert('创建成功!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
        }
        public ActionResult EditCategory(int id)
        {
            var model = _categoryService.Get(id);
            if (model == null)
            {
                return Content("<script>alert('参数错误,返回列表!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }        
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(Category c,int? IsEnableRadios = 0)
        {
            if (string.IsNullOrEmpty(c.CategoryName))
            {
                return Content("<script>alert('编辑失败,分类名不能为空!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }
            var old = _categoryService.Get(c.CategoryId);
            if (old == null)
            {
                return Content("<script>alert('编辑失败,数据错误!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }
            if (old.CategoryName != c.CategoryName && _categoryService.FindByName(c.CategoryName) != null)
            {
                return Content("<script>alert('编辑失败,分类名已存在!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }

            old.IsEnable = (int)IsEnableRadios;
            old.CategoryName = c.CategoryName;
            _categoryService.Update(old);
            return Content("<script>alert('编辑成功!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
        }

        public ActionResult CreateTableForCategory(int id)
        {
            var old = _categoryService.Get(id);
            if (old == null)
            {
                return Content("<script>alert('创建模版失败,参数错误!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }
            old.IsCreateTable = 1;
            old.DataTableName = _helperServices.GetAppSettings("DataTableName") + old.CategoryId;//前缀+ 分类id
            try
            {
                _categoryService.Update(old);


                //执行sql 创建 

            }
            catch (Exception ex)
            {

            }
            return Content("<script>alert('创建成功!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
        }

        /* 分类字段管理 */
        public ActionResult CategoryFieldList()
        {
            return View();
        }

        public ActionResult CategoryFieldsList(int categoryId)
        {
            return View();
        }

        private static string GenerateCode(int length = 4)
        {
            Random Rnd = new Random(DateTime.Now.Millisecond);
            //吧0 1 小写l 都过滤了
            const string vCode = "2356789ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz";

            var rndStr = String.Empty;

            for (var i = 0; i < length; i++)
                rndStr += vCode[Rnd.Next(0, vCode.Length)];

            return rndStr;
        }
    }
}