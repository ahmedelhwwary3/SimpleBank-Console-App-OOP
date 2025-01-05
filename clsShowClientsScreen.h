#pragma once

#include "clsScreen.h"
#include "clsBankClient.h"
using namespace std;
class clsShowClientsScreen:protected clsScreen
{
private:



	static void _PrintClientLine(clsBankClient Client)
	{
		cout << "\t\t" << left << "|" << setw(19) << Client.FullName();
		cout << left << "|" << setw(18) << Client.Email;
		cout << left << "|" << setw(11) << Client.Phone;
		cout << left << "|" << setw(14) << Client.AccountNumber;
		cout << left << "|" << setw(14)  << Client.PinCode;
		cout << left << "|" << setw(8) << Client.AccountBalance;

	}
public:
	static void ShowClientsList()
	{

		if (!
			CheckAccessRights(clsUser::enPermissions::pListClients))
		{
			return;
		}
		vector<clsBankClient>vClients = clsBankClient::GetAllClientsList();
		string Title, SubTitle;
		Title = "Client List";
		SubTitle = to_string(vClients.size())+" Client(s).";
		_DrawScreenHeader(Title,SubTitle);
		cout << "\n\t\t---------------------------------------------------";
		cout << "----------------------------------------------\n";
		cout << "\t\t" << left << setw(20) << "|Client Name";
		cout << left << setw(19) << "|Email";
		cout << left << setw(12) << "|Phone";
		cout << left << setw(15) << "|Account Number";
		cout << left << setw(15) << "|Pincode";
		cout << left << setw(9) << "|Account Balance";
		cout << "\n\t\t--------------------------------------------";
		cout << "-----------------------------------------------------\n";

		if (vClients.size() == 0)
		{
			cout << "\t\t\t\t\t\tNo Clients Available!\n";
		}
		else
		{
			for (clsBankClient Client : vClients)
			{
				_PrintClientLine(Client);
				cout << endl;
			}
		}
		cout << "\n\t\t--------------------------------------------";
		cout << "-----------------------------------------------------\n";





	}

};

