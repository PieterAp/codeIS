using somiodApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.XPath;
using somiodApp.Utils;
using Microsoft.SqlServer.Server;

namespace somiodApp.Controllers
{
    [RoutePrefix("api/somiod")]
    public class somiodController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["somiod.Properties.Settings.ConnStr"].ConnectionString;
        Error errorMessage;
        string applicationXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Application\\application.xsd";       
        string moduleXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Module\\module.xsd";
        string subscriptionXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Subscription\\subscription.xsd";
        string dataXSDPath = AppDomain.CurrentDomain.BaseDirectory + "\\Utils\\XMLandXSD\\Data\\data.xsd";


        #region Subscription/Data
        //POST api/somiod/<applicationName>/<moduleName>
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PostSubscriptionData(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {
            if (xmlFromBody == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Body content is not according to XML syntax";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            if (!DB_utils.existsApplication(applicationName))
            {
                errorMessage = new Error();
                errorMessage.message = "Could not find application with name " + applicationName;
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            if (!DB_utils.existsModuleInApplication(applicationName,moduleName))
            {
                errorMessage = new Error();
                errorMessage.message = "Could not find module with name " + moduleName;
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            try
            {
                String type = xmlFromBody.Name.LocalName;
                if (type == "data")
                    return PostData(applicationName, moduleName, xmlFromBody);
                else if (type == "subscription")
                    return PostSubscription(applicationName, moduleName, xmlFromBody);

                return InternalServerError();
            }
            catch(Exception)
            {
                return InternalServerError();
            }
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
                command.CommandText = "SELECT * FROM Applications WHERE is_deleted = 0";
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
            //XML syntax check
            if (xmlFromBody == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Body content is not according to XML syntax";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            //validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, applicationXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new Error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;
            
            //check whether application in body already exists in db
            if (DB_utils.existsApplication(name))
            {
                errorMessage = new Error();
                errorMessage.message = "An application with such name already exists!";
                return Content(HttpStatusCode.Conflict, errorMessage, Configuration.Formatters.XmlFormatter);
            }

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
                return Content(HttpStatusCode.Created, DB_utils.findApplication(name), Configuration.Formatters.XmlFormatter);

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
        //Body(xml): application
        [Route("{applicationName}")]
        public IHttpActionResult PutApplication(string applicationName, [FromBody] XElement xmlFromBody)
        {
            //check wether application to be updated exists
            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
                return NotFound();

            //XML syntax check
            if (xmlFromBody == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Body content is not according to XML syntax";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            //Validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, applicationXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new Error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            //check whether new application name already exists
            if (DB_utils.existsApplication(name))
            {
                errorMessage = new Error();
                errorMessage.message = "An application with such name already exists!";
                return Content(HttpStatusCode.Conflict, errorMessage, Configuration.Formatters.XmlFormatter);
            }

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

        //DELETE api/somiod/<applicationName>
        //~soft delete~
        [Route("{applicationName}")]
        public IHttpActionResult DeleteApplication(string applicationName)
        {
            DB_utils.findApplication(applicationName);
            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
                return NotFound();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Applications SET is_deleted=1, deletion_dt=@currDate WHERE id = @id";
                command.Parameters.AddWithValue("@id", foundApplication.Id);
                command.Parameters.AddWithValue("@currDate", DateTime.Now);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

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
        public IHttpActionResult GetAllModulesFromApplication(string applicationName)
        {
            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
                return NotFound();

            Modules modules = new Modules();
            Module module;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Modules WHERE applicationID = @applicationId AND is_deleted = 0";
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
                return InternalServerError();
            }
            finally
            {
                conn.Close();
            }
        }

        //POST api/somiod/<applicationName>
        //Body(xml): module
        [Route("{applicationName}")]
        public IHttpActionResult PostModule(string applicationName, [FromBody] XElement xmlFromBody)
        {
            Application foundApplication = DB_utils.findApplication(applicationName);
            if (foundApplication == null)
            {
                errorMessage = new Error();
                errorMessage.message = "application provided in URL was not found, a module can only be created on top of an existing application";
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            if (xmlFromBody == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Body content is not according to XML syntax";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            //Validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, moduleXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new Error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            if (DB_utils.existsModuleInApplication(applicationName, name))
            {
                errorMessage = new Error();
                errorMessage.message = "A module with such name already exists for this application!";
                return Content(HttpStatusCode.Conflict, errorMessage, Configuration.Formatters.XmlFormatter);
            }
                

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "INSERT INTO modules (Name,applicationID) VALUES (@name,@applicationID)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@applicationID", foundApplication.Id);
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
                errorMessage = new Error();
                errorMessage.message = "Body content is not according to XML syntax";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            //Validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, moduleXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new Error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;

            if (!DB_utils.existsApplication(applicationName))
            {
                errorMessage = new Error();
                errorMessage.message = "Application provided in URL was not found";
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            if (DB_utils.existsModuleInApplication(applicationName, name))
            {
                errorMessage = new Error();
                errorMessage.message = "An module with such name already exists for this application";
                return Content(HttpStatusCode.Conflict, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            Module foundModule = DB_utils.findModule(applicationName, moduleName);
            if (foundModule == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Could not find module with name " + moduleName;
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            SqlConnection conn = null;

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

        //DELETE api/somiod/<applicationName>/<moduleName>
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult DeleteModule(string applicationName, string moduleName)
        {
            if (!DB_utils.existsApplication(applicationName))
            {
                errorMessage = new Error();
                errorMessage.message = "Could not find application with name " + applicationName;
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            Module foundModule = DB_utils.findModule(applicationName, moduleName);
            if (foundModule == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Could not find module with name " + moduleName;
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Modules SET is_deleted=1, deletion_dt=@currDate WHERE id = @id";
                command.Parameters.AddWithValue("@id", foundModule.Id);
                command.Parameters.AddWithValue("@currDate", DateTime.Now);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

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


        #region Subscriptions             
        //Redirect function from parent PostSubscriptionData()
        public IHttpActionResult PostSubscription(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {
            //validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, subscriptionXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new Error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String name = xmlFromBody.XPathSelectElement("/name").Value;
            if (DB_utils.existsSubscriptionInModule(moduleName, name))
            {
                errorMessage = new Error();
                errorMessage.message = "An Subscription with such name already exists for this module!";
                return Content(HttpStatusCode.Conflict, errorMessage, Configuration.Formatters.XmlFormatter);
            }
                

            String eventType = xmlFromBody.XPathSelectElement("/eventType").Value;
            String endPoint = xmlFromBody.XPathSelectElement("/endpoint").Value;

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

        //DELETE api/somiod/<applicationName>/<moduleName>/<subscriptionName>
        [Route("{applicationName}/{moduleName}/{subscriptionName}")]
        public IHttpActionResult PutSubscription(string applicationName, string moduleName, string subscriptionName, [FromBody] XElement xmlFromBody)
        {
            //validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, subscriptionXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new Error();
                errorMessage.message = handler.ValidationMessage;
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            if (xmlFromBody == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Body content is not according to XML syntax";
                return Content(HttpStatusCode.BadRequest, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            String res_type = xmlFromBody.XPathSelectElement("/res_type").Value;
            String name = xmlFromBody.XPathSelectElement("/name").Value;

            if (DB_utils.existsSubscriptionInModule(moduleName, name))
            {
                errorMessage = new Error();
                errorMessage.message = "An subscription with such name already exists for this module!";
                return Content(HttpStatusCode.Conflict, errorMessage, Configuration.Formatters.XmlFormatter);
            }

            Subscription foundSubscription = DB_utils.findSubscription(applicationName, moduleName, subscriptionName);
            if (foundSubscription == null)
            {
                errorMessage = new Error();
                errorMessage.message = "Could not find subscription with name " + subscriptionName;
                return Content(HttpStatusCode.NotFound, errorMessage, Configuration.Formatters.XmlFormatter);
            }

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


        #region Data
        //Redirect function from parent PostSubscriptionData()
        public IHttpActionResult PostData(string applicationName, string moduleName, [FromBody] XElement xmlFromBody)
        {
            //validate body contents using XSD
            XML_handler handler = new XML_handler(xmlFromBody, dataXSDPath);
            if (!handler.ValidateXML())
            {
                errorMessage = new Error();
                errorMessage.message = handler.ValidationMessage;
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
                if (subscriptions!=null)
                {
                    foreach (Subscription subscription in subscriptions)
                    {
                        MessageBroker_utils.connectPublish(subscription.endpointType, subscription.endpoint, moduleName, content);
                    }
                }
               

                return Content(HttpStatusCode.Created, content, Configuration.Formatters.XmlFormatter);
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

    }
}
