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
    public class BillController : BaseController
    {
        // GET: Bill
        public ActionResult Index()
        {
            var bILL_HDR_TBL = db.BILL_HDR_TBL.Include(b => b.CUST_MST).Include(b => b.CUST_MST1);
            return View(bILL_HDR_TBL.ToList());
        }

        // GET: Bill/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL_HDR_TBL bILL_HDR_TBL = db.BILL_HDR_TBL.Find(id);
            if (bILL_HDR_TBL == null)
            {
                return HttpNotFound();
            }
            return View(bILL_HDR_TBL);
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            ViewBag.CUST_FROM_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME");
            ViewBag.CUST_TO_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_USER_NAME,MOD_USER_NAME,REG_DATE,MOD_DATE,BILL_HDR_ID,CUST_FROM_ID,CUST_TO_ID,STATUS,COUNTRY_FROM,PROVINCE_FROM,DISTRICT_FROM,ADDRESS_FROM,COUNTRY_TO,PROVINCE_TO,DISTRICT_TO,ADDRESS_TO,COUNTRY_CURRENT,PROVINCE_CURRENT,DISTRICT_CURRENT,ADDRESS_CURRENT")] BILL_HDR_TBL bILL_HDR_TBL)
        {
            if (ModelState.IsValid)
            {
                db.BILL_HDR_TBL.Add(bILL_HDR_TBL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CUST_FROM_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME", bILL_HDR_TBL.CUST_FROM_ID);
            ViewBag.CUST_TO_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME", bILL_HDR_TBL.CUST_TO_ID);
            return View(bILL_HDR_TBL);
        }

        // GET: Bill/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL_HDR_TBL bILL_HDR_TBL = db.BILL_HDR_TBL.Find(id);
            if (bILL_HDR_TBL == null)
            {
                return HttpNotFound();
            }
            ViewBag.CUST_FROM_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME", bILL_HDR_TBL.CUST_FROM_ID);
            ViewBag.CUST_TO_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME", bILL_HDR_TBL.CUST_TO_ID);
            return View(bILL_HDR_TBL);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_USER_NAME,MOD_USER_NAME,REG_DATE,MOD_DATE,BILL_HDR_ID,CUST_FROM_ID,CUST_TO_ID,STATUS,COUNTRY_FROM,PROVINCE_FROM,DISTRICT_FROM,ADDRESS_FROM,COUNTRY_TO,PROVINCE_TO,DISTRICT_TO,ADDRESS_TO,COUNTRY_CURRENT,PROVINCE_CURRENT,DISTRICT_CURRENT,ADDRESS_CURRENT")] BILL_HDR_TBL bILL_HDR_TBL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bILL_HDR_TBL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CUST_FROM_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME", bILL_HDR_TBL.CUST_FROM_ID);
            ViewBag.CUST_TO_ID = new SelectList(db.CUST_MST, "CUST_ID", "REG_USER_NAME", bILL_HDR_TBL.CUST_TO_ID);
            return View(bILL_HDR_TBL);
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL_HDR_TBL bILL_HDR_TBL = db.BILL_HDR_TBL.Find(id);
            if (bILL_HDR_TBL == null)
            {
                return HttpNotFound();
            }
            return View(bILL_HDR_TBL);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BILL_HDR_TBL bILL_HDR_TBL = db.BILL_HDR_TBL.Find(id);
            db.BILL_HDR_TBL.Remove(bILL_HDR_TBL);
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
