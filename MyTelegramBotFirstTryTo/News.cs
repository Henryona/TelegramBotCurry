using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class News
    {
        public class NewsData
        {
            public List<Articles> articles { get; set; }
        }

        public class Articles
        {
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
        }
        
        private RestClient RC = new RestClient();

        public News()
        { }

        public List<string> getNews()
        {
            var CONST = new CONSTANTS();
            string API_URL = "https://newsapi.org/v2/top-headlines";
            string API_COUNTRY = "ru";
            string API_KEY = System.IO.File.ReadAllText(CONST.NEWS_API_FILE_PATH)
                .Replace("\n", "");
            string FINAL_URL = API_URL + "?country=" + API_COUNTRY + "&apiKey=" + API_KEY ;
            
            List<string> NewsList = new List<string>();
            var URL = FINAL_URL;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<NewsData>(Response.Content);

            foreach (var newsArticle in Data.articles)
            {
                NewsList.Add($"Новость: {newsArticle.description}, ссылка: {newsArticle.url}. ");
            }
                
            return NewsList;
        } // method getNews
    } // class News
}