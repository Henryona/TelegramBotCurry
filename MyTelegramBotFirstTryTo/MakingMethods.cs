using System;
using System.Collections.Generic;
using System.Globalization;
using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class AuxiliaryMethods
    {
        // парс json файла с именами и никнеймами 
        static string IdNamesJson = System.IO.File.ReadAllText(CONSTANTS.NAMES_JSON_PATH);
        static Dictionary<string, string> IdNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(IdNamesJson);

        // получение текущей погоды и температуры воздуха 
        public static string MakeTemperature(string UserQuestion)
        {
            // получение города из вопроса пользователя
            var words = UserQuestion.Split(' ');
            var city = words[words.Length - 1];
            
            // запрос к api с погодой
            var WeatherApi = new WeatherForecasts();
            var forecast = WeatherApi.getWeatherInCity(city);
            return forecast;
        } // method MakeTemperature

        // формирование ответа на запрос дня недели
        public static string MakeDay()
        {
            // язык англ (для красоты ответа)
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US") ;
            
            // получения дня недели
            var dayOfWeek = DateTime.Now.ToString("dddd", ci);
            
            // формирование ответа
            return ($"It's {dayOfWeek} my dudes!");
        } // method MakeDay

        // формирование приветствия 
        public static string MakeGreetings(string userName)
        {
            // поиск совпадения никнейма и присваивание имени для образения к пользователю
            string callName = userName;
            foreach (var uName in IdNames)
            {
                if (userName == uName.Key)
                    callName = uName.Value;
            }
            return ($"Приветствую, {callName} :)");
        } // method MakeGreetings

        // получение новостей
        public static List<string> MakeNews()
        {
            var NewsApi = new News();
            var newsCollumn = NewsApi.getNews();
            return newsCollumn;
        } // method MakeNews

        // получение гороскопа
        public static string MakeHoroscope(string UserQuestion)
        {
            // последнее слово должно быть знаком зодиака
            var words = UserQuestion.Split(' ');
            var signOfZodiac = words[words.Length - 1];
            
            // обращение к апи 
            var HoroscopeApi = new Horoscope();
            var horoscopeText = HoroscopeApi.getHororscopeBySign(signOfZodiac);
            return horoscopeText;
        } // method MakeHoroscope

        // получение цитаты
        public static string MakeQuote()
        {
            var QuoteApi = new Quotes();
            var quote = QuoteApi.getRandonQuote();
            return quote;
        } // method MakeQuote
        
        // получение анекдота или истории
        public static string MakeJoke(string UserQuestion)
        {
            // последнее слово должно отражать, что нужно: история или анекдот
            var words = UserQuestion.Split(' ');
            var category = words[words.Length - 1];

            // запрос к апи 
            var JokesApi = new Jokes();
            var joke = JokesApi.getJokeOrStory(category);
            return joke;
        } // method MakeJoke

        // формирование информации для пользователя
        public static string MakeInfo()
        {
            return CONSTANTS.INFO;
        } // method MakeInfo
        
        public AuxiliaryMethods()
        {}
        
        
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