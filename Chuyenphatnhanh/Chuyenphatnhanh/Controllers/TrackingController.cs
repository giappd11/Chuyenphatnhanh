using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using Chuyenphatnhanh.Util;

namespace Chuyenphatnhanh.Controllers
{
    public class TrackingController : Controller
    {
        private DBConnection db = new DBConnection();

        // GET: Tracking
        public ActionResult Index()
        {
            return View(db.BILL_LOG_TBL.ToList());
        }

        // GET: Tracking/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL_LOG_TBL bILL_LOG_TBL = db.BILL_LOG_TBL.Find(id);
            if (bILL_LOG_TBL == null)
            {
                return HttpNotFound();
            }
            return View(bILL_LOG_TBL);
        }

        // GET: Tracking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tracking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,REG_USER_NAME,MOD_DATE,MOD_USER_NAME,BILL_LOG_ID,TRANSACTION_DATE,TRANSACTION_ID,TRANSACTION_UID,CUST_FROM_ID,CUST_TO_ID,STATUS,DISTRICT_ID_FROM,WARD_ID_FROM,ADDRESS_FROM,DISTRICT_ID_TO,WARD_ID_TO,ADDRESS_TO,BRANCH_ID_CURRENT,BRANCH_ID_TEMP,AMOUNT,UID_CURRENT")] BILL_LOG_TBL bILL_LOG_TBL)
        {
            if (ModelState.IsValid)
            {
                db.BILL_LOG_TBL.Add(bILL_LOG_TBL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bILL_LOG_TBL);
        }

        // GET: Tracking/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL_LOG_TBL bILL_LOG_TBL = db.BILL_LOG_TBL.Find(id);
            if (bILL_LOG_TBL == null)
            {
                return HttpNotFound();
            }
            return View(bILL_LOG_TBL);
        }

        // POST: Tracking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,REG_USER_NAME,MOD_DATE,MOD_USER_NAME,BILL_LOG_ID,TRANSACTION_DATE,TRANSACTION_ID,TRANSACTION_UID,CUST_FROM_ID,CUST_TO_ID,STATUS,DISTRICT_ID_FROM,WARD_ID_FROM,ADDRESS_FROM,DISTRICT_ID_TO,WARD_ID_TO,ADDRESS_TO,BRANCH_ID_CURRENT,BRANCH_ID_TEMP,AMOUNT,UID_CURRENT")] BILL_LOG_TBL bILL_LOG_TBL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bILL_LOG_TBL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bILL_LOG_TBL);
        }

        // GET: Tracking/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL_LOG_TBL bILL_LOG_TBL = db.BILL_LOG_TBL.Find(id);
            if (bILL_LOG_TBL == null)
            {
                return HttpNotFound();
            }
            return View(bILL_LOG_TBL);
        }

        // POST: Tracking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BILL_LOG_TBL bILL_LOG_TBL = db.BILL_LOG_TBL.Find(id);
            db.BILL_LOG_TBL.Remove(bILL_LOG_TBL);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string search)
        {
            var _list = db.BILL_LOG_TBL.Where(u => u.TRANSACTION_ID == search).OrderByDescending(u => u.REG_DATE).ToList();

            List<BillLogTblForm> _listForm = new List<BillLogTblForm>();
            foreach (BILL_LOG_TBL _bill in _list)
            {
                BillLogTblForm _form = new BillLogTblForm();
                ComplementUtil.complement(_bill, _form);
                BRANCH_MST _BranchCurrent = db.BRANCH_MST.Find(_bill.BRANCH_ID_CURRENT);
                if (_BranchCurrent != null)
                {
                    _form.BRANCH_NAME_CURRENT = _BranchCurrent.BRANCH_NAME;
                }
                BRANCH_MST _BranchTemp = db.BRANCH_MST.Find(_bill.BRANCH_ID_TEMP);
                if (_BranchTemp != null)
                {
                    _form.BRANCH_NAME_TEMP = _BranchTemp.BRANCH_NAME;
                }
                USER_MST _UserCurrent = db.USER_MST.Find(_bill.UID_CURRENT);
                if (_UserCurrent != null)
                {
                    _form.USERNAME_CURRENT = _UserCurrent.NAME;
                }
                _listForm.Add(_form);
            }
            return View(_listForm);
        }

        // GET: Tracking/Edit/5
        public ActionResult BangGia(string id)
        {
            return View();
        }
        // GET: Tracking/Edit/5
        public ActionResult Branch(string id)
        {
            ViewBag.Json = "";

            var data = db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).ToList();
            List<MapMaker> _list = new List<MapMaker>();
            foreach(BRANCH_MST _branch in data)
            {
                MapMaker _map = new MapMaker();
                _map.lat = _branch.LATITUDE.ToString();
                _map.longi = _branch.LONGITUDE.ToString();
                _map.branchName = _branch.BRANCH_NAME;

                string Address = _branch.ADDRESS;
                if (_branch.WARD_MST != null)
                {
                    Address = Address + "," + _branch.WARD_MST.WARD_NAME;
                }
                if (_branch.DISTRICT_MST != null)
                {
                    Address = Address + "," +  _branch.DISTRICT_MST.DISTRICT_NAME;
                }
                _map.branchAddress = Address;
                _list.Add(_map);
            }
            return View(_list);
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
