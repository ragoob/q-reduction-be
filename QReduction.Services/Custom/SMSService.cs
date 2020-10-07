using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using QReduction.Core.Service.Custom;

namespace QReduction.Services.Custom
{
   public class SMSService : ISMSService
    {
        public async Task<string> Send(string mobileNumber, string message)
        {

            //string Url = $"https://www.hisms.ws/api.php?send_sms&username=966505577348&password=123123&numbers={mobileNumber}&sender=EAASHA&message={message}";
            string Url = $"https://www.alfa-cell.com/api/msgSend.php?apiKey=96a46a7bdc0c524dc5baee5200972443&numbers={mobileNumber}&sender=Eaasha.com&msg={message}&timeSend=0&dateSend=0&applicationType=68&domainName=Eaasha.com&lang=3&returnJson=1";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(Url, null);
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch
                {
                    return null;
                }

            }
        }

    }
}
