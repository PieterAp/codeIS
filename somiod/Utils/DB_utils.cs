using somiod.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;

namespace somiod.Utils
{
    public class DB_utils
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["somiod.Properties.Settings.ConnStr"].ConnectionString;

        public static int getApplicationId(string applicationName)
        {
            int applicationId = -1;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT Id FROM Applications WHERE name like @applicationName";
                command.Parameters.AddWithValue("@applicationName", applicationName);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return -1;
                }
                while (reader.Read())
                {
                    applicationId = (int)reader["Id"];
                }

                reader.Close();
                conn.Close();

                return applicationId;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                conn.Close();
            }

        }


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



        public static Module findModule(string moduleName)
        {
            Module module = new Module();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Modules WHERE name like @moduleName";
                command.Parameters.AddWithValue("@moduleName", moduleName);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }
                while (reader.Read())
                {
                    module.Id = (int)reader["Id"];
                    module.name = (string)reader["name"];
                    module.creation_dt = (DateTime)reader["creation_dt"];
                }

                reader.Close();
                conn.Close();

                return module;
            }
            catch (Exception)
            {
                return new Module();
            }
            finally
            {
                conn.Close();
            }
        }

        
        public static Boolean existsModuleInApplication(string applicationName, string name)
        {
            int applicationID = 0;
            Boolean hasFoundModule = false;
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
                {
                    while (reader.Read())
                    {
                        applicationID = (int)reader["Id"];
                    }

                    reader.Close();

                    command.CommandText = "SELECT * FROM Modules WHERE name like @name AND applicationId like @applicationId";
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@applicationId", applicationID);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = conn;

                    SqlDataReader readerModule = command.ExecuteReader();
                    if (readerModule.HasRows)
                        hasFoundModule = true;
                    else
                        hasFoundModule = false;

                    readerModule.Close();
                }

                conn.Close();

                return hasFoundModule;
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