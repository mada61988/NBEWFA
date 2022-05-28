using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBEWindowsFormApplication
{

    internal class Class3
    {
        int outputValue = 0;
        public string getCashProfileName(string [] cashProfile) {
        //    int type1 = int.TryParse(cashProfile[0], out outputValue) ? int.Parse(cashProfile[0]) : -1;
         //   int type2 = int.TryParse(cashProfile[1], out outputValue) ? int.Parse(cashProfile[1]) : -1;
         //   int type3 = int.TryParse(cashProfile[2], out outputValue) ? int.Parse(cashProfile[2]) : -1;
          //  int type4 = int.TryParse(cashProfile[3], out outputValue) ? int.Parse(cashProfile[3]) : -1;
            string type1 = cashProfile[0];
            string type2 = cashProfile[1];  
            string type3 = cashProfile[2];      
            string type4 = cashProfile[3];  

            if (type1 == "200" && type2=="20" && type3=="50" && type4=="100") {
                return "AMCR";
            }
            if (type1 == "10" && type2 == "50" && type3 == "100" && type4 == "200")
            {
                return "New Forex";
            }

            if (type1 == "5" && type2 == "10" && type3 == "50" && type4 == "200")
            {
                return "Old Forex";
            }
            if (type1 == "20" && type2 == "50" && type3 == "100" && type4 == "200")
            {
                return "Cash";
            }
            else return @"N\A";
        }

        public void insertCashProfile(string regKeyPath,string type1,string type2, string type3, string type4) {
            string path = regKeyPath + @"\ProTopas\CurrentVersion\LYNXPAR\CASH_DISPENSER";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            key.SetValue("VALUE_1", type1);
            key.SetValue("VALUE_2", type2);
            key.SetValue("VALUE_3", type3);
            key.SetValue("VALUE_4", type4);
            key.Close();
        }

       
    
    
    
    }
}
