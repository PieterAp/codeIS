using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace somiod.Models
{
    public class ResourceAux
    {
        public Resource resourceFilledFields { get; set; }
        //public Dictionary<string, string> resourceFieldsRequired { get; set; };

        /*
         var dictionary = new Dictionary<string, object>();
        dictionary.Add("product_id", 12);
        // etc.

        object productId = dictionary["product_id"];
         */

        public Resource resourcesReturnFields;
        /*
        public string res_type { get; set; }
        public string name { get; set; }
        public string endpoint { get; set; }
        public string content { get; set; }
        */
        //public Dictionary<HttpStatusCode, string> errorMessage { get; set; }
        public HttpStatusCode errortype { get; set; }
        public string errorMessage { get; set; }
    }
}