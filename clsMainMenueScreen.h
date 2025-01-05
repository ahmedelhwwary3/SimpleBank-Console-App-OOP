#pragma once
#include "clsScreen.h"
#include "iomanip"
#include "clsShowClientsScreen.h"
#include "clsAddNewClientScreen.h"
#include "clsInputValidate.h"
#include "clsUpdateClientScreen.h"
#include "clsDeleteClientScreen.h"
#include "clsFindClientScreen.h"
#include "clsTransactionsScreen.h"
#include "clsLoginRegisterScreen.h"
#include "clsManageUsersScreen.h"
#include "clsCurrencyExchangeMainScreen.h"
using namespace std;
class clsMainMenueScreen:protected clsScreen
{
private:
	enum enMainMenueOption
	{
		eListClients = 1, eAddNewClient = 2, eDeleteClient = 3,
		eUpdateClient = 4, eFindClient = 5, eShowTransactionsMenue = 6,
		eManageUsers = 7, eLoginRegister = 8, eCurrncyExchange = 9, eExit = 10

	};












	static void _ShowListClients()
	{
		clsShowClientsScreen::ShowClientsList();
	
	}
	static void _ShowAddNewClientScreen()
	{
		clsAddNewClientScreen::ShowAddNewClientSceen();
	}
	static void _ShowDeleteClientScreen()
	{
		clsDeleteClientScreen::ShowDeleteClientScreen();
	}
	static void _ShowUpdateClientScreen()
	{
		clsUpdateClientScreen::ShowUpdateClientScreen();
	}
	static void _GoBackToMainMenue()
	{
		cout << "\nPress any key to go back to main menue...\n";
		system("pause>0");
		ShowMainMenueScreen();
	}
	static void _LogOut()
	{
		CurrentUser = clsUser::Find("", "");
		//it will go back to login while loop
	}
	static void _ShowFindClientScreen()
	{
		clsFindClientScreen::ShowFindClientScreen();
	}
	static void _ShowTransactionsScreen()
	{
		clsTransactionsScreen::ShowTransactionsMenue();
	}
	static void _ShowManageUsersScreen()
	{
		clsManageUsersScreen::ShowManageUsersMenue();
	}
	static void _ShowLoginRegisterScreen()
	{
		clsLoginRegisterScreen::ShowLoginRegisterScreen();
	}
	static void _ShowCurrncyExchangeScreen()
	{
		clsCurrencyExchangeMainScreen::ShowCurrenciesMenue();

	}
	static void _PerformMainMenueOption(enMainMenueOption Option)
	{
		switch (Option)
		{
		case clsMainMenueScreen::eListClients:
		{
			system("cls");
			_ShowListClients();
			_GoBackToMainMenue();
			break;
		}
		case clsMainMenueScreen::eAddNewClient:
		{
			system("cls");
			_ShowAddNewClientScreen();
			_GoBackToMainMenue();
			break;
		}
		case clsMainMenueScreen::eDeleteClient:

		{
			system("cls");
			_ShowDeleteClientScreen();
			_GoBackToMainMenue();
			break;
		}

		case clsMainMenueScreen::eUpdateClient:
		{
			system("cls");
			_ShowUpdateClientScreen();
			_GoBackToMainMenue();
			break;
		}
		case clsMainMenueScreen::eFindClient:
		{
			system("cls");
			_ShowFindClientScreen();
			_GoBackToMainMenue();
			break;
		}


		case clsMainMenueScreen::eShowTransactionsMenue:


		{
			system("cls");
			_ShowTransactionsScreen();
			_GoBackToMainMenue();
			break;
		}
		case clsMainMenueScreen::eManageUsers:



		{
			system("cls");
			_ShowManageUsersScreen();
			_GoBackToMainMenue();
			break;
		}
		case clsMainMenueScreen::eLoginRegister:
		{
			system("cls");
			_ShowLoginRegisterScreen();
			_GoBackToMainMenue();
			break;
		}
		case clsMainMenueScreen::eCurrncyExchange:
		{
			system("cls");
			_ShowCurrncyExchangeScreen();
			_GoBackToMainMenue();
			break;
		}
		case clsMainMenueScreen::eExit:
		{
			_LogOut();
			break;
		}

		}
	}
	static short _ReadMainMenueOption()
	{
		short Option;
		cout << "\n\t\t\t\t\tChoose what do you want to do? [1 to 10]?";
		Option = clsInputValidate<short>::ReadShortNumberBetween(1,10);
		return Option;
	}
public:


	static void ShowMainMenueScreen()
	{
		system("cls");
		_DrawScreenHeader("\tMain Menue Screen");
		cout << "\n\t\t\t\t\t==========================================\n";
		cout << "\t\t\t\t\t\t\tMain Menue";
		cout << "\n\t\t\t\t\t==========================================\n";
		cout << "\t\t\t\t\t[1] Show Client List.\n";
		cout << "\t\t\t\t\t[2] Add New Client.\n";
		cout << "\t\t\t\t\t[3] Delete Client.\n";
		cout << "\t\t\t\t\t[4] Update Client Info.\n";
		cout << "\t\t\t\t\t[5] Find Client.\n";
		cout << "\t\t\t\t\t[6] Transactions.\n";
		cout << "\t\t\t\t\t[7] Manage Users.\n";
		cout << "\t\t\t\t\t[8] Login Register.\n";
		cout << "\t\t\t\t\t[9] Currency Exchange.\n";
		cout << "\t\t\t\t\t[10] Logout.\n";
		cout << "\n\t\t\t\t\t==========================================\n";
		_PerformMainMenueOption((enMainMenueOption)_ReadMainMenueOption());

	}





};

