using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsApplication
    {
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        public enum enApplicationType { NewLocalDrivingLicenseService = 1, RenewDrivingLicenseService = 2, ReplacementForLostDrivingLicense = 3, ReplacementForDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7 };

        public enum enMode { AddNew, Update }
        public enMode Mode;
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson Person;
        public DateTime ApplicationDate { get; set; }
        public clsApplication.enApplicationType ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationType;
        public clsApplication.enApplicationStatus ApplicationStatus { get; set; }
        public string ApplicationStatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        {
                            return "New";
                        }
                    case enApplicationStatus.Completed:
                        {
                            return "Completed";
                        }
                    case enApplicationStatus.Cancelled:
                        {
                            return "Cancelled";
                        }
                    default:
                        return "Unknown";
                }
                
            }
        }
    
        public DateTime LastStatusDate { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUser;
        public decimal PaidFees { get; set; }



        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = enApplicationType.NewLocalDrivingLicenseService;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.PaidFees = -1;


            Mode = enMode.AddNew;
        }
        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, enApplicationType ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, int CreatedByUserID, decimal PaidFees)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationType = clsApplicationType.Find((int)this.ApplicationTypeID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUser = clsUser.FindByUserID(this.CreatedByUserID);
            this.PaidFees = PaidFees;
            this.Person = clsPerson.Find(this.ApplicantPersonID);
            Mode = enMode.Update;
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, (int)this.ApplicationTypeID, this.ApplicationDate, (int)this.ApplicationStatus, this.LastStatusDate, this.CreatedByUserID, this.PaidFees);
            return (this.ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, (int)this.ApplicationTypeID, this.ApplicationDate, (int)this.ApplicationStatus, this.LastStatusDate, this.CreatedByUserID, this.PaidFees);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateApplication();
                    }
                default:
                    break;
            }
            return false;
        }


        public static bool IsExist(int ApplicationID)
        {
            return clsApplicationData.IsExist(ApplicationID);
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, CreadtedByUserID = -1;
            decimal PaidFees = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            int ApplicationTypeID = 0, ApplicationStatus = 0;

            if (clsApplicationData.Find(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreadtedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, (clsApplication.enApplicationType)ApplicationTypeID, (clsApplication.enApplicationStatus)ApplicationStatus, LastStatusDate, CreadtedByUserID, PaidFees); ;
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllApplicationsList()
        {
            return clsApplicationData.GetAllApplicationsList();
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, (int)ApplicationTypeID);
        }
        public static bool DoesPersonHaveActiveApplicationForLicenseClass(int PersonID,clsApplication.enApplicationType ApplicationTypeID,int LicenseClassID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplicationForLicenseClass(PersonID, (int)ApplicationTypeID,LicenseClassID);
        }
        public bool UpdateStatus(int NewStatus)
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID,NewStatus);
        }
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID,(int)enApplicationStatus.Cancelled);
        }
        public bool SetCompleted()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (int)enApplicationStatus.Completed);
        }
        public static int GetActiveApplicationID(int PersonID,clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID,(int)ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID,int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID,LicenseClassID);
        }



    }

}