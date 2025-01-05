using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
 

namespace BusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew, Update };
        public enMode Mode;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            Mode = enMode.AddNew;
        }
        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonInfo = clsPerson.Find(this.PersonID);
            Mode = enMode.Update;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {

                case enMode.AddNew:
                    {
                        if (_AddNewUser())
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
                        return _UpdateUser();
                    }
                default:
                    break;
            }
            return false;
        }


        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool IsActive = false;
            if (clsUserData.FindByUserNameAndPassword(ref UserID, ref PersonID, UserName, Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            bool IsActive = false;
            string UserName = "", Password = "";
            if (clsUserData.FindByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static bool IsExist(int UserID)
        {
            return clsUserData.IsExist(UserID);
        }
        public static bool IsExist(string UserName)
        {
            return clsUserData.IsExist(UserName);
        }
        public static bool Delete(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static DataTable GetAllUsersList()
        {

            return clsUserData.GetAllUsersList();
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            bool IsActive = false;
            string UserName = "", Password = "";
            if (clsUserData.FindByPersonID(ref UserID, PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }




    }
}

