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
    public class TariffMstController : BaseController
    {
        private DBConnection db = new DBConnection();

        // GET: TariffMst
        public ActionResult Index()
        {
            List<TARIFF_MST> _list = db.TARIFF_MST.Where(u => u.DELETE_FLAG == false).ToList();
            List<RoleMstForm> _TariffList = new List<RoleMstForm>();
            foreach (TARIFF_MST _Tariff in _list)
            {
                RoleMstForm _TariffMst = new RoleMstForm();
                ComplementUtil.complement(_Tariff, _TariffMst);
                _TariffList.Add(_TariffMst);
            }
            return View(_TariffList); 
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
            TariffMstForm _form = new TariffMstForm();
            ComplementUtil.complement(tARIFF_MST, _form);
            return View(_form);
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
        public ActionResult Create(TariffMstForm form)
        {
            TARIFF_MST _TariffMst = new TARIFF_MST();
            if (ModelState.IsValid)
            {
                ComplementUtil.complement(form, _TariffMst);
                _TariffMst.DELETE_FLAG = false;
                _TariffMst.MOD_DATE = DateTime.Now;
                _TariffMst.MOD_USER_NAME = _operator.UserName;
                _TariffMst.REG_DATE = DateTime.Now;
                _TariffMst.REG_USER_NAME = _operator.UserName;
                _TariffMst.TARIFF_ID = GenerateID.GennerateID(db, Contant.TARIFFMST_SEQ, Contant.TARIFFMST_PREFIX);
                db.TARIFF_MST.Add(_TariffMst);
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
            }
            return View(form);
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
            TariffMstForm _form = new TariffMstForm();
            ComplementUtil.complement(tARIFF_MST, _form);
            return View(_form);
        }

        // POST: TariffMst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TariffMstForm form)
        {
            if (ModelState.IsValid)
            {
                TARIFF_MST _TariffMst = db.TARIFF_MST.Find(form.TARIFF_ID);
                if (DateTime.Compare((DateTime)_TariffMst.MOD_DATE, form.MOD_DATE) != 0)
                {
                    ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _TariffMst.TARIFF_ID, _TariffMst.MOD_USER_NAME));
                    return View(form);
                }

                ComplementUtil.complement(form, _TariffMst);
                _TariffMst.MOD_DATE = DateTime.Now;
                _TariffMst.MOD_USER_NAME = _operator.UserName;
                db.Entry(_TariffMst).State = EntityState.Modified;
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;
            }
            return View(form);
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
            TariffMstForm _form = new TariffMstForm();
            ComplementUtil.complement(tARIFF_MST, _form);
            return View(_form);
        }

        // POST: TariffMst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(TariffMstForm form)
        {
            TARIFF_MST _TariffMst = db.TARIFF_MST.Find(form.TARIFF_ID);
            if (DateTime.Compare((DateTime)_TariffMst.MOD_DATE, form.MOD_DATE) != 0)
            {
                ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _TariffMst.TARIFF_ID, _TariffMst.MOD_USER_NAME));
                return View(form);
            }

            ComplementUtil.complement(form, _TariffMst);
            _TariffMst.MOD_DATE = DateTime.Now;
            _TariffMst.MOD_USER_NAME = _operator.UserName;
            _TariffMst.DELETE_FLAG = true;
            db.Entry(_TariffMst).State = EntityState.Modified;
            db.SaveChanges();
            ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.EditCustMstSuccess;

            ComplementUtil.complement(_TariffMst, form);
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
