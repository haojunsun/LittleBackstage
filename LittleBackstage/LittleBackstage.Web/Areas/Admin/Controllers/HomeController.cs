using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Basis;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Filters;
using System.Data;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    //[LittleBackstageAuthorize]
    public class HomeController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        private readonly IRoleService _roleService;
        private readonly ISystemLogService _systemLogService;

        public HomeController(IManagerService managerService,
            IHelperServices helperServices,
            IRoleService roleService,
            ISystemLogService systemLogService)
        {
            _managerService = managerService;
            _helperServices = helperServices;
            _roleService = roleService;
            _systemLogService = systemLogService;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            //分类数
            var sql0 = @"select count(*) from Categories where IsDelete=0 ";
            var table0 = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql0, null);
            //条目数
            var sql1 = @"select count(*) from Template_Table_5 ";
            var table1 = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql1, null);
            //待审核
            var sql2 = @"select count(*) from Template_Table_5 where IsExamine=0";
            var table2= SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql2, null);
            //已审核
            var sql3 = @"select count(*) from Template_Table_5 where IsExamine=1";
            var table3 = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql3, null);
            //待发布
            var sql4 = @"select count(*) from Template_Table_5 where IsRelease=0";
            var table4 = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql4, null);
            //已发布
            var sql5 = @"select count(*) from Template_Table_5 where IsRelease=1";
            var table5 = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql5, null);
            //管理员
            var sql6 = @"select count(*) from Managers";
            var table6 = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql6, null);
            //待审核
            var sql7 = @"select count(*) from Managers where IsExamine=0";
            var table7 = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql7, null);

            ViewBag.caCount = table0;
            ViewBag.entryCount = table1;
            ViewBag.exEntryCount = table2;
            ViewBag.exdEntryCount = table3;
            ViewBag.reEntryCount = table4;
            ViewBag.redEntryCount = table5;
            ViewBag.adminCount = table6;
            ViewBag.exAdminCount = table7;
            return View();
        }

        public string GetName()
        {
            var m = UserLogin.GetUserInfo("SESSION_USER_INFO");
            return m.UserName;
        }
    }
}