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
            ViewBag.id = categoryId;
            return View("CategoryFieldsList", model.CategoryFields);
        }

        public ActionResult AddCategoryField(int categoryId)
        {
            ViewBag.id = categoryId;
            return View();
        }

        [HttpPost]
        public ActionResult AddCategoryField(int categoryId, string fieldName, string idEntity, string explain, int? IsShow = 0)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return Content("<script>alert('名称不能为空!');window.location.href='" + Url.Action("AddCategoryField", new { categoryId }) + "';</script>");
            }
            if (string.IsNullOrEmpty(idEntity))
            {
                return Content("<script>alert('标识不能为空!');window.location.href='" + Url.Action("AddCategoryField", new { categoryId }) + "';</script>");
            }
            var old = _categoryFieldService.CategoryFieldFindByName(categoryId, fieldName.Trim(), idEntity.Trim());
            if (old != null)
            {
                return Content("<script>alert('字段标识已存在!');window.location.href='" + Url.Action("AddCategoryField", new { categoryId }) + "';</script>");
            }

            var categoryField = new CategoryField();
            categoryField.CanModify = 1;
            categoryField.CreateTime = DateTime.Now;
            categoryField.Explain = explain;
            categoryField.FieldName = fieldName.Trim();
            categoryField.IdEntity = idEntity.Trim();
            categoryField.IsShow = (int) IsShow;
            var category = _categoryService.Get(categoryId);
            categoryField.Category = category;
            _categoryFieldService.Add(categoryField);//创建 字段

            var table = category.DataTableName;
            if (!string.IsNullOrEmpty(table))
            {
                var findsql = @"select * from syscolumns where name='" + categoryField.IdEntity + "' and id=object_id('" + table + "')";
                var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, findsql, null);
                if (!reader.HasRows)
                {
                    //执行sql  
                    var addSql = @"alter table " + table + " add " + categoryField.IdEntity + " varchar(MAX)";
                    var scalar = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                        addSql, null);
                    if (scalar < 0)
                    {
                        return Content("<script>alert('创建字段成功!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
                    }
                }
                else
                {
                    _logService.Info("------------------创建字段：" + categoryField.IdEntity + " ----" + table + "表中已存在");
                }
            }
            return Content("<script>alert('创建失败,系统错误！');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
        }

        public ActionResult EditCategoryField(int categoryId, int id)
        {
            ViewBag.id = categoryId;
            var model = _categoryFieldService.Get(id);
            if (model == null)
            {
                return Content("<script>alert('参数错误,返回列表!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategoryField(int categoryId, CategoryField cf, int? IsShow = 0)
        {
            if (string.IsNullOrEmpty(cf.FieldName))
            {
                return Content("<script>alert('名称不能为空!');window.location.href='" + Url.Action("EditCategoryField", new { id = cf.CategoryFieldId, categoryId = categoryId }) + "';</script>");
            }
            if (string.IsNullOrEmpty(cf.IdEntity))
            {
                return Content("<script>alert('标识不能为空!');window.location.href='" + Url.Action("EditCategoryField", new { id = cf.CategoryFieldId, categoryId = categoryId }) + "';</script>");
            }

            var old = _categoryFieldService.Get(cf.CategoryFieldId);
            if (old == null)
            {
                return Content("<script>alert('编辑失败,数据错误!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
            }
            if (old.FieldName != cf.FieldName && _categoryFieldService.CategoryFieldFindByName(categoryId, cf.FieldName, cf.IdEntity) != null)
            {
                return Content("<script>alert('编辑失败,字段名已存在!');window.location.href='" + Url.Action("EditCategoryField", new { id = cf.CategoryFieldId, categoryId }) + "';</script>");
            }
            if (old.IdEntity != cf.IdEntity && _categoryFieldService.CategoryFieldFindByName(categoryId, cf.FieldName, cf.IdEntity) != null)
            {
                return Content("<script>alert('编辑失败,标识已存在!');window.location.href='" + Url.Action("EditCategoryField", new { id = cf.CategoryFieldId, categoryId }) + "';</script>");
            }
            var category = _categoryService.Get(categoryId);
            var table = category.DataTableName;
            var addSql = @"sp_rename '" + table + "." + old.IdEntity + "','" + cf.IdEntity + "','column'";
            var scalar = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, addSql, null);
            if (scalar < 0)
            {
                old.FieldName = cf.FieldName;
                old.IdEntity = cf.IdEntity;
                old.Explain = cf.Explain;
                old.IsShow = (int)IsShow;
                _categoryFieldService.Update(old);
                return Content("<script>alert('编辑字段成功!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
            }

            return Content("<script>alert('编辑失败,数据错误!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
        }

        public ActionResult DelCategoryField(int id)
        {
            var m = _categoryFieldService.Get(id);
            if (m == null)
            {
                return Content("<script>alert('删除失败,参数错误!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { m.Category.CategoryId }) + "';</script>");
            }
            var categoryId = m.Category.CategoryId;
            try
            {
                var table = m.Category.DataTableName;
                var sql = @"alter table " + table + " drop column " + m.IdEntity;
                
                var Scalar = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
                if (Scalar < 0)
                {
                    _categoryFieldService.Delete(id);
                    return Content("<script>alert('删除成功!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
                }
            }
            catch (Exception ex)
            {
                _logService.Debug(ex.ToString());
                return Content("<script>alert('删除失败,系统错误!');window.location.href='" + Url.Action("CategoryFieldsListByCategoryId", new { categoryId }) + "';</script>");
            }
            return View();
        }
    }
}