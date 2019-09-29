using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using RestSharp;
using Newtonsoft.Json;
namespace MyTelegramBotFirstTryTo

{
    public class TelegramAPI
    {
        
        private RestClient RC = new RestClient();
        // инициализация id апйдейта
        private int last_update_id = 0;
        
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
        
        static String BotToken;

        // послать сообщение в чат (строку)
        public void sendMessage(string API_URL,string text, int chat_id, string keyboard)
        {
            sendApiRequest(API_URL, "sendMessage", $"chat_id={chat_id}&text={text}&reply_markup={keyboard}");
        } // method sendMessage with answer
        
        // послать сообщение в чат (массив строк)
        public void sendMessage(string API_URL, List<string> text, int chat_id, string keyboard)
        {
            foreach (string part in text)
                sendApiRequest(API_URL, "sendMessage", $"chat_id={chat_id}&text={part}&reply_markup={keyboard}");
        } // method sendMessage with List of answers

        // получение апдейтов
        public Update[] GetUpdates(string API_URL)
        {
            var json = sendApiRequest(API_URL, "getUpdates", "offset=" + last_update_id);
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            var updates = apiResult.result;

            foreach (var update in updates)
            {
                // вывод для отладки
                /*Console.WriteLine($"Получен апдейт {update.update_id}, " + 
                                  $"сообщение от {update.message.from.first_name} " +
                                  $"текст: {update.message.text}");*/
                last_update_id = update.update_id + 1;
            }

            return updates;
        } // method GetUpdates

        public string sendApiRequest(String API_URL, String ApiMethod, string Params)
        {
            // запрос
            var URL = API_URL + ApiMethod + "?" + Params;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            return Response.Content;
        } // method sendApiRequest
        
        public TelegramAPI()
        { }
        
    } // class TelegramAPI
}
