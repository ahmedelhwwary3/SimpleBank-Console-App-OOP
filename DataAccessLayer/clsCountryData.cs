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
    public class clsCountryData
    {


        public static DataTable GetAllCountriesList()
        {
            DataTable dtCountry = new DataTable();
            string query = "Select * From Countries ;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtCountry.Load(reader);
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
            return dtCountry;
        }
        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {
            bool IsFound = false;
            string query = "Select * From Countries where CountryID=@CountryID;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    CountryName = (string)reader["CountryName"];
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
        public static bool GetCountryInfoByName(string CountryName, ref int CountryID)
        {
            bool IsFound = false;
            string query = "Select * From Countries where CountryName=@CountryName;";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IsFound = true;

                    CountryID = (int)reader["CountryID"];
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



    }
}
