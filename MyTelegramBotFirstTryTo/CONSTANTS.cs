namespace MyTelegramBotFirstTryTo
{
    public class CONSTANTS
    {
        // файлик с json вопросами-ответами
        public static string QU_AN_JSON_PATH = "../../../questions.json";
        // файлик с именами\никнеймами
        public static string NAMES_JSON_PATH = "../../../../curryBot/NAMES.json";
        
        // все константы для работы с погодй
        public static string WEATHER_API_FILE_PATH = "../../../../curryBot/API_KEY_WEATHER";
        public static string API_URL_WEATHER = "http://api.apixu.com/v1/current.json";
        
        // все константы для работы с цитатами
        public static string API_URL_QUOTES = "https://api.forismatic.com/api/1.0/";
        public static string API_METHOD = "?method=getQuote";
        public static string API_FORMAT = "&format=json";   
        public static string API_LANGUAGE = "&lang=ru&q=";
        
        // все константы для работы с новостями
        public static string API_URL_NEWS = "https://newsapi.org/v2/top-headlines";
        public static string API_COUNTRY = "?country=ru";
        public static string NEWS_API_FILE_PATH = "../../../../curryBot/API_KEY_NEWS";
        public static string NEWS_ANSWER = "Вот тут немного актуальных новостей";
        
        // токен и урл телеграм апи
        public static string CURRY_BOT_TOKEN_PATH = "../../../../curryBot/API_TOKEN";
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

    } // class CONSTANTS
}