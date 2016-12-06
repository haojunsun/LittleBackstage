using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Core.Services;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IHelperServices _helperServices;
        public UserController(IManagerService managerService, IHelperServices helperServices)
        {
            _managerService = managerService;
            _helperServices = helperServices;
        }
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddManager()
        {
            return View();
        }
        public ActionResult EditManager(int id)
        {
            return View();
        }

        public ActionResult ManagerList(string key, int page, int size)
        {
            return View();
        }

    }
}