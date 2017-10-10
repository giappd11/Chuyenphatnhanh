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
    public class DistrictMstController : BaseController
    {
        // GET: DistrictMst
        public ActionResult Index()
        {
            try
            {
                List<DISTRICT_MST> _list = db.DISTRICT_MST.Where(u => u.DELETE_FLAG == false).ToList();
                List<DistrictMstForm> _districtList = new List<DistrictMstForm>();
                foreach (DISTRICT_MST _dis in _list)
                {
                    DistrictMstForm _district = new DistrictMstForm();
                    ComplementUtil.complement(_dis, _district);
                    _districtList.Add(_district);
                }
                return View(_districtList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: DistrictMst/Details/5
        public ActionResult Details(string id)
        {
            try
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
                DistrictMstForm _form = new DistrictMstForm();
                ComplementUtil.complement(dISTRICT_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
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
        public ActionResult Create(DistrictMstForm form)
        {
            try
            {
                DISTRICT_MST _DistrictMst = new DISTRICT_MST();
                if (ModelState.IsValid)
                {
                    ComplementUtil.complement(form, _DistrictMst);
                    _DistrictMst.DELETE_FLAG = false;
                    _DistrictMst.MOD_DATE = DateTime.Now;
                    _DistrictMst.MOD_USER_NAME = _operator.UserName;
                    _DistrictMst.REG_DATE = DateTime.Now;
                    _DistrictMst.REG_USER_NAME = _operator.UserName;
                    _DistrictMst.DISTRICT_ID = GenerateID.GennerateID(db, Contant.DISTRICTMST_SEQ, Contant.DISTRICTMST_PREFIX);
                    db.DISTRICT_MST.Add(_DistrictMst);
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: DistrictMst/Edit/5
        public ActionResult Edit(string id)
        {
            try
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
                DistrictMstForm _form = new DistrictMstForm();
                ComplementUtil.complement(dISTRICT_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: DistrictMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DistrictMstForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DISTRICT_MST _DistrictMst = db.DISTRICT_MST.Find(form.DISTRICT_ID);
                    if (DateTime.Compare((DateTime)_DistrictMst.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _DistrictMst.DISTRICT_NAME, _DistrictMst.MOD_USER_NAME));
                        return View(form);
                    }

                    ComplementUtil.complement(form, _DistrictMst);
                    _DistrictMst.MOD_DATE = DateTime.Now;
                    _DistrictMst.MOD_USER_NAME = _operator.UserName;
                    db.Entry(_DistrictMst).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: DistrictMst/Delete/5
        public ActionResult Delete(string id)
        {
            try
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
                DistrictMstForm _form = new DistrictMstForm();
                ComplementUtil.complement(dISTRICT_MST, _form);
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: DistrictMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DistrictMstForm form)
        {
            try
            {
                DISTRICT_MST _DistrictMst = db.DISTRICT_MST.Find(form.DISTRICT_ID);
                if (DateTime.Compare((DateTime)_DistrictMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _DistrictMst.DISTRICT_ID, _DistrictMst.MOD_USER_NAME));
                    return View(form);
                }
                _DistrictMst.MOD_DATE = DateTime.Now;
                _DistrictMst.MOD_USER_NAME = _operator.UserName;
                _DistrictMst.DELETE_FLAG = true;
                db.Entry(_DistrictMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.DeleteSuccess;
                ComplementUtil.complement(_DistrictMst, form);
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
    }
}
