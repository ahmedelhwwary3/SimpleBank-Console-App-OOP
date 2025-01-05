#pragma once
#include "clsScreen.h"
using namespace std;



class clsTransferScreen:protected clsScreen
{
private:
	static void _PrintClientCard(clsBankClient Client)
	{
		cout << "\n\t\t\t-------------------------------\n";
		cout << "\t\t\t|Client Name    :" << setw(13) << Client.FullName() << "|" << endl;
		cout << "\t\t\t|Email          :" << setw(13) << Client.Email << "|" << endl;
		cout << "\t\t\t|Phone          :" << setw(13) << Client.Phone << "|" << endl;
		cout << "\t\t\t|Account Number :" << setw(13) << Client.AccountNumber << "|" << endl;
		cout << "\t\t\t|Pincode        :" << setw(13) << Client.PinCode << "|" << endl;
		cout << "\t\t\t|Account Balance:" << setw(13) << Client.AccountBalance << "|" << endl;
		cout << "\t\t\t-------------------------------\n";
	}
	static void _ReadAccountNumberIsExisted(string& AccountNumber)
	{
		AccountNumber = clsInputValidate<int>::ReadString();
		if (!clsBankClient::IsClientExist(AccountNumber))
		{
			cout << "\nNo client found with this account number! ";
			cout << "Enter another one?";
			AccountNumber= clsInputValidate<int>::ReadString();
		}
	}
public:
	static void ShowTransferScreen()
	{
		char Answer;
		double Amount;
		string SourceAcc, DestinationAcc;
		_DrawScreenHeader("Transfer Screen");
		cout << "\nEnter source account number?";
		_ReadAccountNumberIsExisted(SourceAcc);
		clsBankClient SourceClient = clsBankClient::Find(SourceAcc);
		_PrintClientCard(SourceClient);
		cout << "\nEnter Destination account number?";
		_ReadAccountNumberIsExisted(DestinationAcc);
		clsBankClient DestinationClient = clsBankClient::Find(DestinationAcc);
		_PrintClientCard(DestinationClient);
		cout << "\nEnter amount to transfer?";
		Amount = clsInputValidate<double>::ReadDblNumber();
		cout << "\nAre you sure you want to transfer " << Amount << "?y/n?";
		cin >> Answer;
		if (Answer == 'y' || Answer == 'Y')
		{
			if (Amount > SourceClient.AccountBalance)
			{
				cout << "\nTransfer failed.";
				cout << "\nAmount exceeds the balance!";
			}
			else
			{
				SourceClient.Transfer(Amount, DestinationClient, CurrentUser.UserName);
				cout << "\nTransfer Succeeded :)";
				_PrintClientCard(SourceClient);
				//refresh balance
				clsBankClient DestinationClient = clsBankClient::Find(DestinationAcc);
				_PrintClientCard(DestinationClient);
			}
		}
		else
		{
			cout << "\nTransfer was cancelled!";
		}
		


	}



};

