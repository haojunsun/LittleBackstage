using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    public class ResourcesController : Controller
    {
        // GET: Admin/Resources
        public ActionResult Index()
        {
            return View();
        }
    }
}