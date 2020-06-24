using System;
using System.Collections.Generic;

namespace MyTelegramBotFirstTryTo
{
    public class Answerer
    {
        private readonly AuxiliaryMethods _methods;
        private readonly Dictionary<string, Action<string, string, List<string>, List<string>>> questions;

        public Answerer(AuxiliaryMethods methods)
        {
            _methods = methods;
            questions = new Dictionary<string, Action<string, string, List<string>, List<string>>>
            {
                ["какой день недели?"] = MakeDay,
                ["привет"] = MakeGreetings,
                ["хей"] = MakeGreetings,
                ["как твои дела?"] = MakeHowAreYou,
                ["как дела?"] = MakeHowAreYou,
                ["как ты?"] = MakeHowAreYou,
                ["какая погода в городе"] = MakeTemperature,
                ["покажи новости"] = MakeNews,
                ["нужна помощь"] = MakeInfo,
                ["расскажи гороскоп"] = MakeHoroscope,
                ["покажи цитату"] = MakeQuote,
                ["расскажи анекдот"] = MakeJoke,
                ["расскажи историю"] = MakeJoke,
                ["что делаешь?"] = MakeWork,
                ["чем занимаешься?"] = MakeWork,
                ["ты милаха"] = MakeCompliment,
                ["ты симпатяга"] = MakeCompliment,
            };
        }

        private void MakeHowAreYou(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeHowAreYou());
        }

        private void MakeDay(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeDay());
        }
        
        private void MakeTemperature(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeTemperature(userQuestion));
        }
        
        private void MakeGreetings(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeGreetings(userName));
        }
        
        private void MakeNews(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(CONSTANTS.NEWS_ANSWER);
            listContent.AddRange(_methods.MakeNews());
        }
        
        private void MakeInfo(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeInfo());
        }
        
        private void MakeHoroscope(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeHoroscope(userQuestion));
        }
        
        private void MakeQuote(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeQuote());
        }
        
        private void MakeJoke(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeJoke(userQuestion));
        }
        
        private void MakeWork(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeWork(userQuestion));
        }
        
        private void MakeCompliment(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(_methods.MakeCompliment(userQuestion));
        }
        

        public (string answers,  List<string> listContent) Answer(string userQuestion, string userName)
        {
            // вопрос должен быть адресован боту: начинаться с его имени
            /*if (!userQuestion.StartsWith(CONSTANTS.BOT_NAME))
            {
                return ("", new List<string>());
            }*/
            
            var answers = new List<string>();
            var listContent = new List<string>();
            try
            {
                foreach (var variant in questions)
                {
                    userQuestion = userQuestion.ToLower();
                    if (userQuestion.Contains(variant.Key))
                    {
                        variant.Value(userQuestion, userName, answers, listContent);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Не надо посылать стикеры боту, так как: " + e);
            }

            // ни одного ответа не удалось подобрать- был задан некорректный вопрос
            if (answers.Count == 0)
                answers.Add(CONSTANTS.dontknow[new Random().Next(0, CONSTANTS.dontknow.Count)] );     
            
            return (string.Join(", ", answers), listContent);
        }
    }
}