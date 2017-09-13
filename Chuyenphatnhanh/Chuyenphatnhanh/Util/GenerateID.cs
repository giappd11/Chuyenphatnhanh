using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Chuyenphatnhanh.Models;
using System.Data.Entity.Core.Objects;
namespace Chuyenphatnhanh.Util
{
    public static class GenerateID
    {
        public static string GennerateID(DBConnection db, string sequence, string prefix)
        {
            string _nextId ;
            string _id = prefix;
            ObjectParameter _param = new ObjectParameter("nextSeq", 0);
            db.GetNextSequence(sequence, _param);
            _nextId =   _param.Value.ToString();
            _id += addDateToID(_nextId.ToString());
            return _id;
        }
        public static string addDateToID(string id)
        {
            string date = DateTime.Now.ToShortDateString();
            date = date.Replace("-", "");
            date = date.Replace(" ", "");
            date = date.Replace("/", "");
            date = date.Replace(":", "");
            string idAddZero = "0000000000".Substring(0, 10 - id.Length) + id;
            return date + idAddZero;
        }
    }
}