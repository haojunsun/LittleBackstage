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
    public class CategoryController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ILogService _logService;
        private readonly ICategoryFieldService _categoryFieldService;
        public CategoryController(IManagerService managerService,
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
            m.IsDelete = 1;
            _categoryService.Update(m);
            // _categoryService.Delete(id);
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
            category.IsDelete = 0;
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
        public ActionResult EditCategory(Category c, int? IsEnableRadios = 0)
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
                if (old.CategoryFields.Any()) //包含数据 
                {

                }
                else
                {
                    //1.审核状态 发布时间 发布员  审核时间 审核管理员 录入员 录入时间
                    var cf1 = new CategoryField();
                    cf1.Category = old;
                    cf1.CreateTime = DateTime.Now;
                    cf1.Explain = "审核状态";
                    cf1.FieldName = "审核状态";
                    cf1.IdEntity = "IsExamine";
                    cf1.CanModify = 0;
                    old.CategoryFields.Add(cf1);

                    var cf2 = new CategoryField();
                    cf2.Category = old;
                    cf2.CreateTime = DateTime.Now;
                    cf2.Explain = "发布时间";
                    cf2.FieldName = "发布时间";
                    cf2.IdEntity = "ReleaseTime";
                    cf2.CanModify = 0;
                    old.CategoryFields.Add(cf2);

                    var cf3 = new CategoryField();
                    cf3.Category = old;
                    cf3.CreateTime = DateTime.Now;
                    cf3.Explain = "发布员";
                    cf3.FieldName = "发布员";
                    cf3.IdEntity = "ReleaseManager";
                    cf3.CanModify = 0;
                    old.CategoryFields.Add(cf3);

                    var cf4 = new CategoryField();
                    cf4.Category = old;
                    cf4.CreateTime = DateTime.Now;
                    cf4.Explain = "审核时间";
                    cf4.FieldName = "审核时间";
                    cf4.IdEntity = "ExamineTime";
                    cf4.CanModify = 0;
                    old.CategoryFields.Add(cf4);

                    var cf5 = new CategoryField();
                    cf5.Category = old;
                    cf5.CreateTime = DateTime.Now;
                    cf5.Explain = "审核管理员";
                    cf5.FieldName = "审核管理员";
                    cf5.IdEntity = "ExamineManager";
                    cf5.CanModify = 0;
                    old.CategoryFields.Add(cf5);

                    var cf6 = new CategoryField();
                    cf6.Category = old;
                    cf6.CreateTime = DateTime.Now;
                    cf6.Explain = "录入时间";
                    cf6.FieldName = "录入时间";
                    cf6.IdEntity = "InputTime";
                    cf6.CanModify = 0;
                    old.CategoryFields.Add(cf6);

                    var cf7 = new CategoryField();
                    cf7.Category = old;
                    cf7.CreateTime = DateTime.Now;
                    cf7.Explain = "录入员";
                    cf7.FieldName = "录入员";
                    cf7.IdEntity = "InputManager";
                    cf7.CanModify = 0;
                    old.CategoryFields.Add(cf7);
                    _categoryService.Update(old);
                }

                //执行sql 创建 
                //创建数据表
                var field = " ";
                foreach (var cf in old.CategoryFields)
                {
                    //创建基础字段
                    switch (cf.IdEntity)
                    {
                        case "IsExamine":
                            field += " IsExamine int,";
                            break;
                        case "ReleaseTime":
                            field += " ReleaseTime datetime,";
                            break;
                        case "ReleaseManager":
                            field += " ReleaseManager int,";
                            break;
                        case "ExamineTime":
                            field += " ExamineTime datetime,";
                            break;
                        case "ExamineManager":
                            field += " ExamineManager int,";
                            break;
                        case "InputTime":
                            field += " InputTime datetime,";
                            break;
                        case "InputManager":
                            field += " InputManager int";
                            break;
                    }
                }
                //判断表 是否已经存在
                var existenceSql = @"select * from " + _helperServices.GetAppSettings("DataBaseName") + "..sysobjects where xtype='u' and status>=0 and name='" + old.DataTableName + "'";
                var existence = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, existenceSql, null);
                if (existence == null)
                {
                    var sql = @"create table " + old.DataTableName + @"
                            (
                            " + old.DataTableName + @"_Id int primary key,
                            " + field +
                              @" ) ";
                    //_logService.Debug(sql);
                    var Scalar = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
                    if (Scalar < 0)
                    {
                        return Content("<script>alert('创建成功!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('数据模板已存在!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.ToString());

            }
            return Content("<script>alert('创建失败!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
        }

        public ActionResult TemplateList()
        {
            var list = _categoryService.List().Where(x => x.IsCreateTable == 1).OrderByDescending(x => x.CreateTime);
            return View(list.ToList());
        }

        public ActionResult DelTemplate(int id)//删除模板 表 关系记录 
        {
            var m = _categoryService.Get(id);
            if (m == null)
            {
                return Content("<script>alert('删除失败,参数错误!');window.location.href='" + Url.Action("CategoryList") + "';</script>");
            }
            try
            {
                var sql = @"DROP TABLE " + m.DataTableName;
                var Scalar = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
                if (Scalar < 0)
                {
                    m.IsCreateTable = 0;
                    m.DataTableName = "";
                    if (m.CategoryFields != null)
                        _categoryFieldService.Delete(m.CategoryFields);
                    _categoryService.Update(m);
                    return Content("<script>alert('删除成功!');window.location.href='" + Url.Action("TemplateList") + "';</script>");
                }
            }
            catch (Exception ex)
            {
                _logService.Debug(ex.ToString());
                return Content("<script>alert('删除失败,系统错误！');window.location.href='" + Url.Action("TemplateList") + "';</script>");
            }
            return View();
        }

        /// <summary>
        /// 随机生成code
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
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