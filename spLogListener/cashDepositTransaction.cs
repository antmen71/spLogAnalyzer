using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spLogListener;
using System.Collections;
using System.Windows.Forms;


namespace spLogListener
{
    public class cashDepositTransaction
    {


        spLogListener.Form1.spLogBuff spLogLine = new Form1.spLogBuff();

        public static List<Form1.spLogBuff> allCashInLines(spLogListener.Form1.spLogBuff[] spLogFile)
        {
            List<Form1.spLogBuff> allCashInLinesArray = new List<Form1.spLogBuff>();

            foreach (spLogListener.Form1.spLogBuff spLogLine in spLogFile)
            {
                //spLogLine.csModuleNAme == "GrgCRM_CRM9250_XFS30" && spLogLine.csType == "MessageSuccess" && spLogLine.csInfo.Contains("WFS_EXECUTE_COMPLETE") &&
                if (spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_END") || spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN") || spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_START"))
                {

                    allCashInLinesArray.Add(spLogLine);

                }

            }
            return allCashInLinesArray;

        }

        public static List<Form1.spLogBuff> returnCashInEndLines(Form1.spLogBuff[] allCashInLines)
        {

            List<Form1.spLogBuff> cashInEndLines = new List<Form1.spLogBuff>();
            foreach (spLogListener.Form1.spLogBuff spLogLine in allCashInLines)
            {

                /*spLogLine.csModuleNAme == "GrgCRM_CRM9250_XFS30" && spLogLine.csType == "MessageSuccess" && spLogLine.csInfo.Contains("WFS_EXECUTE_COMPLETE") &&*/
                if (spLogLine.csType == "MessageSuccess" && spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_END"))
                {

                    cashInEndLines.Add(spLogLine);

                }



            }

            return cashInEndLines;

        }

        public static List<Form1.spLogBuff> returnCashInStartLines(Form1.spLogBuff[] allCashInLines)
        {

            List<Form1.spLogBuff> cashInStartLines = new List<Form1.spLogBuff>();
            foreach (spLogListener.Form1.spLogBuff spLogLine in allCashInLines)
            {

                /*spLogLine.csModuleNAme == "GrgCRM_CRM9250_XFS30" && spLogLine.csType == "MessageSuccess" && spLogLine.csInfo.Contains("WFS_EXECUTE_COMPLETE") &&*/
                if (spLogLine.csType == "MessageSuccess" && spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_START"))
                {
                    cashInStartLines.Add(spLogLine);
                }



            }

            return cashInStartLines;

        }

        public static bool prepareCashInObjectsForCompareCashInInformation(List<Form1.spLogBuff> cashInLines)
        {
            bool moreCashInLines;

            if (cashInLines.Count == 0)
            { moreCashInLines = false; }
            else
            {
                //compare - cash in end lines
                List<cashUnit.iCashCount> iCashCountObjectList = new List<cashUnit.iCashCount>();//public List<asDenomInfo> denomInfoList
                List<Form1.spLogBuff> cashInTransactionLines = parseOperations.returnCashInTransaction(cashInLines);
                cashUnit.asDenomInfo cashInObjectAsDenomInfo = new cashUnit.asDenomInfo();
                List<cashUnit.lppCashIn> cashInEndObjectList = new List<cashUnit.lppCashIn>();
                List<cashUnit.asDenomInfo> cashInObjectAsDenomInfoList = new List<cashUnit.asDenomInfo>();
                List<cashUnit.hrCashIn> hrCashInObjectList = new List<cashUnit.hrCashIn>();
                List<cashUnit.iStoreMoneyEx> iStoreMoneyExObjectList = new List<cashUnit.iStoreMoneyEx>();
                List<cashUnit.hrCashInEnd> hrCashInEndObjectList = new List<cashUnit.hrCashInEnd>();
                cashUnit.wfsCmdCimCashIn wfsCmdCimCashInObject = new cashUnit.wfsCmdCimCashIn();
                List<cashUnit.cashInEndResult> cashInEndResultObjectList = new List<cashUnit.cashInEndResult>();
                cashUnit.iRetractEnd iRetractEndObject = new cashUnit.iRetractEnd();
                RichTextBox rtxt = Application.OpenForms["Form1"].Controls["richTextBox1"] as RichTextBox;

                foreach (Form1.spLogBuff spLogLine in cashInLines) //cashInTransactionLines
                {

                    if (spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_ROLLBACK"))
                    { }

                    else if (spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_END")) //WFS_CMD_CIM_CASH_IN_END
                    {
                        cashInEndObjectList = parseOperations.parseStringCashInEnd(spLogLine);
                    }
                    else if (spLogLine.csInfo.Contains("hrCashInEnd,"))
                    {
                        hrCashInEndObjectList.Add(parseOperations.parseStringHrCashinEndInfo(spLogLine));
                    }
                    else if (spLogLine.csInfo.Contains("iStoreMoneyEx end"))
                    {
                        iStoreMoneyExObjectList.Add(parseOperations.parseStringIStoreMoneyEx(spLogLine));
                    }
                    else if (spLogLine.csInfo.Contains("iCashCount end")) //iCashCount end
                    {
                        iCashCountObjectList.Add(parseOperations.parseStringIcashCountInfo(spLogLine));
                    }
                    else if (spLogLine.csInfo.Contains("hrCashIn,"))
                    {
                        hrCashInObjectList.Add(parseOperations.parseStringHrCashinInfo(spLogLine));
                    }
                    else if (spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN") && !spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_START") && spLogLine.csInfo.Contains("WFS_EXECUTE_COMPLETE"))
                    {
                        wfsCmdCimCashInObject = parseOperations.parseCmdCimCashInObject(spLogLine);
                    }
                    else if (spLogLine.csInfo.Contains("iRetract end"))
                    {
                        iRetractEndObject = parseOperations.parseStringIRetractEnd(spLogLine);
                        rtxt.Text += iRetractEndObject.iTotalCount + "\r\n";
                        rtxt.Text += iRetractEndObject.iUnknownCount + "\r\n";
                        rtxt.Text += "-------------------------------------------------\r\n";

                    }

                }
                foreach (cashUnit.lppCashIn cashInEndObject in cashInEndObjectList)
                {
                    for (int i = 0; i < cashInEndObject.lppNoteNumberListStruct.usNumOfNoteNumbers; i++)
                    {
                        cashInObjectAsDenomInfo.iCode = cashInEndObject.lppNoteNumberListStruct.lppNoteNumberStructArray[i].usNoteID;
                        cashInObjectAsDenomInfo.iNum = cashInEndObject.lppNoteNumberListStruct.lppNoteNumberStructArray[i].ulCount;
                        cashInObjectAsDenomInfoList.Add(cashInObjectAsDenomInfo);
                    }
                }
                moreCashInLines = true;
                string cashInResult = createCompareObjects(cashInEndObjectList, iCashCountObjectList, hrCashInObjectList, iStoreMoneyExObjectList, hrCashInEndObjectList, wfsCmdCimCashInObject, iRetractEndObject);
                #region


                //cashUnitCIM.iCashCount iCashCountObject = new cashUnitCIM.iCashCount();
                //cashUnitCIM.asDenomInfo iCashcountObjectAsDenomInfo = new cashUnitCIM.asDenomInfo();
                //List<cashUnitCIM.asDenomInfo> iCashCountObjectAsDenomInfoList = new List<cashUnitCIM.asDenomInfo>(); //compare - iCashCount
                //cashUnitCIM.hrCashin hrCashInObject = new cashUnitCIM.hrCashin();
                //cashUnitCIM.asDenomInfo hrCashInObjectAsDenomInfo = new cashUnitCIM.asDenomInfo();
                //List<cashUnitCIM.asDenomInfo> hrCashInObjectAsDenomInfoList = new List<cashUnitCIM.asDenomInfo>(); //compare - iCashCount
                //cashUnitCIM.iStoreMoneyEx iStoreMoneyExObject = new cashUnitCIM.iStoreMoneyEx();//public List<asDenomInfo> denomInfoList;
                //public List<asDenomInfo> denomInfoList;
                //cashUnitCIM.asDenomInfo iStoreMoneyExObjectAsDenomInfo = new cashUnitCIM.asDenomInfo();
                //List<cashUnitCIM.asDenomInfo> iStoreMoneyExObjectAsDenomInfoList = new List<cashUnitCIM.asDenomInfo>(); //compare - iCashCount
                //cashUnitCIM.hrCashInEnd hrCashInEndObject = new cashUnitCIM.hrCashInEnd();
                //cashUnitCIM.cashInEndResult cashInEndResultObject = new cashUnitCIM.cashInEndResult();
                //public List<hrCashInAccept> acceptList;
                //foreach (cashUnitCIM.iCashCount iCashCountObject in iCashCountObjectList)
                //{
                //    for (int i = 0; i < iCashCountObject.denomInfoList.Count; i++)
                //    {
                //        iCashcountObjectAsDenomInfo.iCode = iCashCountObject.denomInfoList[i].iCode;
                //        iCashcountObjectAsDenomInfo.iNum = iCashCountObject.denomInfoList[i].iNum;
                //        iCashCountObjectAsDenomInfoList.Add(iCashcountObjectAsDenomInfo);

                //    }

                //}

                //foreach (cashUnitCIM.hrCashIn hrCashInObject in hrCashInObjectList)
                //{

                //    for (int i = 0; i < hrCashInObject.acceptList.Count; i++)
                //    {
                //        hrCashInObjectAsDenomInfo.iCode = hrCashInObject.acceptList[i].code;
                //        hrCashInObjectAsDenomInfo.iNum = hrCashInObject.acceptList[i].count;
                //        hrCashInObjectAsDenomInfoList.Add(hrCashInObjectAsDenomInfo);
                //    }
                //}


                //foreach (cashUnitCIM.iStoreMoneyEx iStoreMoneyExObject in iStoreMoneyExObjectList)
                //{
                //    for (int i = 0; i < iStoreMoneyExObject.denomInfoList.Count; i++)
                //    {
                //        iStoreMoneyExObjectAsDenomInfo.iCode = iStoreMoneyExObject.denomInfoList[i].iCode;
                //        iStoreMoneyExObjectAsDenomInfo.iNum = iStoreMoneyExObject.denomInfoList[i].iNum;
                //        iStoreMoneyExObjectAsDenomInfoList.Add(iStoreMoneyExObjectAsDenomInfo);
                //    }
                //}

                //foreach (cashUnitCIM.hrCashInEnd hrCashInEndObject in hrCashInEndObjectList)
                //{

                //    for(int i = 0; i< hrCashInEndObject.cashInEndResultList.Count;i++)
                //    {
                //        cashInEndResultObject.slot = hrCashInEndObject.cashInEndResultList[i].slot;
                //        cashInEndResultObject.index = hrCashInEndObject.cashInEndResultList[i].index;
                //        cashInEndResultObject.count = Convert.ToInt16(hrCashInEndObject.cashInEndResultList[i].count);
                //        cashInEndResultObjectList.Add(cashInEndResultObject);
                //    }
                //}

                #endregion
            }
            return moreCashInLines;
        }

        public static string createCompareObjects(List<cashUnit.lppCashIn> cashInEndObjectList, List<cashUnit.iCashCount> iCashCountObjectList, List<cashUnit.hrCashIn> hrCashInObjectList, List<cashUnit.iStoreMoneyEx> iStoreMoneyExObjectList, List<cashUnit.hrCashInEnd> hrCashInEndObjectList, cashUnit.wfsCmdCimCashIn wfsCmdCimCashInObject, cashUnit.iRetractEnd iRetractEndObject)
        {
            cashUnit.compareObject iCashCountCompareObject = new cashUnit.compareObject();
            List<cashUnit.compareObject> iCashCountCompareObjectList = new List<cashUnit.compareObject>();

            cashUnit.compareObject hrCashInCompareObject = new cashUnit.compareObject();
            List<cashUnit.compareObject> hrCashInCompareObjectList = new List<cashUnit.compareObject>();

            cashUnit.compareObject wfsCmdCimCashInCompareObject = new cashUnit.compareObject();
            List<cashUnit.compareObject> wfsCmdCimCashInCompareObjectList = new List<cashUnit.compareObject>();

            cashUnit.compareObject iStoreMoneyExCompareObject = new cashUnit.compareObject();
            List<cashUnit.compareObject> iStoreMoneyExCompareObjectList = new List<cashUnit.compareObject>();

            cashUnit.compareObject hrCashInEndCompareObject = new cashUnit.compareObject();
            List<cashUnit.compareObject> hrCashInEndCompareObjectList = new List<cashUnit.compareObject>();

            cashUnit.compareObject cashInEndCompareObject = new cashUnit.compareObject();
            List<cashUnit.compareObject> cashInEndCompareObjectList = new List<cashUnit.compareObject>();

            cashUnit.compareObject iRetractEndCompareObject = new cashUnit.compareObject();
            List<cashUnit.compareObject> iRetractEndCompareObjectList = new List<cashUnit.compareObject>();

            string result = "";


            foreach (cashUnit.iCashCount iCashCountObject in iCashCountObjectList)
            {
                for (int i = 0; i < iCashCountObject.denomInfoList.Count(); i++)
                {
                    denomTranslation noteValue = denomTranslation.returnNoteInfo(iCashCountObject.denomInfoList[i].iCode);
                    iCashCountCompareObject.iCode = iCashCountObject.denomInfoList[i].iCode;
                    iCashCountCompareObject.iCount = iCashCountObject.denomInfoList[i].iNum;
                    iCashCountCompareObject.compareObjectDateTime = iCashCountObject.iCashCountDateTime;
                    iCashCountCompareObjectList.Add(iCashCountCompareObject);

                }
            }

            foreach (cashUnit.hrCashIn hrCashInObject in hrCashInObjectList)
            {

                for (int i = 0; i < hrCashInObject.acceptList.Count; i++)
                {
                    denomTranslation noteValue = denomTranslation.returnNoteInfo(hrCashInObject.acceptList[i].code);

                    hrCashInCompareObject.iCode = hrCashInObject.acceptList[i].code;
                    hrCashInCompareObject.iCount = hrCashInObject.acceptList[i].count;
                    hrCashInCompareObject.compareObjectDateTime = hrCashInObject.hrCashInDateTime;

                    hrCashInCompareObjectList.Add(hrCashInCompareObject);
                }
            }


            foreach (cashUnit.lppNoteNumber lppNumberList in wfsCmdCimCashInObject.lppNoteNumberListStruct.lppNoteNumberStructArray)
            {
                wfsCmdCimCashInCompareObject.iCode = lppNumberList.usNoteID;
                wfsCmdCimCashInCompareObject.iCount = lppNumberList.ulCount;
                wfsCmdCimCashInCompareObjectList.Add(wfsCmdCimCashInCompareObject);
            }

            foreach (cashUnit.iStoreMoneyEx iStoreMoneyExObject in iStoreMoneyExObjectList)
            {
                for (int i = 0; i < iStoreMoneyExObject.denomInfoList.Count; i++)
                {
                    iStoreMoneyExCompareObject.iCode = iStoreMoneyExObject.denomInfoList[i].iCode;
                    iStoreMoneyExCompareObject.iCount = iStoreMoneyExObject.denomInfoList[i].iNum;
                    iStoreMoneyExCompareObject.compareObjectDateTime = iStoreMoneyExObject.iStoreMoneyExDateTime;
                    iStoreMoneyExCompareObjectList.Add(iStoreMoneyExCompareObject);
                }
            }

            foreach (cashUnit.hrCashInEnd hrCashInEndObject in hrCashInEndObjectList)
            {
                for (int i = 0; i < hrCashInEndObject.cashInEndResultList.Count; i++)
                {
                    hrCashInEndCompareObject.cassetteID = "S" + hrCashInEndObject.cashInEndResultList[i].slot + "_I" + hrCashInEndObject.cashInEndResultList[i].index;
                    hrCashInEndCompareObject.iCount = hrCashInEndObject.cashInEndResultList[i].count;
                    hrCashInEndCompareObject.compareObjectDateTime = hrCashInEndObject.hrCashInEndDateTime;
                    hrCashInEndCompareObjectList.Add(hrCashInEndCompareObject);
                }
            }


            foreach (cashUnit.lppCashIn cashInEndObject in cashInEndObjectList)
            {
                for (int i = 0; i < cashInEndObject.lppNoteNumberListStruct.lppNoteNumberStructArray.Count(); i++)
                {
                    cashInEndCompareObject.cassetteID = cashInEndObject.cUnitID;
                    cashInEndCompareObject.iCode = cashInEndObject.lppNoteNumberListStruct.lppNoteNumberStructArray[i].usNoteID;
                    cashInEndCompareObject.iCount = cashInEndObject.lppNoteNumberListStruct.lppNoteNumberStructArray[i].ulCount;
                    cashInEndCompareObject.cassetteID = cashInEndObject.lppNoteNumberListStruct.lppNoteNumberStructArray[i].lppPhysicalstruct.lpPhysicalPositionName;
                    cashInEndCompareObject.compareObjectDateTime = cashInEndObject.lppCashInDateTime;
                    cashInEndCompareObjectList.Add(cashInEndCompareObject);
                }
            }

            if (iRetractEndObject.denomInfoList != null)
            {
                foreach (cashUnit.asDenomInfo asDenomInfoList in iRetractEndObject.denomInfoList)
                {


                    iRetractEndCompareObject.iCode = asDenomInfoList.iCode;
                    iRetractEndCompareObject.iCount = Convert.ToInt16(asDenomInfoList.iNum);
                    iRetractEndCompareObject.compareObjectDateTime = iRetractEndObject.iRetractEndDateTime;
                    iRetractEndCompareObjectList.Add(iRetractEndCompareObject);



                }

            }

            foreach (cashUnit.compareObject obj in iCashCountCompareObjectList)
            {
                result += "iCashCount:\r\n" + "Casssette Id: " + obj.cassetteID + "\r\n" + "Note ID: " + obj.iCode + "\r\n" + "Count: " + obj.iCount + "\r\n" + "Date Time: " + obj.compareObjectDateTime + "\r\n" + "------------------------------------------------------------\r\n";


            }

            foreach (cashUnit.compareObject obj in hrCashInCompareObjectList)
            {
                result += "hrCashIn:\r\n" + "Casssette Id: " + obj.cassetteID + "\r\n" + "Note ID: " + obj.iCode + "\r\n" + "Count: " + obj.iCount + "\r\n" + "Date Time: " + obj.compareObjectDateTime + "\r\n" + "------------------------------------------------------------\r\n"; 

            }

            foreach (cashUnit.compareObject obj in iStoreMoneyExCompareObjectList)
            {
                result += "iStoreMoneyEx:\r\n" + "Casssette Id: " + obj.cassetteID + "\r\n" + "Note ID: " + obj.iCode + "\r\n" + "Count: " + obj.iCount + "\r\n" + "Date Time: " + obj.compareObjectDateTime + "\r\n" + "------------------------------------------------------------\r\n"; 

            }

            foreach (cashUnit.compareObject obj in hrCashInEndCompareObjectList)
            {
                result += "hrCashInEnd:\r\n" + "Casssette Id: " + obj.cassetteID + "\r\n" + "Note ID: " + obj.iCode + "\r\n" + "Count: " + obj.iCount + "\r\n" + "Date Time: " + obj.compareObjectDateTime + "\r\n" + "------------------------------------------------------------\r\n"; 

            }


            foreach (cashUnit.compareObject obj in cashInEndCompareObjectList)
            {
                result += "cashInEnd:\r\n" + "Casssette Id: " + obj.cassetteID + "\r\n" + "Note ID: " + obj.iCode + "\r\n" + "Count: " + obj.iCount + "\r\n" + "Date Time: " + obj.compareObjectDateTime + "\r\n" + "------------------------------------------------------------\r\n"; 

            }

            foreach (cashUnit.compareObject obj in iRetractEndCompareObjectList)
            {
                result += "iRetractEnd:\r\n" + "Casssette Id: " + obj.cassetteID + "\r\n" + "Note ID: " + obj.iCode + "\r\n" + "Count: " + obj.iCount + "\r\n" + "Date Time: " + obj.compareObjectDateTime + "\r\n" + "------------------------------------------------------------\r\n"; 
               
            }

            List<List<cashUnit.compareObject>> compareObjects = new List<List<cashUnit.compareObject>> { iCashCountCompareObjectList, hrCashInCompareObjectList, iStoreMoneyExCompareObjectList, hrCashInEndCompareObjectList, cashInEndCompareObjectList, iRetractEndCompareObjectList };
            compareCashInObjects(compareObjects);

            RichTextBox rtxt = Application.OpenForms["Form1"].Controls["richTextBox1"] as RichTextBox;
            rtxt.Text += result;
            return result;

        }


        public static int cashCountTotal = 0;
        public static int hrCashInTotal = 0;
        public static int iStoreMoneyTotal = 0;
        public static int hrCashInEndTotal = 0;
        public static int cashInEndTotal = 0;
        public static int iRetractTotal = 0;

        public static void compareCashInObjects(List<List<cashUnit.compareObject>> compareObjects)
        {
            

            denomTranslation cashCompareDenom = new denomTranslation();
            List<int> totalAmount = new List<int>();
            int compareObjectAmount = 0;

            foreach (List<cashUnit.compareObject> compareObjectList in compareObjects)
            {
                compareObjectAmount = 0;
                if (compareObjectList.Count == 1)
                {
                    for (int i = 0; i < compareObjectList.Count; i++)
                    {
                        cashCompareDenom = denomTranslation.returnNoteInfo(compareObjectList[i].iCode);
                        totalAmount.Add(cashCompareDenom.denomValue * compareObjectList[i].iCount);

                    }
                }
                else
                {

                    for (int i = 0; i < compareObjectList.Count; i++)
                    {
                        cashCompareDenom = denomTranslation.returnNoteInfo(compareObjectList[i].iCode);
                        compareObjectAmount += cashCompareDenom.denomValue * compareObjectList[i].iCount;
                    }

                    totalAmount.Add(compareObjectAmount);
                }
            }



            RichTextBox rtxt = Application.OpenForms["Form1"].Controls["richTextBox2"] as RichTextBox;
            RichTextBox rtxt1 = Application.OpenForms["Form1"].Controls["richTextBox3"] as RichTextBox;


            int totalOfArray = (totalAmount[0] + totalAmount[1] + totalAmount[2] + totalAmount[3] + totalAmount[4] + totalAmount[5]);


            if (totalAmount[5] == 0 && (totalAmount[0] + totalAmount[1] + totalAmount[2] + totalAmount[3] + totalAmount[4] + totalAmount[5]) / 4 == totalAmount[0])
            {
                rtxt.ForeColor = System.Drawing.Color.Black;
                rtxt.Text += "iCashCount :" + totalAmount[0] + "\r\n";
                rtxt.Text += "hrCashIn :" + totalAmount[1] + "\r\n";
                rtxt.Text += "iStoreMoneyEx :" + totalAmount[2] + "\r\n";
                rtxt.Text += "hrCashInEnd :" + totalAmount[3] + "\r\n";
                rtxt.Text += "cashInEnd :" + totalAmount[4] + "\r\n";
                rtxt.Text += "iRetract :" + totalAmount[5] + "\r\n";
                //rtxt.Text += "Date Time : " + compareObjects[0][0].compareObjectDateTime + "\r\n";
                rtxt.Text += "------------------------------------------------------------\r\n";
            }

            else if (totalAmount[5] != 0 && (totalAmount[0] + totalAmount[1] + totalAmount[2] + totalAmount[3] + totalAmount[4] + totalAmount[5]) / 4 == totalAmount[0])
            {
                rtxt.ForeColor = System.Drawing.Color.Black;
                rtxt.Text += "iCashCount :" + totalAmount[0] + "\r\n";
                rtxt.Text += "hrCashIn :" + totalAmount[1] + "\r\n";
                rtxt.Text += "iStoreMoneyEx :" + totalAmount[2] + "\r\n";
                rtxt.Text += "hrCashInEnd :" + totalAmount[3] + "\r\n";
                rtxt.Text += "cashInEnd :" + totalAmount[4] + "\r\n";
                rtxt.Text += "iRetract :" + totalAmount[5] + "\r\n";
                //rtxt.Text += "Date Time : " + compareObjects[0][0].compareObjectDateTime + "\r\n";
                rtxt.Text += "------------------------------------------------------------\r\n";
            }
            else
            {
                rtxt1.ForeColor = System.Drawing.Color.Red;
                rtxt1.Text += "iCashCount :" + totalAmount[0] + "\r\n";
                rtxt1.Text += "hrCashIn :" + totalAmount[1] + "\r\n";
                rtxt1.Text += "iStoreMoneyEx :" + totalAmount[2] + "\r\n";
                rtxt1.Text += "hrCashInEnd :" + totalAmount[3] + "\r\n";
                rtxt1.Text += "cashInEnd :" + totalAmount[4] + "\r\n";
                rtxt1.Text += "iRetract :" + totalAmount[5] + "\r\n";
                //rtxt1.Text += "Date Time : " + compareObjects[0][0].compareObjectDateTime + "\r\n";                
                rtxt1.Text += "------------------------------------------------------------\r\n";

            }
            cashCountTotal += totalAmount[0];
            hrCashInTotal += totalAmount[1];
            iStoreMoneyTotal += totalAmount[2];
            hrCashInEndTotal += totalAmount[3];
            cashInEndTotal += totalAmount[4];
            iRetractTotal += totalAmount[5];
            //iCashCountCompareObjectList, hrCashInCompareObjectList, iStoreMoneyExCompareObjectList, hrCashInEndCompareObjectList, cashInEndCompareObjectList 


        }

        public static cashUnit.iCashCount returnIcashCountObject(Form1.spLogBuff spLogLine)
        {
            cashUnit.iCashCount iCashCountObject = parseOperations.parseStringIcashCountInfo(spLogLine);
            return iCashCountObject;

        }

        public static cashUnit.hrCashIn hrCashInObject(Form1.spLogBuff spLogLine)
        {
            cashUnit.hrCashIn hrCashInObject = parseOperations.parseStringHrCashinInfo(spLogLine);
            return hrCashInObject;

        }

        public static cashUnit.iStoreMoneyEx iStoreMoneyExObject(Form1.spLogBuff spLogLine)
        {
            cashUnit.iStoreMoneyEx iStoreMoneyExObject = parseOperations.parseStringIStoreMoneyEx(spLogLine);
            return iStoreMoneyExObject;

        }

        public static List<Form1.spLogBuff> returnCashInLines(Form1.spLogBuff[] spLogFile)
        {
            List<Form1.spLogBuff> cashInLines = new List<Form1.spLogBuff>();

            //|| spLogLine.csInfo.Contains("ParamString: CashInResult:") iRetract end
            foreach (Form1.spLogBuff spLogLine in spLogFile)
            {
                if (spLogLine != null)
                {

                    if ((spLogLine.csType == "MessageSuccess" || spLogLine.csType == "MessageFail" || spLogLine.csType == "CallSpExeSuccess" || spLogLine.csType == "CallSpExeFail" || spLogLine.csType == "CallDevExeSuccess" || spLogLine.csType == "CallDevExeFail") && (spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_START") || spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_END") || spLogLine.csInfo.Contains("iCashCount end") || spLogLine.csInfo.Contains("iStoreMoneyEx end") || spLogLine.csInfo.Contains("hrCashInEnd") || spLogLine.csInfo.Contains("hrCashIn,") || spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN") || spLogLine.csInfo.Contains("iRetract end")) && spLogLine.csType != "WFPExecute")
                    {
                        cashInLines.Add(spLogLine);
                    }
                }
                }
                return cashInLines;

            }
        

        public static List<denomTranslation> totalDeposit(List<spLogListener.cashUnit.lppCashIn> allCashInArray)
        {


            List<string> distinctDenoms = denomTranslation.returnDistinctDenomTypes();
            List<string> totalDepositOut = new List<string>();
            List<denomTranslation> totalDepositDenomList = new List<denomTranslation>();
            int totalDeposit = 0;
            string denomTypeOut = "";
            foreach (cashUnit.lppCashIn lppCashIn in allCashInArray)
            {

                if (lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray.Count() == 1)
                {
                    denomTranslation noteValue = denomTranslation.returnNoteInfo(lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray[0].usNoteID);
                    denomTypeOut = noteValue.denomType;
                    foreach (string denomType in distinctDenoms)
                    {
                        if (noteValue.denomType == denomType)
                        {
                            int depositValue = noteValue.denomValue * lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray[0].ulCount;
                            totalDeposit += depositValue;
                            noteValue.denomTotal += depositValue;
                            noteValue.denomType = denomType;
                            //noteValue.denomValue = depositValue;
                            noteValue.denomCount = lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray[0].ulCount;
                            noteValue.requestID = lppCashIn.requestId;
                            totalDepositDenomList.Add(noteValue);
                        }
                    }
                    totalDepositOut.Add("Total " + noteValue.denomType + " deposited: " + totalDeposit.ToString());
                    //richTextBox2.Text = ("Total " + noteValue.denomType + " deposited: " + totalDeposit.ToString());
                }
                else if (lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray.Count() > 1)
                {
                    for (int lppNoteNumberCount = 0; lppNoteNumberCount < lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray.Count(); lppNoteNumberCount++)
                    {
                        denomTranslation noteValue = denomTranslation.returnNoteInfo(lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray[lppNoteNumberCount].usNoteID);
                        denomTypeOut = noteValue.denomType;
                        foreach (string denomType in distinctDenoms)
                        {
                            if (noteValue.denomType == denomType)
                            {
                                int depositValue = noteValue.denomValue * lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray[lppNoteNumberCount].ulCount;
                                totalDeposit += depositValue;
                                noteValue.denomTotal += depositValue;
                                noteValue.denomType = denomType;
                                //noteValue.denomValue = depositValue;
                                noteValue.denomCount = lppCashIn.lppNoteNumberListStruct.lppNoteNumberStructArray[lppNoteNumberCount].ulCount;
                                noteValue.requestID = lppCashIn.requestId;


                                totalDepositDenomList.Add(noteValue);
                            }
                        }
                        //totalDepositOut.Add("Total " + noteValue.denomType + " deposited: " + totalDeposit.ToString());
                        //richTextBox2.Text = ("Total " + noteValue.denomType + " deposited: " + totalDeposit.ToString());
                    }

                }
            }
            processTotalDepositDenomList(totalDepositDenomList);
            return totalDepositDenomList;

        }

        public static void processTotalDepositDenomList(List<denomTranslation> totalDepositDenomList)
        {


            List<string> denoms = new List<string>();
            foreach (denomTranslation denomInfo in totalDepositDenomList)
            {
                while (!denoms.Contains(denomInfo.denomType))
                    denoms.Add(denomInfo.denomType);
            }

            Dictionary<string, int> denomListoutDict = new Dictionary<string, int>();


            for (int i = 0; i < denoms.Count; i++)
            {
                int totalDeposit = 0;

                foreach (denomTranslation denomInfo in totalDepositDenomList)
                {
                    if (denoms[i] == denomInfo.denomType)
                    {
                        if (totalDepositDenomList.Count() == 0)
                        {
                            //richTextBox1.Text = "No Cash In Transaction.";
                        }
                        else
                        {
                            //richTextBox1.AppendText("Request ID: " + denomInfo.requestID + "\r\n");
                            //richTextBox1.AppendText("Currency: " + denomInfo.spCode + "\r\n");

                            //richTextBox1.AppendText("Denomination: " + denomInfo.denomType + "\r\n");
                            //richTextBox1.AppendText("Value: " + denomInfo.denomValue + "\r\n");
                            //richTextBox1.AppendText("Count: " + denomInfo.denomCount + "\r\n");
                            //richTextBox1.AppendText("Amount: " + denomInfo.denomTotal + "\r\n");
                            ////richTextBox1.AppendText(spLogFile.Name);
                            //richTextBox1.AppendText("------------------------------------------------------------------------\r\n");
                        }
                        totalDeposit += denomInfo.denomTotal;
                    }

                }
                denomListoutDict.Add(denoms[i], totalDeposit);


            }
            foreach (KeyValuePair<string, int> denomListout in denomListoutDict)
            {

                //richTextBox2.AppendText("Total deposited " + denomListout.Key + ": " + denomListout.Value.ToString() + "\r\n");
            }


        }

    }


}