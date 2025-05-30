#pragma once
#include "clsScreen.h"
#include "clsDepositScreen.h"
#include "clsWithdrawScreen.h"
#include "clsTransferScreen.h"
#include "clsTransferLogScreen.h"
#include "clsTotalBalancesScreen.h"
using namespace std;
class clsTransactionsScreen:protected clsScreen
{



    enum enTransactionsMenueOptions {
        eDeposit = 1, eWithdraw = 2,
        eShowTotalBalance = 3, eTransfer = 4, eTransferLog = 5,
        eShowMainMenue = 6

    };

    static short ReadTransactionsMenueOption()
    {
        cout << setw(37) << left << "" << "   Choose what do you want to do? [1 to 6]? ";
        short Choice = clsInputValidate<short>::ReadShortNumberBetween(1, 6, "Enter Number between 1 to 6? ");
        return Choice;
    }
    static void _ShowTransferScreen()
    {
        clsTransferScreen::ShowTransferScreen();
    }
    static void _GoBackToTransactionsMenue()
    {
        cout << "\nPress any key to go back to transactions menue...\n";
        system("pause>0");
        ShowTransactionsMenue();
    
    }
    static void _ShowDepositScreen()
    {
        clsDepositScreen::ShowDepositScreen();
    }
    static void _ShowWithdrawScreen()
    {
        clsWithdrawScreen::ShowWithdrawScreen();
    }
    static void _ShowTotalBalancesScreen()
    {
        clsTotalBalancesScreen::ShowTotalBalancesScreen();
    }
    static void _ShowTransferLogScreen()
    {
        clsTransferLogScreen::ShowTransferLogScreen();
    }
    static void _PerformTransactionsMenueOption(enTransactionsMenueOptions TransactionsMenueOption)
    {
        switch (TransactionsMenueOption)
        {
        case enTransactionsMenueOptions::eDeposit:
        {
            system("cls");
            _ShowDepositScreen();
            _GoBackToTransactionsMenue();
            break;
        }

        case enTransactionsMenueOptions::eWithdraw:
        {
            system("cls");
            _ShowWithdrawScreen();
            _GoBackToTransactionsMenue();
            break;
        }


        case enTransactionsMenueOptions::eShowTotalBalance:
        {
            system("cls");
            _ShowTotalBalancesScreen();
            _GoBackToTransactionsMenue();
            break;
        }

        case enTransactionsMenueOptions::eTransfer:
        {
            system("cls");
            _ShowTransferScreen();
            _GoBackToTransactionsMenue();
            break;
        }


        case enTransactionsMenueOptions::eTransferLog:
        {
            system("cls");
            _ShowTransferLogScreen();
            _GoBackToTransactionsMenue();
            break;
        }

        case enTransactionsMenueOptions::eShowMainMenue:
        {

            //do nothing here the main screen will handle it :-) ;

        }
        }
    }
public:
	static void ShowTransactionsMenue()
	{
        if (!CheckAccessRights(clsUser::enPermissions::pTranactions))
        {
            return;// this will exit the function and it will not continue
        }

        system("cls");
        _DrawScreenHeader("\tTransactions Screen");

        cout << setw(37) << left << "" << "   ===========================================\n";
        cout << setw(37) << left << "" << "\t\t  Transactions Menue\n";
        cout << setw(37) << left << "" << "   ===========================================\n";
        cout << setw(37) << left << "" << "\t   [1] Deposit.\n";
        cout << setw(37) << left << "" << "\t   [2] Withdraw.\n";
        cout << setw(37) << left << "" << "\t   [3] Total Balances.\n";
        cout << setw(37) << left << "" << "\t   [4] Transfer.\n";
        cout << setw(37) << left << "" << "\t   [5] Transfer Log.\n";
        cout << setw(37) << left << "" << "\t   [6] Main Menue.\n";
        cout << setw(37) << left << "" << "   ===========================================\n";

        _PerformTransactionsMenueOption((enTransactionsMenueOptions)ReadTransactionsMenueOption());



	}


};

