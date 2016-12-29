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

        public ActionResult Index()
        {
            var c = new Category();
            TreeBindCategory(0, ref c);
            ViewBag.tableTitle = new List<CategoryField>();
            ViewBag.categoryId = 0;
            ViewBag.idName = "";
            var table = new DataTable();
            if (c != null && c.CategoryFields.Any())
            {
                ViewBag.categoryId = c.CategoryId;
                ViewBag.tableTitle = c.CategoryFields;
                ViewBag.idName = c.DataTableName ;
                var sql = @"select * from " + c.DataTableName;
                table = SqlHelper.QueryDataTable(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
            }

            //return View(new List<CategoryField>());
            return View(table);
        }

        public ActionResult EntryList(int categoryId)
        {
            return View();
        }

        public ActionResult AddEntry(int categoryId)
        {
            return View();
        }

        public ActionResult EditEntry(int categoryId, int id)
        {
            return View();
        }

        public ActionResult DelEntry(int id)
        {
            return View();
        }
    }
}