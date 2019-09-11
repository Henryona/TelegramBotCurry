using System;
using System.Collections.Generic;
using System.Globalization;
using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;

namespace MyTelegramBotFirstTryTo
{
    public class AuxiliaryMethods
    {
        public void MakeSchedule(string option, int chat_id)
        {
            var API = new TelegramAPI();
            //var Main = new Program();
            var singularity = Singularity.Instance;
            var job = new SimpleJob(
                (scheduledTime) =>
                {
                    API.sendMessage(MakeTemperature(option), chat_id);
                } 
            );
            var schedule = new CronSchedule("0 07 05 * * ? *"); // нужно писать на 3 часа меньше (если нужно 18 часов, то писать 15)
            var scheduledJob = singularity.ScheduleJob(
                schedule, job, false); //DateTime.UtcNow.AddMinutes(62));
            singularity.Start();
        } // method MakeSchedule
        
        public string MakeTemperature(string UserQuestion)
        {
            var words = UserQuestion.Split(' ');
            var city = words[words.Length - 1];
            
            var WeatherApi = new WeatherForecasts();
            var forecast = WeatherApi.getWeatherInCity(city);
            return forecast;
        } // method MakeTemperature

        public string MakeDay()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
            return ($"It's {dayOfWeek} my dudes!");
        } // method MakeDay

        public string MakeGreetings(int userId, string userName)
        {
            string callName = userName;
            if (userId == 138200931)
                callName = "Ксюша";
            else if (userId == 162374704)
                callName = "Влад";
            return ($"Приветствую, {callName} :)");
        } // method MakeGreetings

        public List<string> MakeNews()
        {
            var NewsApi = new News();
            var newsCollumn = NewsApi.getNews();
            return newsCollumn;
        } // method MakeNews

        public string MakeHoroscope(string UserQuestion)
        {
            var words = UserQuestion.Split(' ');
            var signOfZodiac = words[words.Length - 1];
            
            var HoroscopeApi = new Horoscope();
            var horoscopeText = HoroscopeApi.getHororscopeBySign(signOfZodiac);
            return horoscopeText;
        } // method MakeHoroscope

        public string MakeQuote()
        {
            var QuoteApi = new Quotes();
            var quote = QuoteApi.getRandonQuote();
            return quote;
        } // method MakeQuote
        
        public string MakeJoke(string UserQuestion)
        {
            var words = UserQuestion.Split(' ');
            var category = words[words.Length - 1];

            var JokesApi = new Jokes();
            var joke = JokesApi.getJokeOrStory(category);
            return joke;
        } // method MakeJoke

        public string MakeInfo()
        {
            var CONST = new CONSTANTS();
            return CONST.INFO;
        } // method MakeInfo
        
        public AuxiliaryMethods()
        {}
    } // class AuxiliaryMethods
}