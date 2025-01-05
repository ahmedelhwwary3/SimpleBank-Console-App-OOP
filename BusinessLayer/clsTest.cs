using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessLayer
{

    public class clsTest
    {

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointment { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser User { get; set; }


        public enum enMode { AddNew, Update }
        enMode Mode;

        public clsTest()
        {
            this.TestID = 1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        private clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.TestAppointment = clsTestAppointment.Find(this.TestAppointmentID);
            this.User = clsUser.FindByUserID(this.CreatedByUserID);
            Mode = enMode.Update;
        }
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewTest())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateTest();
                    }
                default:
                    break;
            }
            return false;
        }


        public static bool IsExist(int TestID)
        {
            return clsTestData.IsExist(TestID);
        }
        public static clsTest Find(int TestID)
        {

            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestData.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);

               
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllTestsList()
        {
            return clsTestData.GetAllTestsList();
        }
        public static bool DeleteTest(int TestID)
        {
            return clsTestData.DeleteTest(TestID);
        }
        public static int GetLastTestInfoForPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID)
        {
            int TestID = -1;
            return clsTestData.GetLastTestIDByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, TestTypeID);
        }

        public static int GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool IsPassedAllTestTypes(int LocalDrivingLicenseApplicationID)
        {
            return (GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3);
        }
        






    }
}
    
