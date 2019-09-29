using System;
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
            // формирование запроса к новостному апи
            string API_KEY = "";
            try
            {
                API_KEY = "&apiKey=" +  System.IO.File.ReadAllText(CONSTANTS.NEWS_API_FILE_PATH)
                              .Replace("\n", "");
            }
            catch
            {
                Console.WriteLine("No file with news api token!");
            }
            // формирование запроса к новостному апи
            string FINAL_URL = CONSTANTS.API_URL_NEWS  + CONSTANTS.API_COUNTRY + API_KEY;
            
            // список новостей
            List<string> NewsList = new List<string>();
            
            // запрос
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<NewsData>(Response.Content);

            // добавление новостей в цикле
            foreach (var newsArticle in Data.articles)
            {
                NewsList.Add($"Новость: {newsArticle.description}, ссылка: {newsArticle.url}");
            }
                
            return NewsList;
        } // method getNews
    } // class News
}