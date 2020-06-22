using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class AuxiliaryMethods
    {
        private readonly WeatherForecasts _weatherForecasts;
        private readonly News _newsApi;
        private readonly Dictionary<string, string> _idNames;

        public AuxiliaryMethods(string namesJson, string weatherToken, string newsToken)
        {
            _weatherForecasts = new WeatherForecasts(weatherToken);
            _newsApi = new News(newsToken);
            _idNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(namesJson);
        }
        
        // получение текущей погоды и температуры воздуха 
        public string MakeTemperature(string UserQuestion)
        {
            // получение города из вопроса пользователя
            var city = FindValue(UserQuestion, "городе", "Москва");
            
            // запрос к api с погодой
            var forecast = _weatherForecasts.getWeatherInCity(city);
            return forecast;
        } // method MakeTemperature

        public static string FindValue(string question, string comparator, string goodRerurn)
        {
            var words = question.Split(' ');
            var found = false;
            foreach (var word in words)
            {
                if (found)
                    return word;
                
                if (word == comparator)
                    found = true;
            }

            return goodRerurn;
        }

        // формирование ответа на запрос дня недели
        public string MakeDay()
        {
            // язык англ (для красоты ответа)
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            
            // получения дня недели
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
            
            // формирование ответа
            return ($"It's {dayOfWeek} my dudes!");
        } // method MakeDay
        

        // формирование приветствия 
        public string MakeGreetings(string userName)
        {
            // поиск совпадения никнейма и присваивание имени для обращения к пользователю
            string callName = userName;
            foreach (var uName in _idNames)
            {
                if (userName == uName.Key)
                    callName = uName.Value;
            }
            return (CONSTANTS.greet_variants[new Random().Next(0, CONSTANTS.greet_variants.Count)] + $"{callName} :)");
        } // method MakeGreetings
        
        public string MakeHowAreYou()
        {
            return (CONSTANTS.how_are_you_variants[new Random().Next(0, CONSTANTS.how_are_you_variants.Count)]);
        } // method MakeGreetings

        // получение новостей
        public List<string> MakeNews()
        {
            var newsCollumn = _newsApi.getNews();
            return newsCollumn;
        } // method MakeNews

        // получение гороскопа
        public string MakeHoroscope(string UserQuestion)
        {
            // получение города из вопроса пользователя
            var signOfZodiac = FindValue(UserQuestion, "гороскоп", "");
            
            // обращение к апи 
            var HoroscopeApi = new Horoscope();
            var horoscopeText = HoroscopeApi.getHororscopeBySign(signOfZodiac);
            return horoscopeText;
        } // method MakeHoroscope

        // получение цитаты
        public string MakeQuote()
        {
            var QuoteApi = new Quotes();
            var quote = QuoteApi.getRandonQuote();
            return quote;
        } // method MakeQuote
        
        // получение анекдота или истории
        public string MakeJoke(string UserQuestion)
        {
            // последнее слово должно отражать, что нужно: история или анекдот
            var words = UserQuestion.Split(' ');
            var category = (words.Contains("анекдот")) ? "1" : "2";

            // запрос к апи 
            var JokesApi = new Jokes();
            var joke = JokesApi.getJokeOrStory(category);
            return joke;
        } // method MakeJoke
        
        public string MakeWork(string UserQuestion)
        {
            return (CONSTANTS.work_variants[new Random().Next(0, CONSTANTS.work_variants.Count)]);
        } // method MakeWork
        
        public string MakeCompliment(string UserQuestion)
        {
            return (CONSTANTS.compliment_variants[new Random().Next(0, CONSTANTS.compliment_variants.Count)]);
        } // method MakeWork

        // формирование информации для пользователя
        public string MakeInfo()
        {
            return CONSTANTS.INFO;
        } // method MakeInfo
        
        


        // тут настройка сообщений по расписанию, но телега добавила
        // отложенные сообщения, так что эти теперь стали бесполезными
        /*public void MakeSchedule(string option, int chat_id)
        {
            var API = new TelegramAPI();
            var CONST = new CONSTANTS();
            var singularity = Singularity.Instance;
            var job = new SimpleJob(
                (scheduledTime) =>
                {
                    var token = API.getApiUrl(CONST.CURRY_BOT_TOKEN_PATH_WIN);
                    API.sendMessage(token, MakeTemperature(option), chat_id);
                } 
            );
            var schedule = new CronSchedule("0 07 05 * * ? *"); // нужно писать на 3 часа меньше (если нужно 18 часов, то писать 15)
            var scheduledJob = singularity.ScheduleJob(
                schedule, job, false); //DateTime.UtcNow.AddMinutes(62));
            singularity.Start();
        } */ // method MakeSchedule

    } // class AuxiliaryMethods
}