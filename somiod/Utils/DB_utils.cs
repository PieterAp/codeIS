using somiod.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web.Http;

namespace somiod.Utils
{
    public class DB_utils
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["somiod.Properties.Settings.ConnStr"].ConnectionString;

        public static ResourceAux checkBodyElems (XElement xmlFromBody, RequiredFields requiredFields)
        {
            ResourceAux resourceAux = new ResourceAux();
            if (requiredFields.name)
            {
                if (!xmlFromBody.XPathSelectElement("/name").HasElements)
                {
                    resourceAux.errortype = HttpStatusCode.BadRequest;
                    resourceAux.errorMessage = "Missing required 'name' element in body!";
                    
                    return resourceAux;
                }
                resourceAux.resourceFilledFields.name = xmlFromBody.XPathSelectElement("/res_type").Value;
            }

            return resourceAux;
        }

        /*
        public static ResourceAux checkBodyResType (XElement xmlFromBody)
        {
            ResourceAux resourceAux = new ResourceAux();

            if (xmlFromBody.XPathSelectElement("/res_type") == null)
                return Content(HttpStatusCode.BadRequest, "Missing required 'res_type' element in body!", Configuration.Formatters.XmlFormatter);
            
            String res_type = xmlFromBody.XPathSelectElement("/res_type").Value;


            return resourceAux.res_type = res_type;
        }
        */

        public static Application findApplication(string applicationName)
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

        public static Boolean existsApplication(string applicationName)
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

        public static Boolean hasModules(string applicationName)
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
    }
}