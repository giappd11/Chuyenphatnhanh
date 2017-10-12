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
using Resource = Chuyenphatnhanh.Content.Texts;


namespace Chuyenphatnhanh.Controllers
{
    public class BillController : BaseController
    {
        // GET: Bill
        public ActionResult Index()
        {
            try
            { 
                List<BILL_HDR_TBL> _BillHdr = db.BILL_HDR_TBL.Where(u => u.DELETE_FLAG == false && u.BRANCH_ID_CURRENT == _operator.BranchID && u.STATUS != Contant.GIAO_HANG_THANH_CONG).ToList();
                List<BillHdrTblForm> _BillHdrList = new List<BillHdrTblForm>();
                BillHdrTblForm BillHdr;
                foreach (BILL_HDR_TBL _bill in _BillHdr)
                {
                    BillHdr = new BillHdrTblForm();
                    ComplementUtil.complement(_bill, BillHdr);
                    string _Address = null;
                    if (_bill.WARD_ID_FROM != null && _bill.DISTRICT_ID_FROM != null)
                    {
                        _Address = _bill.ADDRESS_FROM + ", " + _bill.WARD_MST_FROM.WARD_NAME + ", " + _bill.DISTRICT_MST_FROM.DISTRICT_NAME;
                        BillHdr.AddressFrom = _Address;
                    }
                    if (_bill.WARD_ID_TO != null && _bill.DISTRICT_ID_TO != null)
                    {
                        _Address = _bill.ADDRESS_TO + ", " + _bill.WARD_MST_TO.WARD_NAME + ", " + _bill.DISTRICT_MST_TO.DISTRICT_NAME;
                        BillHdr.AddressTo = _Address;
                    }
                    if (_bill.BRANCH_ID_CURRENT != null)
                    {
                        BRANCH_MST _branch = db.BRANCH_MST.Find(_bill.BRANCH_ID_CURRENT);
                        if (_branch != null) { 
                            _Address = _branch.ADDRESS + ", " + _branch.WARD_MST.WARD_NAME + ", " + _branch.DISTRICT_MST.DISTRICT_NAME;
                            BillHdr.AddressCurrent = _Address;
                        }
                    }
                    _BillHdrList.Add(BillHdr);
                }
                return View(_BillHdrList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // GET: Bill
        public ActionResult ListNH()
        {
            try
            {

                List<BILL_HDR_TBL> _BillHdr = db.BILL_HDR_TBL.Where(u => u.DELETE_FLAG == false && u.BRANCH_ID_CURRENT == _operator.BranchID && u.STATUS == Contant.NHAN_HANG ).ToList();
                List<BillHdrTblForm> _BillHdrList = new List<BillHdrTblForm>();
                BillHdrTblForm BillHdr;
                foreach (BILL_HDR_TBL _bill in _BillHdr)
                {
                    BillHdr = new BillHdrTblForm();
                    ComplementUtil.complement(_bill, BillHdr);
                    string _Address = null;
                    if (_bill.WARD_ID_FROM != null && _bill.DISTRICT_ID_FROM != null)
                    {
                        _Address = _bill.ADDRESS_FROM + ", " + _bill.WARD_MST_FROM.WARD_NAME + ", " + _bill.DISTRICT_MST_FROM.DISTRICT_NAME;
                        BillHdr.AddressFrom = _Address;
                    }
                    if (_bill.WARD_ID_TO != null && _bill.DISTRICT_ID_TO != null)
                    {
                        _Address = _bill.ADDRESS_TO + ", " + _bill.WARD_MST_TO.WARD_NAME + ", " + _bill.DISTRICT_MST_TO.DISTRICT_NAME;
                        BillHdr.AddressTo = _Address;
                    }
                    if (_bill.BRANCH_ID_CURRENT != null)
                    {
                        BRANCH_MST _branch = db.BRANCH_MST.Find(_bill.BRANCH_ID_CURRENT);
                        if (_branch != null)
                        {
                            _Address = _branch.ADDRESS + ", " + _branch.WARD_MST.WARD_NAME + ", " + _branch.DISTRICT_MST.DISTRICT_NAME;
                            BillHdr.AddressCurrent = _Address;
                        }
                    }
                    _BillHdrList.Add(BillHdr);
                }
                return View(_BillHdrList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // GET: Bill/Details/5
        public ActionResult Details(string id)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                BillHdrTblForm _from = new BillHdrTblForm();
                return View(_from);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: Bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillHdrTblForm form)
        {
            try
            {
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (form.DISTRICT_ID_FROM != null || !"".Equals(form.DISTRICT_ID_FROM))
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DISTRICT_ID_FROM), "WARD_ID", "WARD_NAME");
                }else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (form.DISTRICT_ID_TO != null || !"".Equals(form.DISTRICT_ID_TO))
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == form.DISTRICT_ID_TO), "WARD_ID", "WARD_NAME");
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                
                if (ModelState.IsValid)
                {
                    CUST_MST _CustMstFrom = new CUST_MST();
                    DateTime _date = DateTime.Now;
                    if ("".Equals(form.CUST_FROM_ID) || form.CUST_FROM_ID == null)
                    { 
                        _CustMstFrom.DELETE_FLAG = false;
                        _CustMstFrom.REG_DATE = _date;
                        _CustMstFrom.REG_USER_NAME = _operator.UserName;
                        _CustMstFrom.MOD_DATE = _date;
                        _CustMstFrom.MOD_USER_NAME = _operator.UserName;
                        _CustMstFrom.PHONE = form.Cust_From_Phone;
                        _CustMstFrom.CUST_NAME = form.Cust_From_Name;
                        _CustMstFrom.DEFAULT_ADDRESS = form.AddressFrom;
                        _CustMstFrom.DEFAULT_DISTRICT_ID = form.DISTRICT_ID_FROM;
                        _CustMstFrom.DEFAULT_WARD_ID = form.WARD_ID_FROM;
                        _CustMstFrom.CUST_ID = GenerateID.GennerateID(db, Contant.CUSTMST_SEQ, Contant.CUSTMST_PREFIX);
                        db.CUST_MST.Add(_CustMstFrom);
                    }
                    CUST_MST _CustMstTo = new CUST_MST();
                    if ("".Equals(form.CUST_TO_ID)  || form.CUST_TO_ID == null)
                    {
                        _CustMstTo.DELETE_FLAG = false;
                        _CustMstTo.REG_DATE = _date;
                        _CustMstTo.REG_USER_NAME = _operator.UserName;
                        _CustMstTo.MOD_DATE = _date;
                        _CustMstTo.MOD_USER_NAME = _operator.UserName;
                        _CustMstTo.PHONE = form.Cust_From_Phone;
                        _CustMstTo.CUST_NAME = form.Cust_From_Name;
                        _CustMstTo.DEFAULT_ADDRESS = form.AddressFrom;
                        _CustMstTo.DEFAULT_DISTRICT_ID = form.DISTRICT_ID_TO;
                        _CustMstTo.DEFAULT_WARD_ID = form.WARD_ID_TO;
                        _CustMstTo.CUST_ID = GenerateID.GennerateID(db, Contant.CUSTMST_SEQ, Contant.CUSTMST_PREFIX);
                        db.CUST_MST.Add(_CustMstTo); 
                    }
                    
                    string from = "";
                    string to = "";
                    WARD_MST _wardFrom = db.WARD_MST.Find(form.WARD_ID_FROM);
                    WARD_MST _wardTo = db.WARD_MST.Find(form.WARD_ID_TO);
                    DISTRICT_MST _districtFrom = db.DISTRICT_MST.Find(form.DISTRICT_ID_FROM);
                    DISTRICT_MST _districtTo = db.DISTRICT_MST.Find(form.DISTRICT_ID_TO);

                    from = (_wardFrom.WARD_NAME + "," + _districtFrom.DISTRICT_NAME).Replace(" ", "+");
                    to = (_wardTo.WARD_NAME + "," + _districtTo.DISTRICT_NAME).Replace(" ", "+");
                    decimal _Distance = CalcDistance(from, to);

                    string Location_Special = null;
                    if (_districtFrom.PROVINCE_CODE != null && _districtTo.PROVINCE_CODE != null)
                    {
                        if ((_districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN) && _districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM))
                         || (_districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN) && _districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM)))
                        {
                            Location_Special = Contant.SPECIAL_HN_HCM;
                        }
                        else if ((_districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM))
                         || (_districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM))
                         || (_districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN))
                         || (_districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN)))
                        {
                            Location_Special = Contant.SPECIAL_DN_HN_HCM;
                        }
                    }
                    if (_districtTo.DISTRICT_ID.Equals(_districtFrom.DISTRICT_ID))
                    {
                        Location_Special = Contant.SPECIAL_SAMELOCATION;
                    }
                    
                    TariffMstController _TariffController = new TariffMstController();


                    BILL_HDR_TBL _Hdr = new BILL_HDR_TBL();
                    ComplementUtil.complement(form, _Hdr);
                    _Hdr.REG_DATE = _date;
                    _Hdr.MOD_DATE = _date;
                    _Hdr.MOD_USER_NAME = _operator.UserName;
                    _Hdr.REG_USER_NAME = _operator.UserName;
                    _Hdr.DELETE_FLAG = false;
                    _Hdr.BILL_HDR_ID = GenerateID.GennerateID(db, Contant.BILLHDRTBL_SEQ, Contant.BILLHDRTBL_PREFIX);
                    _Hdr.STATUS = Contant.NHAN_HANG;
                    if (_Hdr.CUST_FROM_ID == null || "".Equals(_Hdr.CUST_FROM_ID))
                    {
                        _Hdr.CUST_FROM_ID = _CustMstFrom.CUST_ID;
                    } 
                    if (_Hdr.CUST_TO_ID == null || "".Equals(_Hdr.CUST_TO_ID))
                    {
                        _Hdr.CUST_TO_ID = _CustMstTo.CUST_ID;
                    }

                    BRANCH_MST _branch = db.BRANCH_MST.Where(u => u.BRANCH_ID == _operator.BranchID && u.DELETE_FLAG == false).FirstOrDefault();
                    if (_branch == null)
                    {
                        ViewData[Contant.MESSSAGEERROR] = Chuyenphatnhanh.Content.Texts.RGlobal.UserLoginnotinBranch;
                        return View(form);
                    }
                    _Hdr.BRANCH_ID_CURRENT = _operator.BranchID;

                    db.BILL_HDR_TBL.Add(_Hdr);
                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = new BILL_TBL();
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.REG_DATE = _date;
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;
                        _billTbl.REG_USER_NAME = _operator.UserName;
                        _billTbl.BILL_HDR_ID = _Hdr.BILL_HDR_ID;
                        _billTbl.DELETE_FLAG = false;
                        _billTbl.SEND_DATE = _date;
                        _billTbl.BILL_ID = GenerateID.GennerateID(db, Contant.BILLTBL_SEQ, Contant.BILLTBL_PREFIX);

                        decimal _amount = _TariffController.getTariff((decimal)_billTbl.WEIGHT, _Distance, Location_Special);
                        decimal _overWeightPrice = _TariffController.getTariffOverWeight((decimal)_billTbl.WEIGHT, _Distance, Location_Special);
                        int _count = 0;
                        if ((decimal)_billTbl.WEIGHT > 2000)
                        {
                            _count = (int)((decimal)_billTbl.WEIGHT - 2000) / 500;
                            _amount += (_count * _overWeightPrice);
                        }
                        _billTbl.AMOUNT = _amount;


                        db.BILL_TBL.Add(_billTbl);
                    }
                    WriteLog(_Hdr.BILL_HDR_ID);
                    db.SaveChanges();
                    form.BILL_HDR_ID = _Hdr.BILL_HDR_ID;
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.CreateSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Bill/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BILL_HDR_TBL bILL_HDR_TBL = db.BILL_HDR_TBL.Find(id);
                if (bILL_HDR_TBL == null)
                {
                    return HttpNotFound();
                }
                BillHdrTblForm form = new BillHdrTblForm();
                ComplementUtil.complement(bILL_HDR_TBL, form);

                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == bILL_HDR_TBL.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == bILL_HDR_TBL.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                form.Cust_From_Name = bILL_HDR_TBL.CUST_MST_FROM.CUST_NAME;
                form.Cust_To_Name = bILL_HDR_TBL.CUST_MST_TO.CUST_NAME;
                form.Cust_From_Phone = bILL_HDR_TBL.CUST_MST_FROM.PHONE;
                form.Cust_To_Phone = bILL_HDR_TBL.CUST_MST_TO.PHONE;
                form.statusString = CommonUtil.GetStatusDes(bILL_HDR_TBL.STATUS);
                List<BillTblForm> billTbl = new List<BillTblForm>();
                foreach (BILL_TBL bill in bILL_HDR_TBL.BILL_TBL)
                {
                    BillTblForm _bill = new BillTblForm();
                    ComplementUtil.complement(bill, _bill);
                    billTbl.Add(_bill);
                }
                form.Bill = billTbl;

                logger.Info("END EDIT BILL");

                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                     
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                     
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified; 
                    }
                    
                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(string id)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                BILL_HDR_TBL bILL_HDR_TBL = db.BILL_HDR_TBL.Find(id);
                db.BILL_HDR_TBL.Remove(bILL_HDR_TBL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ActionResult NewBill(int position)
        {
            try
            {
                BillHdrTblForm _form = new BillHdrTblForm();
                List<BillTblForm> _list = new List<BillTblForm>();
                for (int i = 0; i < position; i++)
                {
                    if (i == position - 1)
                    {
                        _list.Add(new BillTblForm());
                    }
                    else
                    {
                        _list.Add(null);
                    }
                }
                _form.Bill = _list;
                return View(_form);
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
        public decimal CalcDistance(String from, string to)
        {
            try
            {
                String ApiKey = WebConfigurationManager.AppSettings["googleMapKey"];
                string url = @"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" + from + "&destinations=" + to + "&key=" + ApiKey;

                WebRequest request = WebRequest.Create(url);

                WebResponse response = request.GetResponse();

                Stream data = response.GetResponseStream();

                StreamReader reader = new StreamReader(data);

                decimal Distance = -1;
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
                        Distance = decimal.Parse(distance.ToString()) / 1000;
                    }
                }
                response.Close();
                return Distance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void WriteLog(string billHdrId)
        {
            try
            {
                BILL_HDR_TBL _BillHdr = db.BILL_HDR_TBL.Find(billHdrId);
                BILL_LOG_TBL _BillLog = new BILL_LOG_TBL();
                ComplementUtil.complement(_BillHdr, _BillLog);
                DateTime _Date = DateTime.Now;
                _BillLog.REG_DATE = _Date;
                _BillLog.MOD_DATE = _Date;
                _BillLog.TRANSACTION_ID = _BillHdr.BILL_HDR_ID;
                _BillLog.TRANSACTION_DATE = _BillHdr.MOD_DATE;
                _BillLog.TRANSACTION_UID = _BillHdr.MOD_USER_NAME;
                _BillLog.BILL_LOG_ID = GenerateID.GennerateID(db, Contant.BILLLOGTBL_SEQ, Contant.BILLLOGTBL_PREFIX);
                db.BILL_LOG_TBL.Add(_BillLog);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FileStreamResult Report(String id)
        {
            try
            {
                BILL_HDR_TBL _bill = db.BILL_HDR_TBL.Find(id);
                List<BILL_TBL> _list = db.BILL_TBL.Where(u => u.BILL_HDR_ID == id).ToList();
                List<BillTblForm> _listForm = new List<BillTblForm>();
                foreach (BILL_TBL bill in _list)
                {
                    BillTblForm _form = new BillTblForm();
                    ComplementUtil.complement(bill, _form);
                    _listForm.Add(_form);
                }

                string _Address = null; 
                BillHdrTblForm _billForm = new BillHdrTblForm();
                ComplementUtil.complement(_bill, _billForm);
                _billForm.Cust_From_Name = _bill.CUST_MST_FROM.CUST_NAME;
                _billForm.Cust_To_Name = _bill.CUST_MST_TO.CUST_NAME;
                _Address = _bill.ADDRESS_FROM + ", " + _bill.WARD_MST_FROM.WARD_NAME + ", " + _bill.DISTRICT_MST_FROM.DISTRICT_NAME;
                _billForm.AddressFrom = _Address;
                _Address = _bill.ADDRESS_TO + ", " + _bill.WARD_MST_TO.WARD_NAME + ", " + _bill.DISTRICT_MST_TO.DISTRICT_NAME;
                _billForm.AddressTo = _Address;
                _billForm.Cust_From_Phone = _bill.CUST_MST_FROM.PHONE;
                _billForm.Cust_To_Phone = _bill.CUST_MST_TO.PHONE;
                var rpt = new PrintController().gennrateRecipt(_billForm, _listForm);

                MemoryStream workStream = new MemoryStream();
                using (FileStream file = new FileStream(AppPath.ApplicationPath + @"\Pdf\" + id + ".pdf", FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    workStream.Write(bytes, 0, (int)file.Length);
                    workStream.Position = 0;
                }

                return new FileStreamResult(workStream, "application/pdf");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public BILL_HDR_TBL ComplementBillUpdateStatus(BillHdrTblForm form, string NextStatus)
        {
            BILL_HDR_TBL _Bill = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
            _Bill.STATUS = NextStatus;
            return _Bill;
        }
        public BILL_HDR_TBL ComplementBillUpdate(BillHdrTblForm form)
        {
            BILL_HDR_TBL _Bill = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
            if (DateTime.Compare((DateTime)_Bill.MOD_DATE, form.MOD_DATE) != 0)
            {
                ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _Bill.BILL_HDR_ID, _Bill.MOD_USER_NAME));
            }
            _Bill.BRANCH_ID_CURRENT = form.BRANCH_ID_CURRENT;
            _Bill.BRANCH_ID_TEMP = form.BRANCH_ID_TEMP;
            return _Bill;
        }
        [HttpGet]
        public JsonResult CaculAmount(string districtfrom, string wardfrom, string districtto, string wardto, string weight)
        {
            try
            {
                string from = "";
                string to = "";
                string[] weights = weight.Split(',');
                WARD_MST _wardFrom = db.WARD_MST.Find(wardfrom);
                WARD_MST _wardTo = db.WARD_MST.Find(wardto);
                DISTRICT_MST _districtFrom = db.DISTRICT_MST.Find(districtfrom);
                DISTRICT_MST _districtTo = db.DISTRICT_MST.Find(districtto);

                if (_wardFrom == null || _wardTo == null || _districtFrom == null || _districtTo == null)
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
                from = (_wardFrom.WARD_NAME + "," + _districtFrom.DISTRICT_NAME).Replace(" ", "+");
                to = (_wardTo.WARD_NAME + "," + _districtTo.DISTRICT_NAME).Replace(" ", "+");
                decimal _Distance = CalcDistance(from, to);

                string Location_Special = null;
                if (_districtFrom.PROVINCE_CODE != null && _districtTo.PROVINCE_CODE != null)
                {
                    if ((_districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN) && _districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM))
                     || (_districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN) && _districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM)))
                    {
                        Location_Special = Contant.SPECIAL_HN_HCM;
                    }
                    else if ((_districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM))
                     || (_districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HCM))
                     || (_districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN))
                     || (_districtTo.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_DN) && _districtFrom.PROVINCE_CODE.Equals(Contant.PROVINCE_CODE_HN)))
                    {
                        Location_Special = Contant.SPECIAL_DN_HN_HCM;
                    }
                }
                if (_districtTo.DISTRICT_ID.Equals(_districtFrom.DISTRICT_ID))
                {
                    Location_Special = Contant.SPECIAL_SAMELOCATION;
                }
                decimal _TotalAmount = 0;
                TariffMstController _TariffController = new TariffMstController();
                foreach (string _Weight in weights)
                {
                    decimal _amount = _TariffController.getTariff(decimal.Parse(_Weight), _Distance, Location_Special);
                    decimal _overWeightPrice = _TariffController.getTariffOverWeight(decimal.Parse(_Weight), _Distance, Location_Special);
                   
                    int _count = 0;
                    if (decimal.Parse( _Weight) > 2000)
                    {
                        _count = (int)(decimal.Parse(_Weight) - 2000) / 500;
                        _amount += (_count * _overWeightPrice);
                    }
                    _TotalAmount += _amount;
                }
                return Json(_TotalAmount, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Search(string search_keyword)
        {
            try
            {

                List<BILL_HDR_TBL> _BillHdr = db.BILL_HDR_TBL.Where(u => u.DELETE_FLAG == false && u.BILL_HDR_ID.Contains(search_keyword)).ToList();
                List<BillHdrTblForm> _BillHdrList = new List<BillHdrTblForm>();
                BillHdrTblForm BillHdr;
                foreach (BILL_HDR_TBL _bill in _BillHdr)
                {
                    BillHdr = new BillHdrTblForm();
                    ComplementUtil.complement(_bill, BillHdr);
                    string _Address = null;
                    if (_bill.WARD_ID_FROM != null && _bill.DISTRICT_ID_FROM != null)
                    {
                        _Address = _bill.ADDRESS_FROM + ", " + _bill.WARD_MST_FROM.WARD_NAME + ", " + _bill.DISTRICT_MST_FROM.DISTRICT_NAME;
                        BillHdr.AddressFrom = _Address;
                    }
                    if (_bill.WARD_ID_TO != null && _bill.DISTRICT_ID_TO != null)
                    {
                        _Address = _bill.ADDRESS_TO + ", " + _bill.WARD_MST_TO.WARD_NAME + ", " + _bill.DISTRICT_MST_TO.DISTRICT_NAME;
                        BillHdr.AddressTo = _Address;
                    }
                    if (_bill.BRANCH_ID_CURRENT != null)
                    {
                        BRANCH_MST _branch = db.BRANCH_MST.Find(_bill.BRANCH_ID_CURRENT);
                        if (_branch != null)
                        {
                            _Address = _branch.ADDRESS + ", " + _branch.WARD_MST.WARD_NAME + ", " + _branch.DISTRICT_MST.DISTRICT_NAME;
                            BillHdr.AddressCurrent = _Address;
                        }
                    }
                    _BillHdrList.Add(BillHdr);
                }
                return View(_BillHdrList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        // GET: Bill/TranferBranch/5
        public ActionResult TranferBranch(string id)
        {
              
            return Edit(id);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TranferBranch(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.STATUS = Contant.DANG_CHUYEN_HANG;
                    _BillHdrTbl.UID_CURRENT = _operator.UserId;
                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // GET: Bill/Edit/5
        public ActionResult TranferBranchComplete(string id)
        {
            return Edit(id);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TranferBranchComplete(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.STATUS = Contant.CHUYEN_HANG_THANH_CONG;
                    _BillHdrTbl.BRANCH_ID_CURRENT = _BillHdrTbl.BRANCH_ID_TEMP;
                    _BillHdrTbl.BRANCH_ID_TEMP = null;
                    _BillHdrTbl.UID_CURRENT = null;
                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Bill/Edit/5
        public ActionResult Delivery(string id)
        {
            return Edit(id);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delivery(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                if (Contant.ROLE_TELLER.Equals(_operator.RoleName))
                {

                }
                else if (Contant.ROLE_ADMIN.Equals(_operator.RoleName))
                {

                }
                else if (Contant.ROLE_CARRIER.Equals(_operator.RoleName))
                {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    

                }
                else if (Contant.ROLE_SHIPPER.Equals(_operator.RoleName))
                {

                }
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.UID_CURRENT = _operator.UserId;
                    _BillHdrTbl.STATUS = Contant.DANG_GIAO_HANG;

                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // GET: Bill/Edit/5
        public ActionResult DeliveryComplete(string id)
        {
            return Edit(id);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryComplete(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.BRANCH_ID_CURRENT = null;
                    _BillHdrTbl.UID_CURRENT = null;
                    _BillHdrTbl.STATUS = Contant.GIAO_HANG_THANH_CONG;

                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Bill/Edit/5
        public ActionResult Cancle(string id)
        {
            return Edit(id);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancle(BillHdrTblForm form)
        {
            try
            {
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                ViewData["UserName"] = new SelectList(db.USER_MST, "USER_ID", "NAME");
                if (ModelState.IsValid)
                { 
                    DateTime _date = DateTime.Now;
                    BILL_HDR_TBL _Cancel = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    _Cancel.MOD_DATE = _date;
                    _Cancel.MOD_USER_NAME = _operator.UserName;
                    _Cancel.STATUS = "XX";
                    _Cancel.DELETE_FLAG = true;

                    db.Entry(_Cancel).State = EntityState.Modified;


                    WARD_MST _wardFrom = db.WARD_MST.Find(form.WARD_ID_FROM);
                    WARD_MST _wardTo = db.WARD_MST.Find(form.WARD_ID_TO);
                    DISTRICT_MST _districtFrom = db.DISTRICT_MST.Find(form.DISTRICT_ID_FROM);
                    DISTRICT_MST _districtTo = db.DISTRICT_MST.Find(form.DISTRICT_ID_TO); 

                    BILL_HDR_TBL _Hdr = new BILL_HDR_TBL();
                    ComplementUtil.complement(form, _Hdr);
                    _Hdr.REG_DATE = _date;
                    _Hdr.MOD_DATE = _date;
                    _Hdr.MOD_USER_NAME = _operator.UserName;
                    _Hdr.REG_USER_NAME = _operator.UserName;
                    _Hdr.DELETE_FLAG = false;
                    _Hdr.AMOUNT = 0;
                    _Hdr.BILL_HDR_ID = GenerateID.GennerateID(db, Contant.BILLHDRTBL_SEQ, Contant.BILLHDRTBL_PREFIX);
                    _Hdr.STATUS = Contant.HUY_DON_HANG;
                    string fromAdd = _Hdr.ADDRESS_FROM;
                    string fromDis = _Hdr.DISTRICT_ID_FROM;
                    string fromwar = _Hdr.WARD_ID_FROM;
                    _Hdr.ADDRESS_FROM = _Hdr.ADDRESS_TO;
                    _Hdr.DISTRICT_ID_FROM = _Hdr.DISTRICT_ID_TO;
                    _Hdr.WARD_ID_FROM = _Hdr.WARD_ID_TO;
                    _Hdr.ADDRESS_TO = fromAdd;
                    _Hdr.DISTRICT_ID_TO = fromDis;
                    _Hdr.WARD_ID_TO = fromwar;
                    BRANCH_MST _branch = db.BRANCH_MST.Where(u => u.BRANCH_ID == _operator.BranchID && u.DELETE_FLAG == false).FirstOrDefault();
                    
                    _Hdr.BRANCH_ID_CURRENT = _operator.BranchID;

                    db.BILL_HDR_TBL.Add(_Hdr);
                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = new BILL_TBL();
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.REG_DATE = _date;
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;
                        _billTbl.REG_USER_NAME = _operator.UserName;
                        _billTbl.BILL_HDR_ID = _Hdr.BILL_HDR_ID;
                        _billTbl.DELETE_FLAG = false;
                        _billTbl.SEND_DATE = _date;
                        _billTbl.BILL_ID = GenerateID.GennerateID(db, Contant.BILLTBL_SEQ, Contant.BILLTBL_PREFIX); 
                        db.BILL_TBL.Add(_billTbl);
                    }
                    db.SaveChanges();
                    WriteLog(_Hdr.BILL_HDR_ID);
                    WriteLog(_Cancel.BILL_HDR_ID);
                    
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        

        // GET: Bill/Edit/5
        public ActionResult TranferCancel(string id)
        {
            return Edit(id);
        }


        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TranferCancel(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.BRANCH_ID_TEMP = form.BRANCH_ID_TEMP;
                    _BillHdrTbl.UID_CURRENT = _operator.UserId;
                    _BillHdrTbl.STATUS = Contant.DANG_CHUYEN_HANG_TRA;

                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // GET: Bill/Edit/5
        public ActionResult TranferCancelComplete(string id)
        {
            return Edit(id);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TranferCancelComplete(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.BRANCH_ID_CURRENT = _BillHdrTbl.BRANCH_ID_TEMP;
                    _BillHdrTbl.BRANCH_ID_TEMP = null;
                    _BillHdrTbl.UID_CURRENT = null;
                    _BillHdrTbl.STATUS = Contant.CHUYEN_HANG_TRA_THANH_CONG;

                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Bill/Edit/5
        public ActionResult DeliveryCancel(string id)
        {
            return Edit(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryCancel(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.UID_CURRENT = _operator.UserId;
                    _BillHdrTbl.STATUS = Contant.DANG_GIAO_HANG_TRA;

                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        // GET: Bill/Edit/5
        public ActionResult DeliveryCancelComplete(string id)
        {
            return Edit(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryCancelComplete(BillHdrTblForm form)
        {
            try
            {
                logger.Info("BEGIN EDIT BILL");
                DateTime _date = DateTime.Now;
                WARD_MST _wardFrom = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_FROM).FirstOrDefault();
                WARD_MST _wardTo = db.WARD_MST.Where(u => u.WARD_ID == form.WARD_ID_TO).FirstOrDefault();
                ViewBag.DISTRICT_ID_FROM = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.DISTRICT_ID_TO = new SelectList(db.DISTRICT_MST.OrderBy(u => u.DISTRICT_NAME), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.BRANCH_ID_TEMP = new SelectList(db.BRANCH_MST.Where(u => u.DELETE_FLAG == false).OrderBy(u => u.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME");
                if (_wardFrom != null)
                {
                    ViewBag.WARD_ID_FROM = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardFrom.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_FROM = _wardFrom.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_FROM = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }
                if (_wardTo != null)
                {
                    ViewBag.WARD_ID_TO = new SelectList(db.WARD_MST.Where(u => u.DISTRICT_ID == _wardTo.DISTRICT_ID), "WARD_ID", "WARD_NAME");
                    form.DISTRICT_ID_TO = _wardTo.DISTRICT_ID;
                }
                else
                {
                    ViewBag.WARD_ID_TO = new SelectList(string.Empty, "WARD_ID", "WARD_NAME");
                }

                if (ModelState.IsValid)
                {
                    BILL_HDR_TBL _BillHdrTbl = new BILL_HDR_TBL();
                    _BillHdrTbl = db.BILL_HDR_TBL.Find(form.BILL_HDR_ID);
                    if (DateTime.Compare((DateTime)_BillHdrTbl.MOD_DATE, form.MOD_DATE) != 0)
                    {
                        ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _BillHdrTbl.BILL_HDR_ID, _BillHdrTbl.MOD_USER_NAME));
                        return View(form);
                    }
                    ComplementUtil.complement(form, _BillHdrTbl);
                    _BillHdrTbl.MOD_DATE = _date;
                    _BillHdrTbl.MOD_USER_NAME = _operator.UserName;
                    _BillHdrTbl.STATUS = Contant.TRA_THANH_CONG;
                    _BillHdrTbl.UID_CURRENT = null;
                    _BillHdrTbl.BRANCH_ID_CURRENT = null;

                    db.Entry(_BillHdrTbl).State = EntityState.Modified;

                    foreach (BillTblForm _form in form.Bill)
                    {
                        BILL_TBL _billTbl = db.BILL_TBL.Find(_form.BILL_ID);

                        if (DateTime.Compare((DateTime)_billTbl.MOD_DATE, _form.MOD_DATE) != 0)
                        {
                            ModelState.AddModelError(Contant.MESSSAGEERROR, string.Format(Resource.RGlobal.CustMstModified, _billTbl.BILL_HDR_ID, _billTbl.MOD_USER_NAME));
                            return View(form);
                        }
                        ComplementUtil.complement(_form, _billTbl);
                        _billTbl.MOD_DATE = _date;
                        _billTbl.MOD_USER_NAME = _operator.UserName;


                        db.Entry(_billTbl).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    WriteLog(_BillHdrTbl.BILL_HDR_ID);
                    ViewData[Contant.MESSAGESUCCESS] = Chuyenphatnhanh.Content.Texts.RGlobal.ChangeSuccess;
                }
                logger.Info("END EDIT BILL");
                return View(form);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
