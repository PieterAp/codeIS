using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;


namespace somiod.Models
{
    [XmlRoot("applications")]
    public class Applications : List<Application> { }

    [XmlType("application")]
    public class Application
    {
        [DataMember]
        [XmlElement("id")]
        public int Id { get; set; }

        [DataMember]
        public string name { get; set; }
        
        [DataMember]
        public DateTime creation_dt { get; set; }
    }
}