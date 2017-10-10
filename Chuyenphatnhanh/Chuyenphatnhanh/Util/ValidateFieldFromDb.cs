using System.ComponentModel.DataAnnotations;
using Chuyenphatnhanh.Models;
using System.Globalization;
using Resource = Chuyenphatnhanh.Content.Texts;
using System;
namespace Chuyenphatnhanh.Util
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ValidateFieldFromDb : ValidationAttribute
    {
        public string comma { get; private set; } 
        public DBConnection db = new DBConnection();
        public ValidateFieldFromDb (string comma ) : base ("Please select an option")
        {
            if (!string.IsNullOrEmpty(comma))
            {
                this.comma = comma; 
            }
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string[] properties = comma.Split(',');
                var type = Type.GetType("Chuyenphatnhanh.Models." + properties[0]);
                if (type == null) { 
                    foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        type = a.GetType("Chuyenphatnhanh.Models." + properties[0]);
                        if (type != null) { 
                            break;
                        }
                    }
                } 
                string _sql = "select  * from " + properties[0] + " t1 where t1." + properties[1] + " = N'" + value +"'" ;
                var _data = db.Database.SqlQuery(type, _sql, new object[] { }) ;
                var _list = _data.ToListAsync();
                if (_list.Result.Count == 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(String.Format( Resource.RGlobal.Exists, value));
                }
            }
            return base.IsValid(value, validationContext);
        }
    }
}