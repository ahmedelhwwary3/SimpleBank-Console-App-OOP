using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessLayer
{
    public class clsTestAppointment
    {


        public enum enMode { AddNew, Update }
        enMode Mode;
        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public clsTestType TestType { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser User { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo { get; set; }
        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.Vision;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;




            Mode = enMode.AddNew;
        }
        private clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.TestType = clsTestType.Find(this.TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(this.LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.User = clsUser.FindByUserID(this.CreatedByUserID);
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestApplicationInfo = clsApplication.FindBaseApplication(this.RetakeTestApplicationID);


            Mode = enMode.Update;
        }
        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }
        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewTestAppointment())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateTestAppointment();
                    }
                default:
                    break;
            }
            return false;
        }


        public static bool IsExist(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsExist(TestAppointmentID);
        }
        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            bool IsLocked = false;
            decimal PaidFees = -1;

            if (clsTestAppointmentData.FindintstAppointmentID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

           
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllTestAppointmentsListPerTestType(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetAllTestAppointmentsListPerTestType(LocalDrivingLicenseApplicationID,(int)TestTypeID);
        }
        public static DataTable GetAllTestAppointmentsList(int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentData.GetAllTestAppointmentsListAllTypes(LocalDrivingLicenseApplicationID);
        }
        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }
        public static clsTestAppointment FindLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
             int TestAppointmentID = -1;
             DateTime AppointmentDate = DateTime.Now;
             decimal PaidFees = 0;
             int CreatedByUserID = -1;
             int RetakeTestApplicationID = -1;
             bool IsLocked = false;

            if (clsTestAppointmentData.GetLastTestAppointmentInfo(LocalDrivingLicenseApplicationID, TestTypeID, ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment( TestAppointmentID,(clsTestType.enTestType) TestTypeID,  LocalDrivingLicenseApplicationID,  AppointmentDate,  PaidFees,  CreatedByUserID,  IsLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }

        public int GetTestID()
        {
            return clsTestAppointmentData.GetTestID(this.TestAppointmentID);
        }






    }
}
