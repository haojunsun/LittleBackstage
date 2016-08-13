using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;
using LittleBackstage.Web.Helpers;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    public class ForExcelController : Controller
    {
        private readonly IHelperServices _helperServices;
        private readonly IForExcelService _forExcelService;
        private readonly ILogService _log;


        public ForExcelController(IHelperServices helperServices, IForExcelService forExcelService, ILogService log)
        {
            _helperServices = helperServices;
            _forExcelService = forExcelService;
            _log = log;
        }

  
        /// <summary>
        /// 高级检索
        /// </summary>
        /// <param name="state">status=1 进行全文匹配，如果status=2进行正题名匹配，如果status=3进行演奏方式匹配，status=4进行民族匹配</param>
        /// <param name="key">关键字</param>
        /// <param name="yzfs"></param>
        /// <param name="mz"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SeniorSearch(int state, string key,string yzfs,string mz, int pageSize, int pageIndex)
        {
            var totalCount = 0;
            if (string.IsNullOrEmpty(key) || state == 0)
            {
                var all = _forExcelService.List(pageIndex, pageSize, ref totalCount);
                return Json(all, JsonRequestBehavior.DenyGet);
            }

            var result = _forExcelService.SeniorSearch(state, key, yzfs, mz,pageIndex, pageSize, ref totalCount);
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Find(int id)
        {
            var result = _forExcelService.Get(id);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}