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
    public class UserConfigMstController : BaseController
    { 

        // GET: UserConfigMst
        public ActionResult Index()
        {
            var uSER_CONFIG_MST = db.USER_CONFIG_MST.Include(u => u.BRANCH_MST).Include(u => u.ROLE_MST).Include(u => u.USER_MST);
            return View(uSER_CONFIG_MST.ToList());
        }

        // GET: UserConfigMst/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_CONFIG_MST uSER_CONFIG_MST = db.USER_CONFIG_MST.Find(id);
            if (uSER_CONFIG_MST == null)
            {
                return HttpNotFound();
            }
            return View(uSER_CONFIG_MST);
        }

        // GET: UserConfigMst/Create
        public ActionResult Create()
        {
            ViewBag.BRANCH_ID = new SelectList(db.BRANCH_MST, "BRANCH_ID", "REG_USER_NAME");
            ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "REG_USER_NAME");
            ViewBag.USER_ID = new SelectList(db.USER_MST, "USER_ID", "REG_USER_NAME");
            return View();
        }

        // POST: UserConfigMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_USER_NAME,MOD_USER_NAME,REG_DATE,MOD_DATE,USER_ID,BRANCH_ID,ROLE_ID")] USER_CONFIG_MST uSER_CONFIG_MST)
        {
            if (ModelState.IsValid)
            {
                db.USER_CONFIG_MST.Add(uSER_CONFIG_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BRANCH_ID = new SelectList(db.BRANCH_MST, "BRANCH_ID", "REG_USER_NAME", uSER_CONFIG_MST.BRANCH_ID);
            ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "REG_USER_NAME", uSER_CONFIG_MST.ROLE_ID);
            ViewBag.USER_ID = new SelectList(db.USER_MST, "USER_ID", "REG_USER_NAME", uSER_CONFIG_MST.USER_ID);
            return View(uSER_CONFIG_MST);
        }

        // GET: UserConfigMst/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_CONFIG_MST uSER_CONFIG_MST = db.USER_CONFIG_MST.Find(id);
            if (uSER_CONFIG_MST == null)
            {
                return HttpNotFound();
            }
            ViewBag.BRANCH_ID = new SelectList(db.BRANCH_MST, "BRANCH_ID", "REG_USER_NAME", uSER_CONFIG_MST.BRANCH_ID);
            ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "REG_USER_NAME", uSER_CONFIG_MST.ROLE_ID);
            ViewBag.USER_ID = new SelectList(db.USER_MST, "USER_ID", "REG_USER_NAME", uSER_CONFIG_MST.USER_ID);
            return View(uSER_CONFIG_MST);
        }

        // POST: UserConfigMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_USER_NAME,MOD_USER_NAME,REG_DATE,MOD_DATE,USER_ID,BRANCH_ID,ROLE_ID")] USER_CONFIG_MST uSER_CONFIG_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_CONFIG_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BRANCH_ID = new SelectList(db.BRANCH_MST, "BRANCH_ID", "REG_USER_NAME", uSER_CONFIG_MST.BRANCH_ID);
            ViewBag.ROLE_ID = new SelectList(db.ROLE_MST, "ROLE_ID", "REG_USER_NAME", uSER_CONFIG_MST.ROLE_ID);
            ViewBag.USER_ID = new SelectList(db.USER_MST, "USER_ID", "REG_USER_NAME", uSER_CONFIG_MST.USER_ID);
            return View(uSER_CONFIG_MST);
        }

        // GET: UserConfigMst/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_CONFIG_MST uSER_CONFIG_MST = db.USER_CONFIG_MST.Find(id);
            if (uSER_CONFIG_MST == null)
            {
                return HttpNotFound();
            }
            return View(uSER_CONFIG_MST);
        }

        // POST: UserConfigMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            USER_CONFIG_MST uSER_CONFIG_MST = db.USER_CONFIG_MST.Find(id);
            db.USER_CONFIG_MST.Remove(uSER_CONFIG_MST);
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
