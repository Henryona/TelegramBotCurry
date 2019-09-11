using Newtonsoft.Json;
using RestSharp;

namespace MyTelegramBotFirstTryTo
{
    public class Quotes
    {

        public class QuoteData
        {
            public string quoteText { get; set; }
            public string quoteAuthor { get; set; }
        }
        
        const string API_URL = "https://api.forismatic.com/api/1.0/";
        const string API_METHOD = "?method=getQuote";
        const string FINAL_URL = API_URL + API_METHOD + "&format=json"  + "&lang=ru&q=";
        private RestClient RC = new RestClient();

        public string getRandonQuote()
        {
            var URL = FINAL_URL;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<Quotes.QuoteData>(Response.Content);
            
            return Data.quoteText + " \n" + Data.quoteAuthor;
            ;
        } // method getRandonQuote
        public Quotes()
        {}
    } // class Quotes
}