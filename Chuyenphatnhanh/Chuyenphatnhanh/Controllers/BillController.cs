using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using Chuyenphatnhanh.Util;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;


namespace Chuyenphatnhanh.Controllers
{
    public class BillController : BaseController
    {
        private DBConnection db = new DBConnection();

        // GET: Bill
        public ActionResult Index()
        {
            var bILL_HDR_TBL = db.BILL_HDR_TBL.Include(b => b.CUST_MST).Include(b => b.CUST_MST1).Include(b => b.WARD_MST).Include(b => b.WARD_MST1).Include(b => b.WARD_MST2);
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
            ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            BillHdrTblForm _from = new BillHdrTblForm();
            return View(_from);
        }

        // POST: Bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillHdrTblForm form)
        {
            ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            if (ModelState.IsValid)
            {
                BILL_HDR_TBL _Hdr = new BILL_HDR_TBL();
                ComplementUtil.complement(form, _Hdr);
                _Hdr.REG_DATE = DateTime.Now;
                _Hdr.MOD_DATE = DateTime.Now;
                _Hdr.MOD_USER_NAME = _operator.UserName;
                _Hdr.REG_USER_NAME = _operator.UserName;
                _Hdr.DELETE_FLAG = false;
                _Hdr.BILL_HDR_ID = GenerateID.GennerateID(db, Contant.BILLHDRTBL_SEQ, Contant.BILLHDRTBL_PREFIX);
                _Hdr.STATUS = Contant.NHAN_HANG;

                BRANCH_MST _branch = db.BRANCH_MST.Where(u => u.BRANCH_ID == _operator.BranchID).FirstOrDefault();
                _Hdr.WARD_ID_CURRENT = _branch.WARD_ID;
                _Hdr.ADDRESS_CURRENT = _branch.ADDRESS;
                _Hdr.BRANCH_ID_CURRENT = _operator.BranchID;

                string from = "";
                string to = "";
                WARD_MST _wardFrom = db.WARD_MST.Find(_Hdr.WARD_ID_FROM);
                WARD_MST _wardTo = db.WARD_MST.Find(_Hdr.WARD_ID_TO);
                from = (  _wardFrom.WARD_NAME + "," + _wardFrom.DISTRICT_MST.DISTRICT_NAME).Replace(" ", "+");
                to =  ( _wardTo.WARD_NAME + "," + _wardTo.DISTRICT_MST.DISTRICT_NAME).Replace(" ", "+");
                CalcDistance(from, to);

                db.BILL_HDR_TBL.Add(_Hdr);
                foreach (BillTblForm _form in form.Bill)
                {
                    BILL_TBL _billTbl = new BILL_TBL();
                    ComplementUtil.complement(_form, _billTbl);
                    _billTbl.REG_DATE = DateTime.Now;
                    _billTbl.MOD_DATE = DateTime.Now;
                    _billTbl.MOD_USER_NAME = _operator.UserName;
                    _billTbl.REG_USER_NAME = _operator.UserName;
                    _billTbl.BILL_HDR_ID = _Hdr.BILL_HDR_ID;
                    _billTbl.DELETE_FLAG = false;
                    _billTbl.SEND_DATE = DateTime.Now;
                    _billTbl.BILL_ID = GenerateID.GennerateID(db, Contant.BILLTBL_SEQ, Contant.BILLTBL_PREFIX);
                    


                    db.BILL_TBL.Add(_billTbl);
                }
                db.SaveChanges();
                ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateCustMstSuccess;
            }
            return View(form);
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
            ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            return View(bILL_HDR_TBL);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DELETE_FLAG,REG_USER_NAME,MOD_USER_NAME,REG_DATE,MOD_DATE,BILL_HDR_ID,CUST_FROM_ID,CUST_TO_ID,STATUS,WARD_ID_FROM,ADDRESS_FROM,WARD_ID_TO,ADDRESS_TO,WARD_ID_CURRENT,ADDRESS_CURRENT,BRANCH_ID_CURRENT")] BILL_HDR_TBL bILL_HDR_TBL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bILL_HDR_TBL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
            ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
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
        public double CalcDistance(String from, string to)
        {
            String ApiKey = WebConfigurationManager.AppSettings["googleMapKey"];
            string url = @"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" + from + "&destinations=" + to + "&key=" + ApiKey;

            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();

            StreamReader reader = new StreamReader(data);

            double Distance = -1;
            // json-formatted string from maps api
             
            string json = reader.ReadToEnd();

            JObject obj = JObject.Parse(json);
            JToken status = obj["status"];
            if ("OK".Equals(status.ToString()))
            {
               status = obj["rows"][0]["elements"][0]["status"];
                if ("OK".Equals(status.ToString()))
                {
                    JToken distance = obj["rows"][0]["elements"][0]["distance"]["value"];
                    try { 
                        Distance = Double.Parse(distance.ToString()) / 1000;
                    } catch( Exception e)
                    {
                        Distance = -1;
                    }

                }

            }
            response.Close();
            return Distance;
        }
    }
}
