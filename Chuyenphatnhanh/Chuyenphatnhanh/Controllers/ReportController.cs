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
    public class ReportController : BaseController
    { 

        // GET: Report
        public ActionResult Index()
        {
              return View( );
        }
        // GET: Report
        public ActionResult Report(DateTime from, DateTime to)
        {
            var tongcong = db.BILL_HDR_TBL.Where(u => u.MOD_DATE > from && u.MOD_DATE < to).Count();
            var hanggiaothanhcong = db.BILL_HDR_TBL.Where(u => u.MOD_DATE > from && u.MOD_DATE < to && u.STATUS == Contant.GIAO_HANG_THANH_CONG).Count();
            var hangbihuy = db.BILL_HDR_TBL.Where(u => u.MOD_DATE > from && u.MOD_DATE < to && u.STATUS == "XX").Count();
            var data = db.BILL_LOG_TBL.Where(u => u.TRANSACTION_DATE > from & u.TRANSACTION_DATE < to);
            var tongtien = db.BILL_HDR_TBL.Where(u => u.MOD_DATE > from && u.MOD_DATE < to & !"HH,TH,CT,GT,TL".Contains(u.STATUS)).Sum(u => u.AMOUNT);

            ViewBag.tongdonhang = tongcong;
            ViewBag.hangthanhcong = hanggiaothanhcong;
            ViewBag.hangbihuy = hangbihuy;
            ViewBag.tongtien = tongtien;
            List<StaffSalary> _list = new List<StaffSalary>();
            bool check = false;
            foreach (BILL_LOG_TBL _bill in data)
            {
                if (Contant.DANG_GIAO_HANG.Equals(_bill.STATUS) || Contant.DANG_GIAO_HANG_TRA.Equals(_bill.STATUS))
                {
                    check = false;
                    foreach(StaffSalary _sal in _list)
                    {
                        if (_sal.u_name.Equals(_bill.UID_CURRENT))
                        {
                            _sal.amount += (decimal)_bill.AMOUNT;
                            check = true;
                            break;
                        }
                        
                    }
                    if (check == false)
                    {
                        StaffSalary s = new StaffSalary();
                        s.amount = (decimal)_bill.AMOUNT;
                        s.uid = _bill.UID_CURRENT;
                        s.u_name = db.USER_MST.Find(s.uid).USER_NAME;
                        _list.Add(s);
                    }
                }
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
    public class StaffSalary
    {
       public  Decimal amount { get; set; }
        public string uid { get; set; }
        public string u_name { get; set; }
    }
}
