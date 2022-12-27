using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;


namespace somiod.Models
{
    [XmlRoot("Applications")]
    public class Applications : List<Application> { }

    [DataContract(Namespace = "")]
    public class Application
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string name { get; set; }
        
        [DataMember]
        public DateTime creation_dt { get; set; }
    }
}