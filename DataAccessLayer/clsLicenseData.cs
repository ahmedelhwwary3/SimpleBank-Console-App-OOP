using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class clsLicenseData
    {

        public static DataTable GetAllLicensesList()
        {
            DataTable dtLicense = new DataTable();
            string query = "select * from Licenses ;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtLicense.Load(reader);
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
            return dtLicense;

        }
        public static DataTable GetAllLicensesListForDriver(int DriverID)
        {
            DataTable dtLicense = new DataTable();
            string query = @"select l.ApplicationID,l.DriverID,l.IssueDate,l.ExpirationDate,l.IsActive,l.PaidFees,lc.ClassName from Licenses l inner join LicenseClasses lc on l.LicenseClass=lc.LicenseClassID
                             where l.DriverID=@DriverID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID",DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtLicense.Load(reader);
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
            return dtLicense;

        }
        public static bool IsExist(int LicenseID)
        {

            bool IsFound = false;
            string query = "Select IsFound=1 From Licenses where LicenseID=@LicenseID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
        public static bool FindByPersonID(int PersonID, ref int  LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {


            bool IsFound = false;
            string query = @"select l.LicenseID,l.ApplicationID,l.DriverID,l.LicenseClass,l.IssueDate,l.ExpirationDate,l.Notes,l.PaidFees,l.IsActive,l.IssueReason,l.CreatedByUserID from Licenses l inner join Applications  a on l.ApplicationID=a.ApplicationID
                             where a.ApplicantPersonID=@PersonID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LicenseID = (int)reader["l.LicenseID"];
                    ApplicationID = (int)reader["l.ApplicationID"];
                    DriverID = (int)reader["l.DriverID"];
                    LicenseClass = (int)reader["l.LicenseClass"];
                    IssueDate = (DateTime)reader["l.IssueDate"];
                    ExpirationDate = (DateTime)reader["l.ExpirationDate"];
                    if (reader["l.Notes"] == System.DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["l.Notes"];
                    }

                    PaidFees = (decimal)reader["l.PaidFees"];
                    IsActive = (bool)reader["l.IsActive"];
                    IssueReason = (int)reader["l.IssueReason"];
                    CreatedByUserID = (int)reader["l.CreatedByUserID"];

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
        public static bool FindByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;
            string query = @"Select * From Licenses where LicenseID=@LicenseID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if (reader["Notes"]==System.DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                   
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (int)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int NewID = -1;
            string query = @"Insert Into Licenses (ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID) 
                                         values  (@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                                         select Scope_Identity();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);

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
        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int AffectedRows = 0;
            string query = @"Update Licenses set  ApplicationID=@ApplicationID ,DriverID=@DriverID,LicenseClass=@LicenseClass,IssueDate=@IssueDate,ExpirationDate=@ExpirationDate,Notes=@Notes,PaidFees=@PaidFees,IsActive=@IsActive,IssueReason=@IssueReason,CreatedByUserID=@CreatedByUserID
                                    where LicenseID=@LicenseID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);


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
        public static bool DeleteLicense(int LicenseID)
        {
            int AffectedRows = 0;
            string query = "Delete From Licenses Where LicenseID=@LicenseID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
        public static int GetActiveLicenseIDByPersonIDForLicenseClass(int PersonID,int LicenseClass)
        {
            int ActiveLicenseID = -1;
            string query = @"select l.LicenseID from Licenses l inner join Drivers d on d.DriverID=l.DriverID 
                             inner join People p on p.PersonID=d.PersonID where (p.PersonID=@PersonID And l.LicenseClass=@LicenseClass)And l.IsActive=1";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            try
            {
                connection.Open();
                object result= command.ExecuteScalar();
                if (result != null&&int.TryParse(result.ToString(),out int ID))
                {
                    ActiveLicenseID = ID;
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
            return ActiveLicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            int AffectedRows = 0;
            string query = @"Update Licenses set  IsActive=0 Where LicenseID=@LicenseID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
