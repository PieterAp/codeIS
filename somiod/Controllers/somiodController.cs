using Newtonsoft.Json.Linq;
using somiod.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using somiod.Utils;
using System.CodeDom;
using System.Net.Http.Formatting;

namespace somiod.Controllers
{
    [RoutePrefix("api/somiod")]
    public class somiodController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["somiod.Properties.Settings.ConnStr"].ConnectionString;
        error errorMessage;
        string applicationXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Application\\application.xsd";
        //module.xml and module.xsd have not been created 
        //string modullleXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Application\\module.xsd";


        #region Applications
        //GET api/somiod
        [Route("")]
        public IHttpActionResult GetAllApplications()
        {
            Applications applications = new Applications();
            Application application;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Applications";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();
                
                //return success but no results when the sql query hasn't returned any data
                if (!reader.HasRows)
                    return Ok();

                //populate the applications list with the result from the sql query
                while (reader.Read())
                {
                    application = new Application();
                    application.Id = (int)reader["Id"];
                    application.name = (string)reader["name"];
                    application.creation_dt = (DateTime)reader["creation_dt"];

                    applications.Add(application);
                }
                reader.Close();
                conn.Close();

                return Content(HttpStatusCode.OK, applications, Configuration.Formatters.XmlFormatter);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            finally
            {
                conn.Close();
            }
        }

        //POST api/somiod/
        //Body(xml): application
        [Route("")]
        public IHttpActionResult PostApplication([FromBody] XElement xmlFromBody)
        {
            if (xmlFromBody == null)
            {
                errorMessage = new error();
                errorMessage.message = "Body content is not well formated";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            //Validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, applicationXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }


            //Not sure whether we're going to need these, just gonna keep them for now
            /*
            if (xmlFromBody.XPathSelectElement("/res_type") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'res_type' element in body!", Configuration.Formatters.XmlFormatter);

            String res_type = xmlFromBody.XPathSelectElement("/res_type").Value;

            if (xmlFromBody.XPathSelectElement("/name") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'name' element in body!", Configuration.Formatters.XmlFormatter);

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            if (res_type != "application")
                return Content(HttpStatusCode.BadRequest, "res_type element not valid, can only be 'application' for this route", Configuration.Formatters.XmlFormatter);

            if (String.IsNullOrEmpty(name))
                return Content(HttpStatusCode.BadRequest, "Missing required 'name' element in body!", Configuration.Formatters.XmlFormatter);
            */

            if (DB_utils.existsApplication(xmlFromBody.XPathSelectElement("/name").Value))
            {
                errorMessage = new error();
                errorMessage.message = "An application with such name already exists!";
                return Content(HttpStatusCode.Conflict, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "INSERT INTO Applications (Name) VALUES (@name)";
                command.Parameters.AddWithValue("@name", name);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                return Content(HttpStatusCode.OK, DB_utils.findApplication(name), Configuration.Formatters.XmlFormatter);

            }
            catch (Exception)
            {
                return InternalServerError();
            }
            finally
            {
                conn.Close();
            }
        }

        //PUT api/somiod/<applicationName>
        //Header: applicationName
        //Body(xml): application
        [Route("{applicationName}")]
        public IHttpActionResult PutApplication(string applicationName, [FromBody] XElement xmlFromBody)
        {
            //Validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, applicationXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;
            /*
            if (resource.res_type != "application")
                return BadRequest("Resource type not valid, can only be 'application' for this route");
            */

            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
                return NotFound();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "UPDATE Applications SET name = @name WHERE id = @id";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", foundApplication.Id);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

                //TODO: Verificar se o registo foi realmente alterado?

                conn.Close();

                return Ok(DB_utils.findApplication(name));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            finally
            {
                conn.Close();
            }
        }

        //soft delete
        //DELETE api/somiod/<applicationName>
        [Route("{applicationName}")]
        public IHttpActionResult DeleteApplication(string applicationName)
        {
            DB_utils.findApplication(applicationName);
            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
                return NotFound();

            if (DB_utils.hasModules(applicationName))
                return Content(HttpStatusCode.Conflict, "Given application has modules related to it!");

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "DELETE FROM Applications WHERE id = @id";
                command.Parameters.AddWithValue("@id", foundApplication.Id);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

                //TODO: Verificar se o registo foi realmente alterado?
                //TODO: Cascade delete in case of having associated modules?

                conn.Close();

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion




        #region Modules
        //GET api/somiod/<applicationName>
        [Route("{applicationName}")]
        public IEnumerable<Module> GetAllModulesFromApplication(string applicationName)
        {
            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
                return null;

            List<Module> modules = new List<Module>();
            Module module;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Modules WHERE applicationID = @applicationId";
                command.Parameters.AddWithValue("@applicationId", foundApplication.Id);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    module = new Module();
                    module.Id = (int)reader["Id"];
                    module.name = (string)reader["name"];
                    module.creation_dt = (DateTime)reader["creation_dt"];
                    module.applicationID = (int)reader["applicationID"];

                    modules.Add(module);
                }
                reader.Close();
                conn.Close();

                return modules;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //POST api/somiod/<applicationName>
        //Body : Resource object, fields of interest (res_type, name)
        [Route("{applicationName}")]
        public IHttpActionResult PostModule(string applicationName, [FromBody] Resource resource)
        {
            if (resource.res_type != "module")
                return BadRequest("Resource type not valid, can only be 'module' for this route");

            if (resource.name == null)
                return BadRequest("Missing 'name' value in body is required!");

            //TODO: Can there exist more than 1 module with the same name

            return Ok(resource);
        }

        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PutModule(string applicationName, string moduleName, [FromBody] Resource resource)
        {
            return InternalServerError();
        }
        #endregion

        #region Subscriptions

        #endregion

    }
}
