 
#pragma once
#include <iostream>
#include <string>
#include "clsString.h"
#include "clsDate.h"

template<typename T>
class clsInputValidate
{

public:

	static bool IsNumberBetween(T Number, T From, T To)
	{
		if (Number >= From && Number <= To)
			return true;
		else
			return false;
	}



	static bool IsDateBetween(T Date, T From, T To)
	{
		//Date>=From && Date<=To
		if ((clsDate::IsDate1AfterDate2(Date, From) || clsDate::IsDate1EqualDate2(Date, From))
			&&
			(clsDate::IsDate1BeforeDate2(Date, To) || clsDate::IsDate1EqualDate2(Date, To))
			)
		{
			return true;
		}

		//Date>=To && Date<=From
		if ((clsDate::IsDate1AfterDate2(Date, To) || clsDate::IsDate1EqualDate2(Date, To))
			&&
			(clsDate::IsDate1BeforeDate2(Date, From) || clsDate::IsDate1EqualDate2(Date, From))
			)
		{
			return true;
		}

		return false;
	}


	static T ReadShortNumber(string ErrorMessage = "Invalid Number, Enter again\n")
	{
		short Number;
		while (!(cin >> Number)) {
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			cout << ErrorMessage;
		}
		return Number;
	}

	static T ReadIntNumber(string ErrorMessage = "Invalid Number, Enter again\n")
	{
		int Number;
		while (!(cin >> Number)) {
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			cout << ErrorMessage;
		}
		return Number;
	}

	static T ReadShortNumberBetween(T From, T To, string ErrorMessage = "Number is not within range, Enter again:\n")
	{
		int Number = ReadShortNumber();

		while (!IsNumberBetween(Number, From, To))
		{
			cout << ErrorMessage;
			Number = ReadShortNumber();
		}
		return Number;
	}

	static T ReadIntNumberBetween(T From, T To, string ErrorMessage = "Number is not within range, Enter again:\n")
	{
		T Number = ReadIntNumber();

		while (!IsNumberBetween(Number, From, To))
		{
			cout << ErrorMessage;
			Number = ReadIntNumber();
		}
		return Number;
	}

	static T ReadFloatNumber(string ErrorMessage = "Invalid Number, Enter again\n")
	{
		T Number;
		while (!(cin >> Number)) {
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			cout << ErrorMessage;
		}
		return Number;
	}

	static T ReadFloatNumberBetween(T From, T To, string ErrorMessage = "Number is not within range, Enter again:\n")
	{
		T Number = ReadFloatNumber();

		while (!IsNumberBetween(Number, From, To)) {
			cout << ErrorMessage;
			Number = ReadDblNumber();
		}
		return Number;
	}

	static T ReadDblNumber(string ErrorMessage = "Invalid Number, Enter again\n")
	{
		T Number;
		while (!(cin >> Number)) {
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			cout << ErrorMessage;
		}
		return Number;
	}

	static T ReadDblNumberBetween(T From, T To, string ErrorMessage = "Number is not within range, Enter again:\n")
	{
		T Number = ReadDblNumber();

		while (!IsNumberBetween(Number, From, To)) {
			cout << ErrorMessage;
			Number = ReadDblNumber();
		}
		return Number;
	}

	static bool IsValideDate(clsDate Date)
	{
		return	clsDate::IsValidDate(Date);
	}

	static string ReadString()
	{
		string  S1 = "";
		// Usage of std::ws will extract allthe whitespace character
		getline(cin >> ws, S1);
		return S1;
	}


};

