using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    public class LogController : Controller
    {
        private readonly IHelperServices _helperServices;
        private readonly ISystemLogService _systemLogService;
        public LogController(IHelperServices helperServices, ISystemLogService systemLogService)
        {
            _helperServices = helperServices;
            _systemLogService = systemLogService;
        }

        // GET: Admin/Log
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogList()
        {
            var list = _systemLogService.List().Where(x => x.LogType == 1).OrderByDescending(x => x.LogTime);
            return View(list.ToList());
        }
        public ActionResult UserLogList()
        {
            var list = _systemLogService.List().Where(x => x.LogType == 2).OrderByDescending(x => x.LogTime);
            return View(list.ToList());
        }
    }
}