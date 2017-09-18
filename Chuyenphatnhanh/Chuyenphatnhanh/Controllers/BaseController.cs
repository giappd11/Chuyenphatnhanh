using System;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using Chuyenphatnhanh.Util;
using System.Linq;
using Chuyenphatnhanh.Helpers;
namespace Chuyenphatnhanh.Controllers
{
    public abstract class BaseController : Controller
    {
        private static string _cookieLangName = "Language";
        protected DBConnection db = new DBConnection();
        protected Operator _operator = new Operator();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureOnCookie = GetCultureOnCookie(filterContext.HttpContext.Request);
            string cultureOnURL = filterContext.RouteData.Values.ContainsKey("lang") ? filterContext.RouteData.Values["lang"].ToString() : GlobalHelper.DefaultCulture;
            string culture = (cultureOnCookie == string.Empty) ? (filterContext.RouteData.Values["lang"].ToString()) : cultureOnCookie;
            var _operator = (Operator)Session[Contant.SESSIONLOGED]; 
            string _value = filterContext.RouteData.Values["controller"].ToString() + "." + filterContext.RouteData.Values["action"].ToString();
            if (_operator != null)
            {
                var _secRole = db.SEC_ROLE_MST.Where(u => u.ROLE_ID == _operator.Role).Where(u => u.VALUE == _value);
                if (_secRole.ToList().Count() == 0)
                {
                    filterContext.HttpContext.Response.RedirectToRoute("LocalizedDefault", new { lang = culture, controller = "Error", action = "ErrorRole" });
                    return;
                }
            }
            if (_operator == null)
            {
                filterContext.HttpContext.Response.RedirectToRoute("LocalizedDefault", new { lang = culture, controller = "Home", action = "Login" });
                return;
            }
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
    }
}