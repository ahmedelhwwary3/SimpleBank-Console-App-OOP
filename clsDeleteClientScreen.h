#pragma once
#include "clsScreen.h"
#include "clsInputValidate.h"
using namespace std;
class clsDeleteClientScreen:protected clsScreen
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
	static void ShowDeleteClientScreen()
	{

		if (!CheckAccessRights(clsUser::enPermissions::pDeleteClient))
		{
			return;
		}
		string AccountNumber;
		_DrawScreenHeader("Delete Client Screen");
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
		cout << "\n\nAre you sure you want to delete this account? y/n?";
		cin >> Answer;
		if (Answer == 'y' || Answer == 'Y')
		{
			if (Client.Delete())
			{
				cout << "\nClient deleted successfully";
				_PrintClientCard(Client);
			}
			else
			{
				cout << "\nClient delete failed!";
				_PrintClientCard(Client);
			}
		}





	}


};

