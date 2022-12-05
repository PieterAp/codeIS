using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiod.Models
{
    public class Resource
    {
        public string res_type { get; set; }
        public string name { get; set; }
        public string endpoint { get; set; }
        public string content { get; set; }
    }
}