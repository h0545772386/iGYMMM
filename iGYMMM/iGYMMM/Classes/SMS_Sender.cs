using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace iGYMMM
{
    public class SMS_Sender
    {
        public async Task<string> SendUsingAPIAsync(string Recipients, string MessageText)
        {
            HttpClient client = new HttpClient();
            //Define the Required Variables
            string key = "hl6vq3eyJ";
            string user = "0545772386";
            string pass = "96336197";
            string sender = "Cezars"; // A-Z, a-z, 0-9, Max lenght 11 
            string recipient = Recipients.Trim();

            var values = new Dictionary<string, string>
            {
                { "key", key }, { "user", user },{ "pass", pass },
                { "sender", sender }, { "recipient", recipient },
                { "msg", MessageText }
            };
            var content = new FormUrlEncodedContent(values); //Encode the Data
            var response = await client.PostAsync("https://www.sms4free.co.il/ApiSMS/SendSMS", content);
            var responseString = await response.Content.ReadAsStringAsync();
            Thread.Sleep(5000); //Sleep for 5 SECOND Until API FINISH His Work
            return responseString; //Gives You How many Recipients the message was sent to           
        }
    }
}
