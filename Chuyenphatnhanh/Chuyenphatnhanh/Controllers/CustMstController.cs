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
    public class CustMstController : BaseController
    {

        // GET: CustMst
        public ActionResult Index()
        {
            try
            {
                List<CUST_MST> _list = db.CUST_MST.Where(u => u.DELETE_FLAG == false).ToList();
                List<CustMstForm> _custList = new List<CustMstForm>();
                CustMstForm _custommer;
                DISTRICT_MST _districtMst = null;
                WARD_MST _wardMst = null;
                foreach (CUST_MST _cust in _list)
                {
                    _wardMst = null;
                    _districtMst = null;
                    _custommer = new CustMstForm();
                    ComplementUtil.complement(_cust, _custommer);
                    if (_custommer.DEFAULT_WARD_ID != null)
                    {
                        _wardMst = db.WARD_MST.Where(u => u.WARD_ID == _custommer.DEFAULT_WARD_ID).FirstOrDefault();
                    }
                    if (_wardMst != null)
                    {
                        _districtMst = db.DISTRICT_MST.Where(u => u.DISTRICT_ID == _wardMst.DISTRICT_ID).FirstOrDefault();
                    }
                    if (_districtMst != null)
                    {
                        _custommer.Display_Address = _custommer.DEFAULT_ADDRESS + ", " + _wardMst.WARD_NAME + ", " + _districtMst.DISTRICT_NAME;
                    }
                    _custList.Add(_custommer);
                }
                return View(_custList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: CustMst/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CUST_MST cUST_MST = db.CUST_MST.Find(id);
                if (cUST_MST == null)
                {
                    return HttpNotFound();
                }
                WARD_MST _ward = db.WARD_MST.Where(u => u.WARD_ID == cUST_MST.DEFAULT_WARD_ID).FirstOrDefault();
                ViewBag.DEFAULT_DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (_ward != null)
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _ward.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                CustMstForm _form = new CustMstForm();
                ComplementUtil.complement(cUST_MST, _form);
                if (_form.DEFAULT_WARD_ID != null)
                {
                    _form.DEFAULT_DISTRICT_ID = cUST_MST.WARD_MST.DISTRICT_ID;
                }
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: CustMst/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.DEFAULT_DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DEFAULT_WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: CustMst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustMstForm form)
        {
            try
            {
                CUST_MST _custMst = new CUST_MST();
                if (ModelState.IsValid)
                {
                    ComplementUtil.complement(form, _custMst);
                    _custMst.DELETE_FLAG = false;
                    _custMst.MOD_DATE = DateTime.Now;
                    _custMst.MOD_USER_NAME = _operator.UserName;
                    _custMst.REG_DATE = DateTime.Now;
                    _custMst.REG_USER_NAME = _operator.UserName;
                    _custMst.CUST_ID = GenerateID.GennerateID(db, Contant.CUSTMST_SEQ, Contant.CUSTMST_PREFIX);
                    db.CUST_MST.Add(_custMst);
                    db.SaveChanges();
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
                }
                ViewBag.DEFAULT_DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (form.DEFAULT_DISTRICT_ID != null)
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DEFAULT_DISTRICT_ID), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: CustMst/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CUST_MST cUST_MST = db.CUST_MST.Find(id);
                if (cUST_MST == null)
                {
                    return HttpNotFound();
                }
                WARD_MST _ward = db.WARD_MST.Where(u => u.WARD_ID == cUST_MST.DEFAULT_WARD_ID).FirstOrDefault();
                ViewBag.DEFAULT_DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (_ward != null)
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _ward.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                CustMstForm _form = new CustMstForm();
                ComplementUtil.complement(cUST_MST, _form);
                if (_form.DEFAULT_WARD_ID != null)
                {
                    _form.DEFAULT_DISTRICT_ID = cUST_MST.WARD_MST.DISTRICT_ID;
                }
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: CustMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustMstForm form)
        {
            try
            {
                ViewBag.DEFAULT_DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DEFAULT_WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DEFAULT_DISTRICT_ID), "WARD_ID", "WARD_NAME");
                if (ModelState.IsValid)
                {
                    CUST_MST _custMst = new CUST_MST();
                    _custMst = db.CUST_MST.Where(u => u.CUST_ID == form.CUST_ID).FirstOrDefault();
                    if (DateTime.Compare((DateTime)_custMst.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _custMst.CUST_NAME, _custMst.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _custMst);
                    _custMst.MOD_DATE = DateTime.Now;
                    _custMst.MOD_USER_NAME = _operator.UserName;
                    db.Entry(_custMst).State = EntityState.Modified;
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

        // GET: CustMst/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CUST_MST cUST_MST = db.CUST_MST.Find(id);
                if (cUST_MST == null)
                {
                    return HttpNotFound();
                }
                WARD_MST _ward = db.WARD_MST.Where(u => u.WARD_ID == cUST_MST.DEFAULT_WARD_ID).FirstOrDefault();
                ViewBag.DEFAULT_DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (_ward != null)
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _ward.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                CustMstForm _form = new CustMstForm();
                ComplementUtil.complement(cUST_MST, _form);
                if (_form.DEFAULT_WARD_ID != null)
                {
                    _form.DEFAULT_DISTRICT_ID = cUST_MST.WARD_MST.DISTRICT_ID;
                }
                return View(_form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: CustMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(CustMstForm form)
        {
            try
            {
                CUST_MST _CustMst = db.CUST_MST.Find(form.CUST_ID);
                if (DateTime.Compare((DateTime)_CustMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _CustMst.CUST_NAME, _CustMst.MOD_USER_NAME));
                    return View(form);
                }
                _CustMst.MOD_DATE = DateTime.Now;
                _CustMst.MOD_USER_NAME = _operator.UserName;
                _CustMst.DELETE_FLAG = true;
                db.Entry(_CustMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.DeleteSuccess;
                ViewBag.DEFAULT_DISTRICT_ID = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (form.DEFAULT_DISTRICT_ID != null)
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DEFAULT_DISTRICT_ID), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.DEFAULT_WARD_ID = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                ComplementUtil.complement(_CustMst, form);

                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public JsonResult GetCustomer(string phone)
        {
            try
            {
                CUST_MST _cust = db.CUST_MST.Where(u => u.PHONE == phone).Where(u => u.DELETE_FLAG == false).FirstOrDefault();
                CustMstForm _form = new CustMstForm();
                if (_cust != null)
                {
                    ComplementUtil.complement(_cust, _form);
                    _form.DEFAULT_DISTRICT_ID = _cust.WARD_MST.DISTRICT_ID;
                }
                return Json(_form, JsonRequestBehavior.AllowGet);
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
