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
        // GET: BranchMst
        public ActionResult Index()
        {
            List<BRANCH_MST> _list = db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).ToList();
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
            BranchMstForm _form = new BranchMstForm();
            ComplementUtil.complement(bRANCH_MST, _form);
            return View(_form);
        }

        // GET: BranchMst/Create
        public ActionResult Create()
        {
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME" );
            ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            return View();
        }

        // POST: BranchMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( BranchMstForm form)
        {
            BRANCH_MST _BranchMst = new BRANCH_MST();
            if (ModelState.IsValid)
            {
                ComplementUtil.complement(form, _BranchMst);
                _BranchMst.DELETE_FLAG = false;
                _BranchMst.MOD_DATE = DateTime.Now;
                _BranchMst.MOD_USER_NAME = _operator.UserName;
                _BranchMst.REG_DATE = DateTime.Now;
                _BranchMst.REG_USER_NAME = _operator.UserName;
                _BranchMst.BRANCH_ID = GenerateID.GennerateID(db, Contant.BRANCHMST_SEQ, Contant.BRANCHMST_PREFIX);
                db.BRANCH_MST.Add(_BranchMst);
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
                 
            }
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME", form.DISTRICT_ID );

            if (form.DISTRICT_ID != null)
            {
                ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DISTRICT_ID), "WARD_ID", "WARD_NAME", form.WARD_ID);
            }
            else
            {
                ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            }
            return View(form);
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
            BranchMstForm _form = new BranchMstForm();
            ComplementUtil.complement(bRANCH_MST, _form);
            _form.DISTRICT_ID = bRANCH_MST.WARD_MST.DISTRICT_ID;
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME", _form.DISTRICT_ID);

            if (_form.DISTRICT_ID != null)
            {
                ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _form.DISTRICT_ID), "WARD_ID", "WARD_NAME", _form.WARD_ID);
            }
            else
            {
                ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            }
            return View(_form);
        }

        // POST: BranchMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BranchMstForm form)
        {
            ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME", form.DISTRICT_ID);
            if (form.DISTRICT_ID != null)
            {
                ViewBag.WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DISTRICT_ID), "WARD_ID", "WARD_NAME", form.WARD_ID);
            }
            else
            {
                ViewBag.WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            }
            if (ModelState.IsValid)
            {
                BRANCH_MST _BranchMst = db.BRANCH_MST.Find(form.BRANCH_ID);
                if (DateTime.Compare((DateTime)_BranchMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BranchMst.BRANCH_NAME, _BranchMst.MOD_USER_NAME));
                    return View(form);
                }

                ComplementUtil.complement(form, _BranchMst);
                _BranchMst.MOD_DATE = DateTime.Now;
                _BranchMst.MOD_USER_NAME = _operator.UserName;
                db.Entry(_BranchMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
            }
           
            return View(form);
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
            BranchMstForm _form = new BranchMstForm();
            ComplementUtil.complement(bRANCH_MST, _form);
            return View(_form);
        }

        // POST: BranchMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(BranchMstForm form)
        { 
            BRANCH_MST _BranchMst = db.BRANCH_MST.Find(form.BRANCH_ID);
            if (DateTime.Compare((DateTime)_BranchMst.MOD_DATE, form.MOD_DATE) != 0)
            {
                ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BranchMst.BRANCH_NAME, _BranchMst.MOD_USER_NAME));
                return View(form);
            } 
            _BranchMst.MOD_DATE = DateTime.Now;
            _BranchMst.MOD_USER_NAME = _operator.UserName;
            _BranchMst.DELETE_FLAG = true;
            db.Entry(_BranchMst).State = EntityState.Modified;
            db.SaveChanges();
            ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
            ComplementUtil.complement(_BranchMst, form);
             
            ViewBag.WARD_ID = new SelectList(db.WARD_MST, "WARD_ID", "REG_USER_NAME", form.WARD_ID);
            return View(form);
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
