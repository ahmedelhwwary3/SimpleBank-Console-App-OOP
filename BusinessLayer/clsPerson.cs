using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew, Update }
        enMode Mode;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int Gendor { get; set; }
        public int NationalityCountryID { get; set; }
        public clsCountry Country;
        public string FullName
        {
            get { return this.FirstName + " " + this.SecondName + " " + (string.IsNullOrEmpty(this.ThirdName) ? "" : this.ThirdName + " ") + this.LastName; }
        }

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public clsPerson()
        {
            this.PersonID = -1;
            this.Gendor = 0;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalityCountryID = -1;
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.ImagePath = "";
            Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, int NationailtyCountryID, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.ThirdName = ThirdName;
            this.SecondName = SecondName;
            this.NationalityCountryID = NationailtyCountryID;
            this.Country = clsCountry.Find(NationailtyCountryID);
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Gendor = Gendor;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }
        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return _UpdatePerson();
                    }
                default:
                    break;
            }
            return false;
        }

        public static bool IsExist(string NationalNo)
        {
            return clsPersonData.IsExist(NationalNo);
        }
        public static bool IsExist(int PersonID)
        {
            return clsPersonData.IsExist(PersonID);
        }
        public static clsPerson Find(string NationalNo)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            int NationailtyCountryID = -1, PersonID = -1;
            int Gendor = 0;
            DateTime DateOfBirth = DateTime.Now;
            if (!clsPersonData.FindByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationailtyCountryID, ref ImagePath))
            {
                return null;
            }
            else
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, NationailtyCountryID, DateOfBirth, Gendor, Address, Phone, Email, ImagePath);
            }
        }
        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            int NationailtyCountryID = -1;
            int Gendor = 0;
            DateTime DateOfBirth = DateTime.Now;
            if (clsPersonData.FindByPersonID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationailtyCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, NationailtyCountryID, DateOfBirth, Gendor, Address, Phone, Email, ImagePath);

           
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllPeopleList()
        {
            return clsPersonData.GetAllPeopleList();
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
    }
}
