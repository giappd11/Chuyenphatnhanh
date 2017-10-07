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
    public class RoleMstController : BaseController
    {

        // GET: RoleMst
        public ActionResult Index()
        {
            try
            {
                List<ROLE_MST> _list = db.ROLE_MST.Where(u => u.DELETE_FLAG == false).ToList();
                List<RoleMstForm> _RoleList = new List<RoleMstForm>();
                foreach (ROLE_MST _role in _list)
                {
                    RoleMstForm _RoleMst = new RoleMstForm();
                    ComplementUtil.complement(_role, _RoleMst);
                    _RoleList.Add(_RoleMst);
                }
                return View(_RoleList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: RoleMst/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ROLE_MST rOLE_MST = db.ROLE_MST.Find(id);
                if (rOLE_MST == null)
                {
                    return HttpNotFound();
                }
                RoleMstForm _form = new RoleMstForm();
                ComplementUtil.complement(rOLE_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: RoleMst/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleMstForm form)
        {
            try
            {
                ROLE_MST _RoleMst = new ROLE_MST();
                if (ModelState.IsValid)
                {
                    ComplementUtil.complement(form, _RoleMst);
                    _RoleMst.DELETE_FLAG = false;
                    _RoleMst.MOD_DATE = DateTime.Now;
                    _RoleMst.MOD_USER_NAME = _operator.UserName;
                    _RoleMst.REG_DATE = DateTime.Now;
                    _RoleMst.REG_USER_NAME = _operator.UserName;
                    _RoleMst.ROLE_ID = GenerateID.GennerateID(db, Contant.ROLEMST_SEQ, Contant.ROLEMST_PREFIX);
                    db.ROLE_MST.Add(_RoleMst);
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

        // GET: RoleMst/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ROLE_MST rOLE_MST = db.ROLE_MST.Find(id);
                if (rOLE_MST == null)
                {
                    return HttpNotFound();
                }
                RoleMstForm _form = new RoleMstForm();
                ComplementUtil.complement(rOLE_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: RoleMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleMstForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ROLE_MST _RoleMst = db.ROLE_MST.Find(form.ROLE_ID);
                    if (DateTime.Compare((DateTime)_RoleMst.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _RoleMst.TYPE_ROLE, _RoleMst.MOD_USER_NAME));
                        return View(form);
                    }

                    ComplementUtil.complement(form, _RoleMst);
                    _RoleMst.MOD_DATE = DateTime.Now;
                    _RoleMst.MOD_USER_NAME = _operator.UserName;
                    db.Entry(_RoleMst).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: RoleMst/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ROLE_MST rOLE_MST = db.ROLE_MST.Find(id);
                if (rOLE_MST == null)
                {
                    return HttpNotFound();
                }
                RoleMstForm _form = new RoleMstForm();
                ComplementUtil.complement(rOLE_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: RoleMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(RoleMstForm form)
        {
            try
            {
                ROLE_MST _RoleMst = db.ROLE_MST.Find(form.ROLE_ID);
                if (DateTime.Compare((DateTime)_RoleMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _RoleMst.TYPE_ROLE, _RoleMst.MOD_USER_NAME));
                    return View(form);
                }
                _RoleMst.MOD_DATE = DateTime.Now;
                _RoleMst.MOD_USER_NAME = _operator.UserName;
                _RoleMst.DELETE_FLAG = true;
                db.Entry(_RoleMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                ComplementUtil.complement(_RoleMst, form);
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
