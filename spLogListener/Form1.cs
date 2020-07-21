using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Remoting.Messaging;

namespace spLogListener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {




        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        public class spLogBuff
        {
            public uint dwNextLogOffset; //4 57
            public uint dwPreLogOffset;//4 61
            public uint dwNowLogOffset;//4 65
            public uint dwCurIndex; //?4 69
            public int nGrade;//4 73
            public uint dwProcessId;//4 77
            public uint dwThreadId;//4 81
            public ushort year;//2 85
            public ushort month;//2 87
            public ushort day;//2 89 
            public ushort hour;//2 91
            public ushort minute;//2 93
            public ushort second;//2 95
            public ushort milisecond;//2 97
            [MarshalAs(UnmanagedType.LPTStr)]
            public string csModuleNAme; //4 99
            [MarshalAs(UnmanagedType.LPTStr)]
            public string csSubSystem;//4
            [MarshalAs(UnmanagedType.LPTStr)]
            public string csType;//4
            [MarshalAs(UnmanagedType.LPTStr)]
            public string csInfo;//4
            public string fileName;

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        public struct tagLogHead
        {
            public uint dwSize;//ÎÄ¼þÍ·´óÐ¡
            public uint dwLogCount;//ÈÕÖ¾¸öÊý
            public uint dwCurIndex;//µ±Ç°Ë÷ÒýºÅ
            public uint dwFirstLog;//µÚÒ»ÌõÈÕÖ¾µÄÆ«ÒÆµØÖ·
            public uint dwLastLogStart;//×îºóÒ»µÚÈÕÖ¾µÄ¿ªÊ¼µØÖ·
            public uint dwLastLogEnd;//×îºóÒ»µÚÈÕÖ¾µÄ½áÊøµØÖ·
            public uint dwVersion;//°æ±¾ºÅ
        };

        //FileStream spLogFile;
        tagLogHead logHeader = new tagLogHead();
        List<string> spLogFileNames = new List<string>();
        int spLogCount;


        public List<string> physicalDevs(FileStream spLogFile, tagLogHead logHeader, int createdYear)
        {

            List<string> physicalDevs = new List<string>();          

            foreach (string dev in physicalDevs)
            {
                richTextBox1.AppendText(dev + "\r\n");

            }


            return physicalDevs;


        }      

        public void displayLogHeader(tagLogHead logHeader)
        {
            MessageBox.Show("dwSize = " + logHeader.dwSize + "\r\n" + "dwLogCount = " + logHeader.dwLogCount + "\r\n" +
            "dwCurIndex = " + logHeader.dwCurIndex + "\r\n" +
            "dwSize = " + logHeader.dwSize + "\r\n" +
            "dwFirstLog = " + logHeader.dwFirstLog + "\r\n" +
            "dwLastLogstart = " + logHeader.dwLastLogStart + "\r\n" +
            "dwLastLogEnd = " + logHeader.dwLastLogEnd + "\r\n" +
            "dwVersion = " + logHeader.dwVersion + "\r\n");


        }

        public tagLogHead returnLogHeader(FileStream spLogFile)
        {
            byte[] headerByteArray = new byte[56];
            spLogFile.Read(headerByteArray, 0, 56);
            logHeader.dwSize = BitConverter.ToUInt32(headerByteArray, 0);
            logHeader.dwLogCount = BitConverter.ToUInt32(headerByteArray, 4);
            logHeader.dwCurIndex = BitConverter.ToUInt32(headerByteArray, 8);
            logHeader.dwFirstLog = BitConverter.ToUInt32(headerByteArray, 12);
            logHeader.dwLastLogStart = BitConverter.ToUInt32(headerByteArray, 16);
            logHeader.dwLastLogEnd = BitConverter.ToUInt32(headerByteArray, 20);
            logHeader.dwVersion = BitConverter.ToUInt32(headerByteArray, 24);


            return logHeader;
        }

        public spLogBuff[] returnSpLogLineArray(FileStream spLogFile, uint logCount)
        {

            spLogBuff[] spLogLineArray = new spLogBuff[logCount];
            spLogFile.Position = logHeader.dwFirstLog;
            MFCStringReader strRead = new MFCStringReader(spLogFile);
            //
            for (int i = 0; i < logHeader.dwLogCount; i++)
            {

                spLogBuff spLogLine = new spLogBuff();
                try
                {
                    spLogLine.dwNextLogOffset = strRead.ReadUInt32();
                    spLogLine.dwPreLogOffset = strRead.ReadUInt32();
                    spLogLine.dwNowLogOffset = strRead.ReadUInt32();
                    spLogLine.dwCurIndex = strRead.ReadUInt32();

                    spLogLine.nGrade = strRead.ReadInt32();
                    //int u = Convert.ToInt32(logCount);
                    if (spLogLine.nGrade == 0)
                    {
                        //Array.Resize(ref spLogLineArray, i);
                        //break;

                    }
                    spLogLine.dwProcessId = strRead.ReadUInt32();
                    spLogLine.dwThreadId = strRead.ReadUInt32();
                    spLogLine.year = strRead.ReadUInt16();
                    spLogLine.month = strRead.ReadUInt16();
                    spLogLine.day = strRead.ReadUInt16();
                    spLogLine.hour = strRead.ReadUInt16();
                    spLogLine.minute = strRead.ReadUInt16();
                    spLogLine.second = strRead.ReadUInt16();
                    spLogLine.milisecond = strRead.ReadUInt16();
                    spLogLine.csModuleNAme = strRead.ReadCString();
                    spLogLine.csSubSystem = strRead.ReadCString();
                    spLogLine.csType = strRead.ReadCString();
                    spLogLine.csInfo = strRead.ReadCString();
                    spLogLine.fileName = spLogFile.Name;
                    if (spLogLine.nGrade != 0)
                    { spLogLineArray[i] = spLogLine; }

                }
                catch (EndOfStreamException ex)
                {

                    break;
                }
               
            }


            spLogLineArray = spLogLineArray.Where(x => x != null).ToArray();
            //16063-16064
            return spLogLineArray;
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            //add sp log names 
            spLogFileNames.Clear();
            OpenFileDialog openFileDialogBox = new OpenFileDialog();

            openFileDialogBox.Multiselect = true;
            openFileDialogBox.Filter = "SP Log | *.grglog";
            openFileDialogBox.ShowDialog();
            foreach (string fileName in openFileDialogBox.FileNames)
            {
                spLogFileNames.Add(fileName);
            }
            spLogCount = spLogFileNames.Count;


        }

        private void readSpLogsBtn_Click(object sender, EventArgs e)
        {

            ArrayList spLogFilesArrayList = new ArrayList();

            for (int i = 0; i < spLogFileNames.Count; i++)
            {
                spLogFilesArrayList.Add(createFileStreamHeaderSpLogLineArray(spLogFileNames[i]));
            }


        }

        public spLogBuff[] createFileStreamHeaderSpLogLineArray(string fileName)
        {
            FileStream spLogFile = File.OpenRead(fileName);
            tagLogHead spLogHeader = returnLogHeader(spLogFile);
            spLogBuff[] spLogLinesArray = returnSpLogLineArray(spLogFile, spLogHeader.dwLogCount);
            return spLogLinesArray;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            foreach (string str in spLogFileNames)
            {
                spLogBuff[] spLogLines = createFileStreamHeaderSpLogLineArray(str);
                foreach (spLogBuff spLogLine in spLogLines)
                {
                    if (spLogLine.csInfo.Contains("WFS_CMD_CDM_START_EXCHANGE") && spLogLine.csInfo.Contains("WFS_EXECUTE_COMPLETE"))
                    {

                        cashUnit.lppCdmCashUnit lppCdmCashUnitInStartExchange = parseOperations.extractCdmCashUnitInStartExchange(spLogLine);

                    }

                }


            }

        }

        private void allCashInBtn_Click(object sender, EventArgs e)
        {

            cashDepositTransaction.cashCountTotal = 0;
            cashDepositTransaction.hrCashInTotal = 0;
            cashDepositTransaction.iStoreMoneyTotal = 0;
            cashDepositTransaction.hrCashInEndTotal = 0;
            cashDepositTransaction.cashInEndTotal = 0;
            cashDepositTransaction.iRetractTotal = 0;

            foreach (string spLogFileNameStr in spLogFileNames)
            {
                FileStream spLogFile = File.OpenRead(spLogFileNameStr);
                tagLogHead logHeader = returnLogHeader(spLogFile);
                if (logHeader.dwSize != 28)
                {
                    MessageBox.Show("SP Log format is not correct. Please check the file.\r\n" + spLogFileNameStr);
                    return;
                }


                spLogBuff[] spLogLines = createFileStreamHeaderSpLogLineArray(spLogFileNameStr);

                List<Form1.spLogBuff> cashInLines = cashDepositTransaction.returnCashInLines(spLogLines);

                if (spLogFileNames.Count == 1 && cashInLines.Count == 0)
                { MessageBox.Show("No CashIn Transaction in the log " + spLogFileNameStr); }

                cashInLines.Reverse();


                while (cashInLines.Count > 0)
                {

                    int removesIndex = parseOperations.returnCashInTransaction(cashInLines).Count;
                    List<Form1.spLogBuff> cashInReturnLines = parseOperations.returnCashInTransaction(cashInLines);
                    bool moreCashInLines = cashDepositTransaction.prepareCashInObjectsForCompareCashInInformation(cashInReturnLines);

                    if (moreCashInLines == true)
                    {
                        cashInLines.RemoveRange(0, removesIndex);
                    }
                    else
                    {
                        cashInLines.Clear();
                    }
                }

                printOperations.allCashInObjectsTotal(cashDepositTransaction.cashCountTotal, cashDepositTransaction.hrCashInTotal, cashDepositTransaction.iStoreMoneyTotal, cashDepositTransaction.hrCashInEndTotal, cashDepositTransaction.cashInEndTotal, cashDepositTransaction.iRetractTotal);




            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void clearTextBoxesBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            dataGridView1.Rows.Clear();

        }

        private void searchKeywordBtn_Click_1(object sender, EventArgs e)
        {

            List<spLogBuff[]> spLogLines = new List<spLogBuff[]>();

            foreach (string spLogFileNameStr in spLogFileNames)
            {

                //FileStream spLogFile = File.OpenRead(spLogFileNameStr);
                //tagLogHead logHeader = returnLogHeader(spLogFile);
                spLogBuff[] spLogLinesArray = createFileStreamHeaderSpLogLineArray(spLogFileNameStr);
                spLogLines.Add(spLogLinesArray);


            }

            foreach (spLogBuff[] spLogLineArray in spLogLines)
            {
                foreach (spLogBuff spLogLine in spLogLineArray)
                {
                    if (spLogLine.csInfo.Contains(textBox1.Text))
                    {

                        richTextBox1.Text += textBox1.Text + "\t" + spLogLine.day + "." + spLogLine.month + "." + spLogLine.year + " " + spLogLine.hour + ":" + spLogLine.minute + "\r\n";



                    }



                }
            }

            //List<cashUnit.lppCashIn> cashInEndList = new List<cashUnit.lppCashIn>();

            //List<List<cashUnit.lppCashIn>> listOfCashInEndList = new List<List<cashUnit.lppCashIn>>();

            //List<cashUnit.lppNoteNumber> cashInEndNoteNumber = new List<cashUnit.lppNoteNumber>();





            ////#region find deposit to AC
            ////foreach (spLogBuff[] spLogLineArray in spLogLines)
            ////{
            ////    foreach (spLogBuff spLogLine in spLogLineArray)
            ////    {

            ////        string valueD = dataGridView1.SelectedCells[0].Value.ToString();
            ////        char[] splitString = { '+' };
            ////        string[] keyWords = textBox1.Text.Split(splitString);


            ////        if (spLogLine.csType == "MessageSuccess" && dataGridView1.SelectedCells[0].Value.ToString() == spLogLine.csModuleNAme && spLogLine.csInfo.Contains(keyWords[0]) && spLogLine.csInfo.Contains(keyWords[1]))
            ////        {
            ////            cashInEndList = parseOperations.parseStringCashInEnd(spLogLine);
            ////            foreach (cashUnitCIM.lppCashIn cashInEnd in cashInEndList)
            ////            {
            ////                for (int i = 0; i < cashInEnd.lppNoteNumberListStruct.lppNoteNumberStructArray.Count(); i++)
            ////                {
            ////                    if ( cashInEnd.cUnitID == "S0_I1" || cashInEnd.cUnitID == "S0_I2" || cashInEnd.cUnitID == "S0_I3")
            ////                    {

            ////                        cashInEndNoteNumber.Add(cashInEnd.lppNoteNumberListStruct.lppNoteNumberStructArray[i]);
            ////                    }
            ////                }
            ////            }


            ////        }

            ////    }


            ////}
            ////#endregion 


            //cashUnit.iRetractEnd iRetractEndObject = new cashUnit.iRetractEnd();
            //List<cashUnit.asDenomInfo> asDenomInfoObjectList = new List<cashUnit.asDenomInfo>();

            //#region find rejected notes AC
            //foreach (spLogBuff[] spLogLineArray in spLogLines)
            //{
            //    foreach (spLogBuff spLogLine in spLogLineArray)
            //    {

            //        //string valueD = dataGridView1.SelectedCells[0].Value.ToString();
            //        char[] splitString = { '+' };
            //        string[] keyWords = textBox1.Text.Split(splitString);

            //        // cashUnitCIM.iRetractEnd parseStringIRetractEnd
            //        if (spLogLine.csInfo.Contains("p_byRetractPos:1"))
            //        {
            //            iRetractEndObject = parseOperations.parseStringIRetractEnd(spLogLine);
            //            foreach (cashUnit.asDenomInfo denomInfo in iRetractEndObject.denomInfoList)
            //            {

            //                asDenomInfoObjectList.Add(denomInfo);

            //            }


            //        }

            //    }
            //    // spLogLine.csType == "CallDevExeSuccess" && dataGridView1.SelectedCells[0].Value.ToString() == spLogLine.csModuleNAme && spLogLine.csInfo.Contains(keyWords[0]) && spLogLine.csInfo.Contains(keyWords[1]))

            //}
            //#endregion
            //foreach (cashUnit.asDenomInfo denomInfo in asDenomInfoObjectList)
            //{
            //    richTextBox1.Text += denomInfo.iNum.ToString() + "-" + denomTranslation.returnNoteInfo(denomInfo.iCode).denomValue.ToString() + "=" + denomInfo.iNum * denomTranslation.returnNoteInfo(denomInfo.iCode).denomValue + "\r\n";

            //}




            //foreach (cashUnitCIM.lppNoteNumber cashInEnd in cashInEndNoteNumber)
            //{
            //    richTextBox1.Text += cashInEnd.ulCount * denomTranslation.returnNoteInfo(cashInEnd.usNoteID).denomValue + "\r\n";
            //    //richTextBox1.Text += parseOperations.extractDateTimeFromSpLogLine(spLogLine) + "\r\n";
            //    //richTextBox1.Text += "File Name : " + spLogLine.fileName + "\r\n";
            //    //richTextBox1.Text += "---------------------------------------------------------\r\n";
            //}

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            List<spLogBuff[]> spLogLines = new List<spLogBuff[]>();
            List<string> phyDevs = new List<string>();
            List<string> spLogFileNamesMod =    new List<string> ();

            if (spLogFileNames.Count > 10)
            {

                spLogFileNamesMod.AddRange(spLogFileNames);
                spLogFileNames.RemoveRange(0, 9);
                spLogFileNamesMod.RemoveRange(10, spLogFileNamesMod.Count - 10);
            
            }

            foreach (string spLogFileNameStr in spLogFileNames)
            {

                spLogBuff[] spLogLinesArray = createFileStreamHeaderSpLogLineArray(spLogFileNameStr);
                spLogLines.Add(spLogLinesArray);
            }


            foreach (spLogBuff[] spLogLineArray in spLogLines)
            {

                foreach (spLogBuff spLogLine in spLogLineArray)
                {
                    if (spLogLine != null)
                    {

                        while (!phyDevs.Contains(spLogLine.csModuleNAme))
                        {

                            phyDevs.Add(spLogLine.csModuleNAme);
                            this.dataGridView1.Rows.Add(spLogLine.csModuleNAme);

                        }

                    }

                }

            }

        }

        private void button2_Click_2(object sender, EventArgs e)
        {

            List<spLogBuff[]> spLogLines = new List<spLogBuff[]>();
            List<spLogBuff> spLogErrorLines = new List<spLogBuff>();

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int columnindex = dataGridView1.CurrentCell.ColumnIndex;
            List<string> errorCodes = new List<string>();
            string physicalDev = dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString();


            foreach (string spLogFileNameStr in spLogFileNames)
            {

                spLogBuff[] spLogLinesArray = createFileStreamHeaderSpLogLineArray(spLogFileNameStr);
                spLogLines.Add(spLogLinesArray);
            }
            spLogBuff spLogLineR = new spLogBuff();
            foreach (spLogBuff[] spLogLineArray in spLogLines)
            {
                foreach (spLogBuff spLogLine in spLogLineArray)
                {
                    spLogLineR = spLogLine;
                    if (physicalDev == spLogLine.csModuleNAme)
                    {
                        string[] stringSeparators = new string[] { "\r\n" };
                        string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);

                        foreach (string str in cuParsedArray)
                        {

                            if ((str.Contains("iLogicCode") || str.Contains("iPhyCode")) && (!str.Contains("iLogicCode: 0") && !str.Contains("iPhyCode: 0")))
                            {

                                if (!errorCodes.Contains(str))
                                {
                                    errorCodes.Add(str);
                                    spLogErrorLines.Add(spLogLine);
                                }
                            }
                        }
                    }
                }
            }

            foreach (spLogBuff spLogErrorLine in spLogErrorLines)
            {


                richTextBox1.Text += spLogErrorLine.csInfo + "\r\n";
                richTextBox1.Text += parseOperations.extractDateTimeFromSpLogLine(spLogErrorLine) + "\r\n";
                richTextBox1.Text += "--------------------------------------------------------------";

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<cashUnit.iStoreMoneyEx> iStoreMoneyExList = new List<cashUnit.iStoreMoneyEx>();
            List<cashUnit.asDenomInfo> asDenomInfoList = new List<cashUnit.asDenomInfo>();

            foreach (string str in spLogFileNames)
            {
                spLogBuff[] spLogLines = createFileStreamHeaderSpLogLineArray(str);
                foreach (spLogBuff spLogLine in spLogLines)
                {

                    if (spLogLine != null)
                    {
                        if (spLogLine.csInfo.Contains("iStoreMoneyEx end") || spLogLine.csInfo.Contains("iStoreMoney,"))
                        {

                            cashUnit.iStoreMoneyEx iStoreMoneyExLine = parseOperations.parseStringIStoreMoneyEx(spLogLine);
                            iStoreMoneyExList.Add(iStoreMoneyExLine);
                            if (iStoreMoneyExLine.cassetteInfoList.Count == 0)
                            {

                                richTextBox1.Text += "Recycle Cassette slot: 0 -  Count: 0 \r\n";
                            }
                            else if (iStoreMoneyExLine.cassetteInfoList.Count != 0)
                            {
                                foreach (cashUnit.asInOutCassetteInfo asInOutCassetteInfo in iStoreMoneyExLine.cassetteInfoList)
                                {

                                    richTextBox1.Text += "Recycle Cassette slot: " + asInOutCassetteInfo.iCassetteSlotNo + " - Count: " + asInOutCassetteInfo.iNum + "\r\n";

                                }
                            }
                            foreach (cashUnit.asDenomInfo asDenomInfo in iStoreMoneyExLine.denomInfoList)
                            {


                                richTextBox1.Text += "NoteId: " + asDenomInfo.iCode + " - " + "Count: " + asDenomInfo.iNum + "\r\n";
                                asDenomInfoList.Add(asDenomInfo);

                            }

                            richTextBox1.Text += "iLogicCode: " + iStoreMoneyExLine.iLogicCode + "\r\n";
                            richTextBox1.Text += "Ac-A: " + iStoreMoneyExLine.acCassetteInfoList.iAbAreaA + "\r\n";
                            richTextBox1.Text += "Ac-B: " + iStoreMoneyExLine.acCassetteInfoList.iAbAreaB + "\r\n";
                            richTextBox1.Text += "Ac-C: " + iStoreMoneyExLine.acCassetteInfoList.iAbAreaC + "\r\n";
                            richTextBox1.Text += "Date Time: " + iStoreMoneyExLine.iStoreMoneyExDateTime + "\r\n";
                            richTextBox1.Text += "--------------------------------------------\r\n";

                        }
                        else if (spLogLine.csInfo.Contains("iRetract end,") || spLogLine.csInfo.Contains("iRetract,"))
                        {

                            List<cashUnit.asDenomInfo> asDenomInfolistFromiRetractEnd = parseOperations.parseStringIRetractEndForAsDenomInfoList(spLogLine);

                            foreach (cashUnit.asDenomInfo asDenomInfoObject in asDenomInfolistFromiRetractEnd)
                            {

                                asDenomInfoList.Add(asDenomInfoObject);
                            }


                        }
                    }
                }


            }

            List<denomTranslation> noteCurrencyValueAmount = denomTranslation.displayCashInCurrencyValueCount(asDenomInfoList);


            richTextBox2.Text += "Currency" + ',' + "Value" + ',' + "Count" + ',' + "Amount" + "\r\n";

            foreach (denomTranslation noteCurrencyValueAmountElement in noteCurrencyValueAmount)
            {

                richTextBox2.Text += noteCurrencyValueAmountElement.denomType + ',' + noteCurrencyValueAmountElement.denomValue + ',' + noteCurrencyValueAmountElement.denomCount + ',' + noteCurrencyValueAmountElement.denomValue * noteCurrencyValueAmountElement.denomCount + "\r\n";



                //{noteCurrencyValueAmountElement.denomType 
                //    richTextBox2.Text += "Currency: " + noteCurrencyValueAmountElement.denomType + "\r\n";
                //    richTextBox2.Text += "Value: " + noteCurrencyValueAmountElement.denomValue + "\r\n";
                //    richTextBox2.Text += "Count: " + noteCurrencyValueAmountElement.denomCount + "\r\n";
                //    richTextBox2.Text += "Amount: " + noteCurrencyValueAmountElement.denomValue * noteCurrencyValueAmountElement.denomCount + "\r\n";
                //    //richTextBox2.Text += "Date Time: " + iStoreMoneyExLine.iStoreMoneyExDateTime + "\r\n";
                //    richTextBox2.Text += "--------------------------------------------\r\n";


            }


            List<denomTranslation> cashInTotalsByCurrency = denomTranslation.cashInTotalsByCurrency(noteCurrencyValueAmount);

            richTextBox3.Text += "Currency" + ',' + "Amount" + "\r\n";

            foreach (denomTranslation cashInTotalElement in cashInTotalsByCurrency)
            {
                richTextBox3.Text += cashInTotalElement.denomType + ',' + cashInTotalElement.denomTotal + "\r\n";


            }

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            List<spLogBuff[]> spLogLines = new List<spLogBuff[]>();
            List<cashUnit.updateCashUnitInfo> cashUnitInfoListList = new List<cashUnit.updateCashUnitInfo>();
            List<cashUnit.lppCashIn> lppCashInEndList = new List<cashUnit.lppCashIn>();
            List<List<cashUnit.lppCashIn>> lppCashInEndListList = new List<List<cashUnit.lppCashIn>>();

            foreach (string spLogFileNameStr in spLogFileNames)
            {
                //FileStream spLogFile = File.OpenRead(spLogFileNameStr);
                //tagLogHead logHeader = returnLogHeader(spLogFile);
                spLogBuff[] spLogLinesArray = createFileStreamHeaderSpLogLineArray(spLogFileNameStr);
                spLogLines.Add(spLogLinesArray);
            }

            foreach (spLogBuff[] spLogBuffArray in spLogLines)
            {
                foreach (spLogBuff spLogBuff in spLogBuffArray)
                {
                    if (spLogBuff.csType == "MessageSuccess" && spLogBuff.csInfo.Contains("WFS_CMD_CIM_CASH_IN_END"))
                    {
                        lppCashInEndList = parseOperations.parseStringCashInEnd(spLogBuff);
                        lppCashInEndListList.Add(lppCashInEndList);

                    }
                    else if (spLogBuff.csSubSystem == "CallDevExeSuccess" && !spLogBuff.csInfo.Contains("iStoreMoney Start") && spLogBuff.csInfo.Contains("iStoreMoney"))
                    {



                    }

                    else if (spLogBuff.csSubSystem == "CashUnit" && spLogBuff.csInfo.StartsWith("Update"))
                    {
                        cashUnit.updateCashUnitInfo cashUnitInfoList = parseOperations.parseUpdateCashUnitInfo(spLogBuff.csInfo);
                        if (cashUnitInfoList.updatedCassettesList.Count == 0)
                        { }
                        else if (cashUnitInfoList.updatedCassettesList.Count != 0)
                        { cashUnitInfoListList.Add(cashUnitInfoList); }
                    }
                }
            }



        }

        private void richTextBox2_MouseDown(object sender, MouseEventArgs e)
        {

            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMenuStrip1.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }

        }

        private void copyToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(richTextBox2.Text);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { '\\' };

            List<spLogBuff[]> spLogLines = new List<spLogBuff[]>();

            foreach (string spLogFileNameStr in spLogFileNames)
            {
                //FileStream spLogFile = File.OpenRead(spLogFileNameStr);
                //tagLogHead logHeader = returnLogHeader(spLogFile);
                spLogBuff[] spLogLinesArray = createFileStreamHeaderSpLogLineArray(spLogFileNameStr);
                spLogLines.Add(spLogLinesArray);
            }


            foreach (spLogBuff[] spLogLineArray in spLogLines)
            {

                foreach (spLogBuff spLogLine in spLogLineArray)
                {
                    if (spLogLine != null)
                    {

                        if (spLogLine.csInfo.Contains("WFS_IDC_DEVHWERROR"))
                        {

                            foreach (string str in spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None))
                            {
                                if (str.Contains(@"0ErrorCode="))
                                {
                                    foreach (string strb in str.Split(charSeperators))
                                    {
                                        if (strb.Contains(@"0ErrorCode="))
                                        {
                                            richTextBox1.Text += strb + "\r\n";
                                            richTextBox1.Text += spLogLine.day.ToString() + " ." + spLogLine.month.ToString() + " ." + spLogLine.year.ToString() + " " + spLogLine.hour + ":" + spLogLine.minute + ":" + spLogLine.second + "." + spLogLine.milisecond + "\r\n";
                                            richTextBox1.Text += "-------------------------------------------------------------------\r\n"; ;

                                        }
                                    }





                                }



                            }


                        }

                    }

                }

            }


        }

        private void button7_Click_1(object sender, EventArgs e)
        {

            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { '\\' };
            List<spLogBuff[]> spLogLines = new List<spLogBuff[]>();
          

            foreach (string spLogFileNameStr in spLogFileNames)
            {
                //FileStream spLogFile = File.OpenRead(spLogFileNameStr);
                //tagLogHead logHeader = returnLogHeader(spLogFile);
                spLogBuff[] spLogLinesArray = createFileStreamHeaderSpLogLineArray(spLogFileNameStr);
                spLogLines.Add(spLogLinesArray);
            }
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int columnindex = dataGridView1.CurrentCell.ColumnIndex;
            foreach (spLogBuff[] spLogLineArray in spLogLines)
            {

                foreach (spLogBuff spLogLine in spLogLineArray)
                {
                    if (spLogLine != null)
                    {
                        if (spLogLine.csType == "EventMessage")
                        {
                            if (spLogLine.csInfo.Contains("MessageType: WFS_SYSTEM_EVENT") && spLogLine.csInfo.Contains("lpszPhysicalName = [" + dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString() + "]"))
                            {

                                string[] csInfoStringArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
                                for (int i = 0; i < csInfoStringArray.Length; i++)
                                {
                                    if (csInfoStringArray[i].Contains("dwState"))
                                    {
                                        richTextBox1.Text += csInfoStringArray[i] + "\t" + spLogLine.day + "." + spLogLine.month + "." + spLogLine.year + "\t" + spLogLine.hour + "." + spLogLine.minute + "." + spLogLine.second + "\r\n"; 
                                    
                                    
                                    }
                                
                                
                                
                                }



                            }

                        }


                    }
                }
            }

        }

        public void groupSpLogFilesByTen(List<string> spLogFileNames)
        { }

    } 





}






