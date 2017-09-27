using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Models
{
    public class DistanceFromGoogle
    {
        public string destination_addresses { get; set; }
        public string origin_addresses { get; set; }
        public string status { get; set; }
        public List<elements> rows { get; set; }
    }
    public class elements
    {
        public ObjectValue distance { get; set; }
        public ObjectValue duration { get; set; }
        public string status { get; set; }
    }
    public class ObjectValue
    {
        public string text { get; set; }
        public string value { get; set; }
    }
}