using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
namespace MyTelegramBotFirstTryTo

{
    public class TelegramAPI
    {
        public class From
        {
            public int id { get; set; }
            public string first_name { get; set; }
        }

        public class Chat
        {
            public int id { get; set; }
            public string first_name { get; set; }
        }

        public class Message
        {
            public Chat chat { get; set; }
            public string text { get; set; }
            public From from { get; set; }
        }

        public class Update
        {
            public int update_id { get; set; }
            public Message message { get; set; }
        }

        public class ApiResult
        {
            public Update[] result { get; set; }
        }
        static String BotToken = System.IO.File.ReadAllText("/home/henryona/Документы/curryBot/currybottoken")
                                    .Replace("\n", "");
        
        String API_URL = "https://api.telegram.org/bot" + BotToken + "/";

        RestClient RC = new RestClient();

        private int last_update_id = 221585541;
        public TelegramAPI()
        {
            
        }

        public void sendMessage(string text, int chat_id)
        {
            sendApiRequest("sendMessage", $"chat_id={chat_id}&text={text}");
        }
        
        public void sendMessage(List<string> text, int chat_id)
        {
            foreach (string part in text)
                sendApiRequest("sendMessage", $"chat_id={chat_id}&text={part}");
        }

        public Update[] GetUpdates()
        {
            var json = sendApiRequest("getUpdates", "offset=" + last_update_id);
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);

            foreach (var update in apiResult.result)
            {
                Console.WriteLine($"Получен апдейт {update.update_id}, " + 
                                  $"сообщение от {update.message.from.first_name} " +
                                  $"текст: {update.message.text}");
                last_update_id = update.update_id + 1;
            }

            return apiResult.result;
        }
        public string sendApiRequest(String ApiMethod, string Params)
        {
            var URL = API_URL + ApiMethod + "?" + Params;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            return Response.Content;
        }
    }
}