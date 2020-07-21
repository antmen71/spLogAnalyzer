using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spLogListener;
using System.Linq;

namespace spLogListener
{
    public class parseOperations
    {

        public static List<cashUnit.lppCashIn> parseStringCashInEnd(Form1.spLogBuff cdmCULine)
        {

            List<cashUnit.lppCashIn> lppCashInObjectArray = new List<cashUnit.lppCashIn>();

            string[] stringSeparators = new string[] { "\r\n" };

            string[] cuParsedArray = cdmCULine.csInfo.Split(stringSeparators, StringSplitOptions.None);

            //usCount - kaç tane lppCashIn objesi olduğu, kaç tane kasetin sayacı değişti
            string usNumberFromcdmCULine = parseOperations.extractFromCdmCuLine(cuParsedArray, "usCount");
            short usCountValue = Convert.ToInt16(splitString(usNumberFromcdmCULine));

            //lppCashIn

            cashUnit.lppCashIn lppCashInObject = new cashUnit.lppCashIn();
            int lppCashInIndex = 0;
            List<int> indexLppCashIn = new List<int>();
            foreach (string str in cuParsedArray)
            {
                if (str.Contains("lppCashIn["))
                {

                    indexLppCashIn.Add(lppCashInIndex + 2);
                }
                else
                {


                }
                lppCashInIndex++;
            }

            //cashUnitCIM.lppPhysical lppPhysicalObject = new cashUnitCIM.lppPhysical();
            //List<cashUnitCIM.lppPhysical> lppPhysicalObjectList = new List<cashUnitCIM.lppPhysical>();

            //foreach (string str in cuParsedArray)
            //{
            //    if (str.Contains("lppCashIn["))
            //    {

            //        indexLppCashIn.Add(lppCashInIndex + 2);
            //    }
            //    lppCashInIndex++;
            //}



            //how many lppNoteNumber are there, and create a lppNoteNumberArray with length
            List<string> usNumberListcdmCULine = parseOperations.extractFromCdmCuLineArray(cuParsedArray, "usNumOfNoteNumbers = [");

            List<int> usNumberListValue = new List<int>();

            foreach (string str in usNumberListcdmCULine)
            { usNumberListValue.Add(Convert.ToInt16(splitString(str))); }

            int usNumOfNotesTotal = 0;
            foreach (int i in usNumberListValue)
            { usNumOfNotesTotal += i; }

            cashUnit.lppNoteNumber[] lppNoteNumberArray = new cashUnit.lppNoteNumber[usNumOfNotesTotal];


            int usNumOfNoteNumbersIndex = 0;

            List<int> indexUsNumOfNoteNumbers = new List<int>();
            foreach (string str in cuParsedArray)
            {
                if (str.Contains("lppNoteNumber["))
                {

                    indexUsNumOfNoteNumbers.Add(usNumOfNoteNumbersIndex + 2);

                }
                usNumOfNoteNumbersIndex++;
            }



            for (int i = 0; i < indexLppCashIn.Count; i++)
            {
                int index = indexLppCashIn[i];
                string[] croppedArray = new string[17];
                Array.Copy(cuParsedArray, index, croppedArray, 0, 17);
                lppCashInObject.usNumber = Convert.ToInt16(splitString(croppedArray[0]));
                lppCashInObject.fwType = splitString(croppedArray[1]);
                lppCashInObject.fwItemType = splitString(croppedArray[2]);
                lppCashInObject.cUnitID = splitString(croppedArray[3]);
                lppCashInObject.cUnitIDHex = splitStringHexArray(croppedArray[4]);
                lppCashInObject.cCurrencyID = splitString(croppedArray[5]);
                lppCashInObject.cCurrencyIDHex = splitStringHexArray(croppedArray[6]);
                lppCashInObject.ulValues = Convert.ToUInt16(splitString(croppedArray[7]));
                lppCashInObject.ulCashInCount = Convert.ToUInt16(splitString(croppedArray[8]));
                lppCashInObject.ulCount = Convert.ToUInt16(splitString(croppedArray[9]));
                lppCashInObject.ulMaximum = Convert.ToUInt16(splitString(croppedArray[10]));
                lppCashInObject.usStatus = splitString(croppedArray[11]);
                lppCashInObject.bAppLock = Convert.ToBoolean(splitString(croppedArray[12]));
                lppCashInObject.requestId = requestIdOperations.findRequestID(cdmCULine);
                lppCashInObject.lppNoteNumberListStruct.usNumOfNoteNumbers = Convert.ToInt16(splitString(croppedArray[15]));
                lppCashInObject.lppCashInDateTime = parseOperations.extractDateTimeFromSpLogLine(cdmCULine);

                //for (int numOfNotesIndexValue = 0; numOfNotesIndexValue < lppCashInObject.lppNoteNumberListStruct.usNumOfNoteNumbers; numOfNotesIndexValue++)
                //{
                //lppNoteNumber[] noteDetail = returnNoteDetails(index + 21, lppCashInObject.lppNoteNumberListStruct.usNumOfNoteNumbers, cuParsedArray, indexUsNumOfNoteNumbers);
                lppCashInObject.lppNoteNumberListStruct.lppNoteNumberStructArray = cashUnit.returnNoteDetails(index + 21, lppCashInObject.lppNoteNumberListStruct.usNumOfNoteNumbers, cuParsedArray, indexUsNumOfNoteNumbers);
                //}

                lppCashInObjectArray.Add(lppCashInObject);
            }
            return lppCashInObjectArray;

        }

        public static cashUnit.iCashCount parseStringIcashCountInfo(Form1.spLogBuff spLogLine)
        {

            cashUnit.iCashCount iCashCountObject = new cashUnit.iCashCount();

            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { ':', ',' };
            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
            List<string> asDenomInfoList = new List<string>();
            List<cashUnit.asDenomInfo> asDenomInfoObjectList = new List<cashUnit.asDenomInfo>();

            foreach (string str in cuParsedArray)
            {
                if (str.Contains("iTotalCount"))
                {

                    iCashCountObject.iTotalCount = Convert.ToInt16(str.Split(charSeperators)[1]);
                    iCashCountObject.iRejectCount = Convert.ToInt16(str.Split(charSeperators)[3]);
                }

                else if (str.Contains("asDenomInfo"))
                {

                    asDenomInfoList.Add(str);

                }
            }

            foreach (string str in asDenomInfoList)
            {
                cashUnit.asDenomInfo asDenomInfoObject = new cashUnit.asDenomInfo();
                string[] asDenomInfoObjectArray = str.Split(charSeperators);
                asDenomInfoObject.iCode = asDenomInfoObjectArray[2];
                asDenomInfoObject.iNum = Convert.ToInt16(asDenomInfoObjectArray[4]);
                asDenomInfoObjectList.Add(asDenomInfoObject);

            }

            iCashCountObject.iCashCountDateTime = parseOperations.extractDateTimeFromSpLogLine(spLogLine);

            iCashCountObject.denomInfoList = asDenomInfoObjectList;



            return iCashCountObject;

        }

        public static string extractFromCdmCuLine(string[] strArray, string keyword)
        {
            string valueFromcdmCULine = Array.Find<string>(strArray, element => element.StartsWith(keyword));
            return valueFromcdmCULine;

        }

        public static List<string> extractFromCdmCuLineArray(string[] strArray, string keyword)
        {
            List<string> valueFromcdmCULineArray = new List<string>();

            foreach (string str in strArray)
            {
                if (str.Contains(keyword))
                {
                    valueFromcdmCULineArray.Add(str);

                }

            }



            return valueFromcdmCULineArray;

        }

        public static string splitString(string strLine)
        {
            string valueFromLine;
            List<string> valueFromLineArray = new List<string>();
            if (strLine.Contains('[') && strLine.Contains(']'))
            {
                char[] splitChars = new char[] { '[', ']' };

                valueFromLineArray = (List<string>)strLine.Split(splitChars).ToList<string>();
            }
            return valueFromLine = valueFromLineArray[1];

        }

        public static byte[] splitStringHexArray(string strLine)
        {

            List<string> valueFromLineArray = new List<string>();

            char[] splitChars = new char[] { '[', ']', ' ' };


            string[] hexStringValueArrayFromLine = strLine.Split(splitChars);
            string hexStringValueFromLine = "";
            foreach (string str in hexStringValueArrayFromLine)
            {
                hexStringValueFromLine += str;
            }

            byte[] hexValueFromLine = System.Text.Encoding.ASCII.GetBytes(hexStringValueFromLine);

            return hexValueFromLine;

        }

        public static int[] splitStringIntArray(string strLine)
        {

            List<int> valueFromLineArray = new List<int>();

            char[] splitChars = new char[] { '[', ']', ' ' };


            string[] stringValuefromLine = strLine.Split(splitChars);
            int[] intValueFromLine = new int[stringValuefromLine.Length];

            foreach (string str in stringValuefromLine)
            {

            }

            //System.Text.Encoding.ASCII.GetBytes(hexStringValueFromLine);

            return intValueFromLine;

        }

        public static cashUnit.hrCashIn parseStringHrCashinInfo(Form1.spLogBuff spLogLine)
        {

            cashUnit.hrCashIn hrCashInObject = new cashUnit.hrCashIn();

            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { ':', ',', '\t', '[', ']' };
            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
            List<string> hrCashInAccept = new List<string>();
            List<cashUnit.hrCashInAccept> hrCashInAcceptObjectList = new List<cashUnit.hrCashInAccept>();

            foreach (string str in cuParsedArray)
            {
                if (str.Contains("[Code:"))
                {
                    cashUnit.hrCashInAccept hrCashInAcceptObject = new cashUnit.hrCashInAccept();
                    string[] hrCashInAcceptObjectArray = str.Split(charSeperators);
                    hrCashInAcceptObject.code = hrCashInAcceptObjectArray[3];
                    hrCashInAcceptObject.count = Convert.ToInt16(hrCashInAcceptObjectArray[5]);
                    hrCashInAcceptObjectList.Add(hrCashInAcceptObject);
                }
                else if (str.Contains("Total:\t"))
                {
                    string[] splittedStr = str.Split(charSeperators);
                    hrCashInObject.totalAcceptCount = Convert.ToInt16(splittedStr[2]);
                }


                else if (str.Contains("Reject:\t"))
                {
                    string[] splittedStr = str.Split(charSeperators);
                    hrCashInObject.rejectCount = Convert.ToInt16(splittedStr[2]);
                }

            }

            hrCashInObject.hrCashInDateTime = parseOperations.extractDateTimeFromSpLogLine(spLogLine);
            hrCashInObject.acceptList = hrCashInAcceptObjectList;
            return hrCashInObject;

        }

        public static cashUnit.iStoreMoneyEx parseStringIStoreMoneyEx(Form1.spLogBuff spLogLine)
        {

            cashUnit.iStoreMoneyEx iStoreMoneyExObject = new cashUnit.iStoreMoneyEx();

            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { ':', ',', '\t', '[', ']' };
            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
            List<string> iStoreMoneyExList = new List<string>();
            List<cashUnit.asInOutCassetteInfo> cassetteInfoList = new List<cashUnit.asInOutCassetteInfo>();
            List<cashUnit.iAbArea> acCassetteInfoList = new List<cashUnit.iAbArea>();
            List<cashUnit.asDenomInfo> denomInfoList = new List<cashUnit.asDenomInfo>();
            List<cashUnit.asDenomInfo> asDenomInfoObjectList = new List<cashUnit.asDenomInfo>();


            foreach (string str in cuParsedArray)
            {
                if (str.Contains("asInOutCassetteInfo["))
                {
                    cashUnit.asInOutCassetteInfo cassetteInfo = new cashUnit.asInOutCassetteInfo();
                    string[] splittedString = str.Split(charSeperators);
                    cassetteInfo.iCassetteSlotNo = splittedString[4];
                    cassetteInfo.iNum = Convert.ToInt16(splittedString[6]);
                    cassetteInfoList.Add(cassetteInfo);

                }
                else if (str.Contains("iAbAreaA:"))
                {

                    cashUnit.iAbArea acCassetteInfo = new cashUnit.iAbArea();
                    string[] splittedStr = str.Split(charSeperators);
                    acCassetteInfo.iAbAreaA = Convert.ToInt16(splittedStr[1]);
                    acCassetteInfo.iAbAreaB = Convert.ToInt16(splittedStr[3]);
                    acCassetteInfo.iAbAreaC = Convert.ToInt16(splittedStr[5]);
                    iStoreMoneyExObject.acCassetteInfoList = acCassetteInfo;

                }


                else if (str.Contains("asDenomInfo["))
                {
                    cashUnit.asDenomInfo asDenomInfoObject = new cashUnit.asDenomInfo();
                    string[] asDenomInfoObjectArray = str.Split(charSeperators);
                    asDenomInfoObject.iCode = asDenomInfoObjectArray[4];
                    asDenomInfoObject.iNum = Convert.ToInt16(asDenomInfoObjectArray[6]);
                    asDenomInfoObjectList.Add(asDenomInfoObject);

                }

                else if (str.Contains("iLogicCode:"))
                {

                    string[] iLogicCode = str.Split(charSeperators);
                    iStoreMoneyExObject.iLogicCode = iLogicCode[1];

                }
            }

            iStoreMoneyExObject.iStoreMoneyExDateTime = parseOperations.extractDateTimeFromSpLogLine(spLogLine);
            iStoreMoneyExObject.denomInfoList = asDenomInfoObjectList;
            iStoreMoneyExObject.cassetteInfoList = cassetteInfoList;


            return iStoreMoneyExObject;

        }

        public static DateTime extractDateTimeFromSpLogLine(Form1.spLogBuff spLogLine)
        {
            DateTime spLogLineDateTime = new DateTime(spLogLine.year, spLogLine.month, spLogLine.day,
                spLogLine.hour, spLogLine.minute, spLogLine.second, spLogLine.milisecond);
            return spLogLineDateTime;
        }

        public static List<Form1.spLogBuff> returnCashInTransaction(List<Form1.spLogBuff> spLogLines)
        {
            List<Form1.spLogBuff> cashInTransactionLines = new List<Form1.spLogBuff>();
            uint cashInTransactionStartIndex = 0;
            DateTime cashInTransactionStartDateTime = new DateTime();
            uint cashInTransactionEndIndex = 0;
            uint iRetractEndIndex = 0;
            DateTime cashInTransactionEndDateTime = new DateTime();

            DateTime iRetractEndDateTime = new DateTime();
            //&& spLogline.csInfo.Contains("")
            foreach (Form1.spLogBuff spLogline in spLogLines)
            {
                if (spLogline.csInfo.Contains("WFS_CMD_CIM_CASH_IN_END") || spLogline.csInfo.Contains("WFS_CMD_CIM_CASH_IN_ROLLBACK"))
                {

                    cashInTransactionEndIndex = spLogline.dwCurIndex;
                    cashInTransactionEndDateTime = extractDateTimeFromSpLogLine(spLogline);
                }
                else if (spLogline.csInfo.Contains("iRetract end"))
                {

                    iRetractEndIndex = spLogline.dwCurIndex;
                    iRetractEndDateTime = extractDateTimeFromSpLogLine(spLogline);

                }
                else if (spLogline.csInfo.Contains("WFS_CMD_CIM_CASH_IN_START"))
                {

                    cashInTransactionStartIndex = spLogline.dwCurIndex;
                    cashInTransactionStartDateTime = extractDateTimeFromSpLogLine(spLogline);
                    break;

                }

            }

            foreach (Form1.spLogBuff spLogline in spLogLines)
            {
                if (cashInTransactionEndIndex == 0)
                {
                    if (spLogline.dwCurIndex >= cashInTransactionStartIndex && spLogline.dwCurIndex <= iRetractEndIndex)
                    {

                        cashInTransactionLines.Add(spLogline);

                    }
                }
                else
                {

                    if (spLogline.dwCurIndex >= cashInTransactionStartIndex && spLogline.dwCurIndex <= cashInTransactionEndIndex)
                    {

                        cashInTransactionLines.Add(spLogline);

                    }
                }



            }






            return cashInTransactionLines;


        }

        public static cashUnit.hrCashInEnd parseStringHrCashinEndInfo(Form1.spLogBuff spLogLine)
        {
            cashUnit.hrCashInEnd hrCashInEndObject = new cashUnit.hrCashInEnd();

            cashUnit.cashInEndResult cashInResultObject = new cashUnit.cashInEndResult();

            List<cashUnit.cashInEndResult> cashInEndResultObjectList = new List<cashUnit.cashInEndResult>();
            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { ':', ',', '\t', '[', ']' };
            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
            List<string> hrCashInEnd = new List<string>();
            List<cashUnit.hrCashInEnd> hrCashInEndObjectList = new List<cashUnit.hrCashInEnd>();

            foreach (string str in cuParsedArray)
            {
                if (str.Contains("[Slot:"))
                {
                    //cashUnitCIM.hrCashInEnd hrCashInEndObject = new cashUnitCIM.hrCashInEnd();
                    string[] hrCashInEndObjectArray = str.Split(charSeperators);
                    cashInResultObject.slot = hrCashInEndObjectArray[2];
                    cashInResultObject.index = hrCashInEndObjectArray[4];
                    cashInResultObject.count = Convert.ToInt16(hrCashInEndObjectArray[6]);

                    cashInEndResultObjectList.Add(cashInResultObject);
                }

                else if (str.Contains("iErrCode:"))
                {
                    string[] cashInEndResultArray = str.Split(charSeperators);
                    hrCashInEndObject.iErrCode = cashInEndResultArray[1];
                }


            }

            hrCashInEndObject.hrCashInEndDateTime = parseOperations.extractDateTimeFromSpLogLine(spLogLine);
            hrCashInEndObject.cashInEndResultList = cashInEndResultObjectList;
            return hrCashInEndObject;

        }

        public static cashUnit.wfsCmdCimCashIn parseCmdCimCashInObject(Form1.spLogBuff spLogLine)
        {

            cashUnit.wfsCmdCimCashIn wfsCmdCimCashInObject = new cashUnit.wfsCmdCimCashIn();
            cashUnit.lppNoteNumber lppNoteNumberObject = new cashUnit.lppNoteNumber();
            List<cashUnit.lppNoteNumber> lppNoteNumberObjectList = new List<cashUnit.lppNoteNumber>();
            cashUnit.lppNoteNumberList lppNoteNumberList = new cashUnit.lppNoteNumberList();

            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { ':', ',', '\t', '[', ']' };
            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
            int index = 0;
            foreach (string str in cuParsedArray)
            {
                if (str.Contains("usNumOfNoteNumbers = ["))
                {

                    lppNoteNumberList.usNumOfNoteNumbers = Convert.ToInt16(str.Split(charSeperators)[1]);

                }
                else if (str.Contains("lppNoteNumber["))
                {

                    lppNoteNumberObject.usNoteID = cuParsedArray[index + 2].Split(charSeperators)[1];
                    lppNoteNumberObject.ulCount = Convert.ToInt16(cuParsedArray[index + 3].Split(charSeperators)[1]);

                    lppNoteNumberObjectList.Add(lppNoteNumberObject);
                }
                index++;
            }

            cashUnit.lppNoteNumber[] lppNoteMuberArray = lppNoteNumberObjectList.ToArray();

            lppNoteNumberList.lppNoteNumberStructArray = lppNoteMuberArray;

            wfsCmdCimCashInObject.lppNoteNumberListStruct = lppNoteNumberList;
            return wfsCmdCimCashInObject;


        }

        public static cashUnit.iRetractEnd parseStringIRetractEnd(Form1.spLogBuff spLogLine)
        {

            cashUnit.iRetractEnd iRetractEndObject = new cashUnit.iRetractEnd();
            List<cashUnit.asDenomInfo> asDenomInfoObjectList = new List<cashUnit.asDenomInfo>();
            cashUnit.iAbArea acCassetteInfoList = new cashUnit.iAbArea();
            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { ':', ',', '\t', '[', ']' };
            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
            foreach (string str in cuParsedArray)
            {
                if (str.Contains("iTotalCount"))
                {
                    string[] asDenomInfoObjectArray = str.Split(charSeperators);
                    iRetractEndObject.iTotalCount = Convert.ToInt16(asDenomInfoObjectArray[1]);
                    iRetractEndObject.iUnknownCount = Convert.ToInt16(asDenomInfoObjectArray[3]);


                }
                else if (str.Contains("asNotesInfo["))
                {
                    cashUnit.asDenomInfo asDenomInfoObject = new cashUnit.asDenomInfo();
                    string[] asDenomInfoObjectArray = str.Split(charSeperators);
                    asDenomInfoObject.iCode = asDenomInfoObjectArray[4];
                    asDenomInfoObject.iNum = Convert.ToInt16(asDenomInfoObjectArray[6]);
                    asDenomInfoObjectList.Add(asDenomInfoObject);
                }
                else if (str.Contains("iAbAreaA:"))
                {
                    cashUnit.iAbArea acCassetteInfo = new cashUnit.iAbArea();
                    string[] splittedStr = str.Split(charSeperators);
                    acCassetteInfo.iAbAreaA = Convert.ToInt16(splittedStr[1]);
                    acCassetteInfo.iAbAreaB = Convert.ToInt16(splittedStr[3]);
                    acCassetteInfo.iAbAreaC = Convert.ToInt16(splittedStr[5]);
                    iRetractEndObject.acCassetteInfoList = acCassetteInfo;
                }
                else if (str.Contains("iLogicCode"))
                {
                    string[] splittedStr = str.Split(charSeperators);
                    iRetractEndObject.iLogicCode = splittedStr[1];

                }
            }


            iRetractEndObject.denomInfoList = asDenomInfoObjectList;
            iRetractEndObject.iRetractEndDateTime = parseOperations.extractDateTimeFromSpLogLine(spLogLine);

            return iRetractEndObject;

        }


        public static List<cashUnit.asDenomInfo> parseStringIRetractEndForAsDenomInfoList(Form1.spLogBuff spLogLine)
        {

            cashUnit.iRetractEnd iRetractEndObject = new cashUnit.iRetractEnd();
            List<cashUnit.asDenomInfo> asDenomInfoObjectList = new List<cashUnit.asDenomInfo>();
            cashUnit.iAbArea acCassetteInfoList = new cashUnit.iAbArea();
            string[] stringSeparators = new string[] { "\r\n" };
            char[] charSeperators = new char[] { ':', ',', '\t', '[', ']' };
            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);
            foreach (string str in cuParsedArray)
            {
                if (str.Contains("iTotalCount"))
                {
                    string[] asDenomInfoObjectArray = str.Split(charSeperators);
                    iRetractEndObject.iTotalCount = Convert.ToInt16(asDenomInfoObjectArray[1]);
                    iRetractEndObject.iUnknownCount = Convert.ToInt16(asDenomInfoObjectArray[3]);


                }
                else if (str.Contains("asNotesInfo["))
                {
                    cashUnit.asDenomInfo asDenomInfoObject = new cashUnit.asDenomInfo();
                    string[] asDenomInfoObjectArray = str.Split(charSeperators);
                    asDenomInfoObject.iCode = asDenomInfoObjectArray[4];
                    asDenomInfoObject.iNum = Convert.ToInt16(asDenomInfoObjectArray[6]);
                    asDenomInfoObjectList.Add(asDenomInfoObject);
                }
                else if (str.Contains("iAbAreaA:"))
                {
                    cashUnit.iAbArea acCassetteInfo = new cashUnit.iAbArea();
                    string[] splittedStr = str.Split(charSeperators);
                    acCassetteInfo.iAbAreaA = Convert.ToInt16(splittedStr[1]);
                    acCassetteInfo.iAbAreaB = Convert.ToInt16(splittedStr[3]);
                    acCassetteInfo.iAbAreaC = Convert.ToInt16(splittedStr[5]);
                    iRetractEndObject.acCassetteInfoList = acCassetteInfo;
                }
                else if (str.Contains("iLogicCode"))
                {
                    string[] splittedStr = str.Split(charSeperators);
                    iRetractEndObject.iLogicCode = splittedStr[1];

                }
            }


            iRetractEndObject.denomInfoList = asDenomInfoObjectList;
            iRetractEndObject.iRetractEndDateTime = parseOperations.extractDateTimeFromSpLogLine(spLogLine);

            return asDenomInfoObjectList;

        }


        public static List<string> errorCodes = new List<string>();

        public static List<string> parseErrorCode(string spLogLineCsInfo)
        {

            string[] stringSeparators = new string[] { "\r\n" };
            string[] cuParsedArray = spLogLineCsInfo.Split(stringSeparators, StringSplitOptions.None);

            foreach (string str in cuParsedArray)
            {

                if ((str.Contains("iLogicCode") || str.Contains("iPhyCode")) && (!str.Contains("iLogicCode: 0") && !str.Contains("iPhyCode: 0")))
                {

                    if (!errorCodes.Contains(str))
                    {
                        errorCodes.Add(str);
                    }
                }

            }

            return errorCodes;



        }

        public static cashUnit.lppCdmCashUnit extractCdmCashUnitInStartExchange(Form1.spLogBuff spLogLine)
        {
            cashUnit.lppCdmCashUnit cdmCashUnit = new cashUnit.lppCdmCashUnit();

            string[] stringSeparators = new string[] { "\r\n" };

            string[] cuParsedArray = spLogLine.csInfo.Split(stringSeparators, StringSplitOptions.None);

            string usCount = (from d in cuParsedArray
                              where d.Contains("usCount = [")
                              select d).First();


            char[] charSeperators = new char[] { ':', ',', '\t', '[', ']' };

            List<cashUnit.lppCdmCashUnit> startExchangeLpplist = new List<cashUnit.lppCdmCashUnit>();

            int cuParsedArrayIndex = 0;
            for (int i = 0; i < Convert.ToInt16(usCount.Split(charSeperators)[1]); i++)
            {
            etiket:
                foreach (string spLog in cuParsedArray)
                {


                    if (spLog.Contains("lppList[" + i + "]"))
                    {

                        cashUnit.lppCdmCashUnit startExchangeLpplistObject = new cashUnit.lppCdmCashUnit();

                        startExchangeLpplistObject.usNumber = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 2].Split(charSeperators)[1]);
                        startExchangeLpplistObject.usType = cuParsedArray[cuParsedArrayIndex + 3].Split(charSeperators)[1];
                        startExchangeLpplistObject.lpszCashUnitName = cuParsedArray[cuParsedArrayIndex + 4].Split(charSeperators)[1];
                        startExchangeLpplistObject.cUnitIDAscii = cuParsedArray[cuParsedArrayIndex + 5].Split(charSeperators)[1];
                        startExchangeLpplistObject.cUnitIDHex = splitStringHexArray(cuParsedArray[cuParsedArrayIndex + 6].Split(charSeperators)[1]);
                        startExchangeLpplistObject.cCurrencyIDAscii = cuParsedArray[cuParsedArrayIndex + 7].Split(charSeperators)[1];
                        startExchangeLpplistObject.cCurrencyIDHex = splitStringHexArray(cuParsedArray[cuParsedArrayIndex + 8].Split(charSeperators)[1]);
                        startExchangeLpplistObject.ulValues = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 9].Split(charSeperators)[1]);
                        startExchangeLpplistObject.ulInitialCount = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 10].Split(charSeperators)[1]);
                        startExchangeLpplistObject.ulCount = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 11].Split(charSeperators)[1]); ;
                        startExchangeLpplistObject.ulRejectCount = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 12].Split(charSeperators)[1]);
                        startExchangeLpplistObject.ulMinimum = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 13].Split(charSeperators)[1]);
                        startExchangeLpplistObject.ulMaximum = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 14].Split(charSeperators)[1]);
                        startExchangeLpplistObject.bAppLock = Convert.ToBoolean(cuParsedArray[cuParsedArrayIndex + 15].Split(charSeperators)[1]);
                        startExchangeLpplistObject.usStatus = cuParsedArray[cuParsedArrayIndex + 16].Split(charSeperators)[1];
                        startExchangeLpplistObject.usNumPhysicalCUs = Convert.ToInt16(cuParsedArray[cuParsedArrayIndex + 17].Split(charSeperators)[1]); ;
                        //List<lppPhysical> lppPhysicalList;
                        startExchangeLpplist.Add(startExchangeLpplistObject);
                        goto etiket;

                    }
                    cuParsedArrayIndex++;
                }


            }

            return cdmCashUnit;

        }

        public static cashUnit.updateCashUnitInfo parseUpdateCashUnitInfo(string spLogLineCsInfo)
        {
            cashUnit.updateCashUnitInfo cashUnitInfo = new cashUnit.updateCashUnitInfo();

            string[] stringSeparators = new string[] { "\r\n" };

            string[] cuParsedArray = spLogLineCsInfo.Split(stringSeparators, StringSplitOptions.None);
            Array.Resize(ref cuParsedArray, cuParsedArray.Length - 1);


            char[] charSeperators = new char[] { ':', '\t' };


            List<string> modifiedCuParsedArrayList = new List<string>();
            foreach (string cuLine in cuParsedArray)
            {

                if (!cuLine.StartsWith("-"))
                {
                    if (cuLine.StartsWith("Update"))
                    {

                    }
                    else if (cuLine.StartsWith("\tSlot"))
                    {

                    }

                    else { modifiedCuParsedArrayList.Add(cuLine); }
                }

                else if (cuLine.StartsWith("-"))
                {
                    break;
                }
            }


            List<cashUnit.updatedCassettes> updatedCassettesList = new List<cashUnit.updatedCassettes>();
            foreach (string modifiedCuLine in modifiedCuParsedArrayList)
            {
                cashUnit.updatedCassettes updatedCassette = new cashUnit.updatedCassettes();
                updatedCassette.updateSlot = modifiedCuLine.Split(charSeperators)[1];
                updatedCassette.updateIndex = modifiedCuLine.Split(charSeperators)[2];
                updatedCassette.updateCount = modifiedCuLine.Split(charSeperators)[3];
                updatedCassettesList.Add(updatedCassette);

            }

            cashUnitInfo.updatedCassettesList = updatedCassettesList;
            //cashUnitInfo
            modifiedCuParsedArrayList.Clear();


            foreach (string cuLine in cuParsedArray)
            {
                if (cuLine.StartsWith("-"))
                { }
                if (cuLine.StartsWith("Update"))
                {}
                else if (cuLine.StartsWith("\tSlot"))
                { }
                else if (cuLine.StartsWith("Slot"))
                { }
                else if (cuLine.StartsWith("\t"))
                {}
                else if (cuLine.StartsWith("-"))
                { }
              
                else
                {

                    modifiedCuParsedArrayList.Add(cuLine);
                
                }
            }


            List<cashUnit.cashUnitInfoCounters> updatedCashUnitCounterList = new List<cashUnit.cashUnitInfoCounters>();
            foreach (string modifiedCuLine in modifiedCuParsedArrayList)
            {
                string[] strArr = modifiedCuLine.Split(charSeperators);
                cashUnit.cashUnitInfoCounters updatedSlot = new cashUnit.cashUnitInfoCounters();
                updatedSlot.cassetteSlot = modifiedCuLine.Split(charSeperators)[0];
                updatedSlot.cassetteIndex = modifiedCuLine.Split(charSeperators)[1];
                updatedSlot.CassetteCount = modifiedCuLine.Split(charSeperators)[2];
                updatedSlot.cassetteCashIn = modifiedCuLine.Split(charSeperators)[3];
                updatedSlot.cassetteReject = modifiedCuLine.Split(charSeperators)[4];
                updatedSlot.cassetteLogStatus = modifiedCuLine.Split(charSeperators)[5];
                updatedSlot.cassettePhyStatus = modifiedCuLine.Split(charSeperators)[7];


                updatedCashUnitCounterList.Add(updatedSlot);

               
            }

            cashUnitInfo.cashUnitInfoCounterList = updatedCashUnitCounterList;

            return cashUnitInfo;

        }






        //public static string parseKeyword(string beginning, string keyword, Form1.spLogBuff spLogLine)
        //{
        //    List<string> spLogLineList = pa

        //    string fullKeyword;
        //    foreach (String 





        //    return fullKeyword;


        //}



    }
}
