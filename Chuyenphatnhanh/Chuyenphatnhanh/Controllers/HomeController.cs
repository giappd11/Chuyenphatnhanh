using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using Chuyenphatnhanh.Util;
using System.ComponentModel.DataAnnotations;
using Resource = Chuyenphatnhanh.Content.Texts;
namespace Chuyenphatnhanh.Controllers
{
    public class HomeController : Controller
    {
        private DBConnection db = new DBConnection();

        
        // GET: Home/Login
        public ActionResult Login()
        {
            var _sessionLoged = (Operator)Session[Contant.SESSIONLOGED];
            if (_sessionLoged != null)
            {
                return RedirectToAction("Index", "CustMst");
            }
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Login(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                String _password = MD5HashGenerator.GenerateKey(form.PASSWORD);

                USER_MST _user = db.USER_MST.Where(u => u.PASSWORD == _password).Where(u => u.USER_NAME == form.USER_NAME).FirstOrDefault();

                if(_user != null )
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
                    _operator.Role = _user.USER_CONFIG_MST.ROLE_ID;
                    Session[Contant.SESSIONLOGED] = _operator;
                    return RedirectToAction("Index", "CustMst");

                }
                else
                {
                    _user = db.USER_MST.Where(u => u.USER_NAME == form.USER_NAME).FirstOrDefault();
                    _user.NUMBER_LOGIN_FAIL++;
                    _user.MOD_DATE = DateTime.Now;
                    db.Entry(_user).State = EntityState.Modified;
                    db.SaveChanges();
                    if (_user.NUMBER_LOGIN_FAIL > 5)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, Resource.RGlobal.AccountLoged);
                        return View(form);
                    }
                    ModelState.AddModelError(Contant.MESSSAGEERROR, Resource.RGlobal.ErrorLogin);
                    return View(form);
                }
            }

            return View(form);
        }
         
        public ActionResult Logout()
        {
            Session[Contant.SESSIONLOGED] = null;
            return RedirectToAction("Login");
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
