using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using System.Diagnostics;

namespace iGYMMM
{
    public class CreateSMS
    {
        /*
        LReplaceItems LRITs;
        public Client client { get; set; }
        public string Error { get; set; }
        public string TemplateFilePath { get; set; }
        MsgS2Clnt msg2clnt;

        Word.Document DocxFile = null;

        public CreateSMS(Client client)
        {
            Error = "";
            this.client = client;
            msg2clnt = new MsgS2Clnt();
            if (client == null)
            {
                Error = "Error client inctance";
                return;
            }
            msg2clnt.ClntNum = client.ClntNum;
            msg2clnt.MsgType = MsgType.SMS.ToString();
            msg2clnt.SentAt = DateTime.Today.DateTo_int_YYYYMMDD();
            msg2clnt.LastBuyDate = client.LastBuyDate != 0 ? client.LastBuyDate.Long_ToDate_YYYYMMDD() : "";
            msg2clnt.LastPayDate = client.LastPayDate != 0 ? client.LastPayDate.Long_ToDate_YYYYMMDD() : "";
            msg2clnt.ClntBalance = client.ClntBalance.ToString("0.00");
            msg2clnt.TotDepos = client.TotDepos.ToString("0.00");
            msg2clnt.TotSales = client.TotSales.ToString("0.00");
            LRITs = new LReplaceItems();
            LRITs.Add(new ReplaceItem() { OldValue = "[CURR_DATE]", NewValue = DateTime.Today.ToStringDateTimeFormatDateTimeYYYYMMDD() });
            LRITs.Add(new ReplaceItem() { OldValue = "[FULL_NAME]", NewValue = client.FullName });
            LRITs.Add(new ReplaceItem() { OldValue = "[DATE1]", NewValue = client.LastBuyDate != 0 ? client.LastBuyDate.Long_ToDate_YYYYMMDD() : "" });
            LRITs.Add(new ReplaceItem() { OldValue = "[DATE2]", NewValue = client.LastPayDate != 0 ? client.LastPayDate.Long_ToDate_YYYYMMDD() : "" });
            LRITs.Add(new ReplaceItem() { OldValue = "[TOTAL]", NewValue = client.ClntBalance.ToString("0.00") });
        }

        public void Do1()
        {

            if (client == null)
                return;
            SendSMS();
            if (Error != "")
                return;
            SaveRecordDB();
        }

        private async void SendSMS()
        {
            string recipients = client.Tel1;//Properties.Settings.Default.SMS_Num;
            if (recipients == null || recipients == "")
                recipients = "0545772386";
            string smsMessage = "שלום {0},  ראינו נכון לשלוח לכבודך תזכורת זו  בכדי לדאוג לסגירת החוב אצלנו על סך: {1} ₪  בהקדם האפשרי. נודה רבות להתייחסותך בהקדם! סיזארס גוליס";
            SMS_Sender sms = new SMS_Sender();
            await sms.SendUsingAPIAsync(recipients, String.Format(smsMessage, client.FullName, client.ClntBalance.ToString("0.00")));
        }

        private void SaveRecordDB()
        {
            msg2clnt.CreatedBy = GlobalContext.Instance.CurrentUser.UserId;
            msg2clnt.CreatedAt = DateTime.Now.DateTo_int_YYYYMMDD();
            using (var db = DB_Provider.Get())
            {
                db.MsgS2Clnt.Add(msg2clnt);
                var Id = db.SaveChanges();
            }
        }
    */
    }
}
