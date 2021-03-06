using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices;
using RestSharp;
using Newtonsoft.Json;
namespace MyTelegramBotFirstTryTo

{
    public class TelegramAPI
    {
        private readonly string apiUrl;
        private RestClient RC = new RestClient();
        // инициализация id апйдейта
        private int last_update_id = 0;

        public TelegramAPI(string token)
        {
            apiUrl = CONSTANTS.API_URL_MAIN + token + "/";
        }
        
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
        
        // послать сообщение в чат (строку)
        public void sendMessage(string text, int chat_id, string keyboard)
        {
            sendApiRequest("sendMessage", $"chat_id={chat_id}&text={text}&reply_markup={keyboard}");
        } // method sendMessage with answer
        
        // послать сообщение в чат (массив строк)
        public void sendMessage(List<string> text, int chat_id, string keyboard)
        {
            foreach (string part in text)
                sendMessage(part, chat_id, keyboard);
        } // method sendMessage with List of answers

        // получение апдейтов
        public Update[] GetUpdates()
        {
            Update[] updates = {};
            try
            {
                var json = sendApiRequest("getUpdates", "offset=" + last_update_id);
                var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
                //updates= {};
                updates = apiResult.result;
                //Console.WriteLine(json + "\n");
                foreach (var update in updates)
                {
                    last_update_id = update.update_id + 1;
                    // вывод для отладки
                    Console.WriteLine($"Для curry получен апдейт {update.update_id}, " +
                                      $"сообщение от {update.message.from.first_name} " +
                                      $"текст: {update.message.text}");
                    //last_update_id = update.update_id + 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Что-то пошло не так при попытке прочитать содержимое апдейта: " + e);
            }

            return updates;
        } // method GetUpdates

        public string sendApiRequest(string ApiMethod, string Params)
        {
            // запрос
            var URL = apiUrl + ApiMethod + "?" + Params;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            return Response.Content;
        } // method sendApiRequest

    } // class TelegramAPI
}
