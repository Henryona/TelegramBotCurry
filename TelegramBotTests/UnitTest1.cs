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
            var CONST = new CONSTANTS();
            var QuesAnJson = System.IO.File.ReadAllText( CONST.QU_AN_JSON_PATH_LIN);
            Program.Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(QuesAnJson);
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
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
        public void TestAnswerQuestionFunctionFewQuestion()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
            string expected = $"Приветствую, Тестовый пользователь :), It's {dayOfWeek} my dudes!";
            string actual = Program.AnswerQuestion(UserQuestion: "curry привет какой день недели", userName: "Тестовый пользователь", userId: 123456789);
            Assert.AreEqual(expected, actual);
        }
        
        
        [Test]
        public void TestMakeGreetingsFunctionId1()
        {
            string expected = $"Приветствую, Ксюша :)";
            string actual = AuxiliaryMethods.MakeGreetings( userId: 138200931, userName: "Тестовый пользователь");
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestMakeGreetingsFunctionId2()
        {
            string expected = $"Приветствую, Влад :)";
            string actual = AuxiliaryMethods.MakeGreetings( userId: 162374704, userName: "Тестовый пользователь");
            Assert.AreEqual(expected, actual);
        }
    }
}