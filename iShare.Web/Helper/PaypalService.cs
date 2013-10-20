using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using ServiceStack.Text;
using iShare.Web.RestServices;

namespace iShare.Web.Helper
{
    public static class PaypalService
    {
        public static void Donate(DonationRestService.DonationDto creditCard, string accessToken)
        {
            var data = new DonateDto { intent = "sale", payer = new Payer { payment_method = "credit_card" } };
            data.payer.funding_instruments = new List<CreditCardDto>{
                new CreditCardDto
                    {
                        credit_card =  new CreditCard
                                        {
                                            cvv2 = creditCard.Code,
                                            expire_month = creditCard.Month,
                                            expire_year = creditCard.Year,
                                            first_name = creditCard.FirstName,
                                            last_name = creditCard.LastName,
                                            number = creditCard.Number,
                                            type = creditCard.Type
                                        }
                    }
            };
            data.transactions = new List<Transaction>
                {
                 new Transaction
                    {
                        amount = new Amount
                            {
                                currency = "USD",
                                total = creditCard.Amount.ToString(CultureInfo.InvariantCulture)
                            },
                        description = "This is the payment transaction description."
                    }   
                };
            var stringData = data.ToJson();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.sandbox.paypal.com/v1/payments/payment");
            WebHeaderCollection myWebHeaderCollection = httpWebRequest.Headers;
            myWebHeaderCollection.Add(@"Authorization", "Bearer " + accessToken);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(stringData);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
        }
        #region RequestObjects
        public class DonateDto
        {
            public string intent { get; set; }
            public Payer payer { get; set; }
            public List<Transaction> transactions { get; set; }
        }

        public class Payer
        {
            public string payment_method { get; set; }
            public List<CreditCardDto> funding_instruments { get; set; }
        }
        public class CreditCardDto
        {
            public CreditCard credit_card { get; set; }
        }
        public class CreditCard
        {
            public string number { get; set; }
            public string type { get; set; }
            public int expire_month { get; set; }
            public int expire_year { get; set; }
            public int cvv2 { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
        }
        public class Transaction
        {
            public Amount amount { get; set; }
            public string description { get; set; }
        }

        public class Amount
        {
            public string total { get; set; }
            public string currency { get; set; }
        }
        #endregion

        public static string Initiate()
        {
            var data = new NameValueCollection { { "grant_type", @"client_credentials" } };
            var authInfo = "Ack8RBCrf06Pxqqo0jDUGHM30UUHiRDt5k35bkaxMkvqPCHzNDn0nR3wU-_5:EOXUkxCdPt66oD3dw8ZaQxXfDA1tDX7yUec4BQvG4Kw8SF5bWnWWqlVTRBoG";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            var headers = new NameValueCollection { { "Accept", @"application/json" }, { "Accept-Language", "en_US" }, { "Authorization", "Basic " + authInfo } };
            using (var client = new WebClient())
            {
                client.Headers.Add(headers);
                byte[] response = client.UploadValues("https://api.sandbox.paypal.com/v1/oauth2/token", data);
                var serializedResponse = response.FromUtf8Bytes().FromJson<AccessToken>();
                Console.WriteLine(serializedResponse.access_token);
                return serializedResponse.access_token;
            }
        }
    }


    public class AccessToken
    {
        public string scope { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public string expires_in { get; set; }
    }

}