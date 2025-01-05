using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicenseClass
    {
        public enum enMode { AddNew, Update }
        enMode Mode;
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = -1;
            Mode = enMode.AddNew;
        }
        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }
        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
            return (this.LicenseClassID != -1);
        }
        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicenseClass())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateLicenseClass();
                    }
                default:
                    break;
            }
            return false;
        }


        public static bool IsExist(int LicenseClassID)
        {
            return clsLicenseClassData.IsExist(LicenseClassID);
        }

        public static clsLicenseClass FindByID(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            int MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = -1;
            if (clsLicenseClassData.FindByClassID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
               
            }
            else
            {
                return null;
            }
        }
        public static clsLicenseClass FindByClassName(string ClassName)
        {
            string ClassDescription = "";
            int LicenseClassID = -1;
            int MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = -1;
            if (clsLicenseClassData.FindByClassName(ref LicenseClassID, ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

                
            }
            else
            {
                return null;
            }
        }
        
        public static DataTable GetAllLicenseClasssList()
        {
            return clsLicenseClassData.GetAllLicenseClassesList();
        }
        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            return clsLicenseClassData.DeleteLicenseClass(LicenseClassID);
        }

    }
}
