using System;
using System.Collections.Generic;

namespace MyTelegramBotFirstTryTo
{
    public class Answerer
    {
        private readonly Dictionary<string, Action<string, string, List<string>, List<string>>> questions;

        public Answerer()
        {
            questions = new Dictionary<string, Action<string, string, List<string>, List<string>>>
            {
                ["какой день недели"] = MakeDay,
                ["привет"] = MakeGreetings,
                ["какая погода в городе"] = MakeTemperature,
                ["покажи новости"] = MakeNews,
                ["нужна помощь"] = MakeInfo,
                ["расскажи гороскоп"] = MakeHoroscope,
                ["покажи цитату"] = MakeQuote,
                ["расскажи анекдот"] = MakeJoke,
                ["расскажи историю"] = MakeJoke,
            };
        }

        private void MakeDay(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(AuxiliaryMethods.MakeDay());
        }
        
        private void MakeTemperature(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(AuxiliaryMethods.MakeTemperature(userQuestion));
        }
        
        private void MakeGreetings(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(AuxiliaryMethods.MakeGreetings(userName));
        }
        
        private void MakeNews(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(CONSTANTS.NEWS_ANSWER);
            listContent.AddRange(AuxiliaryMethods.MakeNews());
        }
        
        private void MakeInfo(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(AuxiliaryMethods.MakeInfo());
        }
        
        private void MakeHoroscope(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(AuxiliaryMethods.MakeHoroscope(userQuestion));
        }
        
        private void MakeQuote(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(AuxiliaryMethods.MakeQuote());
        }
        
        private void MakeJoke(string userQuestion, string userName, List<string> answers, List<string> listContent)
        {
            answers.Add(AuxiliaryMethods.MakeJoke(userQuestion));
        }
        

        public (string answers,  List<string> listContent) Answer(string userQuestion, string userName)
        {
            // вопрос должен быть адресован боту: начинаться с его имени
            if (!userQuestion.StartsWith(CONSTANTS.BOT_NAME))
            {
                return ("", new List<string>());
            }
            
            var answers = new List<string>();
            var listContent = new List<string>();

            foreach (var variant in questions)
            {
                if (userQuestion.Contains(variant.Key))
                {
                    variant.Value(userQuestion, userName, answers, listContent);
                }
            }
            
            // ни одного ответа не удалось подобрать- был задан некорректный вопрос
            if (answers.Count == 0)
                answers.Add("Мне не совсем ясен твой вопрос");     
            
            return (string.Join(", ", answers), listContent);
        }
    }
}