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
        
        // формирование запроса к апи с цитатами
        string FINAL_URL = CONSTANTS.API_URL_QUOTES + CONSTANTS.API_METHOD + 
                           CONSTANTS.API_FORMAT + CONSTANTS.API_LANGUAGE;
        private RestClient RC = new RestClient();

        // получение цитаты
        public string getRandonQuote()
        {
            // запрос цитаты
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);

            // парсим ответ, он в json-формате
            var Data = JsonConvert.DeserializeObject<QuoteData>(Response.Content);
            
            return Data.quoteText + " \n" + Data.quoteAuthor;
            ;
        } // method getRandonQuote
        public Quotes()
        {}
    } // class Quotes
}