
#pragma once
#include "clsScreen.h"
#include "clsInputValidate.h"
#include "clsMainMenueScreen.h"
using namespace std;
class clsLoginScreen:protected clsScreen
{
private:

	static bool _Login()
	{
		bool LoginFailed = false;
		string UserName, Password;
		short Count = 0;
		do
		{
			if (LoginFailed)
			{
				Count++;
				cout << "\nInvalid UserName or Password!";
				cout << "\nYou have (" << 3 - Count << ") trial(s).";
			}
			if (Count == 3)
				return false;
			cout << "\nEnter UserName:";
			UserName = clsInputValidate<int>::ReadString();
			cout << "\nEnter Password";
			Password = clsInputValidate<int>::ReadString();
			CurrentUser = clsUser::Find(UserName, Password);
			LoginFailed = CurrentUser.IsEmpty();
		} while (LoginFailed);
		CurrentUser.RegisterLogIn();
		clsMainMenueScreen::ShowMainMenueScreen();
		
		return true;
	}
public:
	static bool ShowLoginScreen()
	{
		system("cls");
		_DrawScreenHeader("\tLogin Screen");
		return(_Login());
		
	}
};