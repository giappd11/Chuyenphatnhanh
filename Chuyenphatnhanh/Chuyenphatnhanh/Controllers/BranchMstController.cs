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
using Resource = Chuyenphatnhanh.Content.Texts;

namespace Chuyenphatnhanh.Controllers
{
    public class BranchMstController : BaseController
    {
        private DBConnection db = new DBConnection();

        // GET: BranchMst
        public ActionResult Index()
        {
            List<BRANCH_MST> _list = db.BRANCH_MST.ToList();
            List<BranchMstForm> _branchList = new List<BranchMstForm>();
            BranchMstForm _branchMst;
            DISTRICT_MST _districtMst = null;
            WARD_MST _wardMst = null;
            foreach (BRANCH_MST _branch in _list)
            {
                _wardMst = null;
                _districtMst = null;
                _branchMst = new BranchMstForm();
                ComplementUtil.complement(_branch, _branchMst);
                if (_branchMst.WARD_ID != null)
                {
                    _wardMst = db.WARD_MST.Where(u => u.WARD_ID == _branchMst.WARD_ID).FirstOrDefault();
                }
                if (_wardMst != null)
                {
                    _districtMst = db.DISTRICT_MST.Where(u => u.DISTRICT_ID == _wardMst.DISTRICT_ID).FirstOrDefault();
                }
                if (_districtMst != null)
                {
                    _branchMst.Display_Address = _branchMst.ADDRESS + ", " + _wardMst.WARD_NAME + ", " + _districtMst.DISTRICT_NAME;
                }
                _branchList.Add(_branchMst);
            }
            return View(_branchList);



            var bRANCH_MST = db.BRANCH_MST.Include(b => b.WARD_MST);
            return View(bRANCH_MST.ToList());
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
            ViewBag.WARD_ID = new SelectList(db.WARD_MST, "WARD_ID", "REG_USER_NAME");
            return View();
        }

        // POST: BranchMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,BRANCH_ID,BRANCH_NAME,ADDRESS,WARD_ID,LATITUDE,LONGITUDE")] BRANCH_MST bRANCH_MST)
        {
            if (ModelState.IsValid)
            {
                db.BRANCH_MST.Add(bRANCH_MST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WARD_ID = new SelectList(db.WARD_MST, "WARD_ID", "REG_USER_NAME", bRANCH_MST.WARD_ID);
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
            ViewBag.WARD_ID = new SelectList(db.WARD_MST, "WARD_ID", "REG_USER_NAME", bRANCH_MST.WARD_ID);
            return View(bRANCH_MST);
        }

        // POST: BranchMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_DATE,MOD_DATE,REG_USER_NAME,MOD_USER_NAME,BRANCH_ID,BRANCH_NAME,ADDRESS,WARD_ID,LATITUDE,LONGITUDE")] BRANCH_MST bRANCH_MST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bRANCH_MST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WARD_ID = new SelectList(db.WARD_MST, "WARD_ID", "REG_USER_NAME", bRANCH_MST.WARD_ID);
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
