using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessLayer
{
    public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson Person { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser User { get; set; }
        public DateTime CreatedDate { get; set; }





        public enum enMode { AddNew, Update }
        enMode Mode;

        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedDate = DateTime.Now;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.Person = clsPerson.Find(this.PersonID);
            this.CreatedDate = CreatedDate;
            this.User = clsUser.FindByUserID(this.CreatedByUserID);
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1);
        }
        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDriver())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateDriver();
                    }
                default:
                    break;
            }
            return false;
        }


        public static bool IsExist(int DriverID)
        {
            return clsDriverData.IsExist(DriverID);
        }
        public static bool IsDriver(int PersonID)
        {
            return clsDriverData.IsDriver(PersonID);
        }
        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;


            if (clsDriverData.FindByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
              
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllDriversList()
        {
            return clsDriverData.GetAllDriversList();
        }
        public static bool DeleteDriver(int DriverID)
        {
            return clsDriverData.DeleteDriver(DriverID);
        }


        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;


            if (clsDriverData.FindByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
              
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllLocalLicenses(int DriverID)
        {
            return clsDriverData.GetAllLocalLicenses(DriverID);
        }
        public static DataTable GetAllInternationalLicenses(int DriverID)
        {
            return clsDriverData.GetAllInternationalLicenses(DriverID);
        }









    }
}
