using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace somiodApp.Models
{
    [XmlType("error")]
    public class Error
    {
        public string message { get; set; }
    }
}