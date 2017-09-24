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
    public class RoleMstController : BaseController
    {
        private DBConnection db = new DBConnection();

        // GET: RoleMst
        public ActionResult Index()
        {
            return View(db.ROLE_MST.ToList());
        }

        // GET: RoleMst/Details/5
        public ActionResult Details(string id)
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
            return View(rOLE_MST);
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
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_USER_NAME,MOD_USER_NAME,REG_DATE,MOD_DATE,ROLE_ID,TYPE_ROLE,DESCRIPTION")] ROLE_MST rOLE_MST)
        {
            if (ModelState.IsValid)
            {
                db.ROLE_MST.Add(rOLE_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rOLE_MST);
        }

        // GET: RoleMst/Edit/5
        public ActionResult Edit(string id)
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
            return View(rOLE_MST);
        }

        // POST: RoleMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_USER_NAME,MOD_USER_NAME,REG_DATE,MOD_DATE,ROLE_ID,TYPE_ROLE,DESCRIPTION")] ROLE_MST rOLE_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOLE_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rOLE_MST);
        }

        // GET: RoleMst/Delete/5
        public ActionResult Delete(string id)
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
            return View(rOLE_MST);
        }

        // POST: RoleMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ROLE_MST rOLE_MST = db.ROLE_MST.Find(id);
            db.ROLE_MST.Remove(rOLE_MST);
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
