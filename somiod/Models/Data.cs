using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace somiod.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string content { get; set; }
        public DateTime creation_dt { get; set; }
        public int moduleID { get; set; }
    }
}