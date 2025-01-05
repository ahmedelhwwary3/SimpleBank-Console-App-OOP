#pragma once
#include <string>
#include "clsPerson.h"
#include "clsString.h"
#include <vector>
#include <fstream>
#include "clsUtil.h"
#include "clsBankClient.h"
using namespace std;
class clsUser:public clsPerson
{
private:





    struct stLoginRegisterRecord;
	string _UserName;
	string _Password;
	//string _EncryptedPassword;
	int _Permissions;
    enum enMode{eEmpty=1,eAddNew=2,eUpdate=3};
    enMode _Mode;
    bool _MarkedForDelete = false;
    string _ConvertUserObjectToLine(clsUser User,string Seperator="#//#")
    {
        string Line = "";
        Line+=User.FirstName+Seperator;
        Line += User.LastName + Seperator;
        Line += User.Email + Seperator;
        Line += User.Phone + Seperator;
        Line += User.UserName + Seperator;
        Line += clsUtil::EncryptText(User.Password) + Seperator;
        Line +=to_string( User.Permissions);
        return Line;
    }
    static clsUser _GetEmptyUserObject()
    {
        return clsUser(eEmpty, "", "", "", "", "", "", 0);
    }
    static clsUser _ConvertUserLineToObject(string Line)
    {
        vector<string>vUserData = clsString::Split(Line, "#//#");
        return clsUser(eUpdate, vUserData[0], vUserData[1], vUserData[2], vUserData[3], vUserData[4], clsUtil::DecryptText(vUserData[5]), stoi(vUserData[6]));
    }
    static vector<clsUser> _LoadUsersDataFromFile()
    {
        vector<clsUser>vUsers;
        fstream File;
        File.open("Users.txt", ios::in);
        if (File.is_open())
        {
            string Line = "";
            while (getline(File, Line))
            {
                clsUser User = _ConvertUserLineToObject(Line);
                vUsers.push_back(User);
            }
            File.close();
        }
        return vUsers;
    }
    void _SaveUsersVectorToFile(vector<clsUser> vUsers)
    {
        fstream File;
        File.open("Users.txt", ios::out);
        if (File.is_open())
        {
            string Line;
            for (clsUser U : vUsers)
            {
                Line = _ConvertUserObjectToLine(U);
                if (U.IsMarkedForDeleted())
                {
                    File << Line << endl;
                }
            }
            File.close();
        }
    }
    void _AddNew()
    {
 
        fstream File;
        File.open("Users.txt", ios::out | ios::app);
        if (File.is_open())
        {
            string Line = _ConvertUserObjectToLine(*this);
            File << Line << endl;
            File.close();
        }
    }
    void _Update()
    {
        vector<clsUser>vUsers = _LoadUsersDataFromFile();
        for (clsUser& U : vUsers)
        {
            if (U.UserName == UserName)
            {
                U = *this;
                break;
            }
            _SaveUsersVectorToFile(vUsers);
        }
        
    }
    string _PrepareRegisterLogInLine(string Seperator="#//#")
    {
        string Line = "";
        Line += clsDate::GetSystemDateTimeString() + Seperator;
        Line += UserName + Seperator;
        Line += clsUtil::EncryptText(Password) + Seperator;
        Line += to_string(Permissions);
        return Line;

    }
    static stLoginRegisterRecord _ConvertLoginRegisterLineToRecord(string Line,string Seperator="#//#")
    {
        stLoginRegisterRecord LoginRegisterRecord;


        vector <string> LoginRegisterDataLine = clsString::Split(Line, Seperator);
        LoginRegisterRecord.DateTime = LoginRegisterDataLine[0];
        LoginRegisterRecord.UserName = LoginRegisterDataLine[1];
        LoginRegisterRecord.Password = clsUtil::DecryptText(LoginRegisterDataLine[2]);
        LoginRegisterRecord.Permissions = stoi(LoginRegisterDataLine[3]);

        return LoginRegisterRecord;
    }





public:

    struct stLoginRegisterRecord
    {
        string DateTime;
        string UserName;
        string Password;
        int Permissions;
    };
    enum enPermissions
    {
        eAll = -1, pListClients = 1, pAddNewClient = 2, pDeleteClient = 4,
        pUpdateClients = 8, pFindClient = 16, pTranactions = 32, pManageUsers = 64,
        pShowLogInRegister = 128
    };
    clsUser(enMode Mode, string FirstName, string LastName,
        string Email, string Phone, string UserName, string Password,
        int Permissions) :
        clsPerson(FirstName, LastName, Email, Phone)

    {
        _Mode = Mode;
        _UserName = UserName;
        _Password = Password;
        _Permissions = Permissions;
    }
    bool IsEmpty()
    {
        return (_Mode == enMode::eEmpty);
    }
    bool IsMarkedForDeleted()
    {
        return (_MarkedForDelete == true);
    }
    bool MarkedForDeleted()
    {
        return _MarkedForDelete;
    }

    string GetUserName()
    {
        return _UserName;
    }

    void SetUserName(string UserName)
    {
        _UserName = UserName;
    }

    __declspec(property(get = GetUserName, put = SetUserName)) string UserName;
    string GetFullName()
    {
        return FirstName + " " + LastName;
    }
    void SetPassword(string Password)
    {
        _Password = Password;
    }

    string GetPassword()
    {
        return _Password;
    }
    __declspec(property(get = GetPassword, put = SetPassword)) string Password;



    void SetPermissions(int Permissions)
    {
        _Permissions = Permissions;
    }

    int GetPermissions()
    {
        return _Permissions;
    }
    __declspec(property(get = GetPermissions, put = SetPermissions)) int Permissions;
    static clsUser Find(string UserName)
    {
        vector<clsUser>vUsers = _LoadUsersDataFromFile();
        for (clsUser U : vUsers)
        {
            if (U.UserName == UserName)
                return U;
        }
        return _GetEmptyUserObject();
    }
    static clsUser Find(string UserName,string Password)
    {
        vector<clsUser>vUsers = _LoadUsersDataFromFile();
        for (clsUser U : vUsers)
        {
            if (U.UserName == UserName&&U.Password==Password)
                return U;
        }
        return _GetEmptyUserObject();
    }
    static bool IsExist(string UserName)
    {
        clsUser User = clsUser::Find(UserName);
        return (!User.IsEmpty());
    }
    bool Delete()
    {
        vector<clsUser>vUsers = _LoadUsersDataFromFile();
        for (clsUser& U : vUsers)
        {
            if (U.UserName == UserName)
            {
                U._MarkedForDelete = true;
                break;
            }
        }
        _SaveUsersVectorToFile(vUsers);
        return true;
    }
    bool Update()
    {
        if (!IsEmpty())
        {
            _Update();
        }
        return false;
    }



    static clsUser GetAddNewUserObject(string UserName)
    {
        return clsUser(enMode::eAddNew, "", "", "", "",UserName, "", 0);
    }
    enum enSaveResults{svSucceeded=1,svFailedEmptyObject=2,svFailedIsExist=3};
    enSaveResults Save()
    {
        switch (_Mode)
        {
        case clsUser::eEmpty:
            return svFailedEmptyObject;
        case clsUser::eAddNew:
        {
            if (IsExist(UserName))
                return svFailedIsExist;
            else
            {
                _AddNew();
                _Mode = eUpdate;
                return svSucceeded;
            }
        }
       
        case clsUser::eUpdate:
            if (IsEmpty())
            {
                return svFailedEmptyObject;
            }
            else
            {
                _Update();
                return svSucceeded;
            }
 
        }
    }
    static vector<clsUser>GetAllUsersList()
    {
        return _LoadUsersDataFromFile();
    }
    bool DoesHaveAccess(clsUser::enPermissions Permissions)
    {
        //'this' is the All and the other is the part of All
        if (this->Permissions == clsUser::enPermissions::eAll)
            return true;
        if ((Permissions & this->Permissions) == Permissions)
            return true;
        return false;

    }
    void RegisterLogIn()
    {
        string LoginLine = _PrepareRegisterLogInLine();
        fstream File;
        File.open("LoginRegister.txt", ios::out | ios::app);
        if (File.is_open())
        {
            File << LoginLine << endl;
            File.close();
        }


    }
    static vector<stLoginRegisterRecord>GetLoginRegisterList()
    {
        vector <stLoginRegisterRecord> vLoginRegisterRecord;

        fstream MyFile;
        MyFile.open("LoginRegister.txt", ios::in);//read Mode

        if (MyFile.is_open())
        {

            string Line;

            stLoginRegisterRecord LoginRegisterRecord;

            while (getline(MyFile, Line))
            {

                LoginRegisterRecord = _ConvertLoginRegisterLineToRecord(Line);

                vLoginRegisterRecord.push_back(LoginRegisterRecord);

            }

            MyFile.close();

        }

        return vLoginRegisterRecord;




    }


};

