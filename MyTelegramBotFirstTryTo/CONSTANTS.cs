using System;
using System.Collections.Generic;

namespace MyTelegramBotFirstTryTo
{
    public class CONSTANTS
    {
        // файлик с именами\никнеймами
        public static string NAMES_JSON_PATH = Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/NAMES.json";
        
        // все константы для работы с погодй
        public static string WEATHER_API_FILE_PATH = Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/API_KEY_WEATHER";
        public static string API_URL_WEATHER = "http://api.weatherstack.com/current?access_key="; //"http://api.apixu.com/v1/current.json?key=";
        
        // все константы для работы с цитатами
        public static string API_URL_QUOTES = "https://api.forismatic.com/api/1.0/";
        public static string API_METHOD = "?method=getQuote";
        public static string API_FORMAT = "&format=json";   
        public static string API_LANGUAGE = "&language = ru&query=";
        
        // все константы для работы с новостями
        public static string API_URL_NEWS = "https://newsapi.org/v2/top-headlines";
        public static string API_COUNTRY = "?country=ru";
        public static string NEWS_API_FILE_PATH = Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/API_KEY_NEWS";
        public static string NEWS_ANSWER = "Вот тут немного актуальных новостей";
        
        // токен и урл телеграм апи
        public static string CURRY_BOT_TOKEN_PATH = Environment.GetEnvironmentVariable("HOME") + "/Documents/curryBot/API_TOKEN";
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
            "Команда для меня начинается с \"curry \" . \n После обращения можно вводить следующие запросы: \n" +
            "\"привет\" \n \"какой день недели\" \n \"какая погода в городе N\" \n " +
            "\"покажи новости\" \n \"нужна помощь\" \n" +
            "\"расскажи гороскоп N\" \n \"покажи цитату\" \n" +
            "\" расскажи анекдот \" \n \" расскажи историю \" \n";
        public int CHAT_ID = -258099164;
        
        public static List<string> greet_variants = new List<string>() { "Приветствую, ", "О, привет, ", "Хей, "};
        public static List<string> dontknow = new List<string>() { "Что ты пытаешься сказать?", "Я не понимаю", "Что?"};
        public static List<string> how_are_you_variants = new List<string>() {"Да вроде неплохо)", "Сойдет)", "Да всё ок)"};

    } // class CONSTANTS
}