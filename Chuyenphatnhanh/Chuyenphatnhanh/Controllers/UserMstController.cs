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
        private DBConnection db = new DBConnection();

        // GET: UserMst
        public ActionResult Index()
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

        // GET: UserMst/Details/5
        public ActionResult Details(string id)
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
            UserMstForm _form = new UserMstForm();
            ComplementUtil.complement(uSER_MST, _form);
            return View(_form);
        }

        // GET: UserMst/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( UserMstForm form)
        {
            USER_MST _UserMst = new USER_MST();
            if (ModelState.IsValid)
            {
                ComplementUtil.complement(form, _UserMst);
                _UserMst.DELETE_FLAG = false;
                _UserMst.MOD_DATE = DateTime.Now;
                _UserMst.MOD_USER_NAME = _operator.UserName;
                _UserMst.REG_DATE = DateTime.Now;
                _UserMst.REG_USER_NAME = _operator.UserName;
                _UserMst.USER_ID = GenerateID.GennerateID(db, Contant.USERMST_SEQ, Contant.USERMST_PREFIX);
                db.USER_MST.Add(_UserMst);
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
            } 
            return View(form);
        }

        // GET: UserMst/Edit/5
        public ActionResult Edit(string id)
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
            UserMstForm _form = new UserMstForm();
            ComplementUtil.complement(uSER_MST, _form);
            return View(_form);
        }

        // POST: UserMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserMstForm form )
        {
            if (ModelState.IsValid)
            {
                USER_MST _UserMst = db.USER_MST.Find(form.USER_ID);
                if (DateTime.Compare((DateTime)_UserMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _UserMst.USER_NAME, _UserMst.MOD_USER_NAME));
                    return View(form);
                }

                ComplementUtil.complement(form, _UserMst);
                _UserMst.MOD_DATE = DateTime.Now;
                _UserMst.MOD_USER_NAME = _operator.UserName;
                db.Entry(_UserMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
            }
            return View(form);
 
        }

        // GET: UserMst/Delete/5
        public ActionResult Delete(string id)
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
            UserMstForm _form = new UserMstForm();
            ComplementUtil.complement(uSER_MST, _form);
            return View(_form);
        }

        // POST: UserMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserMstForm form)
        {
            USER_MST _UserMst = db.USER_MST.Find(form.USER_ID);
            if (DateTime.Compare((DateTime)_UserMst.MOD_DATE, form.MOD_DATE) != 0)
            {
                ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _UserMst.USER_NAME, _UserMst.MOD_USER_NAME));
                return View(form);
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
