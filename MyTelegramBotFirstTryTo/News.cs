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

        const string API_URL = "https://newsapi.org/v2/top-headlines";
        const string API_COUNTRY = "ru";
        const string API_KEY = "efc7771681bf4945804076af394052b9";
        const string FINAL_URL = API_URL + "?country=" + API_COUNTRY + "&apiKey=" + API_KEY ;
        private RestClient RC = new RestClient();

        public News()
        {
        }

        public List<string> getNews()
        {
            var URL = FINAL_URL;
            
            var Request = new RestRequest(URL);

            var Response = RC.Get(Request);

            var Data = JsonConvert.DeserializeObject<NewsData>(Response.Content);

            List<string> NewsList = new List<string>();

            foreach (var newsArticle in Data.articles)
            {
                NewsList.Add($"Новость: {newsArticle.description}, ссылка: {newsArticle.url}. ");
            }
                
            return NewsList;
        }
    }
}