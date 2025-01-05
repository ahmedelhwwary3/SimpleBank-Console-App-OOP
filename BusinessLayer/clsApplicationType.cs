using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew, Update };
        public enMode Mode;
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }
        public clsApplicationType()
        {
            this.ApplicationTypeID = 0;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = 0;
            Mode = enMode.AddNew;
        }
        private clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationTypeFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = clsApplicationTypeData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);
            return (this.ApplicationTypeID != -1);
        }
        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplicationType())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateApplicationType();
                    }
                default:
                    break;
            }
            return false;
        }



        public static bool IsExist(int ApplicationTypeID)
        {
            return clsApplicationTypeData.IsExist(ApplicationTypeID);
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {

            string ApplicationTypeTitle = "";
            decimal ApplicationTypeFees = 0;

            if (!clsApplicationTypeData.Find(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationTypeFees))
            {
                return null;
            }
            else
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees);
            }
        }

        public static DataTable GetAllApplicationTypesList()
        {
            return clsApplicationTypeData.GetAllApplicationTypesList();
        }

    }
}
