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
    public class CommonCodeController : BaseController
    { 
        // GET: CommonCode
        public ActionResult Index()
        {
            return View(db.COMMON_CODE.ToList());
        }

        // GET: CommonCode/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMON_CODE cOMMON_CODE = db.COMMON_CODE.Find(id);
            if (cOMMON_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cOMMON_CODE);
        }

        // GET: CommonCode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommonCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,COMMON_CODE_ID,COMMON_CODE1,COMMON_CODE_VALUE,COMMON_CODE_DESC,PARENT_CODE_VALUE")] COMMON_CODE cOMMON_CODE)
        {
            if (ModelState.IsValid)
            {
                db.COMMON_CODE.Add(cOMMON_CODE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMMON_CODE);
        }

        // GET: CommonCode/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMON_CODE cOMMON_CODE = db.COMMON_CODE.Find(id);
            if (cOMMON_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cOMMON_CODE);
        }

        // POST: CommonCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,COMMON_CODE_ID,COMMON_CODE1,COMMON_CODE_VALUE,COMMON_CODE_DESC,PARENT_CODE_VALUE")] COMMON_CODE cOMMON_CODE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMON_CODE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOMMON_CODE);
        }

        // GET: CommonCode/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMON_CODE cOMMON_CODE = db.COMMON_CODE.Find(id);
            if (cOMMON_CODE == null)
            {
                return HttpNotFound();
            }
            return View(cOMMON_CODE);
        }

        // POST: CommonCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            COMMON_CODE cOMMON_CODE = db.COMMON_CODE.Find(id);
            db.COMMON_CODE.Remove(cOMMON_CODE);
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
