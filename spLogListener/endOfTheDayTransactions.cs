using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spLogListener
{
    class endOfTheDayTransactions
    {

        public cashUnit.lppCdmCashUnit returnCashUnitInfo(Form1.spLogBuff spLogLine)
        {
            cashUnit.lppCdmCashUnit lppCdmCashUnit =  parseOperations.extractCdmCashUnitInStartExchange(spLogLine);

           







            return lppCdmCashUnit;
        
        }













    }
}
