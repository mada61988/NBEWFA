using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Management;
namespace NBEWindowsFormApplication
{
    internal class Class2
    {
        public bool btnCheck(bool ipv4Check, bool subnetMaskCheck, bool defaultGatewayCheck, bool portNumberCheck, bool terminalIdCheck, bool cameraMachineNumberCheck, bool computerNameCheck, bool remotePeerCheck)
        {
            if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck )
            {
                return true;
            }
            else return false;
        }
        public void executeCmd()
        {
            Process process2 = new Process();
            process2.StartInfo.FileName = "cmd.exe";
            process2.StartInfo.CreateNoWindow = false;
            process2.StartInfo.RedirectStandardInput = true;
            process2.StartInfo.RedirectStandardOutput = false;
            process2.StartInfo.UseShellExecute = false;
            process2.Start();
            process2.StandardInput.WriteLine("ipconfig");
            process2.StandardInput.Flush();
            // process2.StandardInput.Close();
            process2.WaitForExit();
        }
        public void changeIpAddress(string ipv4Address, string defaultGateWay, string subnetMask) {

            Process process2 = new Process();
            process2.StartInfo.FileName = "cmd.exe";
            process2.StartInfo.CreateNoWindow = false;
            process2.StartInfo.RedirectStandardInput = true;
            process2.StartInfo.RedirectStandardOutput = false;
            process2.StartInfo.UseShellExecute = false;
            process2.Start();
            process2.StandardInput.WriteLine("ipconfig");
            process2.StandardInput.WriteLine($"netsh interface ipv4 set address name=\"Ethernet\" static {ipv4Address} {subnetMask} {defaultGateWay}");
            // process2.StandardInput.WriteLine($"netsh interface ipv4 set dns name=\"Ethernet\" static");

            process2.StandardInput.Flush();
            // process2.StandardInput.Close();
            process2.WaitForExit();
        }
        public void changePortNumber(string regkeyPath, string portNumber) {


            string path = regkeyPath + @"\ProTopas\CurrentVersion\CCOPEN\COMMUNICATION\TCPIP\PROJECT";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            key.SetValue("PORTNUMBER", portNumber);
            key.Close();
        }
        public void changeTerminalId(string regkeyPath, string terminalId)
        {
            string path = regkeyPath + @"\PROAGENT\CURRENTVERSION\SSTP";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            key.SetValue("TerminalID", terminalId);
            key.Close();

        }
        public void changeCameraMachineNumber(string regkeyPath, string cameraMachineNumber) {
            string path = regkeyPath + @"\ProTopas\CurrentVersion\CCOPEN\PROTOCOL\PARAMETER";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            key.SetValue("CAMERA_MACHINE_NO", cameraMachineNumber);
            key.Close();
        }



        public void changeRemotePeer(string regkeyPath, string remotePeer)
        {
            string path = regkeyPath + @"\ProTopas\CurrentVersion\CCOPEN\COMMUNICATION\TCPIP\PROJECT";
            
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            key.SetValue("REMOTEPEER", remotePeer);
            key.Close();

        }



        public void changeComputerName(string newName) {
            Process process2 = new Process();
            process2.StartInfo.FileName = "cmd.exe";
            process2.StartInfo.CreateNoWindow = false;
            process2.StartInfo.RedirectStandardInput = true;
            process2.StartInfo.RedirectStandardOutput = false;
            process2.StartInfo.UseShellExecute = false;
            process2.Start();
            process2.StandardInput.WriteLine("ipconfig");
            //process2.StandardInput.WriteLine($"netsh interface ipv4 set address name=\"Ethernet\" static {ipv4Address} {subnetMask} {defaultGateWay}");
            // process2.StandardInput.WriteLine($"netsh interface ipv4 set dns name=\"Ethernet\" static");
            process2.StandardInput.WriteLine($"wmic computersystem where name=\"%computername%\" call rename name=\"{newName}\"");

            process2.StandardInput.Flush();
            // process2.StandardInput.Close();
            process2.WaitForExit();

        }
        public void changeLocalPort(string regkeyPath , string localPort) {
            string path = regkeyPath + @"\ProTopas\CurrentVersion\CCOPEN\COMMUNICATION\TCPIP\PROJECT";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            key.SetValue("LOCALPORT", localPort);
            key.Close();
        }
        public string[] getCashTypes(string regkeyPath) {
            string path = regkeyPath + @"\ProTopas\CurrentVersion\LYNXPAR\CASH_DISPENSER";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            string type1= key.GetValue("VALUE_1").ToString();
            string type2 = key.GetValue("VALUE_2").ToString();
            string type3 = key.GetValue("VALUE_3").ToString();
            string type4 = key.GetValue("VALUE_4").ToString();
            string[] types = { type1, type2, type3, type4 };
            return types;

        }
        public string[] getCurrencies(string regkeyPath) {
            string path = regkeyPath + @"\ProTopas\CurrentVersion\LYNXPAR\CASH_DISPENSER";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            string curr1= key.GetValue("CURRENCY_1").ToString();
            string curr2 = key.GetValue("CURRENCY_2").ToString();
                string curr3 = key.GetValue("CURRENCY_3").ToString();
            string curr4 = key.GetValue("CURRENCY_4").ToString();
            string[] currencies = { curr1, curr2, curr3, curr4 };
            return currencies;
        }

    }
}
