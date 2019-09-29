using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class Program
    {
        public static Dictionary<string, string> Questions;
        private static List<string> ListContent = new List<string>();
        
        public interface IKeyboardMarkup
        {
        }

        public class KeyboardButton
        {
            [JsonProperty("text")] public string Text { get; set; }

            public KeyboardButton(string text)
            {
                Text = text;
            }
        }

        public class ReplyKeyBoardMarkup : IKeyboardMarkup
        {
            [JsonProperty("keyboard")] public KeyboardButton[][] Keyboard { get; set; }
            [JsonProperty("resize_keyboard")] public bool ResizeKeyboard { get; set; } = true;
            [JsonProperty("one_time_keyboard")] public bool OneTimeKeyboard { get; set; } = true;
        }
        
        public static List<String> Answers = new List<String>();

        static void Main(string[] args)
        {
            var CONST = new CONSTANTS();
            //var Methods = new AuxiliaryMethods();
            var API = new TelegramAPI();
            
            // получение списка вопросов из JSON файла
            var QuesAnJson = System.IO.File.ReadAllText( CONST.QU_AN_JSON_PATH_LIN);
            Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(QuesAnJson);
            
            /*// получение списка id разрешенных чатов 
            var ChatIDs = System.IO.File.ReadAllText(CONST.CHAT_ID_FILE_PATH).Replace("\n", "");
            var IdsList = ChatIDs.Split(' '); */

            // добавление сообщения погоды по расписанию
            //Methods.MakeSchedule(CONST.CITY, CONST.CHAT_ID);
            
            var token = API.getApiUrl(CONST.CURRY_BOT_TOKEN_PATH_LIN);
            
            // ожидание бота, получение запросов и ответ на них
            while (true)
            {
                Thread.Sleep(1000);
                var Updates = API.GetUpdates(token);
                foreach (var update in Updates)
                {
                    /*if (IdsList.Contains(update.message.chat.id.ToString()))
                    { */
                        var userName = update.message.from.first_name;
                        var userId = update.message.from.id;
                        var Question = update.message.text;
                        var Answer = AnswerQuestion(Question, userName, userId);
                        
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
                        API.sendMessage(token,Answer, update.message.chat.id, keyboard); 
                        // некоторые ответы записаны в список, посылаем все его элементы
                        if (ListContent.Count != 0)
                            API.sendMessage(token, ListContent, update.message.chat.id, keyboard);
                        
                        // очищаем ответы
                        Answer = "";
                        ListContent.Clear();
                        Answers.Clear();
                    //}
                }
            }
            
        } // method Main

        // формирование ответа
        public static string AnswerQuestion(string UserQuestion, string userName, int userId)
        {
            Answers.Clear();
            var CONST = new CONSTANTS();

            UserQuestion = UserQuestion.ToLower();

            // вопрос должен быть адресован боту: начинаться с его имени
            if (UserQuestion.StartsWith(CONST.BOT_NAME))
            {
                foreach (var question in Questions)
                {
                    // проверка, что в вопросе содержатся необходимые ключи и добавление соответствующих ответов
                    if (UserQuestion.Contains((question.Key)))
                    {
                        switch (question.Value)
                        {
                            case "MakeDay()" : Answers.Add(AuxiliaryMethods.MakeDay());
                                break;
                            case "MakeTemperature()" : Answers.Add(AuxiliaryMethods.MakeTemperature(UserQuestion));
                                break;
                            case "MakeGreetings()" : Answers.Add(AuxiliaryMethods.MakeGreetings(userId, userName));
                                break;
                            case "MakeNews()" : {
                                Answers.Add(CONST.NEWS_ANSWER);
                                ListContent = AuxiliaryMethods.MakeNews();
                            }
                                break;
                            case "MakeInfo()" : Answers.Add(AuxiliaryMethods.MakeInfo());
                                break;
                            case "MakeHoroscope()" : Answers.Add(AuxiliaryMethods.MakeHoroscope(UserQuestion));
                                break;
                            case "MakeQuote()" : Answers.Add(AuxiliaryMethods.MakeQuote());
                                break;
                            case "MakeJoke()" : Answers.Add(AuxiliaryMethods.MakeJoke(UserQuestion));
                                break;
                            default : Answers.Add(question.Value);
                                break;
                        }
                    }
                }
             
                // ни одного ответа не удалось подобрать- был задан некорректный вопрос
                if (Answers.Count == 0)
                    Answers.Add("Мне не совсем ясен твой вопрос"); 
            }
            return string.Join(", ", Answers);
        } // method AnswerQuestion
        
    } // class Program
}
