using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiod.Models
{
    public class RequiredFields
    {
        public Dictionary<string, string> errorMessage { get; set; }
        public Boolean res_type { get; set; }
        public Boolean name { get; set; }
        public Boolean endpoint { get; set; }
        public Boolean content { get; set; }
    }
}