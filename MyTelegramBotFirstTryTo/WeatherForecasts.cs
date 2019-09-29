using System;
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
        private readonly string PRE_FINAL_API;

        public WeatherForecasts(string weatherToken)
        {
            // формирование запроса к погодному апи
            PRE_FINAL_API = CONSTANTS.API_URL_WEATHER + weatherToken + CONSTANTS.API_LANGUAGE;
        }

        public string getWeatherInCity(string city)
        {
            // запрос
            var FINAL_URL = PRE_FINAL_API + city;
            var Request = new RestRequest(FINAL_URL);
            var Response = RC.Get(Request);
            
            // парсим ответ, он в json-формате
            var Data = JsonConvert.DeserializeObject<WeatherData>(Response.Content);
            var Temp = (int) Data.current.temp_c;

            return $"В городе {city.Substring(0, 1).ToUpper() + city.Substring(1, city.Length - 1)} сейчас {Data.current.condition.text}, температура воздуха {Temp}";
        } // method getWeatherInCity

    } // class WeatherForecasts
}