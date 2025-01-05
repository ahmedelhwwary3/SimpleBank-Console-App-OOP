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
    public class clsLicenseClassData
    {
        public static DataTable GetAllLicenseClassesList()
        {
            DataTable dtLicenseClasses = new DataTable();
            string query = "Select * From LicenseClasses order By ClassName ;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtLicenseClasses.Load(reader);
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
            return dtLicenseClasses;

        }
        public static bool IsExist(int LicenseClassID)
        {

            bool IsFound = false;
            string query = "Select IsFound=1 From LicenseClasses where LicenseClassID=@LicenseClassID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
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

        public static bool FindByClassName(ref int LicenseClassID, string ClassName, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref decimal ClassFees)
        {
            bool IsFound = false;
            string query = "Select * From LicenseClasses where ClassName=@ClassName;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (int)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (int)reader["DefaultValidityLength"];
                    ClassFees = (decimal)reader["ClassFees"];


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
        public static bool FindByClassID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref decimal ClassFees)
        {
            bool IsFound = false;
            string query = "Select * From LicenseClasses where LicenseClassID=@LicenseClassID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IsFound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (int)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (int)reader["DefaultValidityLength"];
                    ClassFees = (decimal)reader["ClassFees"];


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
        public static int AddNewLicenseClass(string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            int NewID = -1;
            string query = @"Insert Into LicenseClasses (  ClassName,ClassDescription,MinimumAllowedAge,DefaultValidityLength,ClassFees) 
                                         values (  @ClassName,@ClassDescription,@MinimumAllowedAge,@DefaultValidityLength,@ClassFees);
                                         select Scope_Identity();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

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
        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            int AffectedRows = 0;
            string query = @"Update LicenseClasses set LicenseClassID=@LicenseClassID
                                                     ,ClassName=@ClassName
                                                     ,ClassDescription=@ClassDescription
                                                     ,MinimumAllowedAge=@MinimumAllowedAge
                                                     ,DefaultValidityLength=@DefaultValidityLength
                                                     ,ClassFees=@ClassFees
                                         where LicenseClassID=@LicenseClassID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            int AffectedRows = 0;
            string query = "Delete From LicenseClasses Where LicenseClassID=@LicenseClassID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
