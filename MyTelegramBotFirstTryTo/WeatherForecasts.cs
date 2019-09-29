using System.Globalization;
using RestSharp;
using Newtonsoft.Json;

namespace MyTelegramBotFirstTryTo
{
    public class WeatherForecasts
    {
        public class WeatherData
        {
            public Current current { get; set; }
        }

        public class Current
        {
            public double temp_c { get; set; }
            public Condition condition { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
        }
        
        private RestClient RC = new RestClient();

        public WeatherForecasts()
        { }

        public string getWeatherInCity(string city)
        {
            var CONST = new CONSTANTS();
            string API_URL = "http://api.apixu.com/v1/current.json";
            string API_KEY = System.IO.File.ReadAllText(CONST.WEATHER_API_FILE_PATH)
                .Replace("\n", "");
            string FINAL_URL = API_URL + "?key=" + API_KEY + "&lang=ru&q=";
            var URL = FINAL_URL + city;
            var Request = new RestRequest(URL);
            var Response = RC.Get(Request);
            var Data = JsonConvert.DeserializeObject<WeatherData>(Response.Content);
            var Temp = (int) Data.current.temp_c;

            return $"В городе {city.Substring(0, 1).ToUpper() + city.Substring(1, city.Length - 1)} сейчас {Data.current.condition.text}, температура воздуха {Temp}";
        } // method getWeatherInCity

    } // class WeatherForecasts
}