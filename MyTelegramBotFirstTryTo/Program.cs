using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class Program
    {
        public class KeyboardButton
        {
            [JsonProperty("text")] public string Text { get; set; }

            public KeyboardButton(string text)
            {
                Text = text;
            }
        }

        public class ReplyKeyBoardMarkup
        {
            [JsonProperty("keyboard")] public KeyboardButton[][] Keyboard { get; set; }
            [JsonProperty("resize_keyboard")] public bool ResizeKeyboard { get; set; } = true;
            [JsonProperty("one_time_keyboard")] public bool OneTimeKeyboard { get; set; } = true;
        }

        static void Main(string[] args)
        {
            // добавление сообщения погоды по расписанию
            //Methods.MakeSchedule(CONST.CITY, CONST.CHAT_ID);
            
            // получение токена из файла
            var token = "";
            try
            {
                token = System.IO.File.ReadAllText(CONSTANTS.CURRY_BOT_TOKEN_PATH)
                    .Replace("\n", "");
            }
            catch
            {
                Console.WriteLine("No file with bot token!");
                return;
            }
            
            var API = new TelegramAPI(token);
            var potentialQuestions = new Answerer();
            
            // ожидание бота, получение запросов и ответ на них
            while (true)
            {
                // задержка в одну секунду, чтобы не постоянно обращаться к апи телеграма
                Thread.Sleep(1000);
                
                // получение апдейтовret
                var Updates = API.GetUpdates();
                foreach (var update in Updates)
                {
                    var userName = update.message.from.first_name;
                    var Question = update.message.text;
                    var (answer, listContent) = potentialQuestions.Answer(Question, userName);
                       
                    // кнопочки для телеграма
                    var keyboardMarkUp = new ReplyKeyBoardMarkup();
                    keyboardMarkUp.Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[] {new KeyboardButton("curry привет"), new KeyboardButton("curry какой день недели")},
                        new KeyboardButton[] {new KeyboardButton("curry какая погода в городе Москва"), new KeyboardButton("curry покажи новости") },
                        new KeyboardButton[] {new KeyboardButton("curry нужна помощь"), new KeyboardButton("curry расскажи гороскоп") },
                        new KeyboardButton[] {new KeyboardButton("curry покажи цитату"), new KeyboardButton("curry расскажи анекдот") },
                        new KeyboardButton[] {new KeyboardButton("curry расскажи историю") },
                    };
                    string keyboard = JsonConvert.SerializeObject(keyboardMarkUp);
                        
                    //  большая часть ответов - обычный string, посылаем его
                    if (!string.IsNullOrEmpty(answer))
                        API.sendMessage(answer, update.message.chat.id, keyboard); 
                    // некоторые ответы записаны в список, посылаем все его элементы
                    if (listContent.Count != 0)
                        API.sendMessage(listContent, update.message.chat.id, keyboard);
                }
            }
            
        } // method Main
    } // class Program
}
