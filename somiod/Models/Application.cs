using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace somiod.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime creation_dt { get; set; }
    }
}