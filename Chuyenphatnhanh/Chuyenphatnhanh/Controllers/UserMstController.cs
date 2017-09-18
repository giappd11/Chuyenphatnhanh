using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;

namespace Chuyenphatnhanh.Controllers
{
    public class UserMstController : BaseController
    {
        // GET: UserMst
        public ActionResult Index()
        {
            var uSER_MST = db.USER_MST.Include(u => u.USER_CONFIG_MST);
            return View(uSER_MST.ToList());
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
            return View(uSER_MST);
        }

        // GET: UserMst/Create
        public ActionResult Create()
        {
            ViewBag.USER_ID = new SelectList(db.USER_CONFIG_MST, "USER_ID", "REG_USER_NAME");
            return View();
        }

        // POST: UserMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,USER_ID,USER_NAME,PASSWORD,NUMBER_LOGIN_FAIL,LAST_CHANGE_PASS,OLD_PASSWORD,ADDRESS,PHONE")] USER_MST uSER_MST)
        {
            if (ModelState.IsValid)
            {
                db.USER_MST.Add(uSER_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USER_ID = new SelectList(db.USER_CONFIG_MST, "USER_ID", "REG_USER_NAME", uSER_MST.USER_ID);
            return View(uSER_MST);
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
            ViewBag.USER_ID = new SelectList(db.USER_CONFIG_MST, "USER_ID", "REG_USER_NAME", uSER_MST.USER_ID);
            return View(uSER_MST);
        }

        // POST: UserMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,USER_ID,USER_NAME,PASSWORD,NUMBER_LOGIN_FAIL,LAST_CHANGE_PASS,OLD_PASSWORD,ADDRESS,PHONE")] USER_MST uSER_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USER_ID = new SelectList(db.USER_CONFIG_MST, "USER_ID", "REG_USER_NAME", uSER_MST.USER_ID);
            return View(uSER_MST);
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
            return View(uSER_MST);
        }

        // POST: UserMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            USER_MST uSER_MST = db.USER_MST.Find(id);
            db.USER_MST.Remove(uSER_MST);
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
