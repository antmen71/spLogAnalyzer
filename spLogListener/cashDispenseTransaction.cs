using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spLogListener
{
    class cashDispenseTransaction
    {

        spLogListener.Form1.spLogBuff spLogLine = new Form1.spLogBuff();

        public static List<Form1.spLogBuff> allCashInLines(spLogListener.Form1.spLogBuff[] spLogFile)
        {
            List<Form1.spLogBuff> allCashInLinesArray = new List<Form1.spLogBuff>();

            foreach (spLogListener.Form1.spLogBuff spLogLine in spLogFile)
            {
                if (spLogLine.csModuleNAme == "GrgCRM_CRM9250_XFS30" && spLogLine.csType == "MessageSuccess" && spLogLine.csInfo.Contains("WFS_EXECUTE_COMPLETE") && spLogLine.csInfo.Contains("WFS_CMD_CIM_CASH_IN_END"))
                {

                    allCashInLinesArray.Add(spLogLine);

                }

            }
            return allCashInLinesArray;

        }

    }
}
