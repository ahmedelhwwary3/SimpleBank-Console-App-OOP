using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PresentationLayer.Global
{
    public class clsValidation
    {
        public static bool IsValidEmail(string email)
        {
            // Regular expression for validating an email
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Use Regex.IsMatch to check if the email matches the pattern
            return Regex.IsMatch(email, pattern);
        }
        public static bool IsNumber(string NumberText)
        {
            foreach (char c in NumberText)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }




    }
}
