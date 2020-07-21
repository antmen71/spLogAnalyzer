using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spLogListener
{
    public class cashUnit
    {

        public struct lppCashIn
        {

            public short usNumber; //usNumber = [1]
            public string fwType; //= [WFS_CDM_TYPEREJECTCASSETTE(2)]
            public string fwItemType; //= [WFS_CIM_CITYPINDIVIDUAL(4)]
            public string cUnitID; // = [S0_I0]ASCI
            public byte[] cUnitIDHex; //(hex) = [0x53 0x30 0x5F 0x49 0x30]
            public string cCurrencyID; //(asc) = [   ]
            public byte[] cCurrencyIDHex; //(hex) = [0x20 0x20 0x20]
            public ulong ulValues; // = [0]
            public ulong ulCashInCount; // = [0]
            public ulong ulCount; // = [26]
            public ulong ulMaximum; // = [1500]
            public string usStatus; // = [WFS_CDM_STATCUOK(0)]
            public bool bAppLock; // = [FALSE]       
            //public int usNumPhysicalCUs;
            public lppNoteNumberList lppNoteNumberListStruct;
            public string requestId;
            public DateTime lppCashInDateTime;

        }

        public struct lppNoteNumberList
        {
            public int usNumOfNoteNumbers;
            public lppNoteNumber[] lppNoteNumberStructArray;
        }

        public struct lppNoteNumber
        {
            public string usNoteID;
            public int ulCount;
            public lppPhysical lppPhysicalstruct;

        }

        public static lppNoteNumber[] returnNoteDetails(int index, int recurrence, string[] cuParsedArray, List<int> indexes)
        {
            lppNoteNumber[] noteDetails = new lppNoteNumber[recurrence];
            lppPhysical lppPhysicalObject = new lppPhysical();

            string[] noteDetailsArray = new string[2];

            for (int i = 0; i < recurrence; i++)
            {
                //if (recurrence == 1)
                //{
                if (!cuParsedArray[index].Contains("usNoteID = ["))
                {

                }
                else if (cuParsedArray[index].Contains("usNoteID = ["))
                {
                    Array.Copy(cuParsedArray, index, noteDetailsArray, 0, 2);
                    noteDetails[i].usNoteID = parseOperations.splitString(noteDetailsArray[0]);
                    noteDetails[i].ulCount = Convert.ToInt16(parseOperations.splitString(noteDetailsArray[1]));
                    index = index + 5;
                    if (cuParsedArray[index].Contains("usNumPhysicalCUs = ["))
                    {
                        lppPhysicalObject.lpPhysicalPositionName = parseOperations.splitString(cuParsedArray[index + 6]);

                        lppPhysicalObject.cUnitIDAscii = parseOperations.splitString(cuParsedArray[index + 7]);
                        lppPhysicalObject.cUnitIdHex = parseOperations.splitStringHexArray(cuParsedArray[index + 8]);
                        lppPhysicalObject.ulCashInCount = Convert.ToInt16(parseOperations.splitString(cuParsedArray[index + 9]));
                        lppPhysicalObject.ulCount = Convert.ToInt16(parseOperations.splitString(cuParsedArray[index + 10]));
                        lppPhysicalObject.ulMaximum = Convert.ToInt16(parseOperations.splitString(cuParsedArray[index + 11]));
                        lppPhysicalObject.usPStatus = parseOperations.splitString(cuParsedArray[index + 12]);
                        lppPhysicalObject.bHardwareSensors = parseOperations.splitString(cuParsedArray[index + 13]);
                        lppPhysicalObject.bHardwareSensors = parseOperations.splitString(cuParsedArray[index + 14]);
                        noteDetails[i].lppPhysicalstruct = lppPhysicalObject;


                    }




                }





                //}
            }

            return noteDetails;

        }

        public struct iCashCount
        {
            public int iTotalCount;
            public int iRejectCount;
            public Dictionary<string, int> noteInfo;
            public List<asDenomInfo> denomInfoList;
            public DateTime iCashCountDateTime;

        }

        public struct asDenomInfo
        {
            public string iCode;
            public int iNum;


        }

        public struct hrCashIn
        {

            public List<hrCashInAccept> acceptList;
            public int totalAcceptCount;
            public int rejectCount;
            public DateTime hrCashInDateTime;


        }

        public struct hrCashInAccept
        {

            public string code;
            public int count;

        }

        public struct iStoreMoneyEx
        {
            public List<asInOutCassetteInfo> cassetteInfoList;
            public List<asDenomInfo> denomInfoList;
            public string iLogicCode;
            public iAbArea acCassetteInfoList;
            public DateTime iStoreMoneyExDateTime;

        }

        public struct asInOutCassetteInfo
        {
            public string iCassetteSlotNo;
            public int iNum;

        }

        public struct iAbArea
        {

            public int iAbAreaA;
            public int iAbAreaB;
            public int iAbAreaC;


        }

        public struct hrCashInEnd
        {
            public List<cashInEndResult> cashInEndResultList;
            public string iErrCode;
            public DateTime hrCashInEndDateTime;
            public string fileName;

        }

        public struct cashInEndResult
        {
            public string slot;
            public string index;
            public int count;
            public DateTime cashInEndResultDateTime;

        }

        public struct wfsCmdCimCashIn
        {

            public lppNoteNumberList lppNoteNumberListStruct;
            public DateTime wfsCmdCimCashInDateTime;



        }

        public struct compareObject
        {
            public string iCode;
            public int iCount;
            public string cassetteID;
            public DateTime compareObjectDateTime;
            string iErrorCode;

        }

        public static void cashUnitInformation(Form1.spLogBuff spLogLine)
        {





        }

        public struct iRetractEnd
        {
            public string byRetractPosition;
            public int iTotalCount;
            public int iUnknownCount;
            public List<asDenomInfo> denomInfoList;
            public iAbArea acCassetteInfoList;
            public DateTime iRetractEndDateTime;
            public string iLogicCode;
        }

        public struct updateCashUnitInfo
        {
            public string updatedModule;
          public List<updatedCassettes> updatedCassettesList;
          public List<cashUnitInfoCounters> cashUnitInfoCounterList;
          public string divider;
        }

        public struct updatedCassettes
        {
            
            public string updateSlot;
            public string updateIndex;
            public string updateCount;
        }

        public struct cashUnitInfoCounters
        {

            public string cassetteSlot;
            public string cassetteIndex;
            public string CassetteCount;
            public string cassetteCashIn;
            public string cassetteReject;
            public string cassetteLogStatus;
            public string cassettePhyStatus;


        }

        public struct lpCDMCUInfo //lpCUInfo WFS_CMD_CDM_START_EXCHANGE
        {
            public int usTellerID;
            public int usCount;
            public List<lppCdmCashUnit> lppCdmExchange; //lppList


        }

        public struct lppCdmCashUnit //lppList
        {

            public int usNumber;
            public string usType;
            public string lpszCashUnitName;
            public string cUnitIDAscii;
            public byte[] cUnitIDHex;
            public string cCurrencyIDAscii;
            public byte[] cCurrencyIDHex;
            public int ulValues;
            public int ulInitialCount;
            public int ulCount;
            public int ulRejectCount;
            public int ulMinimum;
            public int ulMaximum;
            public bool bAppLock;
            public string usStatus;
            public int usNumPhysicalCUs;
            public List<lppPhysical> lppPhysicalList;

        }

        public struct lppPhysical
        {
            public string lpPhysicalPositionName;
            public string cUnitIDAscii;
            public byte[] cUnitIDHex;
            public int ulInitialCount;
            public int ulCount;
            public int ulRejectCount;
            public int ulMaximum;
            public string usPStatus;
            bool bHardwareSensor;


            public byte[] cUnitIdHex;
            public int ulCashInCount;

            public string bHardwareSensors;
            public string lpszExtra;
        }

    }
}
//short usTellerID; //usTellerID = [0]
//       short usCount; //usCount = [6]
//       cashUnitCDM[] lppList; //lppList[0]
//       short usNumber; //usNumber = [1]
//       string usType; //= [WFS_CDM_TYPEREJECTCASSETTE(2)]
//       string lpszCashUnitName;// = []
//       string cUnitID; // = [S0_I0]ASCI
//       byte[] cUnitIDHex; //(hex) = [0x53 0x30 0x5F 0x49 0x30]
//       ulong cCurrencyID; //(asc) = [   ]
//       byte[] cCurrencyIDHex; //(hex) = [0x20 0x20 0x20]
//       ulong ulValues; // = [0]
//       ulong ulInitialCount; // = [0]
//       ulong ulCount; // = [26]
//       ulong ulRejectCount; // = [0]
//       ulong ulMinimum; // = [0]
//       ulong ulMaximum; // = [1500]
//       bool bAppLock; // = [FALSE]
//       short usStatus; // = [WFS_CDM_STATCUOK(0)]
//       short usNumPhysicalCUs; // = [1]        
//       short lppPhysicalLppPhysical; //[0]=
//       string lppPhysicalLpPhysicalPositionName;// = [S0_I0]
//       string lppPhysicalCUnitID; //(asc) = [S0_I0]
//       byte[] lppPhysicalCUnitIDHex; //(hex) = [0x53 0x30 0x5F 0x49 0x30]
//       ulong lppPhysicalUlInitialCount; // = [0]
//       ulong lppPhysicalUlCount;  // = [26]
//       ulong lppPhysicalUlRejectCount; // = [0]
//       ulong lppPhysicalUlMaximum; // = [1500]
//       short lppPhysicalUsPStatus; // = [WFS_CDM_STATCUOK(0)]
//       bool lppPhysicalBHardwareSensor; // = [FALSE]


//string[] croppedArray = new string[14];
//                   Array.Copy(cuParsedArray, indexLppCashIn[0], croppedArray, 0, 14);
//                   lppCashIn lppCashInObject = new lppCashIn();
//                   lppCashInObject.usNumber = Convert.ToInt16(splitString(croppedArray[0]));
//                   lppCashInObject.fwType = splitString(croppedArray[1]);
//                   lppCashInObject.fwItemType = splitString(croppedArray[2]);
//                   lppCashInObject.cUnitID = splitString(croppedArray[3]);
//                   lppCashInObject.cUnitIDHex = splitStringHexArray(croppedArray[4]);
//                   lppCashInObject.cCurrencyID = splitString(croppedArray[5]);
//                   lppCashInObject.cCurrencyIDHex = splitStringHexArray(croppedArray[6]);
//                   lppCashInObject.ulValues = Convert.ToUInt16(splitString(croppedArray[7]));
//                   lppCashInObject.ulCashInCount = Convert.ToUInt16(splitString(croppedArray[8]));
//                   lppCashInObject.ulCount = Convert.ToUInt16(splitString(croppedArray[9]));
//                   lppCashInObject.ulMaximum = Convert.ToUInt16(splitString(croppedArray[10]));
//                   lppCashInObject.usStatus = splitString(croppedArray[11]);
//                   lppCashInObject.bAppLock = Convert.ToBoolean(splitString(croppedArray[12]));