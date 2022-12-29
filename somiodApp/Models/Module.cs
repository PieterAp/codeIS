using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiodApp.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime creation_dt { get; set; }
        public int applicationID { get; set; }
    }
}