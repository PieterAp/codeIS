using somiodApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.XPath;
using somiodApp.Utils;

namespace somiodApp.Controllers
{
    [RoutePrefix("api/somiod")]
    public class somiodController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["somiod.Properties.Settings.ConnStr"].ConnectionString;
        error errorMessage;
        string applicationXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Application\\application.xsd";
        string moduleXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Module\\module.xsd";

        #region Subscription/Data
        //POST api/somiod/<applicationName>/<moduleName>
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PostSubscriptionData(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {
            if (xmlFromBody == null)
            {
                errorMessage = new error();
                errorMessage.message = "Body content is not well formated";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String type = xmlFromBody.Name.LocalName;
            if (type == "Data")
                return PostData(applicationName, moduleName, xmlFromBody);
            else if (type == "Subscription")
                return PostSubscription(applicationName, moduleName, xmlFromBody);

            return InternalServerError();
        }

        #endregion

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

            String name = xmlFromBody.XPathSelectElement("/name").Value;
            /*
            if (resource.res_type != "application")
                return BadRequest("Resource type not valid, can only be 'application' for this route");
            

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
            */
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

                //return success but no results when the sql query hasn't returned any data
                if (!reader.HasRows)
                    return Ok();

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

            if (xmlFromBody == null)
            {
                errorMessage = new error();
                errorMessage.message = "Body content is not well formated";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            //Validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, moduleXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

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

                return Content(HttpStatusCode.OK, DB_utils.findModule(applicationName, name), Configuration.Formatters.XmlFormatter);
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

        //PUT api/somiod/<applicationName>/<moduleName>
        //Body : Resource object, fields of interest (res_type, name)
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PutModule(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {

            if (xmlFromBody == null)
            {
                errorMessage = new error();
                errorMessage.message = "Body content is not well formated";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            //Validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, moduleXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            SqlConnection conn = null;

            if (DB_utils.existsModuleInApplication(applicationName, name))
                return Content(HttpStatusCode.Conflict, "An module with such name already exists for this application!", Configuration.Formatters.XmlFormatter);

            Module foundModule = DB_utils.findModule(applicationName, moduleName);
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

                return Content(HttpStatusCode.OK, DB_utils.findModule(applicationName, name), Configuration.Formatters.XmlFormatter);
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
        public IHttpActionResult DeleteModule(string applicationName, string moduleName)
        {
            DB_utils.findModule(applicationName, moduleName);
            Module foundModule = DB_utils.findModule(applicationName, moduleName);
            if (foundModule == null)
                return Content(HttpStatusCode.NotFound, "Could not find module with name " + moduleName, Configuration.Formatters.XmlFormatter);

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "DELETE M FROM Modules M LEFTJOIN Applications A ON A.id=M.applicationID WHERE id = @id AND A.id = @applicationID";
                command.Parameters.AddWithValue("@id", foundModule.Id);
                command.Parameters.AddWithValue("@applicationID", DB_utils.getApplicationId(applicationName));
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
        public IHttpActionResult PostSubscription(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {

            if (xmlFromBody == null)
            {
                errorMessage = new error();
                errorMessage.message = "Body content is not well formated";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;
            String eventType = xmlFromBody.XPathSelectElement("/eventType").Value;
            String endPoint = xmlFromBody.XPathSelectElement("/endPoint").Value;

            string toBeSearched = "://";
            int ix = endPoint.IndexOf(toBeSearched);
            String ip;
            String prefix;
            String endPointPrefix;
            String rawEndPoint;

            if (ix != -1)
            {
                ip = endPoint.Substring(ix + toBeSearched.Length, endPoint.Length - ix - toBeSearched.Length);
                prefix = endPoint.Substring(0, ix);
                rawEndPoint = ip;
                endPointPrefix = prefix;
            }
            else
            {
                rawEndPoint = endPoint;
                endPointPrefix = "";
            }

            if (DB_utils.existsSubscriptionInModule(moduleName, name))
                return Content(HttpStatusCode.Conflict, "An Subscription with such name already exists for this module!", Configuration.Formatters.XmlFormatter);

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "INSERT INTO subscriptions (name,eventType,endPoint,moduleID,endpointType) VALUES (@name,@eventType,@endPoint,@moduleID,@endpointType)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@eventType", eventType);
                command.Parameters.AddWithValue("@endPoint", rawEndPoint);
                command.Parameters.AddWithValue("@endpointType", endPointPrefix);
                command.Parameters.AddWithValue("@moduleID", DB_utils.getModuleId(moduleName));
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                return Content(HttpStatusCode.OK, DB_utils.findSubscription(applicationName, moduleName, name), Configuration.Formatters.XmlFormatter);
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
        //DELETE api/somiod/<applicationName>/<moduleName>/<subscriptionName>
        [Route("{applicationName}/{moduleName}/{subscriptionName}")]
        public IHttpActionResult DeleteSubscription(string applicationName, string moduleName, string subscriptionName)
        {

            Subscription foundSubscription = DB_utils.findSubscription(applicationName, moduleName, subscriptionName);
            if (foundSubscription == null)
                return Content(HttpStatusCode.NotFound, "Could not find subscription with name " + subscriptionName, Configuration.Formatters.XmlFormatter);

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "DELETE S FROM Subscriptions S " +
                    "LEFT JOIN Modules M ON S.moduleID = M.Id " +
                    "LEFT JOIN Applications A ON A.Id = M.applicationID " +
                    "WHERE S.id = @id AND M.id = @moduleID AND A.id = @applicationID";

                command.Parameters.AddWithValue("@id", foundSubscription.Id);
                command.Parameters.AddWithValue("@moduleID", DB_utils.getModuleId(moduleName));
                command.Parameters.AddWithValue("@applicationID", DB_utils.getApplicationId(applicationName));
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

                conn.Close();

                return Content(HttpStatusCode.OK, "", Configuration.Formatters.XmlFormatter);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region Data       
        public IHttpActionResult PostData(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {
            if (xmlFromBody == null)
            {
                errorMessage = new error();
                errorMessage.message = "Body content is not well formated";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String content = xmlFromBody.XPathSelectElement("/content").Value;

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "INSERT INTO data (content,moduleID) VALUES (@content,@moduleID)";
                command.Parameters.AddWithValue("@content", content);
                command.Parameters.AddWithValue("@moduleID", DB_utils.getModuleId(moduleName));
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();

                //Get all endpoints
                List<Subscription> subscriptions = DB_utils.getSubscriptionsByModule(moduleName);
                foreach (Subscription subscription in subscriptions)
                {
                    MessageBroker_utils.connectPublish(subscription.endpointType, subscription.endpoint, moduleName, content);
                }

                return Content(HttpStatusCode.Created, content, Configuration.Formatters.XmlFormatter);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

    }
}
