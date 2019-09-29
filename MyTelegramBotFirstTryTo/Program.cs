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
        static List<String> Answers = new List<String>();
        
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

        static void Main(string[] args)
        {
            var API = new TelegramAPI();
            
            // получение списка вопросов из JSON файла
            var QuesAnJson = System.IO.File.ReadAllText(CONSTANTS.QU_AN_JSON_PATH);
            Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(QuesAnJson);

            // добавление сообщения погоды по расписанию
            //Methods.MakeSchedule(CONST.CITY, CONST.CHAT_ID);
            
            // получение токена из файла
            var token = "";
            try
            {
                string BotToken = System.IO.File.ReadAllText(CONSTANTS.CURRY_BOT_TOKEN_PATH)
                    .Replace("\n", "");
                token =  CONSTANTS.API_URL_MAIN + BotToken + "/";
            }
            catch
            {
                Console.WriteLine("No file with bot token!");
            }
            
            // ожидание бота, получение запросов и ответ на них
            while (true)
            {
                // задержка в одну секунду, чтобы не постоянно обращаться к апи телеграма
                Thread.Sleep(1000);
                
                // получение апдейтов
                var Updates = API.GetUpdates(token);
                foreach (var update in Updates)
                {
                    var userName = update.message.from.first_name;
                    var userId = update.message.from.id;
                    var Question = update.message.text;
                    var Answer = AnswerQuestion(Question, userName, userId);
                       
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
                    API.sendMessage(token,Answer, update.message.chat.id, keyboard); 
                    // некоторые ответы записаны в список, посылаем все его элементы
                    if (ListContent.Count != 0)
                        API.sendMessage(token, ListContent, update.message.chat.id, keyboard);
                        
                    // очищаем ответы
                    ListContent.Clear();
                    Answers.Clear();
                }
            }
            
        } // method Main

        // формирование ответа
        public static string AnswerQuestion(string UserQuestion, string userName, int userId)
        {
            //Answers.Clear();
            UserQuestion = UserQuestion.ToLower();

            // вопрос должен быть адресован боту: начинаться с его имени
            if (UserQuestion.StartsWith(CONSTANTS.BOT_NAME))
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
                            case "MakeGreetings()" : Answers.Add(AuxiliaryMethods.MakeGreetings(userName));
                                break;
                            case "MakeNews()" : {
                                Answers.Add(CONSTANTS.NEWS_ANSWER);
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
