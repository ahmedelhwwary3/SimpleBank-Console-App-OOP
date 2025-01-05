using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {



        public enum enMode { AddNew, Update }
        public enMode Mode;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClass { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationStatus = clsApplication.enApplicationStatus.New;
            this.ApplicationTypeID = clsApplication.enApplicationType.NewLocalDrivingLicenseService;
            this.CreatedByUserID = -1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = -1;

            Mode = enMode.AddNew;
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplication, int LicenseClassID, int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, clsApplication.enApplicationStatus ApplicationStatus, clsApplication.enApplicationType ApplicationTypeID, int CreatedByUserID, DateTime LastStatusDate, decimal PaidFees)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.LicenseClass = clsLicenseClass.FindByID(LicenseClassID);
            this.ApplicantPersonID = ApplicantPersonID;
            this.Person = clsPerson.Find(this.ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationStatus = ApplicationStatus;
            this.ApplicationTypeID = ApplicationTypeID;
            this.CreatedByUserID = CreatedByUserID;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;

            Mode = enMode.Update;
        }
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.LicenseClassID, this.ApplicationID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.LicenseClassID, this.ApplicationID);
        }


        public bool Save()
        {
            base.Mode = (clsApplication.enMode)this.Mode;
            if (!base.Save())
            {
                return false;
            }
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLocalDrivingLicenseApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateLocalDrivingLicenseApplication();
                    }
                default:
                    break;
            }
            return false;
        }


        public static bool IsExist(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.IsExist(LocalDrivingLicenseApplicationID);
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int LicenseClassID = -1, ApplicationID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.FindByID(LocalDrivingLicenseApplicationID, ref LicenseClassID, ref ApplicationID);
            if (IsFound)
            {
                clsApplication BaseApplication = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassID, BaseApplication.ApplicationID, BaseApplication.ApplicantPersonID, BaseApplication.ApplicationDate, (clsApplication.enApplicationStatus)BaseApplication.ApplicationStatus, (clsApplication.enApplicationType)BaseApplication.ApplicationTypeID, BaseApplication.CreatedByUserID, BaseApplication.LastStatusDate, BaseApplication.PaidFees);
            }
            else
            {
                return null;
            }
        }
        public static clsLocalDrivingLicenseApplication FindByApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int LicenseClassID = -1, ApplicationID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID, ref LicenseClassID, ref LocalDrivingLicenseApplicationID);
            if (IsFound)
            {
                clsApplication BaseApplication = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassID, BaseApplication.ApplicationID, BaseApplication.ApplicantPersonID, BaseApplication.ApplicationDate, (clsApplication.enApplicationStatus)BaseApplication.ApplicationStatus, (clsApplication.enApplicationType)BaseApplication.ApplicationTypeID, BaseApplication.CreatedByUserID, BaseApplication.LastStatusDate, BaseApplication.PaidFees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllLocalDrivingLicenseApplicationsList()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplicationsList();
        }
        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
 
        public int GetActiveLicenseID()
        {
            return clsLocalDrivingLicenseApplicationData.GetActiveLicenseID(this.LocalDrivingLicenseApplicationID);
        }
        public bool IsLicenseIssued(int LocalDrivingLicenseApplicationID)
        {
            return (GetActiveLicenseID() != -1);
        }
        public bool IsLicenseIssued()
        {
            return IsLicenseIssued(this.LocalDrivingLicenseApplicationID);
        }
        public int GetPassedTests()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllPassedTests(this.LocalDrivingLicenseApplicationID);
        }
        public static int GetPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetAllPassedTests(LocalDrivingLicenseApplicationID);
        }
        public int CountAllTestTrials(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest((int)TestType, this.LocalDrivingLicenseApplicationID);
        }

        public bool DoesHaveActiveTestAppointment(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesHaveActiveScheduledTestAppointment(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
         
        public bool Delete()
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
        }
        public bool DoesAttentTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool IsThereActiveScheduledTestAppointment(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesHaveActiveScheduledTestAppointment(this.LocalDrivingLicenseApplicationID,(int)TestTypeID);
        }
        public bool DoesPassAllTestTypes()
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassAllTestTypes(this.LocalDrivingLicenseApplicationID);
        }
        public int IssueDrivingLicenseForFirstTime(int UserID,string Notes)
        {
            clsDriver Driver;
            clsLicense NewLicense=new clsLicense();
            if (!clsDriver.IsDriver(this.ApplicantPersonID))
            {
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = UserID;
                Driver.CreatedDate = DateTime.Now;
                if (!Driver.Save())
                {
                    return -1;
                }
            }
            else
            {
                Driver = clsDriver.FindByPersonID(this.Person.PersonID);
            }
            NewLicense.DriverID = Driver.DriverID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClass.DefaultValidityLength);
            NewLicense.CreatedByUserID = UserID;
            NewLicense.ApplicationID = this.ApplicationID;
            NewLicense.Notes = Notes;
            NewLicense.IssueReason = clsLicense.enIssueReason.FirstTime;
            NewLicense.IsActive = true;
            NewLicense.LicenseClass = this.LicenseClassID;
            NewLicense.PaidFees = this.LicenseClass.ClassFees;
            if(NewLicense.Save())
            {
                this.SetCompleted();
                return NewLicense.LicenseID;
            }
            else
            {
                return -1;
            }
        }





        public clsTest GetLastTestAppointmentInfoPerTestType(clsTestType.enTestType TestTypeID)
        {
            int TestID = clsTestData.GetLastTestIDByPersonAndTestTypeAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, (int)TestTypeID); 

             
            if (TestID!=-1)
            {
                return clsTest.Find(TestID);
            }
            else
            {
                return null;
            }

        }






    }
}
