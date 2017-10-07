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
    public class SecRoleMstController : BaseController
    {
        // GET: SecRoleMst
        public ActionResult Index()
        {
            try
            {
                List<SecRoleMstForm> _List = new List<SecRoleMstForm>();
                List<SEC_ROLE_MST> _ListSec = db.SEC_ROLE_MST.Where(u => u.DELETE_FLAG == false).Include(s => s.ROLE_MST).ToList();
                foreach (SEC_ROLE_MST _sec in _ListSec)
                {
                    SecRoleMstForm _form = new SecRoleMstForm();
                    ComplementUtil.complement(_sec, _form);
                    _form.Role_name = _sec.ROLE_MST.TYPE_ROLE;
                    _List.Add(_form);
                }
                return View(_List);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: SecRoleMst/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SEC_ROLE_MST sEC_ROLE_MST = db.SEC_ROLE_MST.Find(id);
                if (sEC_ROLE_MST == null)
                {
                    return HttpNotFound();
                }
                SecRoleMstForm _form = new SecRoleMstForm();
                ComplementUtil.complement(sEC_ROLE_MST, _form);
                ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "TYPE_ROLE", _form.ROLE_ID);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: SecRoleMst/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "TYPE_ROLE");
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: SecRoleMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecRoleMstForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SEC_ROLE_MST sEC_ROLE_MST = new SEC_ROLE_MST();

                    ComplementUtil.complement(form, sEC_ROLE_MST);

                    sEC_ROLE_MST.DELETE_FLAG = false;
                    sEC_ROLE_MST.MOD_DATE = DateTime.Now;
                    sEC_ROLE_MST.REG_DATE = DateTime.Now;
                    sEC_ROLE_MST.MOD_USER_NAME = _operator.UserName;
                    sEC_ROLE_MST.REG_USER_NAME = _operator.UserName;
                    sEC_ROLE_MST.SEC_ROLE_ID = GenerateID.GennerateID(db, Contant.SECROLEMST_SEQ, Contant.SECROLEMST_PREFIX);
                    db.SEC_ROLE_MST.Add(sEC_ROLE_MST);
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
                }

                ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "TYPE_ROLE", form.ROLE_ID);
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: SecRoleMst/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SEC_ROLE_MST sEC_ROLE_MST = db.SEC_ROLE_MST.Find(id);
                if (sEC_ROLE_MST == null)
                {
                    return HttpNotFound();
                }
                SecRoleMstForm _form = new SecRoleMstForm();
                ComplementUtil.complement(sEC_ROLE_MST, _form);

                ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "TYPE_ROLE", _form.ROLE_ID);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: SecRoleMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecRoleMstForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SEC_ROLE_MST SecRoleMst = db.SEC_ROLE_MST.Find(form.SEC_ROLE_ID);
                    if (DateTime.Compare((DateTime)SecRoleMst.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, SecRoleMst.SEC_ROLE_ID, SecRoleMst.MOD_USER_NAME));
                        return View(form);
                    }

                    ComplementUtil.complement(form, SecRoleMst);
                    SecRoleMst.MOD_DATE = DateTime.Now;
                    SecRoleMst.MOD_USER_NAME = _operator.UserName;
                    db.Entry(SecRoleMst).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                }
                ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "TYPE_ROLE", form.ROLE_ID);
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: SecRoleMst/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SEC_ROLE_MST sEC_ROLE_MST = db.SEC_ROLE_MST.Find(id);
                if (sEC_ROLE_MST == null)
                {
                    return HttpNotFound();
                }
                SecRoleMstForm _form = new SecRoleMstForm();
                ComplementUtil.complement(sEC_ROLE_MST, _form);

                ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "TYPE_ROLE", _form.ROLE_ID);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: SecRoleMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(SecRoleMstForm form)
        {
            try
            {
                SEC_ROLE_MST _SecRoleMst = db.SEC_ROLE_MST.Find(form.SEC_ROLE_ID);
                if (DateTime.Compare((DateTime)_SecRoleMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _SecRoleMst.VALUE, _SecRoleMst.MOD_USER_NAME));
                    return View(form);
                }
                _SecRoleMst.MOD_DATE = DateTime.Now;
                _SecRoleMst.MOD_USER_NAME = _operator.UserName;
                _SecRoleMst.DELETE_FLAG = true;
                db.Entry(_SecRoleMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                ComplementUtil.complement(_SecRoleMst, form);
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
