#pragma once
#include "clsScreen.h"
using namespace std;
class clsAddNewClientScreen:protected clsScreen
{
private:

	static void _PrintClientCard(clsBankClient Client)
	{
		cout << "\n\t\t\t-------------------------------\n";
		cout << "\t\t\t|Client Name    :" << setw(13) << Client.FullName()<< "|" << endl;
		cout << "\t\t\t|Email          :" << setw(13) << Client.Email <<  "|" << endl;
		cout << "\t\t\t|Phone          :" << setw(13) << Client.Phone << "|" << endl;
		cout << "\t\t\t|Account Number :" << setw(13) << Client.AccountNumber <<  "|" << endl;
		cout << "\t\t\t|Pincode        :" << setw(13) << Client.PinCode <<  "|" << endl;
		cout << "\t\t\t|Account Balance:" << setw(13) << Client.AccountBalance << "|" << endl;
		cout << "\t\t\t-------------------------------\n";
	}
	static void _ReadClientInfo(clsBankClient& Client)
	{
		cout << "\Enter First Name:";
		Client.FirstName = clsInputValidate<int>::ReadString();
		cout << "\Enter Last Name:";
		Client.LastName=clsInputValidate<int>::ReadString();
		cout << "\Enter Email:";
		Client.Email= clsInputValidate<int>::ReadString();
		cout << "\Enter Phone:";
		Client.Phone= clsInputValidate<int>::ReadString();
		cout << "\Enter Pincode:";
		Client.PinCode= clsInputValidate<int>::ReadString();
		cout << "\Enter Account Balance:";
		Client.AccountBalance = clsInputValidate<double>::ReadDblNumber();

	}
public:
	static void ShowAddNewClientSceen()
	{

		if (!CheckAccessRights(clsUser::enPermissions::pAddNewClient))
		{
			return;
		}
		string AccountNumber;
		_DrawScreenHeader("Add New Client Screen");
		cout << "Adding New Client:\n";
		cout << "\nEnter New Client Account Number?";
		AccountNumber = clsInputValidate<int>::ReadString();
		while (clsBankClient::IsClientExist(AccountNumber))
		{
			cout << "\nThis Client already exists! Enter another one:";
			AccountNumber = clsInputValidate<int>::ReadString();
		}
		clsBankClient NewClient = clsBankClient::GetAddNewClientObject(AccountNumber);
		_ReadClientInfo(NewClient);
		clsBankClient::enSaveResults SaveResult = NewClient.Save();
		switch (SaveResult)
		{
		case clsBankClient::svFaildEmptyObject:
		{
			cout << "\nAdd Failed because object is empty!";
			break;
		}
		
		case clsBankClient::svSucceeded:
		{
			cout << "\nAdd Succeeded :)";
			_PrintClientCard(NewClient);
			break;
		}
		
		case clsBankClient::svFaildAccountNumberExists:
		{
			cout << "\nAdd Failed because object already exists!";
			break;
		}
		
 
		}



	}



};

