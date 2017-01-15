using LPD1.Database;
using LPD1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace LPD1.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Exclude = "Username")]UserInfo user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.Login, user.Password))
                {
                    user = UserDB.FindByLogin(user.Login);
                    FormsAuthentication.SetAuthCookie(user._id.ToString() + user.Username, false);
                    return RedirectToAction("Index", "Series");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegisterInfo user)
        {
            if (ModelState.IsValid)
            {
                if (UserDB.Exists(user.Login))
                {
                    ModelState.AddModelError("User already exists",new Exception("User already exists"));
                }
                UserInfo ui = new UserInfo()
                {
                    Login = user.Login,
                    LoginType = 0,
                    Password = user.Password,
                    Username = user.Username ?? user.Login
                };
                if (!UserDB.Register(ui))
                {
                    ModelState.AddModelError("Something went wrong...", new Exception("Something went wrong..."));
                    return View(user);
                }
                return View("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);
            return RedirectToAction("Index", "Series");
        }
    }
}
