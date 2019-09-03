using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;

namespace MyTelegramBotFirstTryTo
{
    class Program
    {
        static Dictionary<string, string> Questions;
        private static List<string> ListContent = new List<string>();
        private static bool Flag = false;

        static void Main(string[] args)
        {
            var QuesAnJson = System.IO.File.ReadAllText("/home/henryona/RiderProjects/MyTelegramBotFirstTryTo/MyTelegramBotFirstTryTo/questions.json");
            Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(QuesAnJson);
            var ChatIDs = System.IO.File.ReadAllText("/home/henryona/Документы/curryBot/chat_ids").Replace("\n", "");
            var IdsList = ChatIDs.Split(' ');
            var API = new TelegramAPI();
            
            var singularity = Singularity.Instance;
            var job = new SimpleJob(
                (scheduledTime) =>
                {
                    API.sendMessage("start", -258099164);
                } //(MakeTemperature("Москва"), -258099164);            }
                );
            var schedule = new CronSchedule("0 07 05 * * ? *"); // нужно писать на 3 часа меньше (если нужно 18 часов, то писать 15)
            var scheduledJob = singularity.ScheduleJob(
                schedule, job, DateTime.UtcNow.AddMinutes(62)); //starts immediately
            singularity.Start();
            

            while (true)
            {
                Thread.Sleep(1000);
                var Updates = API.GetUpdates();
                foreach (var update in Updates)
                {
                    if (IdsList.Contains(update.message.chat.id.ToString()))
                    {
                        var userName = update.message.from.first_name;
                        var userId = update.message.from.id;
                        //Console.WriteLine(userId);
                        var Question = update.message.text;
                        var Answer = AnswerQuestion(Question, userName, userId);
                        API.sendMessage(Answer, update.message.chat.id); 
                        if (ListContent.Count != 0)
                            API.sendMessage(ListContent, update.message.chat.id);
                    }
                }
            }
            
        }

        static string AnswerQuestion(string UserQuestion, string userName, int userId)
        {
            List<String> Answers = new List<String>();

            UserQuestion = UserQuestion.ToLower();

            if (UserQuestion.StartsWith("curry"))
            {
                foreach (var question in Questions)
                {
                    if (UserQuestion.Contains((question.Key)))
                    {
                        if (question.Value == "MakeDay()")
                            Answers.Add(MakeDay());
                        else if (question.Value == "MakeTemperature()")
                            Answers.Add(MakeTemperature(UserQuestion));
                        else if (question.Value == "MakeGreetings()")
                            Answers.Add(MakeGreetings(userId, userName));
                        else if (question.Value == "MakeNews()")
                        {
                            Answers.Add("Вот тут немного актуальных новостей");
                            ListContent = MakeNews();
                        }
                        else if (question.Value == "TypeInfo()")
                            Answers.Add(TypeInfo());
                        else if (question.Value ==  "MakeHoroscope()")
                            Answers.Add(MakeHoroscope(UserQuestion));
                        else
                            Answers.Add(question.Value);
                    }
                }
             
                if (Answers.Count == 0)
                    Answers.Add("Мне не совсем ясен твой вопрос"); 
            }
            return string.Join(", ", Answers);
        }


        static string MakeDay()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
            return ($"It's {dayOfWeek} my dudes!");
        }

        static string MakeTemperature(string UserQuestion)
        {
            var words = UserQuestion.Split(' ');
            var city = words[words.Length - 1];
            
            var WeatherApi = new Weather();
            var forecast = WeatherApi.getWeatherInCity(city);
            return forecast;
        }

        static string MakeGreetings(int userId, string userName)
        {
            string callName = userName;
            if (userId == 138200931)
                callName = "Ксюша";
            else if (userId == 162374704)
                callName = "Влад";
            return ($"Приветствую, {callName} :)");
        }

        static List<string> MakeNews()
        {
            var NewsApi = new News();
            var newsCollumn = NewsApi.getNews();
            return newsCollumn;
        }

        static string MakeHoroscope(string UserQuestion)
        {
            var words = UserQuestion.Split(' ');
            var signOfZodiac = words[words.Length - 1];
            
            var HoroscopeApi = new Horoscope();
            var horoscopeText = HoroscopeApi.getHororscopeBySign(signOfZodiac);
            return horoscopeText;
        }

        static string TypeInfo()
        {
            return $"Команда для меня начинается с \"curry \" . \n После обращения можно вводить следующие запросы: \n" + 
                   "\"привет\" - приветствие, \n \"какой день недели\" - я подскажу, какой день недели сегодня, \n \"какая погода в городе N\" - расскажу о погоде, \n " +
                   "\"покажи новости\" - покажу немного новостей, \n \"нужна помощь\" - вызов хелпа :)";
        }
    }
}