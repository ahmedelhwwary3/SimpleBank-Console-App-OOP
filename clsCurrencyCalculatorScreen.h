#pragma once
#include <iostream>
#include "clsScreen.h"
#include "clsCurrency.h"
#include "clsInputValidate.h"

class clsCurrencyCalculatorScreen :protected clsScreen

{
private:

  
    static  void _PrintCurrencyCard(clsCurrency Currency, string Title = "Currency Card:")
    {

        cout << "\n" << Title << "\n";
        cout << "_____________________________\n";
        cout << "\nCountry       : " << Currency.Country();
        cout << "\nCode          : " << Currency.CurrencyCode();
        cout << "\nName          : " << Currency.CurrencyName();
        cout << "\nRate(1$) =    : " << Currency.Rate();
        cout << "\n_____________________________\n\n";

    }
    static string _ReadExistedCurrencyCode(string CurrencyCode)
    {
        CurrencyCode = clsInputValidate<int>::ReadString();
        while (!clsCurrency::IsCurrencyExist(CurrencyCode))
        {
            cout << "\nNo Currecy found with this Code!";
            cout << " Enter another code?";
            CurrencyCode = clsInputValidate<int>::ReadString();
        }
        return CurrencyCode;
    }
    static void _PrintConvertCalculationResults(string CurrencyFrom, string CurrencyTo,double Amount)
    {
        clsCurrency Cur1 = clsCurrency::FindByCode(CurrencyFrom);
        clsCurrency Cur2 = clsCurrency::FindByCode(CurrencyTo);
        _PrintCurrencyCard(Cur1,"Currenct From Card");
        cout << endl << endl;
        _PrintCurrencyCard(Cur2, "Currenct To Card");
        cout << endl << endl;
        cout << "\nYou want to convert " << Amount << " " << CurrencyFrom << " to " << CurrencyTo << endl;
        cout << "\nConvert Result =" << Cur1.ConvertToOtherCurrency(Amount, Cur2);
    }
public:

    static void ShowCurrencyCalculatorScreen()
    {
        string CurrencyFrom, CurrencyTo;
        double Amount;
        _DrawScreenHeader("Currency Calculator Screen");
        cout << "\nEnter Currency You want to convert from:";
        CurrencyFrom = _ReadExistedCurrencyCode(CurrencyFrom);
        cout << "\nEnter Currency You want to convert to:";
        CurrencyTo = _ReadExistedCurrencyCode(CurrencyTo);
        cout << "\nEnter Amount You want to convert:";
        Amount = clsInputValidate<double>::ReadDblNumber();
        _PrintConvertCalculationResults(CurrencyFrom,CurrencyTo,Amount);

    }
};

