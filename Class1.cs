using System;
using System.Collections.Generic;
using System.Text;

namespace NBEWindowsFormApplication
{
    internal class Class1
    {
        public bool btnCheck(bool ipv4Check, bool subnetMaskCheck, bool defaultGatewayCheck, bool portNumberCheck, bool terminalIdCheck, bool cameraMachineNumberCheck, bool computerNameCheck)
        {
            if (ipv4Check || subnetMaskCheck || defaultGatewayCheck || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck)
            {
                return true;
            }
            else return false;
        }
    }
}
