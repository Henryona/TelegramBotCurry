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

        private readonly string FINAL_URL;

        public News(string newsToken)
        {
            // формирование запроса к погодному апи
            FINAL_URL = CONSTANTS.API_URL_NEWS  + CONSTANTS.API_COUNTRY + "&apiKey=" + newsToken;
        }

        public List<string> getNews()
        {
            // список новостей
            List<string> NewsList = new List<string>();
            
            // запрос
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<NewsData>(Response.Content);
            try
            {
                // добавление новостей в цикле
                foreach (var newsArticle in Data.articles)
                {
                    NewsList.Add($"Новость: {newsArticle.description}, ссылка: {newsArticle.url}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при парсе ответа от новостного api: " + e);
                NewsList.Add( "Error_in_news_api");
            }
            return NewsList;
        } // method getNews
    } // class News
}