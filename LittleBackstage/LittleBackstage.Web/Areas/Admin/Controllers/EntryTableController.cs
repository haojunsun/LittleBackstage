using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Basis;
using LittleBackstage.Core.Models;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Helpers;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 条目
    /// </summary>
    public class EntryTableController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ILogService _logService;
        private readonly ICategoryFieldService _categoryFieldService;

        public EntryTableController(IManagerService managerService,
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
        private void TreeBindCategory(int id, ref Category c)
        {
            var list = new List<Category>();
            list = _categoryService.List().Where(x => x.IsCreateTable == 1).ToList();
            var selectList = new List<SelectListItem>();
            int i = 0;
            foreach (var r in list)
            {
                if (id == 0 && i == 0)
                {
                    selectList.Add(new SelectListItem()
                    {
                        Value = r.CategoryId.ToString(),
                        Text = r.CategoryName,
                        Selected = true
                    });
                    c = r;
                }
                else if (r.CategoryId == id)
                {
                    selectList.Add(new SelectListItem()
                    {
                        Value = r.CategoryId.ToString(),
                        Text = r.CategoryName,
                        Selected = true
                    });
                    c = r;
                }
                else
                {
                    selectList.Add(new SelectListItem()
                    {
                        Value = r.CategoryId.ToString(),
                        Text = r.CategoryName,
                    });
                }
                i++;
            }
            ViewBag.CategorySelect = selectList;
        }

        public ActionResult Index(int? id = 0)
        {
            var c = new Category();
            TreeBindCategory((int)id, ref c);
            ViewBag.tableTitle = new List<CategoryField>();
            ViewBag.categoryId = 0;
            ViewBag.idName = "";
            var table = new DataTable();
            if (c != null && c.CategoryFields.Any())
            {
                ViewBag.categoryId = c.CategoryId;
                ViewBag.tableTitle = c.CategoryFields;
                ViewBag.idName = c.DataTableName;
                var sql = @"select * from " + c.DataTableName;
                table = SqlHelper.QueryDataTable(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
            }

            //return View(new List<CategoryField>());
            return View(table);
        }

        public ActionResult AddEntryTable(int categoryId)
        {
            ViewBag.categoryId = categoryId;
            return View();
        }

        [HttpPost]
        public ActionResult AddEntryTablePost(int categoryId)
        {
            var insertSql = @"INSERT INTO ";
            var category = _categoryService.Get(categoryId);
            if (category != null && category.IsCreateTable == 1 && category.CategoryFields.Any())
            {
                insertSql += category.DataTableName + "(";
                foreach (var item in category.CategoryFields.Where(x => x.CanModify == 1))
                {
                    insertSql += item.IdEntity + ",";
                }
                //insertSql = _helperServices.DelLastChar(insertSql, ",");//去掉最后的逗号
                insertSql += "InputManager,InputTime";
                insertSql += ") VALUES (";
                foreach (var item in category.CategoryFields.Where(x => x.CanModify == 1))
                {
                    insertSql += "'" + Request.Form[item.IdEntity] + "',";
                }
                var admin = UserLogin.GetUserInfo("SESSION_USER_INFO");
                insertSql += admin.ManagerId + ",'" + DateTime.Now + "') SELECT @@IDENTITY";

                var reader = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, insertSql, null);
                //if ((int)reader > 0)
                //{
                return Content("<script>alert('创建成功!');window.location.href='" + Url.Action("Index", new { id = categoryId }) + "';</script>");
                //}
            }
            return Content("<script>alert('数据错误!');window.location.href='" + Url.Action("Index", new { id = categoryId }) + "';</script>");
        }

        public ActionResult EditEntryTable(int categoryId, int id)
        {
            ViewBag.categoryId = categoryId;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult EditEntryTablePost(int categoryId, int id)
        {
            var updateSql = @"UPDATE ";
            var category = _categoryService.Get(categoryId);
            if (category != null && category.IsCreateTable == 1 && category.CategoryFields.Any())
            {
                updateSql += category.DataTableName + " SET ";
                foreach (var item in category.CategoryFields.Where(x => x.CanModify == 1))
                {
                    updateSql += item.IdEntity + "='" + Request.Form[item.IdEntity] + "',";
                }
                updateSql = _helperServices.DelLastChar(updateSql, ",");
                updateSql += " WHERE " + category.DataTableName + "_Id =" + id;
                var reader = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, updateSql, null);
                return Content("<script>alert('编辑成功!');window.location.href='" + Url.Action("Index", new { id = categoryId }) + "';</script>");
            }
            return Content("<script>alert('数据错误!');window.location.href='" + Url.Action("Index", new { id = categoryId }) + "';</script>");
        }

        public ActionResult DelEntry(int id, int categoryId)
        {
            var admin = UserLogin.GetUserInfo("SESSION_USER_INFO");
            var delSql = @"DELETE FROM ";
            var category = _categoryService.Get(categoryId);
            if (category != null && category.IsCreateTable == 1 && category.CategoryFields.Any())
            {
                delSql += category.DataTableName + " where " + category.DataTableName + "_Id =" + id;
                var del = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, delSql, null);
                if (del > 0)
                {
                    return Content("<script>alert('删除成功!');window.location.href='" + Url.Action("Index", new { id = categoryId }) + "';</script>");
                }
            }
            return Content("<script>alert('数据错误!');window.location.href='" + Url.Action("Index", new { id = categoryId }) + "';</script>");
        }

        /// <summary>
        /// 获取管理员名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(int id)
        {
            var admin = _managerService.Get(id);
            if (admin != null)
            {
                return admin.UserName;
            }
            return "数据错误";
        }

        public string GetAddEntryForm(int categoryId)
        {
            var category = _categoryService.Get(categoryId);
            if (category != null && category.IsCreateTable == 1 && category.CategoryFields.Any())
            {
                var form = "";
                foreach (var item in category.CategoryFields.Where(x => x.CanModify == 1))
                {
                    form += "<div class=\"form-group\">";
                    form += "<label class=\"col-sm-2 control-label\">" + item.FieldName + "</label>";
                    form += "<div class=\"col-sm-6\">";
                    form += "<input type=\"text\" class=\"form-control\" id=\"" + item.IdEntity + "\" name=\"" + item.IdEntity + "\"/>";
                    form += "</div>";
                    form += "</div>";
                    form += " <div class=\"hr-line-dashed\"></div>";
                }
                return form;
            }
            else
            {
                return "<div>无字段</div>";
            }
        }

        public string GetEditEntryForm(int categoryId, int id)
        {

            var category = _categoryService.Get(categoryId);
            if (category != null && category.IsCreateTable == 1 && category.CategoryFields.Any())
            {
                var sql = @"select * from " + category.DataTableName + " where  " + category.DataTableName + "_Id =" + id;
                var table = SqlHelper.QueryDataTable(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
                var form = "";
                foreach (DataRow dr in table.Rows)
                {
                    foreach (var item in category.CategoryFields.Where(x => x.CanModify == 1))
                    {
                        form += "<div class=\"form-group\">";
                        form += "<label class=\"col-sm-2 control-label\">" + item.FieldName + "</label>";
                        form += "<div class=\"col-sm-6\">";
                        form += "<input type=\"text\" class=\"form-control\" id=\"" + item.IdEntity + "\" name=\"" +
                                item.IdEntity + "\" value=\"" + dr[item.IdEntity] + "\"/>";
                        form += "</div>";
                        form += "</div>";
                        form += " <div class=\"hr-line-dashed\"></div>";
                    }
                }
                return form;
            }
            else
            {
                return "<div>无字段</div>";
            }
        }



        public ActionResult Catalogue()
        {
            var c = new Category();
            TreeBindCategory(0);
            return View();
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
        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        public ActionResult ExamineEntryTable(int? categoryId = 0, int? state = -3)
        {
            var c = new Category();
            TreeBindCategory((int)categoryId, ref c);
            ViewBag.tableTitle = new List<CategoryField>();
            ViewBag.categoryId = 0;
            ViewBag.idName = "";
            var table = new DataTable();
            if (c != null && c.CategoryFields.Any())
            {
                //IsRelease 发布状态 默认0 未发布 1 已发布 
                //IsExamine 审核状态 默认 0 待审核 -1 退回审核 -2 审核不通过   审核通过 1
                ViewBag.categoryId = c.CategoryId;
                ViewBag.tableTitle = c.CategoryFields;
                ViewBag.idName = c.DataTableName;
                var sql = @"select * from " + c.DataTableName + " where IsRelease=0 and ";
                if (state == -3)
                {
                    sql += "IsExamine !=1";
                }
                else
                {
                    sql += "IsExamine ==" + state;
                }
                table = SqlHelper.QueryDataTable(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
            }
            return View(table);
        }

        public ActionResult ExamineEntry(int id, int categoryId)
        {
            ViewBag.categoryId = categoryId;
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryId"></param>
        /// <param name="examineState"></param>
        /// <param name="explain">审核说明-存到log中</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExamineData(int id, int categoryId, int examineState, string explain)
        {
            var admin = UserLogin.GetUserInfo("SESSION_USER_INFO");
            var updateSql = "UPDATE ";
            var category = _categoryService.Get(categoryId);
            if (category != null && category.IsCreateTable == 1 && category.CategoryFields.Any())
            {

                updateSql += category.DataTableName + "  SET ";
                updateSql += "  IsExamine= " + examineState + ",";

                updateSql += "  ExamineTime= '" + DateTime.Now + "',";
                updateSql += "  ExamineManager=" + admin.ManagerId;
                updateSql += " WHERE " + category.DataTableName + "_Id =" + id;
                var reader = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, updateSql, null);
                return Content("<script>alert('审核成功!');window.location.href='" + Url.Action("ExamineEntryTable", new { categoryId }) + "';</script>");
            }
            return Content("<script>alert('数据错误!');window.location.href='" + Url.Action("ExamineEntryTable", new { categoryId  }) + "';</script>");
        }

        public ActionResult List()
        {
            return View();
        }
    }
}