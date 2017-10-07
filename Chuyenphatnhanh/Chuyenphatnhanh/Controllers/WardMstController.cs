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
    public class WardMstController : BaseController
    { 

        // GET: WardMst
        public ActionResult Index()
        {
            try
            {
                List<WARD_MST> _list = db.WARD_MST.Where(u => u.DELETE_FLAG == false).ToList();
                List<WardMstForm> _WardList = new List<WardMstForm>();
                foreach (WARD_MST _Ward in _list)
                {
                    WardMstForm _WardMst = new WardMstForm();
                    ComplementUtil.complement(_Ward, _WardMst);
                    _WardList.Add(_WardMst);
                }
                return View(_WardList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: WardMst/Details/5
        public ActionResult Details(string id)
        {
            try
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
                WardMstForm _form = new WardMstForm();
                ComplementUtil.complement(wARD_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: WardMst/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "DISTRICT_NAME");
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: WardMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WardMstForm form)
        {
            try
            {
                WARD_MST _WardMst = new WARD_MST();
                if (ModelState.IsValid)
                {
                    ComplementUtil.complement(form, _WardMst);
                    _WardMst.DELETE_FLAG = false;
                    _WardMst.MOD_DATE = DateTime.Now;
                    _WardMst.MOD_USER_NAME = _operator.UserName;
                    _WardMst.REG_DATE = DateTime.Now;
                    _WardMst.REG_USER_NAME = _operator.UserName;
                    _WardMst.DISTRICT_ID = GenerateID.GennerateID(db, Contant.WARDMST_SEQ, Contant.WARDMST_PREFIX);
                    db.WARD_MST.Add(_WardMst);
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
                }
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "DISTRICT_NAME", form.DISTRICT_ID);
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: WardMst/Edit/5
        public ActionResult Edit(string id)
        {
            try
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
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "DISTRICT_NAME", wARD_MST.DISTRICT_ID);
                WardMstForm _form = new WardMstForm();
                ComplementUtil.complement(wARD_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: WardMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WardMstForm form)
        {
            try
            {
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "DISTRICT_NAME", form.DISTRICT_ID);
                if (ModelState.IsValid)
                {
                    WARD_MST _WardMst = db.WARD_MST.Find(form.WARD_ID);
                    if (DateTime.Compare((DateTime)_WardMst.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _WardMst.WARD_NAME, _WardMst.MOD_USER_NAME));
                        return View(form);
                    }

                    ComplementUtil.complement(form, _WardMst);
                    _WardMst.MOD_DATE = DateTime.Now;
                    _WardMst.MOD_USER_NAME = _operator.UserName;
                    db.Entry(_WardMst).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: WardMst/Delete/5
        public ActionResult Delete(string id)
        {
            try
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
                ViewBag.DISTRICT_ID = new SelectList(db.DISTRICT_MST, "DISTRICT_ID", "DISTRICT_NAME", wARD_MST.DISTRICT_ID);
                WardMstForm _form = new WardMstForm();
                ComplementUtil.complement(wARD_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: WardMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(WardMstForm form)
        {
            try
            {
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
                WARD_MST _WardMst = db.WARD_MST.Find(form.WARD_ID);
                if (DateTime.Compare((DateTime)_WardMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _WardMst.WARD_NAME, _WardMst.MOD_USER_NAME));
                    return View(form);
                }

                ComplementUtil.complement(form, _WardMst);
                _WardMst.MOD_DATE = DateTime.Now;
                _WardMst.MOD_USER_NAME = _operator.UserName;
                _WardMst.DELETE_FLAG = true;
                db.Entry(_WardMst).State = EntityState.Modified;
                db.SaveChanges();
                ComplementUtil.complement(_WardMst, form);
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
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
            try
            {
                List<WARD_MST> wards = db.WARD_MST.Where(u => u.DISTRICT_ID == DistrictID).ToList();
                List<SelectModel> select = new List<SelectModel>();
                foreach (WARD_MST ward in wards)
                {
                    SelectModel model = new SelectModel();
                    model.value = ward.WARD_ID;
                    model.displayValue = ward.WARD_NAME;
                    select.Add(model);
                }

                return Json(select.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
