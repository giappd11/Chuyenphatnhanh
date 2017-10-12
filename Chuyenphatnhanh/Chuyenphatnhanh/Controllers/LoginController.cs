using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using Chuyenphatnhanh.Util;
using Resource = Chuyenphatnhanh.Content.Texts;
using Chuyenphatnhanh.Helpers;

namespace Chuyenphatnhanh.Controllers
{
    public class LoginController : Controller
    {
        private DBConnection db = new DBConnection();
        private static string _cookieLangName = "Language";
        // GET: Home/Login
        public ActionResult Login()
        {
            var _sessionLoged = (Operator)Session[Contant.SESSIONLOGED];
            if (_sessionLoged != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                String _password = MD5HashGenerator.GenerateKey(form.PASSWORD);

                USER_MST _user = db.USER_MST.Where(u => u.PASSWORD == _password).Where(u => u.USER_NAME == form.USER_NAME).FirstOrDefault();

                if (_user != null)
                {
                    if (_user.NUMBER_LOGIN_FAIL > 5)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, Resource.RGlobal.AccountLoged);
                        return View(form);
                    }
                    Operator _operator = new Operator();
                    _operator.ManagerTime = DateTime.Now;
                    _operator.UserId = _user.USER_ID;
                    _operator.UserName = _user.USER_NAME;
                    _operator.NameUser = _user.NAME;
                    if (_user.USER_CONFIG_MST != null)
                    {
                        _operator.Role = _user.USER_CONFIG_MST.ROLE_ID;
                        _operator.RoleName = _user.USER_CONFIG_MST.ROLE_MST.TYPE_ROLE;
                        _operator.BranchID = _user.USER_CONFIG_MST.BRANCH_ID;
                    }
                    Session[Contant.SESSIONLOGED] = _operator;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    _user = db.USER_MST.Where(u => u.USER_NAME == form.USER_NAME).FirstOrDefault();
                    if (_user != null) { 
                        _user.NUMBER_LOGIN_FAIL++;
                        _user.MOD_DATE = DateTime.Now;
                        db.Entry(_user).State = EntityState.Modified;
                        db.SaveChanges();
                        if (_user.NUMBER_LOGIN_FAIL > 5)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, Resource.RGlobal.AccountLoged);
                            return View(form);
                        }
                    }
                    ModelState.AddModelError(Contant.MESSSAGEERROR, Resource.RGlobal.ErrorLogin);
                    return View(form);
                }
            }

            return View(form);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureOnCookie = GetCultureOnCookie(filterContext.HttpContext.Request);
            string cultureOnURL = filterContext.RouteData.Values.ContainsKey("lang") ? filterContext.RouteData.Values["lang"].ToString() : GlobalHelper.DefaultCulture;
            string culture = (cultureOnCookie == string.Empty) ? (filterContext.RouteData.Values["lang"].ToString()) : cultureOnCookie;
            if (cultureOnURL != culture)
            {
                filterContext.HttpContext.Response.RedirectToRoute("LocalizedDefault", new { lang = culture, controller = filterContext.RouteData.Values["controller"], action = filterContext.RouteData.Values["action"] });
                return;
            }
            SetCurrentCultureOnThread(culture);
            if (culture != MultiLanguageViewEngine.CurrentCulture)
            {
                (ViewEngines.Engines[0] as MultiLanguageViewEngine).SetCurrentCulture(culture);
            }
            base.OnActionExecuting(filterContext);
        }

        private static void SetCurrentCultureOnThread(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = GlobalHelper.DefaultCulture;
            var cultureInfo = new System.Globalization.CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        public static String GetCultureOnCookie(HttpRequestBase request)
        {
            var cookie = request.Cookies[_cookieLangName];
            string culture = string.Empty;
            if (cookie != null)
            {
                culture = cookie.Value;
            }
            return culture;
        }

        public ActionResult Logout()
        {
            Session[Contant.SESSIONLOGED] = null;
            return RedirectToAction("Login","Login");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
