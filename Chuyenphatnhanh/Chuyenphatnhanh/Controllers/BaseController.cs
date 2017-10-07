using System;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using Chuyenphatnhanh.Util;
using System.Linq;
using Chuyenphatnhanh.Helpers;
using NLog;
using Resource = Chuyenphatnhanh.Content.Texts;
namespace Chuyenphatnhanh.Controllers
{
    public abstract class BaseController : Controller
    {
        private static string _cookieLangName = "Language";
        protected Logger logger = LogManager.GetCurrentClassLogger();
        protected DBConnection db = new DBConnection();
        protected Operator _operator;
        protected override void OnAuthorization(AuthorizationContext context)
        {
            _operator = (Operator)Session[Contant.SESSIONLOGED]; 
            if (_operator == null)
            {
                var url = new UrlHelper(context.RequestContext);
                var logonUrl = url.Action("Login", "Login" );
                context.Result = new RedirectResult(logonUrl);

                return;
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureOnCookie = GetCultureOnCookie(filterContext.HttpContext.Request);
            string cultureOnURL = filterContext.RouteData.Values.ContainsKey("lang") ? filterContext.RouteData.Values["lang"].ToString() : GlobalHelper.DefaultCulture;
            string culture = (cultureOnCookie == string.Empty) ? (filterContext.RouteData.Values["lang"].ToString()) : cultureOnCookie;

            string _value = filterContext.RouteData.Values["controller"].ToString() + "." + filterContext.RouteData.Values["action"].ToString();
            if (_operator != null)
            {
                var _secRole = db.SEC_ROLE_MST.Where(u => u.ROLE_ID == _operator.Role && u.DELETE_FLAG == false).Where(u => u.VALUE == _value);
                if (_secRole.ToList().Count() == 0)
                {
                    throw new    InvalidOperationException( Resource.RGlobal.accessdenis);
                }
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