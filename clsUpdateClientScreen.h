#pragma once
#include "clsScreen.h"
using namespace std;
class clsUpdateClientScreen:protected clsScreen
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
	static void _ReadClientInfo(clsBankClient& Client)
	{
		cout << "\Enter First Name:";
		Client.FirstName = clsInputValidate<int>::ReadString();
		cout << "\Enter Last Name:";
		Client.LastName = clsInputValidate<int>::ReadString();
		cout << "\Enter Email:";
		Client.Email = clsInputValidate<int>::ReadString();
		cout << "\Enter Phone:";
		Client.Phone = clsInputValidate<int>::ReadString();
		cout << "\Enter Pincode:";
		Client.PinCode = clsInputValidate<int>::ReadString();
		cout << "\Enter Account Balance:";
		Client.AccountBalance = clsInputValidate<double>::ReadDblNumber();
	}
public:
	static void ShowUpdateClientScreen()
	{

		if (!CheckAccessRights(clsUser::enPermissions::pUpdateClients))
		{
			return;
		}
		string AccountNumber;
		_DrawScreenHeader("Update Client Screen");
		cout << "\nEnter Account Number?";
		AccountNumber = clsInputValidate<int>::ReadString();
		while (!clsBankClient::IsClientExist(AccountNumber))
		{
			cout << "No client is found with this account number!Enter another one?";
			AccountNumber = clsInputValidate<int>::ReadString();
		}

		clsBankClient Client = clsBankClient::Find(AccountNumber);
		_PrintClientCard(Client);
		char Answer;
		cout << "\n\nAre you sure you want to update this account? y/n?";
		cin >> Answer;
		if (Answer == 'y' || Answer == 'Y')
		{
			cout << "\nUpdating Client:\n";
			_ReadClientInfo(Client);
			clsBankClient::enSaveResults SaveResult = Client.Save();
			if (SaveResult == clsBankClient::enSaveResults::svSucceeded)
			{
				cout << "\nUpdate succeeded :)";
				_PrintClientCard(Client);
			}
			else
			{
				cout << "\nUpdate failed! :(";
			}

		}










	}



};

