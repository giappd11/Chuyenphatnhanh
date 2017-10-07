using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chuyenphatnhanh.Models;
using System.Text;
using Chuyenphatnhanh.Util;
namespace Chuyenphatnhanh.Controllers
{
    public class ErrorController : Controller
    {
        DBConnection db = new DBConnection();

        public ActionResult Error(int statusCode, Exception exception)
        {
            Operator _operator = (Operator)Session[Contant.SESSIONLOGED];
            DateTime _date = DateTime.Now;
            COMMON_ERROR _error = new COMMON_ERROR();
            StringBuilder err = new StringBuilder();
            err.Append(exception.StackTrace);
            int _length = err.Length > 3999 ? 3999 : err.Length;
            _error.COMMON_ERROR_CODE = statusCode.ToString(); 
            _error.MESSAGE = err.ToString().Substring(0, _length - 1);
            _error.MOD_DATE = _date;
            _error.REG_DATE = _date;
            _error.REG_USER_NAME = _operator.UserName;
            _error.MOD_USER_NAME = _operator.UserName;
            _error.DELETE_FLAG = false;
            _error.COMMON_ERROR_ID = GenerateID.GennerateID(db, Contant.COMMONERROR_SEQ, Contant.COMMONERROR_PREFIX); ;
            db.COMMON_ERROR.Add(_error);
             
            db.SaveChanges();

            Response.StatusCode = statusCode;
            var error = new Models.Error
            {
                StatusCode = statusCode.ToString(),
                StatusDescription = HttpWorkerRequest.GetStatusDescription(statusCode),
                Message = exception.Message,
                DateTime = _date
            };
            return View(error);
        }

    }
}