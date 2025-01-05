using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessLayer
{
    public class clsTestType
    {

        public enum enMode { AddNew, Update };
        public enMode Mode;
        public enum enTestType { Vision = 1, Written = 2, Street = 3 };

        public clsTestType.enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestType()
        {
            this.TestTypeID = enTestType.Vision;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = -1;

            Mode = enMode.AddNew;
        }
        private clsTestType(enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;



            Mode = enMode.Update;
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((decimal)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }
        private bool _AddNewTestType()
        {
            this.TestTypeID = (enTestType)clsTestTypeData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
            return ((decimal)this.TestTypeID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {

                case enMode.AddNew:
                    {
                        if (_AddNewTestType())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return _UpdateTestType();
                    }
                default:
                    break;
            }
            return false;
        }



        public static clsTestType Find(enTestType TestTypeID)
        {
            string TestTypeDescription = "", TestTypeTitle = "";
            decimal TestTypeFees =0;
            if (clsTestTypeData.Find((decimal)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }
        public static bool IsExist(decimal TestTypeID)
        {
            return clsTestTypeData.IsExist(TestTypeID);
        }
        public static bool IsExist(string TestTypeName)
        {
            return clsTestTypeData.IsExist(TestTypeName);
        }

        public static DataTable GetAllTestTypesList()
        {

            return clsTestTypeData.GetAllTestTypesList();
        }



    }
}
