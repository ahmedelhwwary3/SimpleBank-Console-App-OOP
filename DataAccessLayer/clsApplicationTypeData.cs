using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class clsApplicationTypeData
    {
        public static bool IsExist(int ApplicationTypeID)
        {
            bool IsFound = false;
            string query = "Select IsFound=1 From ApplicationTypes where ApplicationTypeID=@ApplicationTypeID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();

            }
            catch(Exception ex) 
            {
                IsFound = false;
                string sourceName = "DVLD1";
                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
                EventLog.WriteEntry(sourceName, $"{ex}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static bool Find(int ApplicationTypeID, ref string ApplicationTypeTitle, ref decimal ApplicationFees)
        {
            bool IsFound = false;
            string query = "Select * From ApplicationTypes where ApplicationTypeID=@ApplicationTypeID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = (decimal)reader["ApplicationFees"];
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                IsFound = false;
                string sourceName = "DVLD1";
                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
                EventLog.WriteEntry(sourceName, $"{ex}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static DataTable GetAllApplicationTypesList()
        {
            DataTable dtUsers = new DataTable();
            string query = "Select * From ApplicationTypes ;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtUsers.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                 
                string sourceName = "DVLD1";
                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
                EventLog.WriteEntry(sourceName, $"{ex}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dtUsers;
        }
        public static int AddNewApplicationType(string ApplicationTypeTitle, decimal ApplicationTypeFees)
        {
            int NewID = -1;
            string query = @"Insert Into ApplicationTypes (  ApplicationTypeTitle, ApplicationTypeFees) 
                                         values ( @ApplicationTypeTitle, @ApplicationTypeFees);
                                         select Scope_Identity();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationTypeFees", ApplicationTypeFees);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    NewID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                
                string sourceName = "DVLD1";
                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
                EventLog.WriteEntry(sourceName, $"{ex}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return NewID;
        }
        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle,decimal ApplicationFees)
        {
            int AffectedRows = 0;
            string query = @"Update ApplicationTypes set ApplicationTypeTitle=@ApplicationTypeTitle,
                                                         ApplicationFees=@ApplicationFees
                                         where ApplicationTypeID=@ApplicationTypeID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                 
                string sourceName = "DVLD1";
                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
                EventLog.WriteEntry(sourceName, $"{ex}", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (AffectedRows > 0);
        }




    }
}
