﻿using somiodApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;

namespace somiodApp.Utils
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

        public static int getModuleId(string moduleName)
        {
            int moduleId = -1;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT Id FROM Modules WHERE name like @moduleName";
                command.Parameters.AddWithValue("@moduleName", moduleName);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return -1;
                }
                while (reader.Read())
                {
                    moduleId = (int)reader["Id"];
                }

                reader.Close();
                conn.Close();

                return moduleId;
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
                command.CommandText = "SELECT * FROM Applications WHERE name like @applicationName AND is_deleted = 0";
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
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static Module findModule(string applicationName, string moduleName)
        {
            Module module = new Module();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Modules M JOIN Applications A ON A.ID = M.applicationID WHERE M.name = @moduleName AND A.ID = @applicationID AND A.is_deleted = 0 AND M.is_deleted = 0";
                command.Parameters.AddWithValue("@moduleName", moduleName);
                command.Parameters.AddWithValue("@applicationID", getApplicationId(applicationName));
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
                    module.applicationID = (int)reader["applicationID"];
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
        
        public static Subscription findSubscription(string applicationName, string moduleName, string subscriptionName)
        {
            Subscription subscription = new Subscription();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Subscriptions S " +
                    "LEFT JOIN MODULES M ON M.ID = S.moduleID " +
                    "LEFT JOIN APPLICATIONS A ON A.ID = M.applicationID " +
                    "WHERE S.name like @subscriptionName AND M.ID = @moduleID AND A.ID = @applicationID AND " +
                    " S.is_deleted = 0 AND A.is_deleted = 0 AND M.is_deleted = 0";

                command.Parameters.AddWithValue("@subscriptionName", subscriptionName);
                command.Parameters.AddWithValue("@moduleID", getModuleId(moduleName));
                command.Parameters.AddWithValue("@applicationID", getApplicationId(applicationName));
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }
                while (reader.Read())
                {
                    subscription.Id = (int)reader["Id"];
                    subscription.name = (string)reader["name"];
                    subscription.creation_dt = (DateTime)reader["creation_dt"];
                    subscription.endpoint = (string)reader["endpoint"];
                    subscription.endpointType = (string)reader["endpointType"];
                    subscription.eventType = (string)reader["eventType"];
                    subscription.moduleID = (int)reader["moduleID"];
                }

                reader.Close();
                conn.Close();

                return subscription;
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

        public static Boolean existsSubscriptionInModule(string moduleName, string name)
        {
            int moduleID = 0;
            Boolean hasFoundSubscription = false;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Modules WHERE name like @moduleName and is_deleted = 0";
                command.Parameters.AddWithValue("@moduleName", moduleName);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        moduleID = (int)reader["Id"];
                    }

                    reader.Close();

                    command.CommandText = "SELECT * FROM Subscriptions WHERE name like @name AND moduleId like @moduleId AND is_deleted = 0";
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@moduleId", moduleID);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = conn;

                    SqlDataReader readerModule = command.ExecuteReader();
                    if (readerModule.HasRows)
                        hasFoundSubscription = true;
                    else
                        hasFoundSubscription = false;

                    readerModule.Close();
                }

                conn.Close();

                return hasFoundSubscription;
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
                command.CommandText = "SELECT * FROM Applications WHERE name like @applicationName AND is_deleted = 0";
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

                    command.CommandText = "SELECT * FROM Modules WHERE name like @name AND applicationId like @applicationId AND is_deleted = 0";
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
                command.CommandText = "SELECT * FROM Applications WHERE name like @applicationName AND is_deleted = 0";
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

        public static List<Subscription> getSubscriptionsByModule(string moduleName)
        {
            List<Subscription> subscriptions = new List<Subscription>();
            Subscription subscription = new Subscription();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM (SELECT  *, ROW_NUMBER() OVER(PARTITION BY endpoint ORDER BY ID DESC) rn FROM Subscriptions) a WHERE rn = 1 AND a.moduleID = @moduleId";
                command.Parameters.AddWithValue("@moduleID", getModuleId(moduleName));       
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }
                while (reader.Read())
                {
                    subscription.Id = (int)reader["Id"];
                    subscription.name = (string)reader["name"];
                    subscription.creation_dt = (DateTime)reader["creation_dt"];
                    subscription.endpoint = (string)reader["endpoint"];
                    subscription.endpointType = (string)reader["endpointType"];
                    subscription.eventType = (string)reader["eventType"];
                    subscription.moduleID = (int)reader["moduleID"];

                    subscriptions.Add(subscription);
                }

                reader.Close();
                conn.Close();

                return subscriptions;
            }
            catch (Exception e)
            {
                e.ToString();
                return new List<Subscription>();
            }
            finally
            {
                conn.Close();
            }
        }
        
        public static Modules getModulesFromApplication(string applicationName)
        {
            int applicationId = getApplicationId(applicationName);

            Modules modules = new Modules();
            Module module;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Modules WHERE applicationID = @applicationId";
                command.Parameters.AddWithValue("@applicationId", applicationId);
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
    }
}