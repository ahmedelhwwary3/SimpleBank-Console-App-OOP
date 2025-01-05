using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
namespace PresentationLayer.Global
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;
        public static void GetStoredCredentialsFromFile(ref string UserName,ref string Password)
        {
            string FilePath = @"F:\Programming\C#\Course19 (Full Project)\Credentials.txt";
            if (!File.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            using (StreamReader sr=new StreamReader(FilePath))
            {
                UserName= sr.ReadLine();
                Password= sr.ReadLine();
                sr.Close();
            }
            


        }
        public static bool GetStoredCredentialsFromRegistry(ref string UserName, ref string Password)
        {

            string Path = @"HKEY_CURRENT_USER\Software\MyCredentials";
            try
            {
                string LoggedUserName = "LoggedUserName";
                string LoggedPassword = "LoggedPassword";
                UserName = Registry.GetValue(Path,LoggedUserName,null)as string;
                Password = Registry.GetValue(Path, LoggedPassword, null) as string;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static bool RememberCredentialsTotxtFile(string UserName, string Password)
        {
            string FilePath = @"F:\Programming\C#\Course19 (Full Project)\Credentials.txt";
            if (!File.Exists(FilePath))
            {
                return false;
            }
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.WriteLine(UserName);
                sw.WriteLine(Password);
                sw.Close();
            }
            return  true;
        }
        public static bool RememberCredentialsToRegistry(string UserName, string Password)
        {
            string Path = @"HKEY_CURRENT_USER\Software\MyCredentials";
            try
            {
                string LoggedPassword = "LoggedPassword";
                string LoggedUserName = "LoggedUserName";
                Registry.SetValue(Path,LoggedUserName, UserName, RegistryValueKind.String);
                Registry.SetValue(Path,LoggedPassword, Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }






    }
}
