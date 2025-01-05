#pragma once
#include "clsPerson.h"
#include <fstream>
#include <vector>
#include "clsString.h"
#include "clsDate.h"
#include <string>
#include "clsString.h"
using namespace std;
class clsBankClient:public clsPerson
{
private:

	

    enum enMode { EmptyMode = 0, UpdateMode = 1, AddNewMode = 2 };
    enMode _Mode;
	string _AccountNumber;
	string _PinCode;
	float _AccountBalance;
	bool _MarkedForDelete = false;

    struct stTransferLogRecord;
    
    static stTransferLogRecord _ConvertTransferLogLineToRecord(string Line)
    {
        stTransferLogRecord Record;
        vector<string>vData = clsString::Split(Line, "#//#");
        Record.Date = vData[0];
        Record.SourceAccNumber = vData[1];
        Record.DestinationAccNumber= vData[2];
        Record.Amount= stod(vData[3]);
        Record.SourceAccBalance = stod(vData[4]);
        Record.DestinationAccBalance= stod(vData[5]);
        Record.UserName = vData[6];
        return Record;
    }
    string _PrepareTranferLogLine(clsBankClient Destination,double Amount,string UserName,string Seperator="#//#")
    {
        string Line = "";
        Line += clsDate::GetSystemDateTimeString() + Seperator;
        Line += AccountNumber + Seperator;
        Line += Destination.AccountNumber + Seperator;
        Line += to_string(Amount) + Seperator;
        Line += to_string(AccountBalance) + Seperator;
        Line += to_string(Destination.AccountBalance) + Seperator;
        Line += UserName;
        return Line;

    }
    void _RegisterTransferLog(clsBankClient Destination,double Amount,string UserName)
    {
        string Line = _PrepareTranferLogLine( Destination,  Amount,  UserName);
        fstream File;
        File.open("TransferLog.txt",ios::out|ios::app);
        if (File.is_open())
        {
            File << Line << endl;
            File.close();
        }
    }
    static string _ConverClientObjectToLine(clsBankClient Client, string Seperator = "#//#")
    {

        string stClientRecord = "";
        stClientRecord += Client.FirstName + Seperator;
        stClientRecord += Client.LastName + Seperator;
        stClientRecord += Client.Email + Seperator;
        stClientRecord += Client.Phone + Seperator;
        stClientRecord += Client.AccountNumber + Seperator;
        stClientRecord += Client.PinCode + Seperator;
        stClientRecord += to_string(Client.AccountBalance);

        return stClientRecord;

    }
    static  vector <clsBankClient> _LoadClientsDataFromFile()
    {

        vector <clsBankClient> vClients;

        fstream MyFile;
        MyFile.open("Clients.txt", ios::in);//read Mode

        if (MyFile.is_open())
        {

            string Line;


            while (getline(MyFile, Line))
            {

                clsBankClient Client = _ConvertLinetoClientObject(Line);

                vClients.push_back(Client);
            }

            MyFile.close();

        }

        return vClients;

    }
    static void _SaveCleintsDataToFile(vector <clsBankClient> vClients)
    {

        fstream MyFile;
        MyFile.open("Clients.txt", ios::out);//overwrite

        string DataLine;

        if (MyFile.is_open())
        {

            for (clsBankClient C : vClients)
            {
                if (C.MarkedForDeleted() == false)
                {
                    //we only write records that are not marked for delete.  
                    DataLine = _ConverClientObjectToLine(C);
                    MyFile << DataLine << endl;

                }

            }

            MyFile.close();

        }

    }

    void _Update()
    {
        vector <clsBankClient> _vClients;
        _vClients = _LoadClientsDataFromFile();

        for (clsBankClient& C : _vClients)
        {
            if (C.AccountNumber == AccountNumber)
            {
                C = *this;
                break;
            }

        }

        _SaveCleintsDataToFile(_vClients);

    }

    void _AddNew()
    {

        _AddDataLineToFile(_ConverClientObjectToLine(*this));
    }

    void _AddDataLineToFile(string  stDataLine)
    {
        fstream MyFile;
        MyFile.open("Clients.txt", ios::out | ios::app);

        if (MyFile.is_open())
        {

            MyFile << stDataLine << endl;

            MyFile.close();
        }

    }

    static clsBankClient _GetEmptyClientObject()
    {
        return clsBankClient(enMode::EmptyMode, "", "", "", "", "", "", 0);
    }
    static clsBankClient _ConvertLinetoClientObject(string Line, string Seperator = "#//#")
    {
        vector<string> vClientData;
        vClientData = clsString::Split(Line, Seperator);

        return clsBankClient(enMode::UpdateMode, vClientData[0], vClientData[1], vClientData[2],
            vClientData[3], vClientData[4], vClientData[5], stod(vClientData[6]));

    }
public:
    
  
    struct stTransferLogRecord
    {
        double  Amount, SourceAccBalance, DestinationAccBalance;
        string Date, SourceAccNumber, DestinationAccNumber, UserName;
    };
    
	clsBankClient(enMode Mode,string FirstName, string LastName, string Email, string Phone,string AccountNumber,string Pincode,float AccountBalance)
		:clsPerson( FirstName,  LastName,  Email,  Phone)
	{
		_Mode = Mode;
		_AccountNumber = AccountNumber;
		_PinCode = Pincode;
		_AccountBalance = AccountBalance;
	}
	static vector<clsBankClient> GetAllClientsList()
	{
		vector<clsBankClient>vClients;
		fstream File;
		File.open("Clients.txt", ios::in);
		if (File.is_open())
		{
			string Line;
			while (getline(File, Line))
			{
                clsBankClient Client = _ConvertLinetoClientObject(Line);
				vClients.push_back(Client);
			}
			File.close();
		}
		return vClients;
	}
    bool IsEmpty()
    {
        return (_Mode == enMode::EmptyMode);
    }

    bool MarkedForDeleted()
    {
        return _MarkedForDelete;
    }

    string GetAccountNumber()
    {
        return _AccountNumber;
    }

    void SetAccountNumber(string AccountNumber)
    {
        _AccountNumber = AccountNumber;
    }
    __declspec(property(get = GetAccountNumber, put = SetAccountNumber)) string AccountNumber;
    void SetPinCode(string PinCode)
    {
        _PinCode = PinCode;
    }

    string GetPinCode()
    {
        return _PinCode;
    }
    __declspec(property(get = GetPinCode, put = SetPinCode)) string PinCode;

    void SetAccountBalance(float AccountBalance)
    {
        _AccountBalance = AccountBalance;
    }

    float GetAccountBalance()
    {
        return _AccountBalance;
    }
    __declspec(property(get = GetAccountBalance, put = SetAccountBalance)) float AccountBalance;


    static clsBankClient Find(string AccountNumber)
    {


        fstream MyFile;
        MyFile.open("Clients.txt", ios::in);//read Mode

        if (MyFile.is_open())
        {
            string Line;
            while (getline(MyFile, Line))
            {
                clsBankClient Client = _ConvertLinetoClientObject(Line);
                if (Client.AccountNumber == AccountNumber)
                {
                    MyFile.close();
                    return Client;
                }

            }

            MyFile.close();

        }

        return _GetEmptyClientObject();
    }

    static clsBankClient Find(string AccountNumber, string PinCode)
    {



        fstream MyFile;
        MyFile.open("Clients.txt", ios::in);//read Mode

        if (MyFile.is_open())
        {
            string Line;
            while (getline(MyFile, Line))
            {
                clsBankClient Client = _ConvertLinetoClientObject(Line);
                if (Client.AccountNumber == AccountNumber && Client.PinCode == PinCode)
                {
                    MyFile.close();
                    return Client;
                }

            }

            MyFile.close();

        }
        return _GetEmptyClientObject();
    }

    enum enSaveResults { svFaildEmptyObject = 0, svSucceeded = 1, svFaildAccountNumberExists = 2 };
    enSaveResults Save()
    {

        switch (_Mode)
        {
        case enMode::EmptyMode:
        {
            if (IsEmpty())
            {

                return enSaveResults::svFaildEmptyObject;

            }

        }

        case enMode::UpdateMode:
        {


            _Update();

            return enSaveResults::svSucceeded;

            break;
        }

        case enMode::AddNewMode:
        {
            //This will add new record to file or database
            if (clsBankClient::IsClientExist(_AccountNumber))
            {
                return enSaveResults::svFaildAccountNumberExists;
            }
            else
            {
                _AddNew();

                //We need to set the mode to update after add new
                _Mode = enMode::UpdateMode;
                return enSaveResults::svSucceeded;
            }

            break;
        }
        }



    }

    static bool IsClientExist(string AccountNumber)
    {

        clsBankClient Client1 = clsBankClient::Find(AccountNumber);
        return (!Client1.IsEmpty());
    }

    bool Delete()
    {
        vector <clsBankClient> _vClients;
        _vClients = _LoadClientsDataFromFile();

        for (clsBankClient& C : _vClients)
        {
            if (C.AccountNumber == _AccountNumber)
            {
                C._MarkedForDelete = true;
                break;
            }

        }

        _SaveCleintsDataToFile(_vClients);

        *this = _GetEmptyClientObject();

        return true;

    }

    static clsBankClient GetAddNewClientObject(string AccountNumber)
    {
        return clsBankClient(enMode::AddNewMode, "", "", "", "", AccountNumber, "", 0);
    }

    static vector <clsBankClient> GetClientsList()
    {
        return _LoadClientsDataFromFile();
    }
    void Deposit(double Amount)
    {
        AccountBalance += Amount;
        Save();
     }
    bool Withdraw(double Amount)
    {
        if (Amount > AccountBalance)
            return false;
        else
        {
            AccountBalance -= Amount;
            Save();
            return true;
        }
    }
    static double GetTotalBalances()
    {
        double Total = 0;
        vector<clsBankClient>vClients = _LoadClientsDataFromFile();
        for (clsBankClient C : vClients)
        {
            Total += C.AccountBalance;
        }
        return Total;

    }
    void Transfer(double Amount,clsBankClient DestinationClient,string UserName)
    {
        Withdraw(Amount);
        DestinationClient.Deposit(Amount);
        _RegisterTransferLog(DestinationClient,Amount,UserName);
    }
    
   static vector<stTransferLogRecord>LoadTransferLogRecords()
    {
        vector<stTransferLogRecord>vRecords;
        fstream File;
        File.open("TransferLog.txt", ios::in);
        if (File.is_open())
        {
            string Line;
            while (getline(File, Line))
            {
                stTransferLogRecord Record = _ConvertTransferLogLineToRecord(Line);
                vRecords.push_back(Record);
            }
            File.close();
        }
        return vRecords;
    }
};

