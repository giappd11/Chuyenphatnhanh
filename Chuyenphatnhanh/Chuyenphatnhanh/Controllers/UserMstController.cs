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

namespace Chuyenphatnhanh.Controllers
{
    public class UserMstController : BaseController
    {

        // GET: UserMst
        public ActionResult Index()
        {
            try
            {
                List<USER_MST> _list = db.USER_MST.Where(u => u.DELETE_FLAG == false).Include(u => u.USER_CONFIG_MST).ToList();
                List<UserMstForm> _UserList = new List<UserMstForm>();
                foreach (USER_MST _User in _list)
                {
                    UserMstForm _UserMst = new UserMstForm();
                    ComplementUtil.complement(_User, _UserMst);
                    _UserList.Add(_UserMst);
                }
                return View(_UserList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: UserMst/Details/5
        public ActionResult Details(string id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                USER_MST uSER_MST = db.USER_MST.Find(id);
                if (uSER_MST == null)
                {
                    return HttpNotFound();
                }
                ViewData["Config.ROLE_ID"] = new SelectList(db.ROLE_MST.Where(u => u.DELETE_FLAG == false), "ROLE_ID", "TYPE_ROLE");
                ViewData["Config.BRANCH_ID"] = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false), "BRANCH_ID", "BRANCH_NAME");
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");

                WARD_MST _wardTo = db.WARD_MST.Find(uSER_MST.WARD_ID);
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == uSER_MST.DISTRICT_ID && u.DELETE_FLAG == false), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                UserMstForm _form = new UserMstForm();
                ComplementUtil.complement(uSER_MST, _form);
                _form.Config = new UserConfigMstForm();
                ComplementUtil.complement(uSER_MST.USER_CONFIG_MST, _form.Config);

                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: UserMst/Create
        public ActionResult Create()
        {
            ViewData["Config.ROLE_ID"] = new SelectList(db.ROLE_MST.Where(u => u.DELETE_FLAG == false), "ROLE_ID", "TYPE_ROLE");
            ViewData["Config.BRANCH_ID"] = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false), "BRANCH_ID", "BRANCH_NAME" );
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            return View();
        }

        // POST: UserMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserMstForm form)
        {
            try
            {
                ViewData["Config.ROLE_ID"] = new SelectList(db.ROLE_MST.Where(u => u.DELETE_FLAG == false), "ROLE_ID", "TYPE_ROLE", form.Config.ROLE_ID);
                ViewData["Config.BRANCH_ID"] = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false), "BRANCH_ID", "BRANCH_NAME", form.Config.BRANCH_ID);
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                
                WARD_MST _wardTo = db.WARD_MST.Find( form.WARD_ID);
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DISTRICT_ID && u.DELETE_FLAG == false), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                USER_MST _UserMst = new USER_MST();
                if (ModelState.IsValid)
                {
                    var list = db.USER_MST.Where(u => u.USER_NAME == form.USER_NAME);
                    if (list.Count() > 0)
                    {
                        ViewData[Contant.MESSSAGEERROR] = string.Format( Chuyenphatnhanh.Content.Texts.RGlobal.Exists, form.USER_NAME);
                        return View(form);
                    }
                    
                    DateTime _date = DateTime.Now;
                    ComplementUtil.complement(form, _UserMst);
                    _UserMst.DELETE_FLAG = false;
                    _UserMst.MOD_DATE = _date;
                    _UserMst.MOD_USER_NAME = _operator.UserName;
                    _UserMst.REG_DATE = _date;
                    _UserMst.REG_USER_NAME = _operator.UserName;
                    _UserMst.PASSWORD = MD5HashGenerator.GenerateKey("1");
                    _UserMst.USER_ID = GenerateID.GennerateID(db, Contant.USERMST_SEQ, Contant.USERMST_PREFIX);
                    db.USER_MST.Add(_UserMst);

                    USER_CONFIG_MST _config = new USER_CONFIG_MST();
                    _config.ROLE_ID = form.Config.ROLE_ID;
                    _config.BRANCH_ID = form.Config.BRANCH_ID;
                    _config.DELETE_FLAG = false;
                    _config.MOD_DATE = _date;
                    _config.MOD_USER_NAME = _operator.UserName;
                    _config.REG_DATE = _date;
                    _config.REG_USER_NAME = _operator.UserName;
                    _config.USER_ID = _UserMst.USER_ID;
                    db.USER_CONFIG_MST.Add(_config);
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: UserMst/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                USER_MST uSER_MST = db.USER_MST.Find(id);
                if (uSER_MST == null)
                {
                    return HttpNotFound();
                }
                ViewData["Config.ROLE_ID"]  = new SelectList(db.ROLE_MST.Where(u => u.DELETE_FLAG == false), "ROLE_ID", "TYPE_ROLE");
                ViewData["Config.BRANCH_ID"] = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false), "BRANCH_ID", "BRANCH_NAME");
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");

                WARD_MST _wardTo = db.WARD_MST.Find(uSER_MST.WARD_ID);
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == uSER_MST.DISTRICT_ID&& u.DELETE_FLAG == false), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                UserMstForm _form = new UserMstForm();
                ComplementUtil.complement(uSER_MST, _form);
                _form.Config = new UserConfigMstForm();
                ComplementUtil.complement(uSER_MST.USER_CONFIG_MST, _form.Config);
                 
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: UserMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserMstForm form)
        {
            try
            {

                ViewData["Config.ROLE_ID"] = new SelectList(db.ROLE_MST.Where(u => u.DELETE_FLAG == false), "ROLE_ID", "TYPE_ROLE", form.Config.ROLE_ID);
                ViewData["Config.BRANCH_ID"] = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG ==false), "BRANCH_ID", "BRANCH_NAME", form.Config.BRANCH_ID);
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");

                WARD_MST _wardTo = db.WARD_MST.Find(form.WARD_ID);
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DISTRICT_ID && u.DELETE_FLAG == false), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                } 
                if (ModelState.IsValid)
                {
                    
                    DateTime _date = DateTime.Now;
                    USER_MST _UserMst = db.USER_MST.Find(form.USER_ID);
                    var list = db.USER_MST.Where(u => u.USER_NAME == form.USER_NAME);
                    if (list.Count() > 0 && !form.USER_NAME.Equals(_UserMst.USER_NAME))
                    {
                        ViewData[Contant.MESSSAGEERROR] = string.Format(Chuyenphatnhanh.Content.Texts.RGlobal.Exists, form.USER_NAME);
                        return View(form);
                    }

                    if (DateTime.Compare((DateTime)_UserMst.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _UserMst.USER_NAME, _UserMst.MOD_USER_NAME));
                        return View(form);
                    }

                    ComplementUtil.complement(form, _UserMst);
                    _UserMst.MOD_DATE = DateTime.Now;
                    _UserMst.MOD_USER_NAME = _operator.UserName;
                    db.Entry(_UserMst).State = EntityState.Modified;

                    USER_CONFIG_MST _config = db.USER_CONFIG_MST.Find(_UserMst.USER_ID);

                    if (DateTime.Compare((DateTime)_config.MOD_DATE, form.Config.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _UserMst.USER_NAME, _UserMst.MOD_USER_NAME));
                        return View(form);
                    }

                    ComplementUtil.complement(form.Config, _config);
                    _config.MOD_DATE = _date;
                    _config.MOD_USER_NAME = _operator.UserName; 
                    db.Entry(_config).State = EntityState.Modified;

                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // GET: UserMst/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                USER_MST uSER_MST = db.USER_MST.Find(id);
                if (uSER_MST == null)
                {
                    return HttpNotFound();
                }
                ViewData["Config.ROLE_ID"] = new SelectList(db.ROLE_MST.Where(u => u.DELETE_FLAG == false), "ROLE_ID", "TYPE_ROLE");
                ViewData["Config.BRANCH_ID"] = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false), "BRANCH_ID", "BRANCH_NAME");
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");

                WARD_MST _wardTo = db.WARD_MST.Find(uSER_MST.WARD_ID);
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == uSER_MST.DISTRICT_ID && u.DELETE_FLAG == false), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                UserMstForm _form = new UserMstForm();
                ComplementUtil.complement(uSER_MST, _form);
                _form.Config = new UserConfigMstForm();
                ComplementUtil.complement(uSER_MST.USER_CONFIG_MST, _form.Config);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: UserMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserMstForm form)
        {
            try
            {
                USER_MST _UserMst = db.USER_MST.Find(form.USER_ID);
                if (DateTime.Compare((DateTime)_UserMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _UserMst.USER_NAME, _UserMst.MOD_USER_NAME));
                    return View(form);
                }
                ViewData["Config.ROLE_ID"] = new SelectList(db.ROLE_MST.Where(u => u.DELETE_FLAG == false), "ROLE_ID", "TYPE_ROLE");
                ViewData["Config.BRANCH_ID"] = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false), "BRANCH_ID", "BRANCH_NAME");
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");

                WARD_MST _wardTo = db.WARD_MST.Find(form.WARD_ID);
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DISTRICT_ID&& u.DELETE_FLAG == false), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                ComplementUtil.complement(form, _UserMst);
                _UserMst.MOD_DATE = DateTime.Now;
                _UserMst.MOD_USER_NAME = _operator.UserName;
                _UserMst.DELETE_FLAG = true;
                db.Entry(_UserMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                ComplementUtil.complement(_UserMst, form);

                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
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
