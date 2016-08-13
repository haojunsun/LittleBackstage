using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleBackstage.Web.Controllers
{
    public class HomeController : Controller
    {
        //首页
        public ActionResult Main()
        {
            return View();
        }
        //视频列表页
        public ActionResult VideoList()
        {
            return View();
        }

        //详情页
        public ActionResult Detail()
        {
            ViewBag.Url = "~/Uploads/1/14098101030102040102.mp4";
            return View();
        }

        //非遗介绍页面
        //public ActionResult ICHintroduce()
        //{
        //    return View();
        //}

        //引导页
        public ActionResult GuidePage()
        {
            return View();
        }
    }
}