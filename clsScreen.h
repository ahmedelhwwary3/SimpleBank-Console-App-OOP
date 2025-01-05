#pragma once
#include "clsBankClient.h"
#include "Global.h"
#include "clsDate.h"
using namespace std;

class clsScreen
{
	static void _ShowAccessDeniedMessage()
	{
		cout << "\n\t\t\t--------------------------------------------\n";
		cout << "\t\t\t\tAccess Denied !!Please Contact Your Admin.\n";
		cout << "\n\t\t\t--------------------------------------------\n";
	}
protected:
	static void _DrawScreenHeader(string Title, string SubTitle = "")
	{
		cout << "\n\t\t\t\t\t------------------";
		cout << "------------------------\n";
		cout << "\t\t\t\t\t\t" << Title << endl;
		if (SubTitle != "")
		{
			cout << "\n\t\t\t\t\t" << SubTitle << endl;
		}
		cout << "\t\t\t\t\t------------------";
		cout << "------------------------\n";
		cout << "\t\t\t\t\tDate:" << clsDate::GetSystemDate().DateToString() << endl;
		cout << "\t\t\t\t\tUser:" << CurrentUser.UserName<< endl;
		cout << "\t\t\t\t\t------------------";
		cout << "------------------------\n";

	}
	static bool CheckAccessRights(clsUser::enPermissions Permissions)
	{
		if (CurrentUser.DoesHaveAccess(Permissions))
		{
			return true;
		}
		else
		{
			_ShowAccessDeniedMessage();
			return false;
		}
	}






};

