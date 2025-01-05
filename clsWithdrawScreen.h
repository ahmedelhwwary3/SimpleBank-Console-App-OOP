#pragma once
#include "clsScreen.h"
using namespace std;
class clsWithdrawScreen:protected clsScreen
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
	static void ShowWithdrawScreen()
	{
		char Answer;
		double Amount;
		string AccountNumber;
		_DrawScreenHeader("Withdraw Screen");
		cout << "\nEnter Account Number?";
		AccountNumber = clsInputValidate<int>::ReadString();
		while (!clsBankClient::IsClientExist(AccountNumber))
		{
			cout << "No client is found with this account number!";
			cout << " Please enter another one?";
			AccountNumber = clsInputValidate<int>::ReadString();
		}
		clsBankClient Client = clsBankClient::Find(AccountNumber);
		_PrintClientCard(Client);
		cout << "\nEnter withdraw amount?";
		Amount = clsInputValidate<double>::ReadDblNumber();
		cout << "\nAre you sure you want to withdraw " << Amount << "?y/n?";
		cin >> Answer;
		if (Answer == 'y' || Answer == 'Y')
		{
			if (Client.Withdraw(Amount))
			{
				cout << "\nWithdraw succeeded :)";
				cout << "\nYour balance now is " << Client.AccountBalance;
			}
			else
			{
				cout << "\nWithdraw failed!";
				cout << "\nAmount exceeds balance!";
				cout << "\nYour balance is " << Client.AccountBalance;
			}
			
		}
		else
		{
			cout << "\nDeposit was cancelled!";
		}
	}



};

