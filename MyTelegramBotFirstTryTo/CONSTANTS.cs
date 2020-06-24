using System;
using System.Collections.Generic;

namespace MyTelegramBotFirstTryTo
{
    public class CONSTANTS
    {
        // файлик с именами\никнеймами
        public static string NAMES_JSON_PATH =
            Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/NAMES.json";

        // все константы для работы с погодй
        public static string WEATHER_API_FILE_PATH =
            Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/API_KEY_WEATHER";

        public static string
            API_URL_WEATHER =
                "http://api.weatherstack.com/current?access_key="; //"http://api.apixu.com/v1/current.json?key=";

        // все константы для работы с цитатами
        public static string API_URL_QUOTES = "https://api.forismatic.com/api/1.0/";
        public static string API_METHOD = "?method=getQuote";
        public static string API_FORMAT = "&format=json";
        public static string API_LANGUAGE = "&language = ru&query=";

        // все константы для работы с новостями
        public static string API_URL_NEWS = "https://newsapi.org/v2/top-headlines";
        public static string API_COUNTRY = "?country=ru";

        public static string NEWS_API_FILE_PATH =
            Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/API_KEY_NEWS";

        public static string NEWS_ANSWER = "Вот тут немного актуальных новостей";

        // токен и урл телеграм апи
        public static string CURRY_BOT_TOKEN_PATH =
            Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/API_TOKEN";

        public static string API_URL_MAIN = "https://api.telegram.org/bot";

        // имя бота
        public static string BOT_NAME = "curry";

        // все константы для работы с историями\анекдотами
        public static string API_URL_JOKE = "http://rzhunemogu.ru/Rand.aspx";
        public static string TYPE = "?CType=";

        // все константы для работы с гороскопами
        public static string API_URL_HORO = "https://ignio.com/r/export/utf/xml/daily/";
        public static string API_HORO_TYPE = "cook";
        public static string API_XML = ".xml";
        public static string UNKNOWN_ZODIAC = "скажи мне, кто ты по гороскопу!";

        // хелп для бота
        public static string INFO =
            "- Я могу отвечать на простые вопросы и фразы типа: \n" +
            "\"как твои дела?\" \n \"привет\" \n \"что делаешь?\" \n \"ты милаха(симпатяга)\"\n \n " +
            "- Также я могу назвать день недели, если меня спросить: \n" +
            "\"какой день недели?\" \n " +
            "- И сообщю данные о погоде в определённом городе: \n " +
            "\"какая погода в городе N\" \n " +
            "- Ещё я знаю новости, гороскоп еды, цитаты, анекдоты и истории: \n " +
            "\"покажи новости\" " +
            "\"расскажи гороскоп N\" \n \"покажи цитату\" \n" +
            "\" расскажи анекдот \" \n \" расскажи историю \" \n";

        public int CHAT_ID = -258099164;

        public static List<string> greet_variants = new List<string>() {"Приветствую, ", "О, привет, ", "Хей, "};
        public static List<string> dontknow = new List<string>() {"Что ты пытаешься сказать?", "Я не понимаю", "Что?", "А?"};

        public static List<string> how_are_you_variants = new List<string>()
            {"Да вроде неплохо)", "Сойдет)", "Да всё ок)", "да и так и сяк, если честно)",
             "ощущаю себя странно...странное чуство, однако"};
        
        public static List<string> work_variants = new List<string>()
        {
            "Да в принципе ничем не занимаюсь", "Работаю, много работы(", "Листаю каналы в телеге", 
            "планирую пойти отдыхать, но что-то всё никак)", "изучаю vim :wq", "собираюсь покушать",
            "сижу, залипаю", "играю в Sims. ты в курсе, что вышел новый аддон? Наверно вышел, они постоянно выходят",
            "читаю статью о том, как писать ботов", "да что-то скучно, сижу(", "Пытаюсь поймать шайни оддиша",
            "Общаюсь со своим другом - @HenryonasSaffranBot . Какой же он странный)"
            
        };

        public static List<string> compliment_variants = new List<string>()
        {
            "Спасибо))", "Ми-ми)", "Это верно)", ":)))"
        };

        public static Dictionary<string, string> WeatherStateTranslates = new Dictionary<string, string>
        {
            ["Sunny"] = "солнечно",
            ["Light Rain Shower"] = "лёгкий дождик",
            ["Clear"] = "ясно",
            ["Patchy rain possible"] = "местами дождь",
            ["Partly cloudy"] = "переменная облачность",
            ["Cloudy"] = "облачно",
            ["Overcast"] = "пасмурно",
            ["Mist"] = "дымка",
            ["Patchy snow possible"] = "местами снег",
            ["Patchy sleet possible"] = "местами дождь со снегом",
            ["Patchy freezing drizzle possible"] = "местами замерзающая морось",
            ["Thundery outbreaks possible"] = "местами грозы",
            ["Blowing snow"] = "поземок", 
            ["Blizzard"] = "метель",
            ["Fog"] = "туман",
            ["Freezing fog"] = "морозный туман", 
            ["Patchy light drizzle"] = "местами слабая морось",
            ["Light drizzle"] = "слабая морось",
            ["Freezing drizzle"] = "замерзающая морось",
            ["Heavy freezing drizzle"] = "сильная замерзающая морось",
            ["Patchy light rain"] = "местами небольшой дождь",
            ["Light rain"] = "небольшой дождь",
            ["Moderate rain at times"] = "временами умеренный дождь",
            ["Moderate rain"] = "умеренный дождь",
            ["Heavy rain at times"] = "временами сильный дождь",
            ["Heavy rain"] = "сильный дождь",
            ["Light freezing rain"] = "слабый ледяной дождь" 
        };

} // class CONSTANTS
}