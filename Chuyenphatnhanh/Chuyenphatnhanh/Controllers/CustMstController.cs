﻿using System;
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
    public class CustMstController : BaseController
    { 

        // GET: CustMst
        public ActionResult Index()
        {
            return View(db.CUST_MST.ToList());
        }

        // GET: CustMst/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUST_MST cUST_MST = db.CUST_MST.Find(id);
            if (cUST_MST == null)
            {
                return HttpNotFound();
            }
            return View(cUST_MST);
        }

        // GET: CustMst/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustMstForm form)
        {
            CUST_MST _custMst = new CUST_MST();
            if (ModelState.IsValid)
            {
                ComplementUtil.complement(form, _custMst);
                _custMst.DELETE_FLAG = false; 
                _custMst.MOD_DATE = DateTime.Now;
                _custMst.MOD_USER_NAME = _operator.UserName;
                _custMst.REG_DATE = DateTime.Now;
                _custMst.REG_USER_NAME = _operator.UserName;
                _custMst.CUST_ID = GenerateID.GennerateID(db, Contant.CUSTMST_SEQ, Contant.CUSTMST_PREFIX);
                db.CUST_MST.Add(_custMst);
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
            }
            return View(form);
        }

        // GET: CustMst/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUST_MST cUST_MST = db.CUST_MST.Find(id);
            if (cUST_MST == null)
            {
                return HttpNotFound();
            }
            CustMstForm _form = new CustMstForm();
            ComplementUtil.complement(cUST_MST, _form);
            return View(_form);
        }

        // POST: CustMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustMstForm form)
        {
            if (ModelState.IsValid)
            {
                CUST_MST _custMst = new CUST_MST();
                var _operator = (Operator)Session[Contant.SESSIONLOGED];
                _custMst = db.CUST_MST.Where(u => u.CUST_ID == form.CUST_ID).FirstOrDefault();
                if (DateTime.Compare((DateTime) _custMst.MOD_DATE, form.MOD_DATE) != 0)
                { 
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _custMst.CUST_NAME, _custMst.MOD_USER_NAME));
                    return View(form);
                }
                ComplementUtil.complement(form, _custMst);
                _custMst.MOD_DATE = DateTime.Now;
                _custMst.MOD_USER_NAME = _operator.UserName;
                db.Entry(_custMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                return View(form);
            }
            return View(form);
        }

        // GET: CustMst/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUST_MST cUST_MST = db.CUST_MST.Find(id);
            if (cUST_MST == null)
            {
                return HttpNotFound();
            }
            return View(cUST_MST);
        }

        // POST: CustMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CUST_MST cUST_MST = db.CUST_MST.Find(id);
            db.CUST_MST.Remove(cUST_MST);
            db.SaveChanges();
            return RedirectToAction("Index");
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
