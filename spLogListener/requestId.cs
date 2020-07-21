using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spLogListener
{
   public class requestIdOperations
    {
        public static List<Form1.spLogBuff> searchWithRequestId(string requestId, Form1.spLogBuff[] spLogLineArray)
        {
            List<Form1.spLogBuff> returnSearchWithRequestIdArray = new List<Form1.spLogBuff>();
            foreach (Form1.spLogBuff spLogLine in spLogLineArray)
            {

                if (spLogLine.csInfo.Contains("REQUESTID: " + requestId))
                { returnSearchWithRequestIdArray.Add(spLogLine); }

            }

            return returnSearchWithRequestIdArray;

        }

        public static string findRequestID(Form1.spLogBuff spLogLine)
        {
            string requestId = "";

            char[] requestIdCharArray = new char[10];
            int spLogLineCsInfoLenght = spLogLine.csInfo.Length;
            int requestIdIndex = spLogLine.csInfo.IndexOf("REQUESTID:") + 11;
            string requestIdPart = spLogLine.csInfo.Substring(requestIdIndex, spLogLineCsInfoLenght-requestIdIndex);
            
            requestIdCharArray = requestIdPart.ToCharArray();
            for (int i = 0; i < requestIdCharArray.Length; i++)
            {
                if (!char.IsDigit(requestIdCharArray[i]))
                    Array.Resize(ref requestIdCharArray, i );
            }

            

            return requestId = String.Join(null,requestIdCharArray);
        }

        public static string findRequestID(string csInfo)
        {
            string requestId = "";

            char[] requestIdCharArray = new char[10];
            int spLogLineCsInfoLenght = csInfo.Length;
            int requestIdIndex = csInfo.IndexOf("REQUESTID:") + 11;
            string requestIdPart = csInfo.Substring(requestIdIndex, spLogLineCsInfoLenght - requestIdIndex);

            requestIdCharArray = requestIdPart.ToCharArray();
            for (int i = 0; i < requestIdCharArray.Length; i++)
            {
                if (!char.IsDigit(requestIdCharArray[i]))
                    Array.Resize(ref requestIdCharArray, i);
            }



            return requestId = String.Join(null, requestIdCharArray);
        }
    }
}
