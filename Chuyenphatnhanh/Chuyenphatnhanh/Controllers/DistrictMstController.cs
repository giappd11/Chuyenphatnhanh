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
    public class DistrictMstController : BaseController
    {
        private DBConnection db = new DBConnection();

        // GET: DistrictMst
        public ActionResult Index()
        {
            return View(db.DISTRICT_MST.ToList());
        }

        // GET: DistrictMst/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISTRICT_MST dISTRICT_MST = db.DISTRICT_MST.Find(id);
            if (dISTRICT_MST == null)
            {
                return HttpNotFound();
            }
            return View(dISTRICT_MST);
        }

        // GET: DistrictMst/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DistrictMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,DISTRICT_ID,DISTRICT_NAME")] DISTRICT_MST dISTRICT_MST)
        {
            if (ModelState.IsValid)
            {
                db.DISTRICT_MST.Add(dISTRICT_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dISTRICT_MST);
        }

        // GET: DistrictMst/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISTRICT_MST dISTRICT_MST = db.DISTRICT_MST.Find(id);
            if (dISTRICT_MST == null)
            {
                return HttpNotFound();
            }
            return View(dISTRICT_MST);
        }

        // POST: DistrictMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,DISTRICT_ID,DISTRICT_NAME")] DISTRICT_MST dISTRICT_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dISTRICT_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dISTRICT_MST);
        }

        // GET: DistrictMst/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISTRICT_MST dISTRICT_MST = db.DISTRICT_MST.Find(id);
            if (dISTRICT_MST == null)
            {
                return HttpNotFound();
            }
            return View(dISTRICT_MST);
        }

        // POST: DistrictMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DISTRICT_MST dISTRICT_MST = db.DISTRICT_MST.Find(id);
            db.DISTRICT_MST.Remove(dISTRICT_MST);
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
