using somiod.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.XPath;
using somiod.Utils;

namespace somiod.Controllers
{
    [RoutePrefix("api/somiod")]
    public class somiodController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["somiod.Properties.Settings.ConnStr"].ConnectionString;

        #region Applications
        //GET api/somiod
        [Route("")]
        public IHttpActionResult GetAllApplications()
        {
            List<Application> applications = new List<Application>();
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
        //Body(xml): res_type, name
        [Route("")]
        public IHttpActionResult PostApplication([FromBody] XElement xmlFromBody)
        {
            if (xmlFromBody.XPathSelectElement("/res_type") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'res_type' element in body!", Configuration.Formatters.XmlFormatter);

            String res_type = xmlFromBody.XPathSelectElement("/res_type").Value;

            if (xmlFromBody.XPathSelectElement("/name") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'name' element in body!", Configuration.Formatters.XmlFormatter);

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            if (res_type != "application")
                return Content(HttpStatusCode.BadRequest, "res_type element not valid, can only be 'application' for this route", Configuration.Formatters.XmlFormatter);
           
            if (DB_utils.existsApplication(name))
                return Content(HttpStatusCode.Conflict, "An application with such name already exists!", Configuration.Formatters.XmlFormatter);

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
        //Body(xml): res_type, name
        [Route("{applicationName}")]
        public IHttpActionResult PutApplication(string applicationName, [FromBody] XElement xmlFromBody)
        {

            if (xmlFromBody.XPathSelectElement("/res_type") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'res_type' element in body!", Configuration.Formatters.XmlFormatter);

            String res_type = xmlFromBody.XPathSelectElement("/res_type").Value;

            if (res_type != "application")
                return BadRequest("Resource type not valid, only the 'application' resource type is valid for this route");

            if (xmlFromBody.XPathSelectElement("/name") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'name' element in body!", Configuration.Formatters.XmlFormatter);

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            if (res_type != "application")
                return Content(HttpStatusCode.BadRequest, "res_type element not valid, can only be 'application' for this route", Configuration.Formatters.XmlFormatter);

            if (String.IsNullOrEmpty(name))
                return Content(HttpStatusCode.BadRequest, "Missing required 'name' element in body!", Configuration.Formatters.XmlFormatter);

            SqlConnection conn = null;

            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)

                return Content(HttpStatusCode.NotFound, "Could not find application with name " + applicationName, Configuration.Formatters.XmlFormatter);

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

        //soft delete
        //DELETE api/somiod/<applicationName>
        [Route("{applicationName}")]
        public IHttpActionResult DeleteApplication(string applicationName)
        {
            DB_utils.findApplication(applicationName);
            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
                return Content(HttpStatusCode.NotFound, "Could not find module with name " + applicationName, Configuration.Formatters.XmlFormatter);

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

                return Content(HttpStatusCode.OK, "", Configuration.Formatters.XmlFormatter);
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
        public IHttpActionResult GetAllModulesFromApplication(string applicationName)
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

                return Content(HttpStatusCode.OK, modules, Configuration.Formatters.XmlFormatter);

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
        public IHttpActionResult PostModule(string applicationName, [FromBody] XElement xmlFromBody)
        {

            if (xmlFromBody.XPathSelectElement("/res_type") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'res_type' element in body!", Configuration.Formatters.XmlFormatter);

            String res_type = xmlFromBody.XPathSelectElement("/res_type").Value;

            if (res_type != "module")
                return BadRequest("Resource type not valid, only the 'module' resource type is valid for this route");

            if (xmlFromBody.XPathSelectElement("/name") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'name' element in body!", Configuration.Formatters.XmlFormatter);

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            if (DB_utils.existsModuleInApplication(applicationName, name))
                return Content(HttpStatusCode.Conflict, "An module with such name already exists for this application!", Configuration.Formatters.XmlFormatter);

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "INSERT INTO modules (Name,applicationID) VALUES (@name,@applicationID)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@applicationID", DB_utils.getApplicationId(applicationName));
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                return Content(HttpStatusCode.OK, DB_utils.findModule(name), Configuration.Formatters.XmlFormatter);
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

        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PutModule(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {

            if (xmlFromBody.XPathSelectElement("/res_type") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'res_type' element in body!", Configuration.Formatters.XmlFormatter);

            String res_type = xmlFromBody.XPathSelectElement("/res_type").Value;

            if (res_type != "module")
                return BadRequest("Resource type not valid, only the 'module' resource type is valid for this route");

            if (xmlFromBody.XPathSelectElement("/name") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'name' element in body!", Configuration.Formatters.XmlFormatter);

            String name = xmlFromBody.XPathSelectElement("/name").Value;          

            SqlConnection conn = null;

            Module foundModule = DB_utils.findModule(moduleName);
            if (foundModule == null)
                return Content(HttpStatusCode.NotFound, "Could not find module with name " + moduleName, Configuration.Formatters.XmlFormatter);

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "UPDATE Modules SET name = @name WHERE id = @id";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", foundModule.Id);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

                conn.Close();

                return Content(HttpStatusCode.OK, DB_utils.findModule(name), Configuration.Formatters.XmlFormatter);
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
        //DELETE api/somiod/<applicationName>/<moduleName>
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult DeleteModule(string moduleName)
        {
            DB_utils.findModule(moduleName);
            Module foundModule = DB_utils.findModule(moduleName);
            if (foundModule == null)
                return Content(HttpStatusCode.NotFound, "Could not find module with name " + moduleName, Configuration.Formatters.XmlFormatter);

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "DELETE FROM Modules WHERE id = @id";
                command.Parameters.AddWithValue("@id", foundModule.Id);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();
              
                conn.Close();

                return Content(HttpStatusCode.OK, "", Configuration.Formatters.XmlFormatter);
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

        #region Subscriptions

        #endregion

    }
}
