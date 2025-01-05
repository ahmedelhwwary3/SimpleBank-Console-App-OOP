#pragma once
#include "clsScreen.h"
#include "clsBankClient.h"
using namespace std;


class clsTransferLogScreen:protected clsScreen
{
private:

    static void _PrintTransferLogRecordLine(clsBankClient::stTransferLogRecord TransferLogRecord)
    {

        cout << setw(8) << left << "" << "| " << setw(23) << left << TransferLogRecord.Date;
        cout << "| " << setw(8) << left << TransferLogRecord.SourceAccNumber;
        cout << "| " << setw(8) << left << TransferLogRecord.DestinationAccNumber;
        cout << "| " << setw(8) << left << TransferLogRecord.Amount;
        cout << "| " << setw(10) << left << TransferLogRecord.SourceAccBalance;
        cout << "| " << setw(10) << left << TransferLogRecord.DestinationAccBalance;
        cout << "| " << setw(8) << left << TransferLogRecord.UserName;


    }
public:
	static void ShowTransferLogScreen()
	{
		vector<clsBankClient::stTransferLogRecord>vTransferLogRecords = clsBankClient::LoadTransferLogRecords();
		string Title, SubTitle;
		Title = "Transfer Log Screen\n\t";
		SubTitle = to_string(vTransferLogRecords.size()) + "Log Record(s).";
		_DrawScreenHeader(Title, SubTitle);
        cout << setw(8) << left << "" << "\n\t_______________________________________________________";
        cout << "_________________________________________\n" << endl;

        cout << setw(8) << left << "" << "| " << left << setw(23) << "Date/Time";
        cout << "| " << left << setw(8) << "s.Acct";
        cout << "| " << left << setw(8) << "d.Acct";
        cout << "| " << left << setw(8) << "Amount";
        cout << "| " << left << setw(10) << "s.Balance";
        cout << "| " << left << setw(10) << "d.Balance";
        cout << "| " << left << setw(8) << "User";

        cout << setw(8) << left << "" << "\n\t_______________________________________________________";
        cout << "_________________________________________\n" << endl;

        if (vTransferLogRecords.size() == 0)
            cout << "\t\t\t\tNo Transfers Available In the System!";
        else

            for (clsBankClient::stTransferLogRecord Record : vTransferLogRecords)
            {

                _PrintTransferLogRecordLine(Record);
                cout << endl;
            }

        cout << setw(8) << left << "" << "\n\t_______________________________________________________";
        cout << "_________________________________________\n" << endl;






	}



};

