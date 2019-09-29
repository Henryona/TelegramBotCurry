using System;
using System.Collections.Generic;
using System.Globalization;
using MyTelegramBotFirstTryTo;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Program.PotentialQuestions = JsonConvert.DeserializeObject<Dictionary<string, string>>(QuesAnJson);
        }

        [TestCase("привет", "", TestName = "first_case_question_привет")]
        [TestCase("curry привет", "Приветствую, Тестовый пользователь :)", TestName = "second_case_question_curry_привет")]
        public void TestAnswerQuestionFunctionOneQuestion(string question, string answer)
        {
            string expected = answer;
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: question, userName: "Тестовый пользователь", userId: 123456789);
            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAnswerQuestionFunctionFewQuestions()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
            string expected = $"Приветствую, Тестовый пользователь :), It's {dayOfWeek} my dudes!";
            string actual = Program.AnswerQuestion(UserQuestion: "curry привет какой день недели", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeJoke()
        {
            string notExpected = "Мне не совсем ясен твой вопрос";
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: "curry расскажи анекдот", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreNotEqual(notExpected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeStory()
        {
            string notExpected = "Мне не совсем ясен твой вопрос";
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: "curry расскажи историю", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreNotEqual(notExpected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeQuote()
        {
            string notExpected = "Мне не совсем ясен твой вопрос";
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: "curry покажи цитату", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreNotEqual(notExpected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeHoroscopePos()
        {
            string notExpected = "Мне не совсем ясен твой вопрос";
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: "curry расскажи гороскоп Водолей", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreNotEqual(notExpected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeHoroscopeNeg()
        {
            string expected = "скажи мне, кто ты по гороскопу!";
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: "curry расскажи гороскоп КозЯрог", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeNews()
        {
            string notExpected = "Мне не совсем ясен твой вопрос";
            string actual = "";
            
            actual = Program.AnswerQuestion(UserQuestion: "curry покажи новости", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreNotEqual(notExpected, actual);
        }
        
        
        [Test]
        public void TestMakeGreetingsFunctionId1()
        {
            string expected = $"Приветствую, Ксюша :)";
            string actual = AuxiliaryMethods.MakeGreetings(userName: "Henryona");
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestMakeGreetingsFunctionId2()
        {
            string expected = $"Приветствую, Влад :)";
            string actual = AuxiliaryMethods.MakeGreetings(userName: "vestild");
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestMakeInfoFunction()
        {
            string expected = "Команда для меня начинается с \"curry \" . \n После обращения можно вводить следующие запросы: \n" +
                              "\"привет\" \n \"какой день недели\" \n \"какая погода в городе N\" \n " +
                              "\"покажи новости\" \n \"нужна помощь\" \n" +
                              "\"расскажи гороскоп N\" \n \"покажи цитату\" \n" +
                              "\" расскажи анекдот \" \n \" расскажи историю \" \n";
            string actual = AuxiliaryMethods.MakeInfo();
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionMakeDay()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
            string expected = $"It's {dayOfWeek} my dudes!";
            string actual = Program.AnswerQuestion(UserQuestion: "curry какой день недели", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeTemperaturePos()
        {
            string notExpected = "Мне не совсем ясен твой вопрос";
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: "curry какая погода в городе Москва", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreNotEqual(notExpected, actual);
        }
        
        [Test]
        public void TestAnswerQuestionFunctionOneQuestionMakeTemperature()
        {
            string city = "Баку";
            string actual = "";

            actual = Program.AnswerQuestion(UserQuestion: "curry какая погода в городе Баку", userName: "Тестовый пользователь", userId: 123456789);
            Assert.True(actual.Contains(city));
        }
        
        
        
    }
}