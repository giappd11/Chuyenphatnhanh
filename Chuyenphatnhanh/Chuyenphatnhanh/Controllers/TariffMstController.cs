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
    public class TariffMstController : BaseController
    {
        private DBConnection db = new DBConnection();

        // GET: TariffMst
        public ActionResult Index()
        {
            return View(db.TARIFF_MST.ToList());
        }

        // GET: TariffMst/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TARIFF_MST tARIFF_MST = db.TARIFF_MST.Find(id);
            if (tARIFF_MST == null)
            {
                return HttpNotFound();
            }
            return View(tARIFF_MST);
        }

        // GET: TariffMst/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TariffMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,TARIFF_ID,WEIGHT_FROM,WEIGHT_TO,DISTANCE_FROM,DISTANCE_TO,PRICE")] TARIFF_MST tARIFF_MST)
        {
            if (ModelState.IsValid)
            {
                db.TARIFF_MST.Add(tARIFF_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tARIFF_MST);
        }

        // GET: TariffMst/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TARIFF_MST tARIFF_MST = db.TARIFF_MST.Find(id);
            if (tARIFF_MST == null)
            {
                return HttpNotFound();
            }
            return View(tARIFF_MST);
        }

        // POST: TariffMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,TARIFF_ID,WEIGHT_FROM,WEIGHT_TO,DISTANCE_FROM,DISTANCE_TO,PRICE")] TARIFF_MST tARIFF_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tARIFF_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tARIFF_MST);
        }

        // GET: TariffMst/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TARIFF_MST tARIFF_MST = db.TARIFF_MST.Find(id);
            if (tARIFF_MST == null)
            {
                return HttpNotFound();
            }
            return View(tARIFF_MST);
        }

        // POST: TariffMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TARIFF_MST tARIFF_MST = db.TARIFF_MST.Find(id);
            db.TARIFF_MST.Remove(tARIFF_MST);
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
