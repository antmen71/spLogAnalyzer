using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace spLogListener
{
    class printOperations
    {       
        
        public static void fillRichTextBox(List<cashUnit.lppCashIn> allCashInArray)
        {
            Form1 myForm = new Form1();
            RichTextBox richTextBox1 = myForm.Controls["richTextBox1"] as RichTextBox;
            richTextBox1.Text = "";
            foreach (cashUnit.lppCashIn lppCashInObjectl in allCashInArray)
            {

                if (lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray.Count() == 0)
                {
                    richTextBox1.Text = "No Cash In Transaction.";

                }
                else if (lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray.Count() == 1)
                {
                    richTextBox1.AppendText("Request ID: " + lppCashInObjectl.requestId + "\r\n");
                    richTextBox1.AppendText("Currency: " + lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray[0].usNoteID + "\r\n");
                    denomTranslation noteValue = denomTranslation.returnNoteInfo(lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray[0].usNoteID);

                    richTextBox1.AppendText("Denomination: " + noteValue.denomType + "\r\n");
                    richTextBox1.AppendText("Value: " + noteValue.denomValue + "\r\n");
                    richTextBox1.AppendText("Amount: " + lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray[0].ulCount + "\r\n");
                    richTextBox1.AppendText("Cash Unit: " + lppCashInObjectl.fwType + "\r\n");
                    //richTextBox1.AppendText(spLogFile.Name);
                    richTextBox1.AppendText("------------------------------------------------------------------------\r\n");
                }
                if (lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray.Count() > 1)
                {
                    for (int lppNoteNumberCount = 0; lppNoteNumberCount < lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray.Count(); lppNoteNumberCount++)
                    {
                        richTextBox1.AppendText("Request ID: " + lppCashInObjectl.requestId + "\r\n");
                        richTextBox1.AppendText("Currency: " + lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray[lppNoteNumberCount].usNoteID + "\r\n");
                        denomTranslation noteValue = denomTranslation.returnNoteInfo(lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray[lppNoteNumberCount].usNoteID);

                        richTextBox1.AppendText("Denomination: " + noteValue.denomType + "\r\n");
                        richTextBox1.AppendText("Value: " + noteValue.denomValue + "\r\n");
                        richTextBox1.AppendText("Amount: " + lppCashInObjectl.lppNoteNumberListStruct.lppNoteNumberStructArray[lppNoteNumberCount].ulCount + "\r\n");
                        richTextBox1.AppendText("Cash Unit: " + lppCashInObjectl.fwType + "\r\n");
                        //richTextBox1.AppendText(spLogFile.Name);

                        richTextBox1.AppendText("------------------------------------------------------------------------\r\n");
                    }
                }
            }
        }

        public static void allCashInObjectsTotal(int cashCountTotal, int hrCashInTotal, int iStoreMoneyTotal, int hrCashInEndTotal, int cashInEndTotal, int iRetractTotal)
        {

            RichTextBox rtxt = Application.OpenForms["Form1"].Controls["richTextBox4"] as RichTextBox;

            rtxt.Text += "Cash Count Total :" + cashCountTotal.ToString() + "\r\n";
            rtxt.Text += "hrCash In Total :" + hrCashInTotal.ToString() + "\r\n";
            rtxt.Text += "Store Money Total :" + iStoreMoneyTotal.ToString() + "\r\n";
            rtxt.Text += "hrCash In End Total :" + hrCashInEndTotal.ToString() + "\r\n";
            rtxt.Text += "Cash In End Total :" + cashInEndTotal.ToString() + "\r\n";
            rtxt.Text += "Retract Total :" + iRetractTotal.ToString() + "\r\n";

        
        }



    }
}
