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
    public class BranchMstController : BaseController
    { 
        // GET: BranchMst
        public ActionResult Index()
        {
            return View(db.BRANCH_MST.ToList());
        } 

        // GET: BranchMst/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRANCH_MST bRANCH_MST = db.BRANCH_MST.Find(id);
            if (bRANCH_MST == null)
            {
                return HttpNotFound();
            }
            return View(bRANCH_MST);
        }

        // GET: BranchMst/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BranchMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,BRANCH_ID,BRANCH_NAME,ADDRESS,COUNTRY,PROVINCE,DISTRICT,LATITUDE,LONGITUDE")] BRANCH_MST bRANCH_MST)
        {
            if (ModelState.IsValid)
            {
                db.BRANCH_MST.Add(bRANCH_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bRANCH_MST);
        }

        // GET: BranchMst/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRANCH_MST bRANCH_MST = db.BRANCH_MST.Find(id);
            if (bRANCH_MST == null)
            {
                return HttpNotFound();
            }
            return View(bRANCH_MST);
        }

        // POST: BranchMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,BRANCH_ID,BRANCH_NAME,ADDRESS,COUNTRY,PROVINCE,DISTRICT,LATITUDE,LONGITUDE")] BRANCH_MST bRANCH_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bRANCH_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bRANCH_MST);
        }

        // GET: BranchMst/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRANCH_MST bRANCH_MST = db.BRANCH_MST.Find(id);
            if (bRANCH_MST == null)
            {
                return HttpNotFound();
            }
            return View(bRANCH_MST);
        }

        // POST: BranchMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BRANCH_MST bRANCH_MST = db.BRANCH_MST.Find(id);
            db.BRANCH_MST.Remove(bRANCH_MST);
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
