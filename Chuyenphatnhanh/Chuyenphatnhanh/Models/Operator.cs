using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class Operator
    {
        public string UserId { get; set; }
        public DateTime ManagerTime { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public string BranchID { get; set; }
    }
}