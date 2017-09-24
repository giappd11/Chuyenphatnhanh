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
    public class WardMstController : BaseController
    {
        private DBConnection db = new DBConnection();

        // GET: WardMst
        public ActionResult Index()
        {
            var wARD_MST = db.WARD_MST.Include(w => w.DISTRICT_MST);
            return View(wARD_MST.ToList());
        }

        // GET: WardMst/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WARD_MST wARD_MST = db.WARD_MST.Find(id);
            if (wARD_MST == null)
            {
                return HttpNotFound();
            }
            return View(wARD_MST);
        }

        // GET: WardMst/Create
        public ActionResult Create()
        {
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "DISTRICT_NAME");
            return View();
        }

        // POST: WardMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,WARD_ID,WARD_NAME,DISTRICT_ID")] WARD_MST wARD_MST)
        {
            if (ModelState.IsValid)
            {
                db.WARD_MST.Add(wARD_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "REG_USER_NAME", wARD_MST.DISTRICT_ID);
            return View(wARD_MST);
        }

        // GET: WardMst/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WARD_MST wARD_MST = db.WARD_MST.Find(id);
            if (wARD_MST == null)
            {
                return HttpNotFound();
            }
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "REG_USER_NAME", wARD_MST.DISTRICT_ID);
            return View(wARD_MST);
        }

        // POST: WardMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,WARD_ID,WARD_NAME,DISTRICT_ID")] WARD_MST wARD_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wARD_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "REG_USER_NAME", wARD_MST.DISTRICT_ID);
            return View(wARD_MST);
        }

        // GET: WardMst/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WARD_MST wARD_MST = db.WARD_MST.Find(id);
            if (wARD_MST == null)
            {
                return HttpNotFound();
            }
            return View(wARD_MST);
        }

        // POST: WardMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WARD_MST wARD_MST = db.WARD_MST.Find(id);
            db.WARD_MST.Remove(wARD_MST);
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


        [HttpGet]
        public JsonResult DistrictChange(string DistrictID)
        {

            List<WARD_MST> wards = db.WARD_MST.Where(u => u.DISTRICT_ID == DistrictID).ToList();
            List<SelectModel> select = new List<SelectModel>();
            foreach(WARD_MST ward in wards)
            {
                SelectModel model = new SelectModel();
                model.value = ward.WARD_ID;
                model.displayValue = ward.WARD_NAME;
                select.Add(model);
            }

            return Json(select.ToList(), JsonRequestBehavior.AllowGet);

        }

    }
}
