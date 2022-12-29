using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace somiod.Models
{
    [XmlType("error")]
    public class Error
    {
        public string message { get; set; }
    }
}