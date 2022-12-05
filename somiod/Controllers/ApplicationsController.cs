using somiod.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

namespace somiod.Controllers
{
    //Allowed requests types: GET / POST / PUT / DELETE

    //[RoutePrefix("api/somiod")]
    public class ApplicationsController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["somiod.Properties.Settings.ConnStr"].ConnectionString;

        //GET api/somiod
        [Route("")]
        public IEnumerable<Application> GetAllApplications()
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
                return applications;
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

        //POST api/somiod/
        //Body Resource object
        [Route("")]
        public IHttpActionResult PostApplication([FromBody] Resource resource)
        {
            if (resource.res_type != "application")
                return BadRequest("Resource type not valid, can only be 'application' for this route");

            if (existsApplication(resource.name))
                return BadRequest("An application with such name already exists!");

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

                return Ok(findApplication(resource.name));
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



        private Application findApplication(string name)
        {
            Application application = new Application();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Applications WHERE name like @applicationName";
                command.Parameters.AddWithValue("@applicationName", name);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return application;
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

        private Boolean existsApplication(string name)
        {
            Boolean hasFoundApplication = false;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Applications WHERE name like @applicationName";
                command.Parameters.AddWithValue("@applicationName", name);
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
    }
}
