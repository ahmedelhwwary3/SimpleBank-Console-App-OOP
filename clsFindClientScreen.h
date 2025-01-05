#pragma once
#include "clsScreen.h"
using namespace std;
class clsFindClientScreen:protected clsScreen
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
public:
	static void ShowFindClientScreen()
	{

		if (!CheckAccessRights(clsUser::enPermissions::pFindClient))
		{
			return;
		}
		string AccountNumber;
		_DrawScreenHeader("Find Client Screen");
		cout << "\nEnter Account Number?";
		AccountNumber = clsInputValidate<int>::ReadString();

		while (!clsBankClient::IsClientExist(AccountNumber))
		{
			cout << "\nNo client found with this account number\nEnter Account Number?";
			AccountNumber = clsInputValidate<int>::ReadString();
		}
		clsBankClient Client = clsBankClient::Find(AccountNumber);
		if (!Client.IsEmpty())
		{
			cout << "\nClient is found :)";
			_PrintClientCard(Client);
		}
		else
		{
			cout << "\nNo client found with this account number";
		}



	}




};

