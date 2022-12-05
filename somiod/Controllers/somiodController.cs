using somiod.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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

                return Ok(applications);
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
        //Body : Resource object, fields of interest (res_type, name)
        [Route("")]
        public IHttpActionResult PostApplication([FromBody] Resource resource)
        {
            if (resource.res_type != "application")
                return BadRequest("Resource type not valid, can only be 'application' for this route");

            if (resource.name == null)
                return BadRequest("Missing 'name' value in body is required!");

            if (existsApplication(resource.name))
                return Content(HttpStatusCode.Conflict, "An application with such name already exists!");

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "INSERT INTO Applications (Name) VALUES (@name)";
                command.Parameters.AddWithValue("@name", resource.name);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

                conn.Close();

                //return Created<Application>(findApplication(resource.name))
                return Ok(findApplication(resource.name));
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
        //Body : Resource object, fields of interest (name)
        [Route("{applicationName}")]
        public IHttpActionResult PutApplication(string applicationName, [FromBody] Resource resource)
        {
            if (resource.res_type != "application")
                return BadRequest("Resource type not valid, can only be 'application' for this route");

            Application foundApplication = findApplication(applicationName);
            if (foundApplication == null)
                return NotFound();

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "UPDATE Applications SET name = @name WHERE id = @id";
                command.Parameters.AddWithValue("@name", resource.name);
                command.Parameters.AddWithValue("@id", foundApplication.Id);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.ExecuteNonQuery();

                //TODO: Verificar se o registo foi realmente alterado?

                conn.Close();

                return Ok(findApplication(resource.name));
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
        [Route("{applicationName}")]
        public IHttpActionResult DeleteApplication(string applicationName)
        {
            Application foundApplication = findApplication(applicationName);
            if (foundApplication == null)
                return NotFound();

            if (hasModules(applicationName))
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
                return NotFound();
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
            Application foundApplication = findApplication(applicationName);
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
        #endregion

        #region Subscriptions

        #endregion



        #region utils
        public Application findApplication(string applicationName)
        {
            Application application = new Application();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Applications WHERE name like @applicationName";
                command.Parameters.AddWithValue("@applicationName", applicationName);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }
                while (reader.Read())
                {
                    application.Id = (int)reader["Id"];
                    application.name = (string)reader["name"];
                    application.creation_dt = (DateTime)reader["creation_dt"];
                }

                reader.Close();
                conn.Close();

                return application;
            }
            catch (Exception)
            {
                return new Application();
            }
            finally
            {
                conn.Close();
            }
        }   

        private Boolean existsApplication(string applicationName)
        {
            Boolean hasFoundApplication = false;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Applications WHERE name like @applicationName";
                command.Parameters.AddWithValue("@applicationName", applicationName);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    hasFoundApplication = true;
                else
                    hasFoundApplication = false;

                reader.Close();
                conn.Close();

                return hasFoundApplication;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

        }

        public Boolean hasModules(string applicationName)
        {
            Application foundApplication = findApplication(applicationName);
            
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

                if (reader.HasRows)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}
