using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using Chuyenphatnhanh.Util;
using System.ComponentModel.DataAnnotations;
using Resource = Chuyenphatnhanh.Content.Texts;
namespace Chuyenphatnhanh.Controllers
{
    public class HomeController : BaseController
    { 

        public ActionResult Index()
        {
            try
            {
                List<BILL_HDR_TBL> _BillHdr;
                if (Contant.ROLE_ADMIN.Equals(_operator.RoleName)) { 
                    _BillHdr = db.BILL_HDR_TBL.Where(u => u.DELETE_FLAG == false && u.BRANCH_ID_CURRENT == _operator.BranchID).ToList();
                }else
                {
                    _BillHdr = db.BILL_HDR_TBL.Where(u => u.DELETE_FLAG == false && u.UID_CURRENT == _operator.UserId).ToList();
                }
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
