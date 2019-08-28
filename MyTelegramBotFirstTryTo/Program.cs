using System;
using System.Collections.Generic;
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
            //API.sendApiRequest("getMe");
            //API.GetUpdates();
            //API.sendMessage("Проба пера");

            while (true)
            {
                Thread.Sleep(1000);
                var Updates = API.GetUpdates();
                foreach (var update in Updates)
                {
                    var Question = update.message.text;
                    var Answer = AnswerQuestion(Question);
                    API.sendMessage(Answer, update.message.chat.id);
                }
            }
            
        }

        static string AnswerQuestion(string UserQuestion)
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
                        else
                            Answers.Add(question.Value);
                    }
                }
             
                /*if (Answers.Count == 0)
                    Answers.Add("Мне не совсем ясен твой вопрос"); */  
            }
            return string.Join(", ", Answers);
        }

        static string MakeDay()
        {
            var dayOfWeek = DateTime.Now.ToString("dddd");
            return ($"It's {dayOfWeek} my dudes!");
        }
    }
}