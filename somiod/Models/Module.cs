using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace somiodApp.Models
{
    [XmlRoot("modules")]
    public class Modules : List<Module> { }

    [XmlType("module")]
    public class Module
    {
        [XmlElement("id")]
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime creation_dt { get; set; }
        public int applicationID { get; set; }
    }
}