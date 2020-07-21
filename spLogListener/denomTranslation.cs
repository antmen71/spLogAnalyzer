using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace spLogListener
{
    public class denomTranslation
    {


        public int spCode; //noteID 531,532,533,534...
        public string denomType; //currency TRL,USD
        public int denomValue; //value 10,20,50,100....
        public int denomTotal;// total deposit value*count
        public int denomCount; //pieces of banknotes
        public string requestID;

        public static List<denomTranslation> createNoteInfoArray()
        {
            List<denomTranslation> noteIdsArray = new List<denomTranslation>();
            string[] noteIdLinesFromFile = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\noteIds.txt");
            foreach (string line in noteIdLinesFromFile)
            {
                denomTranslation noteId = new denomTranslation();
                string[] noteIdArray = line.Split(',');
                noteId.spCode = Convert.ToInt16(noteIdArray[0]);
                noteId.denomValue = Convert.ToInt16(noteIdArray[1]);
                noteId.denomType = noteIdArray[2];
                noteIdsArray.Add(noteId);
            }
            return noteIdsArray;
        }

        public static denomTranslation returnNoteInfo(string noteSpCode)
        {
            denomTranslation noteInfo = new denomTranslation();
            List<denomTranslation> noteInfoArray = createNoteInfoArray();

            foreach (denomTranslation noteInf in noteInfoArray)
            {
                if (noteInf.spCode.ToString() == noteSpCode)
                    noteInfo = noteInf;

            }
            return noteInfo;
        }

        public static List<string> returnDistinctDenomTypes()
        {
            List<string> distinctNoteTypes = new List<string>();
            List<denomTranslation> noteInfoArray = createNoteInfoArray();
            for (int i = 0; i < noteInfoArray.Count; i++)
            {
                while (!distinctNoteTypes.Contains(noteInfoArray[i].denomType))
                {
                    distinctNoteTypes.Add(noteInfoArray[i].denomType);
                }
            }


            return distinctNoteTypes;
        }

        //currency kodu ile birlikte değerinin de gelmesi lazım
        public static denomTranslation returnCurrencyTypeValue(string noteId)
        {


            denomTranslation denomTranslate = returnNoteInfo(noteId);

            //string  currencyType = denomTranslate.denomType;
            //int currencyValue = denomTranslate.denomValue;


            return denomTranslate;


        }

        public static List<denomTranslation> displayCashInCurrencyValueCount(List<cashUnit.asDenomInfo> denomList)
        {

            List<denomTranslation> noteInfoList = new List<denomTranslation>();
            denomTranslation noteInfo = new denomTranslation();
            foreach (cashUnit.asDenomInfo denomInfo in denomList)
            {

                denomTranslation denTra = denomTranslation.returnCurrencyTypeValue(denomInfo.iCode);
                denTra.denomCount = denomInfo.iNum;



                noteInfoList.Add(denTra);
            }

            return noteInfoList;






        }

        public static List<denomTranslation> cashInTotalsByCurrency(List<denomTranslation> cashInByCurrencyList)
        {
            List<denomTranslation> cashInTotalsByCurrencyList = new List<denomTranslation>();
            
          

            foreach (denomTranslation cashInByCurrencyElement in cashInByCurrencyList)
            {
                cashInByCurrencyElement.denomTotal = cashInByCurrencyElement.denomCount * cashInByCurrencyElement.denomValue;

            }       



            var teamTotalScores =
                from player in cashInByCurrencyList
    group player by player.denomType into playerGroup
    select new
    {
        Team = playerGroup.Key,
        TotalScore = playerGroup.Sum(x => x.denomTotal),
    };

            foreach (var totalScore in teamTotalScores)
            { 
            denomTranslation cashInTotalsByCurrencyElement = new denomTranslation();
            cashInTotalsByCurrencyElement.denomType = totalScore.Team;
            cashInTotalsByCurrencyElement.denomTotal = totalScore.TotalScore;
            cashInTotalsByCurrencyList.Add(cashInTotalsByCurrencyElement);
            
            }

            return cashInTotalsByCurrencyList;




        }

    }
}

