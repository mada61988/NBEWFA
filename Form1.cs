/***********************************************************************/
/* Developer: Mohamed Ayman Shokry */
/* Number   :     01129805828      */
/*    Date  :       14.9.2022      */
/*    Bank  : NationalBankOfEgypt  */
/*  Version :         04           */
/***********************************************************************/

/********************** VERSION-04 ******************************/
/*  Fixes    :   * ComputerName entery box is not having the same value of TerminalID box.
                 * Gateway Third Box value has only one integer, it is updated to 3 integers. 
                 * Currency Box in Cashprofiles section was not dynamic, now it's changing with the current value in registry.
                 * Message Box Which appeared when importing is updated, displaying the imported data only.
/***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace NBEWindowsFormApplication
{

    public partial class Form1 : Form
    {

        Class2 publicClass = new Class2();
        Class3 publicClass2 = new Class3();

        bool ipv4Check = false;
        bool subnetMaskCheck = false;
        bool defaultGatewayCheck = false;
        bool portNumberCheck = false;
        bool terminalIdCheck = false;
        bool cameraMachineNumberCheck = false;
        bool computerNameCheck = false;
        bool cashProfileCheck = false;
        int outputValue = 0;
        bool remotePeerCheck = false;
        string[] currentCashTypes = { };
        string currentCashProfileName = "";
        string[] currentCurrencies = { };
        string[] cashTypeAMCR = { };
        string[] AMCR = {"200","20","50","100" };
        string[] newForex = { "10", "50", "100", "200" };
        string[] oldForex = { "5", "10", "50", "200" };
        string[] cash = { "20", "50", "100", "200" };
        string regkeyPath = "";
        string probaseRegkeyPath = "";
        
        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
          

        }
        public void changeSelected() { 
        
        }
        private void label2_Click(object sender, EventArgs e)
        {
            label1.AutoSize = false;
            label1.Height = 2;
            label1.BorderStyle = BorderStyle.Fixed3D;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            importBtn.Enabled = false;
            button2.Enabled = false;
            ipv4Success.Visible = false;
            ipv4FalseLogo.Visible = false;
            defaultGatewayFalseLogo.Visible = false;
            defaultGatewaySuccessLogo.Visible = false;
            portNumberFalseLogo.Visible = false;
            portNumberSuccessLogo.Visible = false;
            terminalIdFalseLogo.Visible = false;
            terminalIdSuccessLogo.Visible = false;
            cameraMachineNumberFalseLogo.Visible = false;
            cameraMachineNumberSuccessLogo.Visible = false;
            computerNameSuccessLogo.Visible = false;
            computerNameFlaseLogo.Visible = false;
            subnetMaskFalseLogo.Visible = false;
            subnetMaskSuccessLogo.Visible = false;
            defaultGatewayFourthBox.ReadOnly = true;
            currency1Value.Text = "EGP";
            currency2Value.Text = "EGP";
            currency3Value.Text = "EGP";
            currency4Value.Text = "EGP";
            int bitmapsNumberOfItems = Directory.GetFiles(@"C:\ProTopas\BITMAPS", "*", SearchOption.TopDirectoryOnly).Length;
            bitmapsFolderItemsText.Text = $"Bitmaps Folder Items {bitmapsNumberOfItems+1}";
            bitmapsNumberOfItemsTrueCheck.Visible = bitmapsNumberOfItems == 579 ? true : false;
            bitmapsNumberOfItemsFalseCheck.Visible = bitmapsNumberOfItems == 579 ? false : true;
            bitmapsNumberOfItemsTrueCheck.Text = "\u2714";
            int audioNumberOfFiles = Directory.GetFiles(@"C:\Audio", "*", SearchOption.TopDirectoryOnly).Length;
            audioFolderItmes.Text = $"Audio Folder Items {audioNumberOfFiles}";
           audioNumberOfFilesTrueCheck.Visible = audioNumberOfFiles == 17 ? true : false;
            audioNumberOfFilesFalseCheck.Visible = audioNumberOfFiles == 17 ? false : true;
            audioNumberOfFilesTrueCheck.Text = "\u2714";
            publicClass2.getHDDData();
            
            
           // label54.ForeColor = System.Drawing.Color.Red;
            camerHDDExistsTrueCheck.Text = "\u2714";
            camerHDDExistsFalseCheck.Visible= publicClass2.socondDriveFlag?false:true;
            camerHDDExistsTrueCheck.Visible = publicClass2.socondDriveFlag ? true : false;
            cameraHDDDriveLetterTrueCheck.Text = "\u2714";
            if (publicClass2.socondDriveFlag)
            {
                cameraHDDLetterText.Text = $"Camera HDD Letter {publicClass2.secondDriveCollectedLetters[0]}";
                cameraHDDDriveLetterFalseCheck.Visible = publicClass2.secondDriveCollectedLetters[0] == "F:" ? false : true;
                cameraHDDDriveLetterTrueCheck.Visible = publicClass2.secondDriveCollectedLetters[0] == "F:" ? true : false;
                cameraHDDDriveLetterFixButton.Visible = cameraHDDDriveLetterFalseCheck.Visible == true ? true : false;
                    }
            else {
                cameraHDDDriveLetterFalseCheck.Visible = false; 
                cameraHDDDriveLetterTrueCheck.Visible = false;
                cameraHDDLetterText.Visible = false;
                cameraHDDDriveLetterFixButton.Visible = false;
            }
            
            
            
            
            
            
            string[] cashTypes = { };
            bool validator = false;
            var key1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wincor Nixdorf", true);
            var key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Wincor Nixdorf", true);
          
            
            
            
            
            
            
            
            regkeyPath = @"SOFTWARE\Wincor Nixdorf";
            if (key1 != null)
            {
                regkeyPath = "SOFTWARE\\Wincor Nixdorf";
                probaseRegkeyPath = "SOFTWARE\\WOW6432Node\\Diebold Nixdorf\\ProBase";
                validator = true;
            }
           if (key2 != null)
            {
                regkeyPath = @"SOFTWARE\WOW6432Node\Wincor Nixdorf";
                probaseRegkeyPath = "SOFTWARE\\WOW6432Node\\Diebold Nixdorf\\ProBase";
                validator = true;
            }


            if (validator) {
                string[] getTypes = publicClass.getCashTypes(regkeyPath);
                string[] getCurrencies = publicClass.getCurrencies(regkeyPath);
                currentCashTypes = getTypes;
                currentCurrencies = getCurrencies;
                type1Value.Text = getTypes[0];
                type2Value.Text = getTypes[1];
                type3Value.Text = getTypes[2];
                type4Value.Text = getTypes[3];
                currency1Value.Text = getCurrencies[0];
                currency2Value.Text = getCurrencies[1];
                currency3Value.Text = getCurrencies[2];
                currency4Value.Text = getCurrencies[3];
               currentCashProfileName= publicClass2.getCashProfileName(currentCashTypes);
           
                selectedCashProfile.Text = currentCashProfileName;
                currentCashProfile.Text = currentCashProfileName;
                
                //.Show(currentCashProfileName);
            }
            publicClass2.getIpAddress();
            publicClass2.getCurrentInfoFromRegistry(regkeyPath);
            publicClass2.getsubnetMask();
            publicClass2.getDefaultGateway();
            publicClass2.getSerialNumber(probaseRegkeyPath);
            publicClass2.getSystemName(probaseRegkeyPath);

        }

        private void ipv4FirstBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
     (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void ipv4SocondBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
     (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void ipv4ThirdBox_TextChanged(object sender, EventArgs e)
        {


            defaultGatewayFirstBox.Text = ipv4FirstBox.Text;
            int outputValue = 0;
            /*
               int ipv4FirstBoxValue = int.TryParse(ipv4FirstBox.Text, out outputValue) ? int.Parse(ipv4FirstBox.Text) : -1;
              int ipv4SocondBoxValue = int.TryParse(ipv4SocondBox.Text, out outputValue) ? int.Parse(ipv4SocondBox.Text) : -1;
              int ipv4ThirdBoxValue = int.TryParse(ipv4ThirdBox.Text, out outputValue) ? int.Parse(ipv4ThirdBox.Text) : -1;
              int ipv4FourthBoxValue = int.TryParse(ipv4FourthBox.Text, out outputValue) ? int.Parse(ipv4FourthBox.Text) : -1;

              */
            string ipv4FirstBoxValue = ipv4FirstBox.Text;
            string ipv4SocondBoxValue = ipv4SocondBox.Text;
            string ipv4ThirdBoxValue = ipv4ThirdBox.Text;
            string ipv4FourthBoxValue = ipv4FourthBox.Text;

            if (ipv4FirstBoxValue != "" && ipv4SocondBoxValue != "" && ipv4ThirdBoxValue != "" && ipv4FourthBoxValue != "")
            {
                // importBtn.Enabled = true;
                ipv4Check = true;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = true;
                ipv4FalseLogo.Visible = false;
            }
            else
            {
                // importBtn.Enabled = false;
                ipv4Check = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = true;
            }
            if (ipv4FirstBoxValue == "" && ipv4SocondBoxValue == "" && ipv4ThirdBoxValue == "" && ipv4FourthBoxValue == "")
            {
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = false;
            }

            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }

        }

        private void ipv4ThirdBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void ipv4FourthBox_TextChanged(object sender, EventArgs e)
        {


            defaultGatewayFirstBox.Text = ipv4FirstBox.Text;
            int outputValue = 0;
            /*
               int ipv4FirstBoxValue = int.TryParse(ipv4FirstBox.Text, out outputValue) ? int.Parse(ipv4FirstBox.Text) : -1;
              int ipv4SocondBoxValue = int.TryParse(ipv4SocondBox.Text, out outputValue) ? int.Parse(ipv4SocondBox.Text) : -1;
              int ipv4ThirdBoxValue = int.TryParse(ipv4ThirdBox.Text, out outputValue) ? int.Parse(ipv4ThirdBox.Text) : -1;
              int ipv4FourthBoxValue = int.TryParse(ipv4FourthBox.Text, out outputValue) ? int.Parse(ipv4FourthBox.Text) : -1;

              */
            string ipv4FirstBoxValue = ipv4FirstBox.Text;
            string ipv4SocondBoxValue = ipv4SocondBox.Text;
            string ipv4ThirdBoxValue = ipv4ThirdBox.Text;
            string ipv4FourthBoxValue = ipv4FourthBox.Text;

            if (ipv4FirstBoxValue != "" && ipv4SocondBoxValue != "" && ipv4ThirdBoxValue != "" && ipv4FourthBoxValue != "")
            {
                // importBtn.Enabled = true;
                ipv4Check = true;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = true;
                ipv4FalseLogo.Visible = false;
            }
            else
            {
                // importBtn.Enabled = false;
                ipv4Check = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = true;
            }
            if (ipv4FirstBoxValue == "" && ipv4SocondBoxValue == "" && ipv4ThirdBoxValue == "" && ipv4FourthBoxValue == "")
            {
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = false;
            }

            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }



        }







        private void ipv4FourthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void defaultGatewayFirstBox_TextChanged(object sender, EventArgs e)
        {
                    int defaultGatewayFirstBoxValue = int.TryParse(defaultGatewayFirstBox.Text, out outputValue) ? int.Parse(defaultGatewayFirstBox.Text) : -1;
            int defaultGatewaySocondBoxValue = int.TryParse(defaultGatewaySocondBox.Text, out outputValue) ? int.Parse(defaultGatewaySocondBox.Text) : -1;
            int defaultGatewayThirdBoxValue = int.TryParse(defaultGatewayThirdBox.Text, out outputValue) ? int.Parse(defaultGatewayThirdBox.Text) : -1;
            int defaultGatewayFourthBoxValue = int.TryParse(defaultGatewayFourthBox.Text, out outputValue) ? int.Parse(defaultGatewayFourthBox.Text) : -1;
            if (defaultGatewayFirstBoxValue != -1 && defaultGatewaySocondBoxValue != -1 && defaultGatewayThirdBoxValue != -1 && defaultGatewayFourthBoxValue != -1)
            {
                defaultGatewayCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = true;
                defaultGatewayFalseLogo.Visible = false;

            }
            else
            {
                defaultGatewayCheck = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = true;
            }

            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }

            if (defaultGatewayFirstBoxValue == -1 && defaultGatewaySocondBoxValue == -1 && defaultGatewayThirdBoxValue == -1 && defaultGatewayFourthBoxValue == -1)
            {
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = false;
            }

        }

        private void subnetMaskFirstBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void subnetMaskSocondBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void subnetMaskThirdBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void subnetMaskFourthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void defaultGatewayFirstBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void defaultGatewaySocondBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void defaultGatewayThirdBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void defaultGatewayFourthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void portNumberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void terminalIdFirstBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void terminalIdSocondBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cameraMachineNumberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void computerNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void computerNameFIrstBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void computerNameSocondBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void terminalIdFirstBox_TextChanged(object sender, EventArgs e)
        {
            /*
             string computerNameFirstBoxValue = computerNameFirstBox.Text;
            string computerNameSocondBoxValue = computerNameSocondBox.Text;
            if (computerNameFirstBoxValue != "" && computerNameSocondBoxValue != "" && computerNameFirstBoxValue.Length ==3 && computerNameSocondBoxValue.Length==8)
           
            */
            computerNameFirstBox.Text = terminalIdFirstBox.Text;
          //  int terminalIdFirstBoxValue = int.TryParse(terminalIdFirstBox.Text, out outputValue) ? int.Parse(terminalIdFirstBox.Text) : -1;
           // int terminalIdSocondBoxValue = int.TryParse(terminalIdSocondBox.Text, out outputValue) ? int.Parse(terminalIdSocondBox.Text) : -1;
            
            string terminalIdFirstBoxValue = terminalIdFirstBox.Text;
            string terminalIdSocondBoxValue = terminalIdSocondBox.Text;

            //  if (terminalIdFirstBoxValue != -1 && terminalIdSocondBoxValue != -1 && terminalIdFirstBoxValue.ToString().Length == 3 && terminalIdSocondBoxValue.ToString().Length == 8)

            if (terminalIdFirstBoxValue != "" && terminalIdSocondBoxValue != "" && terminalIdFirstBoxValue.ToString().Length == 3 && terminalIdSocondBoxValue.ToString().Length == 8)

            {
                terminalIdCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                terminalIdSuccessLogo.Visible = true;
                terminalIdFalseLogo.Visible = false;

            }
            else
            {
                terminalIdCheck = false;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                terminalIdSuccessLogo.Visible = false;
                terminalIdFalseLogo.Visible = true;
            }
            if (terminalIdFirstBoxValue == "" && terminalIdSocondBoxValue == "") {
                terminalIdSuccessLogo.Visible = false;
                terminalIdFalseLogo.Visible = false;
            }
        }

        private void terminalIdSocondBox_TextChanged(object sender, EventArgs e)
        {
            /*
             string computerNameFirstBoxValue = computerNameFirstBox.Text;
            string computerNameSocondBoxValue = computerNameSocondBox.Text;
            if (computerNameFirstBoxValue != "" && computerNameSocondBoxValue != "" && computerNameFirstBoxValue.Length ==3 && computerNameSocondBoxValue.Length==8)
           
            */
            computerNameFirstBox.Text = terminalIdFirstBox.Text;
            computerNameSocondBox.Text = terminalIdSocondBox.Text;

            //  int terminalIdFirstBoxValue = int.TryParse(terminalIdFirstBox.Text, out outputValue) ? int.Parse(terminalIdFirstBox.Text) : -1;
            // int terminalIdSocondBoxValue = int.TryParse(terminalIdSocondBox.Text, out outputValue) ? int.Parse(terminalIdSocondBox.Text) : -1;

            string terminalIdFirstBoxValue = terminalIdFirstBox.Text;
            string terminalIdSocondBoxValue = terminalIdSocondBox.Text;

            //  if (terminalIdFirstBoxValue != -1 && terminalIdSocondBoxValue != -1 && terminalIdFirstBoxValue.ToString().Length == 3 && terminalIdSocondBoxValue.ToString().Length == 8)

            if (terminalIdFirstBoxValue != "" && terminalIdSocondBoxValue != "" && terminalIdFirstBoxValue.ToString().Length == 3 && terminalIdSocondBoxValue.ToString().Length == 8)

            {
                terminalIdCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                terminalIdSuccessLogo.Visible = true;
                terminalIdFalseLogo.Visible = false;

            }
            else
            {
                terminalIdCheck = false;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                terminalIdSuccessLogo.Visible = false;
                terminalIdFalseLogo.Visible = true;
            }
            if (terminalIdFirstBoxValue == "" && terminalIdSocondBoxValue == "")
            {
                terminalIdSuccessLogo.Visible = false;
                terminalIdFalseLogo.Visible = false;
            }
        }

        private void computerNameFIrstBox_TextChanged(object sender, EventArgs e)
        {
            string computerNameFirstBoxValue = computerNameFirstBox.Text;
            string computerNameSocondBoxValue = computerNameSocondBox.Text;
            if (computerNameFirstBoxValue != "" && computerNameSocondBoxValue != "" && computerNameFirstBoxValue.Length ==3 && computerNameSocondBoxValue.Length==8)
            {
                computerNameCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                computerNameSuccessLogo.Visible = true;
                computerNameFlaseLogo.Visible = false;
            }
            else
            {
                computerNameCheck = false;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                // computerNameSuccessLogo.Visible = true;
                // computerNameFlaseLogo.Visible = false;
            }
        }

        private void computerNameSocondBox_TextChanged(object sender, EventArgs e)
        {
            string computerNameFirstBoxValue = computerNameFirstBox.Text;
            string computerNameSocondBoxValue = computerNameSocondBox.Text;
            if (computerNameFirstBoxValue != "" && computerNameSocondBoxValue != "" && computerNameFirstBoxValue.Length == 3 && computerNameSocondBoxValue.Length == 8)
            {
                computerNameCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                computerNameSuccessLogo.Visible = true;
                computerNameFlaseLogo.Visible = false;
            }
            else
            {
                computerNameCheck = false;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                computerNameSuccessLogo.Visible = false;
                // computerNameFlaseLogo.Visible = false;
            }
        }

        private void ipv4FirstBox_TextChanged(object sender, EventArgs e)
        {
            
            defaultGatewayFirstBox.Text = ipv4FirstBox.Text;
            int outputValue = 0;
           
               int ipv4FirstBoxValue = int.TryParse(ipv4FirstBox.Text, out outputValue) ? int.Parse(ipv4FirstBox.Text) : -1;
              int ipv4SocondBoxValue = int.TryParse(ipv4SocondBox.Text, out outputValue) ? int.Parse(ipv4SocondBox.Text) : -1;
              int ipv4ThirdBoxValue = int.TryParse(ipv4ThirdBox.Text, out outputValue) ? int.Parse(ipv4ThirdBox.Text) : -1;
              int ipv4FourthBoxValue = int.TryParse(ipv4FourthBox.Text, out outputValue) ? int.Parse(ipv4FourthBox.Text) : -1;

          
          
            /*
            string ipv4FirstBoxValue = ipv4FirstBox.Text;
            string ipv4SocondBoxValue = ipv4SocondBox.Text;
            string ipv4ThirdBoxValue = ipv4ThirdBox.Text;
            string ipv4FourthBoxValue = ipv4FourthBox.Text;
            */
            if (ipv4FirstBoxValue != -1 && ipv4SocondBoxValue != -1 && ipv4ThirdBoxValue != -1 && ipv4FourthBoxValue != -1)
            {
                // importBtn.Enabled = true;
                ipv4Check = true;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = true;
                ipv4FalseLogo.Visible = false;
            }
            else
            {
                // importBtn.Enabled = false;
                ipv4Check = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = true;
            }
            if (ipv4FirstBoxValue == -1 && ipv4SocondBoxValue == -1 && ipv4ThirdBoxValue == -1 && ipv4FourthBoxValue == -1)
            {
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = false;
            }

            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }


        }

        private void ipv4SocondBox_TextChanged(object sender, EventArgs e)
        {


            defaultGatewayFirstBox.Text = ipv4FirstBox.Text;
            int outputValue = 0;
            /*
               int ipv4FirstBoxValue = int.TryParse(ipv4FirstBox.Text, out outputValue) ? int.Parse(ipv4FirstBox.Text) : -1;
              int ipv4SocondBoxValue = int.TryParse(ipv4SocondBox.Text, out outputValue) ? int.Parse(ipv4SocondBox.Text) : -1;
              int ipv4ThirdBoxValue = int.TryParse(ipv4ThirdBox.Text, out outputValue) ? int.Parse(ipv4ThirdBox.Text) : -1;
              int ipv4FourthBoxValue = int.TryParse(ipv4FourthBox.Text, out outputValue) ? int.Parse(ipv4FourthBox.Text) : -1;

              */
            string ipv4FirstBoxValue = ipv4FirstBox.Text;
            string ipv4SocondBoxValue = ipv4SocondBox.Text;
            string ipv4ThirdBoxValue = ipv4ThirdBox.Text;
            string ipv4FourthBoxValue = ipv4FourthBox.Text;

            if (ipv4FirstBoxValue != "" && ipv4SocondBoxValue != "" && ipv4ThirdBoxValue != "" && ipv4FourthBoxValue != "")
            {
                // importBtn.Enabled = true;
                ipv4Check = true;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = true;
                ipv4FalseLogo.Visible = false;
            }
            else
            {
                // importBtn.Enabled = false;
                ipv4Check = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = true;
            }
            if (ipv4FirstBoxValue == "" && ipv4SocondBoxValue == "" && ipv4ThirdBoxValue == "" && ipv4FourthBoxValue == "")
            {
                ipv4Success.Visible = false;
                ipv4FalseLogo.Visible = false;
            }

            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }


        }

        private void subnetMaskFirstBox_TextChanged(object sender, EventArgs e)
        {
            int subnetMaskFirstBoxValue = int.TryParse(subnetMaskFirstBox.Text, out outputValue) ? int.Parse(subnetMaskFirstBox.Text) : -1;
            int subnetMaskSocondBoxValue = int.TryParse(subnetMaskSocondBox.Text, out outputValue) ? int.Parse(subnetMaskSocondBox.Text) : -1;
            int subnetMaskThirdBoxValue = int.TryParse(subnetMaskThirdBox.Text, out outputValue) ? int.Parse(subnetMaskThirdBox.Text) : -1;
            int subnetMaskFourthBoxValue = int.TryParse(subnetMaskFourthBox.Text, out outputValue) ? int.Parse(subnetMaskFourthBox.Text) : -1;
            if (subnetMaskFirstBoxValue != -1 && subnetMaskSocondBoxValue != -1 && subnetMaskThirdBoxValue != -1 && subnetMaskFourthBoxValue != -1)
            {
                subnetMaskCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = true;
                subnetMaskFalseLogo.Visible = false;
            }
            else
            {
                subnetMaskCheck = false;
                //   importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = false;
                subnetMaskFalseLogo.Visible = true;

            }
        }

        private void importBtn_Click(object sender, EventArgs e)
        {


            string ComputerName = Environment.MachineName.ToString();


            



            //============ Ipv4 Box Values===============
            int ipv4FirstBoxValue = int.TryParse(ipv4FirstBox.Text, out outputValue) ? int.Parse(ipv4FirstBox.Text) : -1;
            int ipv4SocondBoxValue = int.TryParse(ipv4SocondBox.Text, out outputValue) ? int.Parse(ipv4SocondBox.Text) : -1;
            int ipv4ThirdBoxValue = int.TryParse(ipv4ThirdBox.Text, out outputValue) ? int.Parse(ipv4ThirdBox.Text) : -1;
            int ipv4FourthBoxValue = int.TryParse(ipv4FourthBox.Text, out outputValue) ? int.Parse(ipv4FourthBox.Text) : -1;
            //============ Ipv4 Box Values===============

            //============ SubnetMask Box Values===============

            int subnetMaskFirstBoxValue = int.TryParse(subnetMaskFirstBox.Text, out outputValue) ? int.Parse(subnetMaskFirstBox.Text) : -1;
            int subnetMaskSocondBoxValue = int.TryParse(subnetMaskSocondBox.Text, out outputValue) ? int.Parse(subnetMaskSocondBox.Text) : -1;
            int subnetMaskThirdBoxValue = int.TryParse(subnetMaskThirdBox.Text, out outputValue) ? int.Parse(subnetMaskThirdBox.Text) : -1;
            int subnetMaskFourthBoxValue = int.TryParse(subnetMaskFourthBox.Text, out outputValue) ? int.Parse(subnetMaskFourthBox.Text) : -1;

            //============ SubnetMask Box Values===============

            //============ Default Gateway Box Values===============
            int defaultGatewayFirstBoxValue = int.TryParse(defaultGatewayFirstBox.Text, out outputValue) ? int.Parse(defaultGatewayFirstBox.Text) : -1;
            int defaultGatewaySocondBoxValue = int.TryParse(defaultGatewaySocondBox.Text, out outputValue) ? int.Parse(defaultGatewaySocondBox.Text) : -1;
            int defaultGatewayThirdBoxValue = int.TryParse(defaultGatewayThirdBox.Text, out outputValue) ? int.Parse(defaultGatewayThirdBox.Text) : -1;
            int defaultGatewayFourthBoxValue = int.TryParse(defaultGatewayFourthBox.Text, out outputValue) ? int.Parse(defaultGatewayFourthBox.Text) : -1;
            //============ Default Gateway Box Values===============


            //============ PortNumber Box Values===============
            int portNumberBoxValue = int.TryParse(portNumberBox.Text, out outputValue) ? int.Parse(portNumberBox.Text) : -1;
            //============ PortNumber Box Values===============


            //============ Terminal Id Box Values===============
            int terminalIdFirstBoxValue = int.TryParse(terminalIdFirstBox.Text, out outputValue) ? int.Parse(terminalIdFirstBox.Text) : -1;
            int terminalIdSocondBoxValue = int.TryParse(terminalIdSocondBox.Text, out outputValue) ? int.Parse(terminalIdSocondBox.Text) : -1;
            //============ Terminal Id Box Values===============


            //============ Camera Machine Number Box Values===============
            int cameraMachineNumberBoxValue = int.TryParse(cameraMachineNumberBox.Text, out outputValue) ? int.Parse(cameraMachineNumberBox.Text) : -1;
            //============ Camera Machine Number Box Values===============
            int remotePeerFirstBoxValue = int.TryParse(remotePeerFirstBox.Text, out outputValue) ? int.Parse(remotePeerFirstBox.Text) : -1;
            int remotePeerSocondBoxValue = int.TryParse(remotePeerSocondBox.Text, out outputValue) ? int.Parse(remotePeerSocondBox.Text) : -1;
            int remotePeerThirdBoxValue = int.TryParse(remotePeerThirdBox.Text, out outputValue) ? int.Parse(remotePeerThirdBox.Text) : -1;
            int remotePeerFourthtBoxValue = int.TryParse(remotePeerFourthBox.Text, out outputValue) ? int.Parse(remotePeerFourthBox.Text) : -1;

            //============ Camera Machine Number Box Values===============
            string computerNameBoxValue = computerNameFirstBox.Text + "-" + computerNameSocondBox.Text;
            //============ Camera Machine Number Box Values===============
            string ipv4Address = ipv4Check ? ipv4FirstBoxValue + "." + ipv4SocondBoxValue + "." + ipv4ThirdBoxValue + "." + ipv4FourthBoxValue : null;
            string subnetMask = subnetMaskCheck ? subnetMaskFirstBoxValue + "." + subnetMaskSocondBoxValue + "." + subnetMaskThirdBoxValue + "." + subnetMaskFourthBoxValue : null;
            string defaultGateWay = defaultGatewayCheck ? defaultGatewayFirstBoxValue + "." + defaultGatewaySocondBoxValue + "." + defaultGatewayThirdBoxValue + "." + defaultGatewayFourthBoxValue : null;
            string portNumber = portNumberCheck ? portNumberBoxValue.ToString() : null;
            string terminalId = terminalIdCheck ? terminalIdFirstBoxValue + "-" + terminalIdSocondBoxValue : null;
            string cameraMachineNumber = cameraMachineNumberCheck ? cameraMachineNumberBoxValue.ToString() : null;
            string computerName = computerNameCheck ? computerNameBoxValue : null;
            string remotePeer = remotePeerFirstBoxValue + "." + remotePeerSocondBoxValue + "." + remotePeerThirdBoxValue + "." + remotePeerFourthtBoxValue;


            //============ Cash Profiles Box Values===============


            //============ Cash profiles  Box Values===============




            // //.Show("IPv4: "+ ipv4FirstBoxValue+"."+ ipv4SocondBoxValue+"."+ ipv4ThirdBoxValue+"."+ ipv4FourthBoxValue+"\n"+
            //  "SubnetMask : "+subnetMaskFirstBoxValue+ subnetMaskSocondBoxValue+ subnetMaskThirdBoxValue + subnetMaskFourthBoxValue +"\n"+
            //  "Default Gateway : "+defaultGatewayFirstBoxValue+ defaultGatewaySocondBoxValue+ defaultGatewayThirdBoxValue+ defaultGatewayFourthBoxValue+"\n"+
            //  "PortNumber : "+portNumberBoxValue+"\n"+
            //  "Camera Machine Number : "+ cameraMachineNumberBoxValue+"\n"+
            //  "Computer Name : "+ computerNameBoxValue+"\n"+ 
            // "Ipv4Summ = "+ ipv4Address+ "\n"+
            //  "subnetMaskSumm:  "+ subnetMask
            //   );

            var changedData = "             Impact List     \n\n\n";
            if (ipv4Check) changedData = changedData + "Ipv4:   " + ipv4Address + "\n" + "\n";
            if (cashProfileCheck) changedData = changedData + "CashProfile:   " + selectedCashProfile.Text + "\n" + "\n";
            if (subnetMaskCheck) changedData = changedData + "Subnet Mask:   " + subnetMask + "\n" + "\n";
            if (portNumberCheck) changedData = changedData + "PortNumber:   " + portNumber + "\n" + "\n";
            if (portNumberCheck) changedData = changedData + "LocalPort:   " + localPortBox.Text + "\n" + "\n";
            if (terminalIdCheck) changedData = changedData + "TerminalID:   " + terminalId + "\n" + "\n";
            if (cameraMachineNumberCheck) changedData = changedData + "CameraMachineNumber:   " + cameraMachineNumber + "\n" + "\n";
            if (computerNameCheck) changedData = changedData + "Computer Name:   " + computerName + "\n" + "\n";
            if (remotePeerCheck) changedData = changedData + "RemotePeer:   " + remotePeer + "\n" + "\n";


            MessageBox.Show(changedData);
               /*
               MessageBox.Show("Ipv4: " + ipv4Address + 
               "\n" + "Default Gateway: " + defaultGateWay +
               "\n" + "Subnet Mask:" + subnetMask +
               "\n" + "PortNumber: " + portNumber +
               "\n" + "LocalPort: " + localPortBox.Text +
               "\n" + "TerminalID:" + terminalId +
               "\n" + "Camera Machine Number" + cameraMachineNumber +
               "\n" + "Computer Name:" + computerName +
               "\n" + "RemotePeer:" + remotePeer+
               "\n" + "CashProfile" + selectedCashProfile.Text
                   );
               */


               // //.Show(publicClass.testt());
               // string test = publicClass.GetLocalIPAddress();
               ////.Show(test);
               // if (ipv4Check && defaultGatewayCheck && subnetMaskCheck) {
               //publicClass.changeIpAddress(ipv4Address, defaultGateWay, subnetMask);

               //}

               var key1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wincor Nixdorf", true);
            var key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Wincor Nixdorf", true);
            
           regkeyPath = @"SOFTWARE\Wincor Nixdorf";
            if (key1!=null) {
                regkeyPath = "SOFTWARE\\Wincor Nixdorf";
                
               
            }
            if (key2!=null)
            {
                regkeyPath = @"SOFTWARE\WOW6432Node\Wincor Nixdorf";

            }
            if (portNumberCheck) {
                publicClass.changePortNumber(regkeyPath,portNumber);
                publicClass.changeLocalPort(regkeyPath, portNumber);
               
            }
            if (terminalIdCheck) {
                publicClass.changeTerminalId(regkeyPath,terminalId);
            }
            if (cameraMachineNumberCheck) {
                publicClass.changeCameraMachineNumber(regkeyPath,cameraMachineNumber);
            }
          
            if (remotePeerCheck) {
                publicClass.changeRemotePeer(regkeyPath,remotePeer);
            }

            if (computerNameCheck) {
                publicClass.changeComputerName(computerName);
            }

            if (ipv4Check) {
                publicClass.changeIpAddress(ipv4Address,defaultGateWay,subnetMask);
            }

            if (cashProfileCheck) {
                publicClass2.insertCashProfile(regkeyPath, type1Value.Text,type2Value.Text,type3Value.Text,type4Value.Text);
            }

            publicClass.changeRemotePeer(regkeyPath, remotePeer);

            //getting the value
            //string data = key.GetValue("TRANSPORTSIZE").ToString();  //returns the text found in 'someValue'
            // key1.SetValue("PORTNUMBER", "2222");
            //  key1.Close();
            publicClass2.generatePDFLog(ipv4Address,subnetMask,defaultGateWay,portNumber,localPortBox.Text,terminalId,cameraMachineNumber,computerName,selectedCashProfile.Text);

        }

        private void subnetMaskFourthBox_TextChanged(object sender, EventArgs e)
        {
            int subnetMaskFirstBoxValue = int.TryParse(subnetMaskFirstBox.Text, out outputValue) ? int.Parse(subnetMaskFirstBox.Text) : -1;
            int subnetMaskSocondBoxValue = int.TryParse(subnetMaskSocondBox.Text, out outputValue) ? int.Parse(subnetMaskSocondBox.Text) : -1;
            int subnetMaskThirdBoxValue = int.TryParse(subnetMaskThirdBox.Text, out outputValue) ? int.Parse(subnetMaskThirdBox.Text) : -1;
            int subnetMaskFourthBoxValue = int.TryParse(subnetMaskFourthBox.Text, out outputValue) ? int.Parse(subnetMaskFourthBox.Text) : -1;
            if (subnetMaskFirstBoxValue != -1 && subnetMaskSocondBoxValue != -1 && subnetMaskThirdBoxValue != -1 && subnetMaskFourthBoxValue != -1)
            {
                subnetMaskCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = true;
                subnetMaskFalseLogo.Visible = false;
            }
            else
            {
                subnetMaskCheck = false;
                //    importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = false;
                subnetMaskFalseLogo.Visible = true;

            }
        }

        private void subnetMaskThirdBox_TextChanged(object sender, EventArgs e)
        {
            int subnetMaskFirstBoxValue = int.TryParse(subnetMaskFirstBox.Text, out outputValue) ? int.Parse(subnetMaskFirstBox.Text) : -1;
            int subnetMaskSocondBoxValue = int.TryParse(subnetMaskSocondBox.Text, out outputValue) ? int.Parse(subnetMaskSocondBox.Text) : -1;
            int subnetMaskThirdBoxValue = int.TryParse(subnetMaskThirdBox.Text, out outputValue) ? int.Parse(subnetMaskThirdBox.Text) : -1;
            int subnetMaskFourthBoxValue = int.TryParse(subnetMaskFourthBox.Text, out outputValue) ? int.Parse(subnetMaskFourthBox.Text) : -1;
            if (subnetMaskFirstBoxValue != -1 && subnetMaskSocondBoxValue != -1 && subnetMaskThirdBoxValue != -1 && subnetMaskFourthBoxValue != -1)
            {
                subnetMaskCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = true;
                subnetMaskFalseLogo.Visible = false;
            }
            else
            {
                subnetMaskCheck = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = false;
                subnetMaskFalseLogo.Visible = true;

            }
        }

        private void subnetMaskSocondBox_TextChanged(object sender, EventArgs e)
        {
            int subnetMaskFirstBoxValue = int.TryParse(subnetMaskFirstBox.Text, out outputValue) ? int.Parse(subnetMaskFirstBox.Text) : -1;
            int subnetMaskSocondBoxValue = int.TryParse(subnetMaskSocondBox.Text, out outputValue) ? int.Parse(subnetMaskSocondBox.Text) : -1;
            int subnetMaskThirdBoxValue = int.TryParse(subnetMaskThirdBox.Text, out outputValue) ? int.Parse(subnetMaskThirdBox.Text) : -1;
            int subnetMaskFourthBoxValue = int.TryParse(subnetMaskFourthBox.Text, out outputValue) ? int.Parse(subnetMaskFourthBox.Text) : -1;
            if (subnetMaskFirstBoxValue != -1 && subnetMaskSocondBoxValue != -1 && subnetMaskThirdBoxValue != -1 && subnetMaskFourthBoxValue != -1)
            {
                subnetMaskCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = true;
                subnetMaskFalseLogo.Visible = false;
            }
            else
            {
                subnetMaskCheck = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                subnetMaskSuccessLogo.Visible = false;
                subnetMaskFalseLogo.Visible = true;

            }
        }

        private void defaultGatewayFourthBox_TextChanged(object sender, EventArgs e)
        {
           
            int defaultGatewayFirstBoxValue = int.TryParse(defaultGatewayFirstBox.Text, out outputValue) ? int.Parse(defaultGatewayFirstBox.Text) : -1;
            int defaultGatewaySocondBoxValue = int.TryParse(defaultGatewaySocondBox.Text, out outputValue) ? int.Parse(defaultGatewaySocondBox.Text) : -1;
            int defaultGatewayThirdBoxValue = int.TryParse(defaultGatewayThirdBox.Text, out outputValue) ? int.Parse(defaultGatewayThirdBox.Text) : -1;
            int defaultGatewayFourthBoxValue = int.TryParse(defaultGatewayFourthBox.Text, out outputValue) ? int.Parse(defaultGatewayFourthBox.Text) : -1;
            int ipv4FourthBoxValue = int.TryParse(ipv4FourthBox.Text, out outputValue) ? int.Parse(ipv4FourthBox.Text) : -1;
           
            if (defaultGatewayFirstBoxValue != -1 && defaultGatewaySocondBoxValue != -1 && defaultGatewayThirdBoxValue != -1 && defaultGatewayFourthBoxValue != -1)
            {
                defaultGatewayCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = true;
                defaultGatewayFalseLogo.Visible = false;
            }
            else
            {
                defaultGatewayCheck = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = true;
            }
            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }

            if (defaultGatewayFirstBoxValue == -1 && defaultGatewaySocondBoxValue == -1 && defaultGatewayThirdBoxValue == -1 && defaultGatewayFourthBoxValue == -1)
            {
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = false;
            }
            if (defaultGatewayFourthBoxValue>= ipv4FourthBoxValue) {
                defaultGatewayCheck = false;
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }

        }


        private void defaultGatewayThirdBox_TextChanged(object sender, EventArgs e)
        {
          
            int defaultGatewayFirstBoxValue = int.TryParse(defaultGatewayFirstBox.Text, out outputValue) ? int.Parse(defaultGatewayFirstBox.Text) : -1;
            int defaultGatewaySocondBoxValue = int.TryParse(defaultGatewaySocondBox.Text, out outputValue) ? int.Parse(defaultGatewaySocondBox.Text) : -1;
            int defaultGatewayThirdBoxValue = int.TryParse(defaultGatewayThirdBox.Text, out outputValue) ? int.Parse(defaultGatewayThirdBox.Text) : -1;
            int defaultGatewayFourthBoxValue = int.TryParse(defaultGatewayFourthBox.Text, out outputValue) ? int.Parse(defaultGatewayFourthBox.Text) : -1;
            if (defaultGatewayFirstBoxValue != -1 && defaultGatewaySocondBoxValue != -1 && defaultGatewayThirdBoxValue != -1 && defaultGatewayFourthBoxValue != -1)
            {
                defaultGatewayCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = true;
                defaultGatewayFalseLogo.Visible = false;
            }
            else
            {
                defaultGatewayCheck = false;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = true;
            }
            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }
            if (defaultGatewayFirstBoxValue == -1 && defaultGatewaySocondBoxValue == -1 && defaultGatewayThirdBoxValue == -1 && defaultGatewayFourthBoxValue == -1)
            {
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = false;
            }
        }

        private void defaultGatewaySocondBox_TextChanged(object sender, EventArgs e)
        {
           
            int defaultGatewayFirstBoxValue = int.TryParse(defaultGatewayFirstBox.Text, out outputValue) ? int.Parse(defaultGatewayFirstBox.Text) : -1;
            int defaultGatewaySocondBoxValue = int.TryParse(defaultGatewaySocondBox.Text, out outputValue) ? int.Parse(defaultGatewaySocondBox.Text) : -1;
            int defaultGatewayThirdBoxValue = int.TryParse(defaultGatewayThirdBox.Text, out outputValue) ? int.Parse(defaultGatewayThirdBox.Text) : -1;
            int defaultGatewayFourthBoxValue = int.TryParse(defaultGatewayFourthBox.Text, out outputValue) ? int.Parse(defaultGatewayFourthBox.Text) : -1;
            if (defaultGatewayFirstBoxValue != -1 && defaultGatewaySocondBoxValue != -1 && defaultGatewayThirdBoxValue != -1 && defaultGatewayFourthBoxValue != -1)
            {
                defaultGatewayCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = true;
                defaultGatewayFalseLogo.Visible = false;
            }
            else
            {
                defaultGatewayCheck = false;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = true;
            }

            if (ipv4Check)
            {
                defaultGatewayFourthBox.ReadOnly = false;
            }
            else
            {
                defaultGatewayFourthBox.ReadOnly = true;
            }
            if (defaultGatewayFirstBoxValue == -1 && defaultGatewaySocondBoxValue == -1 && defaultGatewayThirdBoxValue == -1 && defaultGatewayFourthBoxValue == -1)
            {
                defaultGatewaySuccessLogo.Visible = false;
                defaultGatewayFalseLogo.Visible = false;
            }
        }

        private void portNumberBox_TextChanged(object sender, EventArgs e)
        {
              int outputValue = 0;
            int portNumberBoxValue = int.TryParse(portNumberBox.Text, out outputValue) ? int.Parse(portNumberBox.Text) : -1;
            if (portNumberBoxValue != -1 && (portNumberBoxValue.ToString().Length== 4 || portNumberBoxValue.ToString().Length == 5))
            {
                portNumberCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                portNumberSuccessLogo.Visible = true;
                portNumberFalseLogo.Visible = false;
            }
            else
            {
                portNumberCheck = false;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck,remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                portNumberSuccessLogo.Visible = false;
                portNumberFalseLogo.Visible = true;
            }
            localPortBox.Text = portNumberBox.Text;
            if (portNumberBoxValue==-1) {
                portNumberSuccessLogo.Visible = false;
                portNumberFalseLogo.Visible = false;
            }
        }
            
        

        private void cameraMachineNumberBox_TextChanged(object sender, EventArgs e)
        {

            // int cameraMachineNumberBoxValue = int.TryParse(cameraMachineNumberBox.Text, out outputValue) ? int.Parse(cameraMachineNumberBox.Text) : -1;
            string cameraMachineNumberBoxValue = cameraMachineNumberBox.Text;


            if (cameraMachineNumberBoxValue != "" && cameraMachineNumberBoxValue.ToString().Length == 8) {
                cameraMachineNumberCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                cameraMachineNumberSuccessLogo.Visible = true;
                cameraMachineNumberFalseLogo.Visible = false;
            }
           
                else
                {
                    cameraMachineNumberCheck = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
                cameraMachineNumberSuccessLogo.Visible = false;
                    cameraMachineNumberFalseLogo.Visible = true;
                }
            if (cameraMachineNumberBoxValue == "")
            {
                cameraMachineNumberSuccessLogo.Visible = false;
                cameraMachineNumberFalseLogo.Visible = false;
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void remotePeerFirstBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
(e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void remotePeerSocondBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
(e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void remotePeerThirdBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
(e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void remotePeerFourthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
(e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void remotePeerThirdBox_TextChanged(object sender, EventArgs e)
        {
            int remotePeerFirstBoxValue = int.TryParse(remotePeerFirstBox.Text, out outputValue) ? int.Parse(remotePeerFirstBox.Text) : -1;
            int remotePeerSocondBoxValue = int.TryParse(remotePeerSocondBox.Text, out outputValue) ? int.Parse(remotePeerSocondBox.Text) : -1;
            int remotePeerThirdBoxValue = int.TryParse(remotePeerThirdBox.Text, out outputValue) ? int.Parse(remotePeerThirdBox.Text) : -1;
            int remotePeerFourthBoxValue = int.TryParse(remotePeerFourthBox.Text, out outputValue) ? int.Parse(remotePeerFourthBox.Text) : -1;

            if (remotePeerFirstBoxValue != -1 && remotePeerSocondBoxValue != -1 && remotePeerThirdBoxValue != -1 && remotePeerFourthBoxValue != -1)
            {
                remotePeerCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }
            else
            {
                remotePeerCheck = false;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }
        }

        private void remotePeerFirstBox_TextChanged(object sender, EventArgs e)
        {
            int remotePeerFirstBoxValue = int.TryParse(remotePeerFirstBox.Text, out outputValue) ? int.Parse(remotePeerFirstBox.Text) : -1;
            int remotePeerSocondBoxValue = int.TryParse(remotePeerSocondBox.Text, out outputValue) ? int.Parse(remotePeerSocondBox.Text) : -1;
            int remotePeerThirdBoxValue = int.TryParse(remotePeerThirdBox.Text, out outputValue) ? int.Parse(remotePeerThirdBox.Text) : -1;
            int remotePeerFourthBoxValue = int.TryParse(remotePeerFourthBox.Text, out outputValue) ? int.Parse(remotePeerFourthBox.Text) : -1;

            if (remotePeerFirstBoxValue != -1 && remotePeerSocondBoxValue != -1 && remotePeerThirdBoxValue != -1 && remotePeerFourthBoxValue != -1)
            {
                remotePeerCheck = true;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }
            else
            {
                remotePeerCheck = false;
                //importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }
        }

        private void remotePeerSocondBox_TextChanged(object sender, EventArgs e)
        {
            int remotePeerFirstBoxValue = int.TryParse(remotePeerFirstBox.Text, out outputValue) ? int.Parse(remotePeerFirstBox.Text) : -1;
            int remotePeerSocondBoxValue = int.TryParse(remotePeerSocondBox.Text, out outputValue) ? int.Parse(remotePeerSocondBox.Text) : -1;
            int remotePeerThirdBoxValue = int.TryParse(remotePeerThirdBox.Text, out outputValue) ? int.Parse(remotePeerThirdBox.Text) : -1;
            int remotePeerFourthBoxValue = int.TryParse(remotePeerFourthBox.Text, out outputValue) ? int.Parse(remotePeerFourthBox.Text) : -1;

            if (remotePeerFirstBoxValue != -1 && remotePeerSocondBoxValue != -1 && remotePeerThirdBoxValue != -1 && remotePeerFourthBoxValue != -1)
            {
                remotePeerCheck = true;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }
            else
            {
                remotePeerCheck = false;
                //  importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }
        }

        private void remotePeerFourthBox_TextChanged(object sender, EventArgs e)
        {
            int remotePeerFirstBoxValue = int.TryParse(remotePeerFirstBox.Text, out outputValue) ? int.Parse(remotePeerFirstBox.Text) : -1;
            int remotePeerSocondBoxValue = int.TryParse(remotePeerSocondBox.Text, out outputValue) ? int.Parse(remotePeerSocondBox.Text) : -1;
            int remotePeerThirdBoxValue = int.TryParse(remotePeerThirdBox.Text, out outputValue) ? int.Parse(remotePeerThirdBox.Text) : -1;
            int remotePeerFourthBoxValue = int.TryParse(remotePeerFourthBox.Text, out outputValue) ? int.Parse(remotePeerFourthBox.Text) : -1;

            if (remotePeerFirstBoxValue != -1 && remotePeerSocondBoxValue != -1 && remotePeerThirdBoxValue != -1 && remotePeerFourthBoxValue != -1)
            {
                remotePeerCheck = true;
                // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
                if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
            }
            else
            {
                remotePeerCheck = false;
               // importBtn.Enabled = publicClass.btnCheck(ipv4Check, subnetMaskCheck, defaultGatewayCheck, portNumberCheck, terminalIdCheck, cameraMachineNumberCheck, computerNameCheck, remotePeerCheck);
               if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else {
                    importBtn.Enabled = false;
                }
            }
        }

        private void localPortBox_TextChanged(object sender, EventArgs e)
        {
            //int localPortBoxValue=int.TryParse(localPortBox.Text,out outputValue)?int.Parse(localPortBox.Text):-1;
         //   string test = localPortBoxValue.ToString();
        }

        private void localPortBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
     (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void profile1_CheckedChanged(object sender, EventArgs e)
        {

            cashProfileCheck = currentCashProfileName == "AMCR" ? false : true;


                  type1Value.Text = "200";
                 type2Value.Text = "20";
                  type3Value.Text = "50";
                type4Value.Text = "100";

            currency4Value.Text = "EGP";
            currency3Value.Text = "EGP";
            currency2Value.Text = "EGP";
            currency1Value.Text = "EGP";

            selectedCashProfile.Text = "AMCR";
            if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
            {
                importBtn.Enabled = true;
            }
            else
            {
                importBtn.Enabled = false;
            }

        }

        private void profile1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void profile1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void profile1_Click(object sender, EventArgs e)
        {
           
        }

        private void profile1_MouseClick(object sender, MouseEventArgs e)
        {
           // profile1.Checked = false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void selectedCashProfile_Click(object sender, EventArgs e)
        {

        }

        private void profile2_CheckedChanged(object sender, EventArgs e)
        {
            cashProfileCheck = currentCashProfileName == "New Forex" ? false : true;
            type1Value.Text = "10";
            type2Value.Text = "50";
            type3Value.Text = "100";
            type4Value.Text = "200";

            currency4Value.Text = "EGP";
            currency3Value.Text = "EGP";
            currency2Value.Text = "EGP";
            currency1Value.Text = "EGP";

            selectedCashProfile.Text = "New Forex";
            //  //.Show(cashProfileCheck.ToString());
            if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
            {
                importBtn.Enabled = true;
            }
            else
            {
                importBtn.Enabled = false;
            }
        }

        private void profile3_CheckedChanged(object sender, EventArgs e)
        {
            cashProfileCheck = currentCashProfileName == "Old Forex" ? false : true;

            type1Value.Text = "5";
                 type2Value.Text = "10";
                  type3Value.Text = "50";
                type4Value.Text = "200";

            currency4Value.Text = "EGP";
            currency3Value.Text = "EGP";
            currency2Value.Text = "EGP";
            currency1Value.Text = "EGP";

            selectedCashProfile.Text = "Old Forex";
            // //.Show(cashProfileCheck.ToString());
            if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
            {
                importBtn.Enabled = true;
            }
            else
            {
                importBtn.Enabled = false;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cashProfileCheck = currentCashProfileName == "Cash" ? false : true;

            type1Value.Text = "20";
            type2Value.Text = "50";
            type3Value.Text = "100";
            type4Value.Text = "200";

            currency4Value.Text = "EGP";
            currency3Value.Text = "EGP";
            currency2Value.Text = "EGP";
            currency1Value.Text = "EGP";

            selectedCashProfile.Text = "Cash";
            ////.Show(cashProfileCheck.ToString());
            if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
            {
                importBtn.Enabled = true;
            }
            else
            {
                importBtn.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            profile1.Checked = false;
            profile2.Checked = false;
            profile3.Checked = false;
            radioButton1.Checked = false;
            selectedCashProfile.Text = currentCashProfileName;
            type1Value.Text = currentCashTypes[0];
            type2Value.Text = currentCashTypes[1];      
            type3Value.Text = currentCashTypes[2];
            type4Value.Text = currentCashTypes[3];
            cashProfileCheck = false;
            if ((ipv4Check && subnetMaskCheck && defaultGatewayCheck) || portNumberCheck || terminalIdCheck || cameraMachineNumberCheck || computerNameCheck || remotePeerCheck || cashProfileCheck)
                {
                    importBtn.Enabled = true;
                }
                else
                {
                    importBtn.Enabled = false;
                }
         
        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void cameraHDDDriveLetterFixButton_Click(object sender, EventArgs e)
        {
            //.Show(publicClass2.firstDriveCollectedLetters.Count.ToString());
            Process process2 = new Process();
            process2.StartInfo.FileName = "cmd.exe";
            process2.StartInfo.CreateNoWindow = false;
            process2.StartInfo.RedirectStandardInput = true;
            process2.StartInfo.RedirectStandardOutput = false;
            process2.StartInfo.UseShellExecute = false;
            process2.Start();
           // process2.StandardInput.WriteLine("ipconfig");
            process2.StandardInput.WriteLine("diskpart");
            process2.StandardInput.WriteLine("list vol");
            process2.StandardInput.WriteLine($"select volume {publicClass2.drive0PartitionsNumber}");
            process2.StandardInput.WriteLine("assign letter=F");

            process2.StandardInput.Flush();
          process2.StandardInput.Close();
            process2.WaitForExit();
        }

        private void bitmapsFolderItemsText_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newFile = @".\Data\ConfigData.xml";

            File.Copy(newFile, @"C:\Protopas\bin\ConfigData.xml", true);
            MessageBox.Show("ConfigData.XML ADDED");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                button2.Enabled = true;
            }
            else button2.Enabled = false;   
        }

        private void audioNumberOfFilesTrueCheck_Click(object sender, EventArgs e)
        {

        }
    }
}
