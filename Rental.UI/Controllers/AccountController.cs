using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
namespace Rental.UI.Controllers
{
    public class AccountController : Controller
    {
        UserService userSer = new UserService();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Rental.Service.domain.UserModel user)
        {
            var md5Pwd = Utility.DataHelper.MD5(user.UserPwd);
            user.UserPwd = md5Pwd;
            bool result = userSer.Login(user);
            if(result)
            {
                Session["rental.user"] = "user";
                return RedirectToAction("Index", "Admin", new { lang = "zh-CN" });
            }
            ModelState.AddModelError("error", "用户名或密码错误");
            return View();

        }

        

    }
}
