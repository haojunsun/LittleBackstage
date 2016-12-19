using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult CategoryList()
        {
            return View();
        }

        public ActionResult CategoryFieldList()
        {
            return View();
        }
    }
}