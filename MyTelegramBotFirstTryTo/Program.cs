using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    class Program
    {
        static Dictionary<string, string> Questions;

        static void Main(string[] args)
        {
            var QuesAnJson = System.IO.File.ReadAllText("/home/henryona/RiderProjects/MyTelegramBotFirstTryTo/MyTelegramBotFirstTryTo/questions.json");
            Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(QuesAnJson);
            
            var API = new TelegramAPI();

            while (true)
            {
                Thread.Sleep(1000);
                var Updates = API.GetUpdates();
                foreach (var update in Updates)
                {
                    if (update.message.chat.id == 138200931 || update.message.chat.id == 162374704 || update.message.chat.id == -258099164)
                    {
                        var userName = update.message.from.first_name;
                        var userId = update.message.from.id;
                        Console.WriteLine(userId);
                        var Question = update.message.text;
                        var Answer = AnswerQuestion(Question, userName, userId);
                        API.sendMessage(Answer, update.message.chat.id); 
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
    }
}