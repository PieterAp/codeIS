using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiod.Models
{
    public class Subscription
    {
        //public int Id { get; }
        public int Id { get; set; }
        public string name { get; set; }
        //public DateTime creation_dt { get; }
        public DateTime creation_dt { get; set; }
        public int moduleID { get; set; }
        public string eventType { get; set; }
        public string endpoint { get; set; }
    }
}