using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleBackstage.Infrastructure.Services;

namespace LittleBackstage.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISimpleAccountManager _simpleAccountManager;
        public AccountController(ISimpleAccountManager simpleAccountManager)
        {
            _simpleAccountManager = simpleAccountManager;
        }
        public ActionResult Login()
        {
            return View(false);
        }

        [HttpPost]
        public ActionResult Login(string username, string password, bool remember = false)
        {
            var result = _simpleAccountManager.LoginByPassword(username, password);
            if (!result)
            {
                return View(true);
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            _simpleAccountManager.Logout();

            return RedirectToAction("Login");
        }

    }
}