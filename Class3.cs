using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace NBEWindowsFormApplication
{

    internal class Class3
    {
		public bool socondDriveFlag { get; set; }
		public List<string> firstDriveCollectedLetters = new List<string>();
		public int drive0PartitionsNumber = 0;
		public string currentCashProfile = "N/A";
		//List<string> firstDriveCollectedLetters = new List<string>();
		public List<string> secondDriveCollectedLetters = new List<string>();
		public string currentIpAddresss = "N/A";
		public string currentSubnetmask = "N/A";
		public string currentRemotePeer = "N/A";
		public string currentTerminalId = "N/A";
		public string currentLocalPort = "N/A";
		public string currentPortNumber = "N/A";
		public string currentCameraMachineNumber = "N/A";
		public string currentDefaultGateway = "N/A";
		public string currentComputerName = System.Environment.MachineName!=null? System.Environment.MachineName:"N/A";
		public string systemSerialNumber = "";
		public string systemName = "";


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
				currentCashProfile = "AMCR";

				return "AMCR";
            }
            if (type1 == "10" && type2 == "50" && type3 == "100" && type4 == "200")
            {
				currentCashProfile = "New Forex";
				return "New Forex";
            }

            if (type1 == "5" && type2 == "10" && type3 == "50" && type4 == "200")
            {
				currentCashProfile = "Old Forex";
				return "Old Forex";
            }
			if (type1 == "20" && type2 == "50" && type3 == "100" && type4 == "200")
			{
				currentCashProfile = "Cash";
				return "Cash";
			}
			else
			{
				currentCashProfile = @"N\A";
				return @"N\A";
			}
			

		}

        public void insertCashProfile(string regKeyPath,string type1,string type2, string type3, string type4) {
            string path = regKeyPath + @"\ProTopas\CurrentVersion\LYNXPAR\CASH_DISPENSER";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);
            key.SetValue("VALUE_1", type1);
            key.SetValue("VALUE_2", type2);
            key.SetValue("VALUE_3", type3);
            key.SetValue("VALUE_4", type4);

			key.SetValue("CURRENCY_1","EGP");
			key.SetValue("CURRENCY_2","EGP");
			key.SetValue("CURRENCY_3","EGP");
			key.SetValue("CURRENCY_4","EGP");
			key.Close();
        }

		
		public void getHDDData() {
			int i =0;
			//===================================================================
			//		Get Drive 0 Peritions Number
			//  ======================================================================

			var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskPartition");

			foreach (var queryObj in searcher.Get())
			{
				//Console.WriteLine("-----------------------------------");
				//	Console.WriteLine("Win32_DiskPartition instance");
				//Console.WriteLine("Name:{0}", (string)queryObj["Name"]);
				//Console.WriteLine("Index:{0}", (uint)queryObj["Index"]);
				//Console.WriteLine("DiskIndex:{0}", (uint)queryObj["DiskIndex"]);
				//Console.WriteLine("BootPartition:{0}", (bool)queryObj["BootPartition"]);
				if ((uint)queryObj["DiskIndex"]==0) {
					drive0PartitionsNumber++;
				}
			}

			//===================================================================
			//		Get Drive 0 Peritions Number
			//  ======================================================================








			var driveQuery = new ManagementObjectSearcher("select * from Win32_DiskDrive");
			foreach (ManagementObject d in driveQuery.Get())
			{
				var deviceId = d.Properties["DeviceId"].Value;
				//Console.WriteLine("Device");
				//Console.WriteLine(d);
				var partitionQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_DiskDriveToDiskPartition", d.Path.RelativePath);
				var partitionQuery = new ManagementObjectSearcher(partitionQueryText);
				foreach (ManagementObject p in partitionQuery.Get())
				{
					//Console.WriteLine("Partition");
					//Console.WriteLine(p);
					var logicalDriveQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_LogicalDiskToPartition", p.Path.RelativePath);
					var logicalDriveQuery = new ManagementObjectSearcher(logicalDriveQueryText);
					foreach (ManagementObject ld in logicalDriveQuery.Get())
					{
						//Console.WriteLine("Logical drive");
						//Console.WriteLine(ld);


						var physicalName = Convert.ToString(d.Properties["Name"].Value); // \\.\PHYSICALDRIVE2
						var diskName = Convert.ToString(d.Properties["Caption"].Value); // WDC WD5001AALS-xxxxxx
						var diskModel = Convert.ToString(d.Properties["Model"].Value); // WDC WD5001AALS-xxxxxx
						var diskInterface = Convert.ToString(d.Properties["InterfaceType"].Value); // IDE
						var capabilities = (UInt16[])d.Properties["Capabilities"].Value; // 3,4 - random access, supports writing
						var mediaLoaded = Convert.ToBoolean(d.Properties["MediaLoaded"].Value); // bool
						var mediaType = Convert.ToString(d.Properties["MediaType"].Value); // Fixed hard disk media
						var mediaSignature = Convert.ToUInt32(d.Properties["Signature"].Value); // int32
						var mediaStatus = Convert.ToString(d.Properties["Status"].Value); // OK

						var driveName = Convert.ToString(ld.Properties["Name"].Value); // C:
						var driveId = Convert.ToString(ld.Properties["DeviceId"].Value); // C:
						var driveCompressed = Convert.ToBoolean(ld.Properties["Compressed"].Value);
						var driveType = Convert.ToUInt32(ld.Properties["DriveType"].Value); // C: - 3
						var fileSystem = Convert.ToString(ld.Properties["FileSystem"].Value); // NTFS
						var freeSpace = Convert.ToUInt64(ld.Properties["FreeSpace"].Value); // in bytes
						var totalSpace = Convert.ToUInt64(ld.Properties["Size"].Value); // in bytes
						var driveMediaType = Convert.ToUInt32(ld.Properties["MediaType"].Value); // c: 12
						var volumeName = Convert.ToString(ld.Properties["VolumeName"].Value); // System
						var volumeSerial = Convert.ToString(ld.Properties["VolumeSerialNumber"].Value); // 12345678


						if (physicalName == @"\\.\PHYSICALDRIVE0")
						{
							firstDriveCollectedLetters.Add(driveName);
						}



						if (physicalName == @"\\.\PHYSICALDRIVE1")
						{

							if (mediaType == "Fixed hard disk media")
							{
								secondDriveCollectedLetters.Add(driveName);
								socondDriveFlag = true;
								//.Show(socondDriveFlag.ToString());
							}
							else
							{
								socondDriveFlag = false;
								//.Show(socondDriveFlag.ToString());
							}



						}

						//MessageBox.Show("DriveNameVar: ",driveName);

						








/*
						Console.WriteLine("PhysicalName: {0}", physicalName);
						Console.WriteLine("DiskName: {0}", diskName);
						Console.WriteLine("DiskModel: {0}", diskModel);
						Console.WriteLine("DiskInterface: {0}", diskInterface);
						// Console.WriteLine("Capabilities: {0}", capabilities);
						Console.WriteLine("MediaLoaded: {0}", mediaLoaded);
						Console.WriteLine("MediaType: {0}", mediaType);
						Console.WriteLine("MediaSignature: {0}", mediaSignature);
						Console.WriteLine("MediaStatus: {0}", mediaStatus);

						Console.WriteLine("DriveName: {0}", driveName);
						Console.WriteLine("DriveId: {0}", driveId);
						Console.WriteLine("DriveCompressed: {0}", driveCompressed);
						Console.WriteLine("DriveType: {0}", driveType);
						Console.WriteLine("FileSystem: {0}", fileSystem);
						Console.WriteLine("FreeSpace: {0}", freeSpace);
						Console.WriteLine("TotalSpace: {0}", totalSpace);
						Console.WriteLine("DriveMediaType: {0}", driveMediaType);
						Console.WriteLine("VolumeName: {0}", volumeName);
						Console.WriteLine("VolumeSerial: {0}", volumeSerial);

						Console.WriteLine(new string('-', 79));
						i++;
*/
					}
				}
			}

		}

		public void generatePDFLog(string ipv4Address, string subnetMask, string defaultGateWay, string portNumber, string localPortBox, string terminalId, string cameraMachineNumber, string computerName, string selectedCashProfile)
		{
			try
			{
				#region Common Part
				PdfPTable pdfTableBlank = new PdfPTable(1);

				//Footer Section
				PdfPTable blankText = new PdfPTable(1);
				blankText.DefaultCell.BorderWidth = 0;
				blankText.WidthPercentage = 100;
				blankText.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

				Chunk cnkFooter = new Chunk("  ", FontFactory.GetFont("Times New Roman"));
				//cnkFooter.Font.SetStyle(1);
				cnkFooter.Font.Size = 10;
				blankText.AddCell(new Phrase(cnkFooter));
				//End Of Footer Section

				pdfTableBlank.AddCell(new Phrase(" "));
				pdfTableBlank.DefaultCell.Border = 0;
				#endregion

				#region Page
				#region Section-1

				PdfPTable headerText = new PdfPTable(1);//Here 1 is Used For Count of Column
				PdfPTable oldEnteriesText = new PdfPTable(1);
				PdfPTable oldEnteriesTable = new PdfPTable(2);
				PdfPTable pdfTable4 = new PdfPTable(2);
				PdfPTable newEnteriesTable = new PdfPTable(2);
				PdfPTable newEnteriesText = new PdfPTable(1);
				PdfPTable systemNameTable = new PdfPTable(2);
				PdfPTable systemNameText = new PdfPTable(1);

				//Font Style
				System.Drawing.Font fontH1 = new System.Drawing.Font("Currier", 16);

				//headerText.DefaultCell.Padding = 5;
				headerText.WidthPercentage = 80;
				headerText.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
				headerText.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
				//headerText.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
				headerText.DefaultCell.BorderWidth = 0;

				systemNameText.WidthPercentage = 80;
				systemNameText.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
				systemNameText.DefaultCell.VerticalAlignment = Element.ALIGN_LEFT;
				//headerText.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
				systemNameText.DefaultCell.BorderWidth = 0;



				newEnteriesText.WidthPercentage = 80;
				newEnteriesText.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
				newEnteriesText.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
				//headerText.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
				newEnteriesText.DefaultCell.BorderWidth = 0;

				systemNameText.WidthPercentage = 80;
				systemNameText.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
				systemNameText.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
				//headerText.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
				systemNameText.DefaultCell.BorderWidth = 0;


				//headerText.DefaultCell.Padding = 5;
				oldEnteriesText.WidthPercentage = 80;
				oldEnteriesText.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
				oldEnteriesText.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
				//pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
				oldEnteriesText.DefaultCell.BorderWidth = 0;

				oldEnteriesTable.DefaultCell.Padding = 5;
				oldEnteriesTable.WidthPercentage = 80;
				oldEnteriesTable.DefaultCell.BorderWidth = 0.5f;

				pdfTable4.DefaultCell.Padding = 5;
				pdfTable4.WidthPercentage = 80;
				pdfTable4.DefaultCell.BorderWidth = 0.5f;

				newEnteriesTable.DefaultCell.Padding = 5;
				newEnteriesTable.WidthPercentage = 80;
				newEnteriesTable.DefaultCell.BorderWidth = 0.5f;


				Chunk c1 = new Chunk("NBEWFA lOG", FontFactory.GetFont("Times New Roman"));
				c1.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
				c1.Font.SetStyle(0);
				c1.Font.Size = 14;
				Phrase p1 = new Phrase();
				p1.Add(c1);
				headerText.AddCell(p1);
				Chunk c2 = new Chunk(" ", FontFactory.GetFont("Times New Roman"));
				c2.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
				c2.Font.SetStyle(0);//0 For Normal Font
				c2.Font.Size = 11;
				Phrase p2 = new Phrase();
				p2.Add(c2);
				oldEnteriesText.AddCell(p2);
				Chunk c3 = new Chunk("OLD ENTERIES", FontFactory.GetFont("Times New Roman"));
				c3.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
				c3.Font.SetStyle(0);
				c3.Font.Size = 11;
				Phrase p3 = new Phrase();
				p3.Add(c3);
				oldEnteriesText.AddCell(p3);

				Chunk c4 = new Chunk("NEW ENTERIES", FontFactory.GetFont("Times New Roman"));
				c4.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
				c4.Font.SetStyle(0);
				c4.Font.Size = 11;
				Phrase p4 = new Phrase();
				p4.Add(c4);
				newEnteriesText.AddCell(p4);


				Chunk c5 = new Chunk($"{systemName}", FontFactory.GetFont("Times New Roman"));
				c5.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
				c5.Font.SetStyle(0);
				c5.Font.Size = 13;
				Phrase p5 = new Phrase();
				p5.Add(c5);
				systemNameText.AddCell(p5);

				#endregion
				#region Section-1
				//PdfPTable pdfTable4 = new PdfPTable(4);
				pdfTable4.DefaultCell.Padding = 5;
				pdfTable4.WidthPercentage = 80;
				pdfTable4.DefaultCell.BorderWidth = 0.0f;

				pdfTable4.AddCell(new Phrase("Bill No "));
				pdfTable4.AddCell(new Phrase("B001"));
				pdfTable4.AddCell(new Phrase("Date "));
				pdfTable4.AddCell(new Phrase("01-01-2017"));
				pdfTable4.AddCell(new Phrase("Vendor "));
				pdfTable4.AddCell(new Phrase("Demo Vendor"));
				pdfTable4.AddCell(new Phrase("Address "));
				pdfTable4.AddCell(new Phrase("Kolkata"));
				#endregion
				#region Section-Image

				//string imageURL = @"E:\test\image.jpeg";

				//iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

				//Resize image depend upon your need
			//	jpg.ScaleToFit(140f, 120f);
				//Give space before image
			//	jpg.SpacingBefore = 10f;
				//Give some space after the image
			//	jpg.SpacingAfter = 1f;

			//	jpg.Alignment = Element.ALIGN_CENTER;
				#endregion

				#region section Table
				oldEnteriesTable.AddCell(new Phrase("Ipv4Address"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentIpAddresss}"));
				
				oldEnteriesTable.AddCell(new Phrase("Subnet Mask"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentSubnetmask}"));

				oldEnteriesTable.AddCell(new Phrase("Default Gateway"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentDefaultGateway}"));
				oldEnteriesTable.AddCell(new Phrase("Port Number"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentPortNumber}"));
				oldEnteriesTable.AddCell(new Phrase("Local Port"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentLocalPort}"));
				oldEnteriesTable.AddCell(new Phrase("Terminal ID"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentTerminalId}"));
				oldEnteriesTable.AddCell(new Phrase("Camera Machine Number"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentCameraMachineNumber}"));
				oldEnteriesTable.AddCell(new Phrase("Computer Name"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentComputerName}"));
				oldEnteriesTable.AddCell(new Phrase("Remoot Peer"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentRemotePeer}"));
				oldEnteriesTable.AddCell(new Phrase("Cash Profile"));
				oldEnteriesTable.AddCell(new Phrase($"                             {currentCashProfile}"));
				#endregion



				#region section Table
				newEnteriesTable.AddCell(new Phrase("Ipv4Address"));
				newEnteriesTable.AddCell(new Phrase($"                             {ipv4Address}"));
				newEnteriesTable.AddCell(new Phrase("Subnet Mask"));
				newEnteriesTable.AddCell(new Phrase($"                             {subnetMask}"));

				newEnteriesTable.AddCell(new Phrase("Default Gateway"));
				newEnteriesTable.AddCell(new Phrase($"                             {defaultGateWay}"));
				newEnteriesTable.AddCell(new Phrase("Port Number"));
				newEnteriesTable.AddCell(new Phrase($"                             {portNumber}"));
				newEnteriesTable.AddCell(new Phrase("Local Port"));
				newEnteriesTable.AddCell(new Phrase(""));
				newEnteriesTable.AddCell(new Phrase("Terminal ID"));
				newEnteriesTable.AddCell(new Phrase($"                             {terminalId}"));
				newEnteriesTable.AddCell(new Phrase("Camera Machine Number"));
				newEnteriesTable.AddCell(new Phrase($"                             {cameraMachineNumber}"));
				newEnteriesTable.AddCell(new Phrase("Computer Name"));
				newEnteriesTable.AddCell(new Phrase($"                             {computerName}"));
				newEnteriesTable.AddCell(new Phrase("Remoot Peer"));
				newEnteriesTable.AddCell(new Phrase("                              10.40.12.25"));
				newEnteriesTable.AddCell(new Phrase("Cash Profile"));
				newEnteriesTable.AddCell(new Phrase($"                             {selectedCashProfile}"));
				#endregion

				#endregion




				#region Pdf Generation
				string folderPath = "C:\\NBEWFA\\LOG\\";
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				//File Name
				int fileCount = Directory.GetFiles(folderPath).Length;
				//string strFileName = "DescriptionForm" + (fileCount + 1) + ".pdf";
				string timeNow = DateTime.Now.ToString("yyyyMMdd hhmmss"); ;
				string strFileName = timeNow +" "+ systemSerialNumber + ".pdf";
				//Console.WriteLine(DateTime.Now.ToString());
				using (FileStream stream = new FileStream(folderPath + strFileName, FileMode.Create))
				{
					Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
					PdfWriter.GetInstance(pdfDoc, stream);
					pdfDoc.Open();
					#region PAGE-1
					pdfDoc.Add(headerText);
					pdfDoc.Add(blankText);
					pdfDoc.Add(blankText);
					pdfDoc.Add(systemNameText);
						pdfDoc.Add(blankText);
					pdfDoc.Add(blankText);
					pdfDoc.Add(oldEnteriesText);

					//	pdfDoc.Add(jpg);
					pdfDoc.Add(oldEnteriesTable);
					pdfDoc.Add(blankText);
					pdfDoc.Add(blankText);
					pdfDoc.Add(newEnteriesText);
					//	pdfDoc.Add(pdfTableBlank);
					pdfDoc.Add(newEnteriesTable);
					pdfDoc.Add(blankText);
					pdfDoc.Add(blankText);
					//pdfDoc.Add(systemNameTable);
					pdfDoc.NewPage();
					#endregion

					// pdfDoc.Add(jpg);

					//pdfDoc.Add(oldEnteriesText);
					pdfDoc.Close();
					stream.Close();
				}
				#endregion

				#region Display PDF
				//System.Diagnostics.Process.Start(folderPath + "\\" + strFileName);
				#endregion

			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public void getIpAddress() {
			string hostName = Dns.GetHostName(); // Retrive the Name of HOST
			Console.WriteLine(hostName);
			// Get the IP
			currentIpAddresss = Dns.GetHostByName(hostName).AddressList[0].ToString();
			//.Show(currentIpAddresss);
		}

		public void getDefaultGateway() {
			foreach (NetworkInterface f in NetworkInterface.GetAllNetworkInterfaces())
				if (f.OperationalStatus == OperationalStatus.Up)
					foreach (GatewayIPAddressInformation d in f.GetIPProperties().GatewayAddresses)
                    {
						currentDefaultGateway = d.Address.ToString();
				}
						////.Show("This is the Defaul Gateway"+d.Address.ToString());



		}

		public void getsubnetMask() {
			
				int i = 0;
				foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
				{
					foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
					{
						if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							if (i == 0) { currentSubnetmask = unicastIPAddressInformation.IPv4Mask.ToString(); }
							Console.WriteLine("New Subnet Mask Is =======" + unicastIPAddressInformation.IPv4Mask);
						//.Show("Current Subnet Mask is ==========="+ unicastIPAddressInformation.IPv4Mask);
						i++;
						}
					}
			}

		}
		public void getCurrentInfoFromRegistry(string regkeyPath)
		{
			string path1 = regkeyPath + @"\ProTopas\CurrentVersion\CCOPEN\COMMUNICATION\TCPIP\PROJECT";
			string path2 = regkeyPath + @"\ProAgent\CurrentVersion\SSTP";
			string path3 = regkeyPath + @"\ProTopas\CurrentVersion\CCOPEN\PROTOCOL\PARAMETER";
			RegistryKey key3 = Registry.LocalMachine.OpenSubKey(path3, true);
			RegistryKey key1 = Registry.LocalMachine.OpenSubKey(path1, true);
			RegistryKey key2 = Registry.LocalMachine.OpenSubKey(path2, true);
			currentLocalPort = key1.GetValue("LOCALPORT").ToString();
			currentPortNumber = key1.GetValue("PORTNUMBER").ToString();
			currentRemotePeer = key1.GetValue("REMOTEPEER").ToString();
			currentTerminalId = key2.GetValue("TerminalId").ToString();
			currentCameraMachineNumber = key3.GetValue("CAMERA_MACHINE_NO").ToString();
			
		

		}

		public void getSerialNumber(string probaseRegkeyPath) {
			string path = probaseRegkeyPath + @"\System\Public Overview\System";

			//MessageBox.Show(path);
				RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);

			systemSerialNumber = key!=null ? key.GetValue("SERIAL-NO").ToString() : @"N-A";

		//	MessageBox.Show(systemSerialNumber);
		}

		public void getSystemName(string probaseRegkeyPath)
		{
			string path = probaseRegkeyPath + @"\System\Public Overview\System";

		//	MessageBox.Show(path);
			RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);

			systemName = key != null ? key.GetValue("NAME").ToString() : @"N-A";

		//	MessageBox.Show(systemSerialNumber);
		}
	}
}
