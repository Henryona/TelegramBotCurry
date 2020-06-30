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
            
            // получение токенов и json из файла
            var token = "";
            var weatherToken = "";
            var newsToken = "";
            var namesJson = "";
            try
            {
                token = System.IO.File.ReadAllText(CONSTANTS.CURRY_BOT_TOKEN_PATH)
                    .Replace("\n", "");
                weatherToken = System.IO.File.ReadAllText(CONSTANTS.WEATHER_API_FILE_PATH)
                    .Replace("\n", "");
                newsToken = System.IO.File.ReadAllText(CONSTANTS.NEWS_API_FILE_PATH)
                    .Replace("\n", "");
                namesJson = System.IO.File.ReadAllText(CONSTANTS.NAMES_JSON_PATH);
            }
            catch (Exception e)
            {
                
                Console.WriteLine("No file with token or json! " + e.Message);
                return;
            }
            
            var API = new TelegramAPI(token);
            var auxiliaryMethods = new AuxiliaryMethods(namesJson, weatherToken, newsToken);
            var potentialQuestions = new Answerer(auxiliaryMethods);
            
            // ожидание бота, получение запросов и ответ на них
            while (true)
            {
                // задержка в одну секунду, чтобы не постоянно обращаться к апи телеграма
                Thread.Sleep(1000);
                try
                {
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
                            new KeyboardButton[]
                                {new KeyboardButton("нужна помощь"), new KeyboardButton("какой день недели?")},
                            new KeyboardButton[]
                            {
                                new KeyboardButton("какая погода в городе Москва"), new KeyboardButton("покажи новости")
                            },
                            new KeyboardButton[]
                                {new KeyboardButton("покажи цитату"), new KeyboardButton("расскажи анекдот")},
                            new KeyboardButton[] {new KeyboardButton("расскажи историю")},
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
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при попытке ответить на некорретный запрос");
                }
            } // circle while
            
            
        } // method Main
    } // class Program
}
